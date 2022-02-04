using System.Collections.Generic;

namespace WindowsInput.Events {
    public abstract class ButtonEvent : AggregateEvent {
        public ButtonCode Button { get; }

        protected override string GetDebuggerDisplay() { 
            var ret = $@"{this.GetType().Name}: {Button}";

            return ret;
        }

        protected ButtonEvent(ButtonCode Button, IEnumerable<IEvent> Children) : base(Children) {
            this.Button = Button;
        }

    }

}
