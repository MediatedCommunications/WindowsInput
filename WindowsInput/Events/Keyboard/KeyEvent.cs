using System.Collections.Generic;

namespace WindowsInput.Events {
    public abstract class KeyEvent : AggregateEvent {
        public KeyCode Key { get; }
        public bool Extended { get; }

        protected override string GetDebuggerDisplay() {
            var ret = $@"{this.GetType().Name}: {Key}" + (Extended ? "(Extended)" : "");

            return ret;
        }

        protected KeyEvent(KeyCode Key, bool? Extended = default) {
            this.Key = Key;
            this.Extended = Extended ?? Key.IsExtended();
        }

    }

}
