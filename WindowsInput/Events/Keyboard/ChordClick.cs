using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsInput.Events {
    public class ChordClick : AggregateEvent {

        public IReadOnlyCollection<KeyCode> Keys { get; private set; }

        protected override string DebuggerDisplay => $@"{this.GetType().Name}: {String.Join("+", Keys)}";

        public ChordClick(params KeyCode[] Keys) : base(CreateChildren(Keys)) {
            this.Keys = Keys;
        }

        private static IEnumerable<IEvent> CreateChildren(params KeyCode[] Keys) {
            var ret = new List<IEvent>();

            foreach (var item in Keys) {
                ret.Add(new KeyDown(item));
            }

            foreach (var item in Keys.Reverse()) {
                ret.Add(new KeyUp(item));
            }

            return ret;
        }


    }
}
