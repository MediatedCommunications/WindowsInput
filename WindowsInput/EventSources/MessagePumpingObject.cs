using System;
using System.Threading;
using System.Windows.Forms;

namespace WindowsInput.EventSources {
    public class MessagePumpingObject<T> : IDisposable {

        public MessagePumpingObject(Func<T> Creator) {
            var Created = new ManualResetEventSlim(false);

            Thread = new System.Threading.Thread(() => {
                this.AC = new System.Windows.Forms.ApplicationContext();
                
                Instance = Creator();
                Created.Set();
                System.Windows.Forms.Application.Run(AC);
            });

            Thread.Start();

            Created.Wait();

        }

        public T Instance { get; private set; }
        private ApplicationContext AC { get; set; }
        private Thread Thread { get; set; }

        public void Dispose() {

            if(Instance is IDisposable V1) {
                V1.Dispose();
            }

            AC?.ExitThread();

        }
    }

}
