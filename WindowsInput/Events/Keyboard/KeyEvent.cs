using System.Collections.Generic;

namespace WindowsInput.Events {
    public abstract class KeyEvent : AggregateEvent {
        public KeyCode Key { get; private set; }
        public bool Extended { get; private set; }

        protected override string DebuggerDisplay => $@"{this.GetType().Name}: {Key}" + (Extended ? "(Extended)" : "");

        protected KeyEvent(KeyCode Key, bool? Extended = default) {
            this.Key = Key;
            this.Extended = Extended ?? Key.IsExtended();
        }

    }

}
