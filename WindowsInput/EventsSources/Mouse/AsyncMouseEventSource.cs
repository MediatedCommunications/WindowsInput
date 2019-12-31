using System;
using System.Collections.Generic;
using WindowsInput.Events;

namespace WindowsInput.EventSources {
    public class AsyncMouseEventSource : IMouseEventSource {

        private MessagePumpingObject<IMouseEventSource> AsyncObject;


        public AsyncMouseEventSource(Func<IMouseEventSource> Creator) {
            this.AsyncObject = new MessagePumpingObject<IMouseEventSource>(Creator);
        }

        public void Dispose() {
            AsyncObject?.Dispose();
        }

        public bool Enabled {
            get => AsyncObject.Instance.Enabled;
            set => AsyncObject.Dispatcher.Invoke(()=> AsyncObject.Instance.Enabled = value);
        }

        public event EventHandler<EnabledChangedEventArgs> EnabledChanged {
            add => AsyncObject.Instance.EnabledChanged += value;
            remove => AsyncObject.Instance.EnabledChanged -= value;
        }

        public event EventHandler<EventSourceEventArgs<MouseEvent>> MouseEvent {
            add => AsyncObject.Instance.MouseEvent += value;
            remove => AsyncObject.Instance.MouseEvent -= value;
        }

        public event EventHandler<EventSourceEventArgs<Wait>> Wait {
            add => AsyncObject.Instance.Wait += value;
            remove => AsyncObject.Instance.Wait -= value;
        }

        public event EventHandler<EventSourceEventArgs<MouseMove>> MouseMove {
            add => AsyncObject.Instance.MouseMove += value;
            remove => AsyncObject.Instance.MouseMove -= value;
        }

        public event EventHandler<EventSourceEventArgs<ButtonClick>> ButtonClick {
            add => AsyncObject.Instance.ButtonClick += value;
            remove => AsyncObject.Instance.ButtonClick -= value;
        }

        public event EventHandler<EventSourceEventArgs<ButtonDown>> ButtonDown {
            add => AsyncObject.Instance.ButtonDown += value;
            remove => AsyncObject.Instance.ButtonDown -= value;
        }

        public event EventHandler<EventSourceEventArgs<ButtonUp>> ButtonUp {
            add => AsyncObject.Instance.ButtonUp += value;
            remove => AsyncObject.Instance.ButtonUp -= value;
        }

        public event EventHandler<EventSourceEventArgs<ButtonScroll>> ButtonScroll {
            add => AsyncObject.Instance.ButtonScroll += value;
            remove => AsyncObject.Instance.ButtonScroll -= value;
        }

        public event EventHandler<EventSourceEventArgs<ButtonClickHold>> ButtonClickHold {
            add => AsyncObject.Instance.ButtonClickHold += value;
            remove => AsyncObject.Instance.ButtonClickHold -= value;
        }

        public event EventHandler<EventSourceEventArgs<ButtonDoubleClick>> ButtonDoubleClick {
            add => AsyncObject.Instance.ButtonDoubleClick += value;
            remove => AsyncObject.Instance.ButtonDoubleClick -= value;
        }

        public event EventHandler<EventSourceEventArgs<IReadOnlyCollection<DragStart>>> DragStarted {
            add => AsyncObject.Instance.DragStarted += value;
            remove => AsyncObject.Instance.DragStarted -= value;
        }

        public event EventHandler<EventSourceEventArgs<IReadOnlyCollection<DragDrop>>> DragFinished {
            add => AsyncObject.Instance.DragFinished += value;
            remove => AsyncObject.Instance.DragFinished -= value;
        }


    }

}
