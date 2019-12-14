using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WindowsInput.Events;

namespace WindowsInput.Events {


    public partial class EventBuilder : IEvent {
        public List<IEvent> Events { get; private set; } = new List<IEvent>();

        public static EventBuilder Create() {
            return new EventBuilder();
        }

        public Task<bool> Invoke(InvokeOptions Options) {
            return Simulate.Events(Options, Events.ToList());
        }

        public Task<bool> Invoke() {
            return Invoke(null);
        }

    }

}
