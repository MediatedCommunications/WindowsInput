using System.Collections.Generic;
using System.Linq;


namespace WindowsInput.Events {
    public class KeysDown : AggregateEvent {

        public IReadOnlyCollection<KeyCode> Keys { get; }

        public KeysDown(IEnumerable<KeyCode> Keys) {
            this.Keys = Keys.ToList().AsReadOnly();
            Initialize(CreateChildren());
        }

        private IEnumerable<IEvent> CreateChildren() {
            foreach (var item in Keys) {
                yield return new KeyDown(item);
            }
        }

    }



}
