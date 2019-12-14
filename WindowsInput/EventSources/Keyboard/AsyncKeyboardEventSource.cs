using System;

using WindowsInput.Events;

namespace WindowsInput.EventSources {
    public class AsyncKeyboardEventSource : IKeyboardEventSource {

        private MessagePumpingObject<IKeyboardEventSource> AsyncObject;
        public AsyncKeyboardEventSource(Func<IKeyboardEventSource> Creator) {
            this.AsyncObject = new MessagePumpingObject<IKeyboardEventSource>(Creator);

        }

        public void Dispose() {
            AsyncObject?.Dispose();
        }

        public bool Enabled {
            get => AsyncObject.Instance.Enabled;
            set => AsyncObject.Instance.Enabled = value;
        }

        public void Enable() => AsyncObject.Instance.Enable();
        public void Disable() => AsyncObject.Instance.Disable();

        public event EventHandler<EventSourceEventArgs<KeyboardEvent>> KeyEvent {
            add => AsyncObject.Instance.KeyEvent += value;
            remove => AsyncObject.Instance.KeyEvent -= value;
        }

        public event EventHandler<EventSourceEventArgs<Wait>> Wait {
            add => AsyncObject.Instance.Wait += value;
            remove => AsyncObject.Instance.Wait -= value;
        }

        public event EventHandler<EventSourceEventArgs<KeyDown>> KeyDown {
            add => AsyncObject.Instance.KeyDown += value;
            remove => AsyncObject.Instance.KeyDown -= value;
        }

        public event EventHandler<EventSourceEventArgs<KeyUp>> KeyUp {
            add => AsyncObject.Instance.KeyUp += value;
            remove => AsyncObject.Instance.KeyUp -= value;
        }

        public event EventHandler<EventSourceEventArgs<TextClick>> TextClick {
            add => AsyncObject.Instance.TextClick += value;
            remove => AsyncObject.Instance.TextClick -= value;
        }

    }

}
