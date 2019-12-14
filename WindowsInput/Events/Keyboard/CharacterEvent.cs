using System.Collections.Generic;

namespace WindowsInput.Events {
    public abstract class  CharacterEvent : AggregateEvent {
        public char Text { get; private set; }

        protected override string DebuggerDisplay => $@"{this.GetType().Name}: {Text}";

        protected CharacterEvent(char Text, IEnumerable<IEvent> Children) : base(Children) {
            this.Text = Text;
        }

    }

}
