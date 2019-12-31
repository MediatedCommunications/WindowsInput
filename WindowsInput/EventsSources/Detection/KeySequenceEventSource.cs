using System;
using System.Collections.Generic;
using WindowsInput.Events;

namespace WindowsInput.EventSources {

    public class KeySequenceEventArgs : EventArgs {
        public EventSourceEventArgs<KeyboardEvent> Input { get; set; }
        public SequenceClick Sequence { get; set; }
    }


    public class KeySequenceEventSource : KeyboardMonitoringEventSource {
        public event EventHandler<KeySequenceEventArgs> Triggered;

        public SequenceClick Sequence { get; private set; }

        public KeySequenceEventSource(IKeyboardEventSource Monitor, SequenceClick Sequence) : base(Monitor) {
            this.Sequence = Sequence;
        }

        protected override void Enable() {
            base.Enable();
            Monitor.KeyEvent += this.Monitor_KeyEvent;
        }

        protected override void Disable() {
            base.Disable();
            Monitor.KeyEvent -= Monitor_KeyEvent;
            
        }

        private KeyEventSourceStateMachine State;
        protected override void Reset() {
            base.Reset();
            State = new KeyEventSourceStateMachine(Sequence);
        }

        private void Monitor_KeyEvent(object sender, EventSourceEventArgs<KeyboardEvent> e) {
            if (State.Next(e.Data) == StateMachineResult.Complete) {
                var args = new KeySequenceEventArgs() {
                    Input = e,
                    Sequence = Sequence
                };
                Triggered?.Invoke(this, args);
            }
        }
    }

    public class KeyEventSourceStateMachine : StateMachine<KeyboardEvent, StateMachineResult> {

        public SequenceClick Sequence { get; private set; }

        public KeyEventSourceStateMachine(SequenceClick Sequence) {
            this.Sequence = Sequence;
        }

        protected override IEnumerable<StateMachineResult> Next(Value Input) {

            while (true) {
                var Start = Sequence.Keys.GetEnumerator();
                Start.MoveNext();

                //Each time that we get an input that is a KeyDown
                while (Input.Current is { } Current) {

                    if (Current.KeyDown is { } V1) {
                        //If the current key is the same as the key that was pressed, move forward.
                        if (V1.Key == Start.Current) {
                            //Advance our iterator for next time.
                            //If it returns false, we've matched all our keys so we should trigger!
                            if (!Start.MoveNext()) {
                                yield return StateMachineResult.Complete;
                            } else {
                                yield return StateMachineResult.Accepted;
                            }
                            //Otherwise, restart.
                        } else {
                            yield return StateMachineResult.Rejected;
                            break;
                        }

                    } else {
                        yield return StateMachineResult.Rejected;
                    }

                }
            }

        }

    }

}
