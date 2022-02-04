using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace WindowsInput.Events {
    [DebuggerDisplay(Debugger2.GetDebuggerDisplay)]
    public abstract class AggregateEvent : EventBase, IEnumerable<IEvent> {

        protected override string GetDebuggerDisplay() {
            var ret = base.GetDebuggerDisplay();

            if(this.Children.Count == 1) {
                ret = $@"{ret} ({this.Children.FirstOrDefault()})";
            }

            return ret;
        }

        public AggregateEvent(params IEvent[] Children) : this(Children.AsEnumerable())   {
            
        }

        public AggregateEvent(IEnumerable<IEvent> Children) {
            this.Children = Array.Empty<IEvent>();

            Initialize(Children);
        }


        protected void Initialize(params IEvent[] Children) {
            Initialize(Children.AsEnumerable());
        }

        protected void Initialize(IEnumerable<IEvent> Children) {
            var SubCommands = new List<IEvent>();
            if (Children != null) {
                foreach (var item in Children) {
                    if(item is { }) {
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
            return Children.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return Children.GetEnumerator();
        }

        public IReadOnlyList<IEvent> Children { get; private set; }


    }

}
