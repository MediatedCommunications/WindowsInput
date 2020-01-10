using System.Collections.Generic;
using System.Linq;


namespace WindowsInput.Events {
    public class KeysUp : AggregateEvent {

        public IReadOnlyCollection<KeyCode> Keys { get; private set; }

        public KeysUp(IEnumerable<KeyCode> Keys) {
            this.Keys = Keys.ToList().AsReadOnly();
            Initialize(CreateChildren());
        }

        private IEnumerable<IEvent> CreateChildren() {
            foreach (var item in Keys) {
                yield return new KeyUp(item);
            }
        }

    }



}
