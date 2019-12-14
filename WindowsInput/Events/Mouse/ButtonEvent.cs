using System.Collections.Generic;

namespace WindowsInput.Events {
    public abstract class ButtonEvent : AggregateEvent {
        public ButtonCode Button { get; private set; }

        protected override string DebuggerDisplay => $@"{this.GetType().Name}: {Button}";

        protected ButtonEvent(ButtonCode Button, IEnumerable<IEvent> Children) : base(Children) {
            this.Button = Button;
        }

    }

}
