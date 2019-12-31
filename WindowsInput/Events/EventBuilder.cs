using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WindowsInput.Events;

namespace WindowsInput.Events {


    public partial class EventBuilder : IEvent, IEnumerable<IEvent> {
        public List<IEvent> Events { get; private set; } = new List<IEvent>();

        public static EventBuilder Create() {
            return new EventBuilder();
        }

        /// <summary>
        /// Simulate the events contained within this <see cref="EventBuilder"/>
        /// </summary>
        /// <param name="Options">Options that control the flow of these events</param>
        /// <returns></returns>
        public Task<bool> Invoke(InvokeOptions Options) {
            return Simulate.Events(Options, Events.ToList());
        }

        /// <inheritdoc cref="Invoke(InvokeOptions)"/>
        public Task<bool> Invoke() {
            return Invoke(null);
        }

        public IEnumerator<IEvent> GetEnumerator() {
            return ((IEnumerable<IEvent>)this.Events).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return ((IEnumerable<IEvent>)this.Events).GetEnumerator();
        }
    }

}
