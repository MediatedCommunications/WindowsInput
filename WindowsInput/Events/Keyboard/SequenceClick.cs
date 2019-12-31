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

        public SequenceClick(IEnumerable<KeyCode> Keys) {
            var NewKeys = new List<KeyCode>();
            if(Keys != default) {
                NewKeys.AddRange(Keys);
            }
            this.Keys = NewKeys;

            Initialize(CreateChildren());
        }

        public SequenceClick(params KeyCode[] Keys) : this((IEnumerable<KeyCode>)Keys) {
            
        }

        private IEnumerable<IEvent> CreateChildren() {

            foreach (var item in Keys) {
                yield return new KeyClick(item);
            }
        }


    }
}

