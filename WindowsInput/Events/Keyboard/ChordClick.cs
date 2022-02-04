using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsInput.Events {
    public class ChordClick : AggregateEvent {

        public IReadOnlyCollection<KeyCode> Keys { get; }

        protected override string GetDebuggerDisplay() {
            var ret = $@"{this.GetType().Name}: {String.Join("+", Keys)}";
            return ret;
        }

        public ChordClick(IEnumerable<KeyCode> Keys) {
            var NewKeys = new List<KeyCode>();
            if (Keys is { }) {
                NewKeys.AddRange(Keys);
            }
            this.Keys = NewKeys;

            Initialize(CreateChildren());
        }

        public ChordClick(params KeyCode[] Keys) : this((IEnumerable<KeyCode>)Keys) {
            
        }

        private IEnumerable<IEvent> CreateChildren() {

            foreach (var item in Keys) {
                yield return new KeyDown(item);
            }

            foreach (var item in Keys.Reverse()) {
                yield return new KeyUp(item);
            }

        }


    }
}
