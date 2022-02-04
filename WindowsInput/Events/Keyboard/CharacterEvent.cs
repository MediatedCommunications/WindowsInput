using System.Collections.Generic;

namespace WindowsInput.Events {
    public abstract class  CharacterEvent : AggregateEvent {
        public char Text { get; }

        protected override string GetDebuggerDisplay() {
            var ret = $@"{this.GetType().Name}: {Text}";

            return ret;
        }

        protected CharacterEvent(char Text, IEnumerable<IEvent> Children) : base(Children) {
            this.Text = Text;
        }

    }

}
