using System;
using System.Threading;
using System.Windows.Threading;

namespace WindowsInput.Events.Sources {
    public class MessagePumpingObject<T> : IDisposable {

        public event EventHandler Shutdown;

        public MessagePumpingObject(Func<T> Creator) {
            using (var Created = new ManualResetEventSlim(false)) {

                Thread = new System.Threading.Thread(() => {
                    Thread.Name = $@"{nameof(MessagePumpingObject<T>)}";
                    this.Dispatcher = Dispatcher.CurrentDispatcher;
                    this.Instance = Creator();
                    Created.Set();
                    Dispatcher.Run();

                    Shutdown?.Invoke(this, null);
                });

                Thread.SetApartmentState(ApartmentState.STA);
                Thread.Start();

                Created.Wait();
            }

            if(this.Dispatcher == default) {
                throw new InvalidOperationException();
            }

        }

        public T Instance { get; private set; }
        public Dispatcher Dispatcher { get; private set; }
        private Thread Thread { get; set; }

        public void Dispose() {

            if(Instance is IDisposable V1) {
                V1.Dispose();
            }

            Dispatcher?.InvokeShutdown();

        }
    }

}
