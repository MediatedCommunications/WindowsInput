using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace WindowsInput.Events {
    [DebuggerDisplay(Debugger2.DISPLAY)]
    public abstract class AggregateEvent : EventBase, IEnumerable<IEvent> {

        protected override string DebuggerDisplay {
            get {
                var ret = base.DebuggerDisplay;

                if(this.Children.Count == 1) {
                    ret = $@"{ret} ({this.Children.FirstOrDefault()})";
                }

                return ret;
            }
        }

        public AggregateEvent() {
            Initialize(new IEvent[] { });
        }

        public AggregateEvent(params IEvent[] Children)  {
            Initialize(Children);
        }


        protected void Initialize(params IEvent[] Children) {
            Initialize((IEnumerable<IEvent>)Children);
        }

        public AggregateEvent(IEnumerable<IEvent> Children) {
            Initialize(Children);
        }

        protected void Initialize(IEnumerable<IEvent> Children) {
            var SubCommands = new List<IEvent>();
            if (Children != null) {
                foreach (var item in Children) {
                    if(item != default) {
                        SubCommands.Add(item);
                    }
                }
                
            }

            this.Children = SubCommands.AsReadOnly();
        }
        
        protected sealed override Task<bool> Invoke(InvokeOptions Options) {
            return Simulate.Events(Options, Children);
        }

        public IEnumerator<IEvent> GetEnumerator() {
            return ((IEnumerable<IEvent>)this.Children).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return ((IEnumerable<IEvent>)this.Children).GetEnumerator();
        }

        public System.Collections.ObjectModel.ReadOnlyCollection<IEvent> Children { get; private set; }


    }

}
