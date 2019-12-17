using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsInput.Events {
    public class SequenceClick : AggregateEvent {

        public IReadOnlyCollection<KeyCode> Keys { get; private set; }

        protected override string DebuggerDisplay => $@"{this.GetType().Name}: {String.Join("+", Keys)}";

        public SequenceClick(params KeyCode[] Keys) : base(CreateChildren(Keys)) {
            this.Keys = Keys;
        }

        private static IEnumerable<IEvent> CreateChildren(params KeyCode[] Keys) {
            var ret = new List<IEvent>();

            foreach (var item in Keys) {
                ret.Add(new KeyClick(item));
            }

            return ret;
        }


    }
}
