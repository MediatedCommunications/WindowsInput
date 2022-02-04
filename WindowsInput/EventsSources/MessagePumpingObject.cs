using System;
using System.Threading;
using System.Windows.Threading;

namespace WindowsInput.Events.Sources {
    public class MessagePumpingObject<T> : IDisposable {

        public event EventHandler<object?>? Shutdown;

        public MessagePumpingObject(Func<T> Creator) {
            try {
                using var Created = new ManualResetEventSlim(false);

                var LocalDispatcher = default(Dispatcher?);
                var LocalInstance = default(T?);

                Thread = new Thread(() => {
                    LocalDispatcher = Dispatcher.CurrentDispatcher;
                    LocalInstance = Creator();
                    Created.Set();
                    Dispatcher.Run();

                    Shutdown?.Invoke(this, null);
                });

                Thread.SetApartmentState(ApartmentState.STA);
                Thread.Name = $@"{nameof(MessagePumpingObject<T>)}";
                
                Thread.Start();

                Created.Wait();

                this.Dispatcher = LocalDispatcher ?? throw new InvalidOperationException();
                this.Instance = LocalInstance ?? throw new InvalidOperationException();
            } catch {
                Thread?.Abort();
                throw;
            }
        }

        public T Instance { get; private set; }
        public Dispatcher Dispatcher { get; private set; }
        private Thread Thread { get; }

        public void Dispose() {

            if(Instance is IDisposable V1) {
                V1.Dispose();
            }

            Dispatcher?.InvokeShutdown();

        }
    }

}
