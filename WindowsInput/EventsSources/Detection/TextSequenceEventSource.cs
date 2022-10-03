using System;
using System.Collections.Generic;
using WindowsInput.Events;

namespace WindowsInput.Events.Sources {
    public class TextSequenceEventArgs : EventArgs {
        public EventSourceEventArgs<KeyboardEvent> Input { get; }
        public TextClick Sequence { get; }

        public TextSequenceEventArgs(EventSourceEventArgs<KeyboardEvent> Input, TextClick Sequence) {
            this.Input = Input;
            this.Sequence = Sequence;
        }
    }

    public class TextSequenceEventSource : KeyboardMonitoringEventSource {
        public event EventHandler<TextSequenceEventArgs>? Triggered;

        public TextClick Sequence { get; }
        public StringComparison Comparison { get; }

        public TextSequenceEventSource(IKeyboardEventSource Monitor, TextClick Sequence) : this(Monitor, Sequence, StringComparison.InvariantCultureIgnoreCase) {
        
        }

        public TextSequenceEventSource(IKeyboardEventSource Monitor, TextClick Sequence, StringComparison Comparison) : base(Monitor) {
            this.Sequence = Sequence;
            this.Comparison = Comparison;
        }

        protected override bool Enable() {
            Monitor.KeyEvent += this.Monitor_KeyEvent;

            var ret = base.Enable();

            return ret;            
        }

        protected override void Disable() {
            base.Disable();
            Monitor.KeyEvent -= Monitor_KeyEvent;
        }

        private TextSequenceStateMachine? State;
        protected override void Reset() {
            base.Reset();
            State = new TextSequenceStateMachine(Sequence, Comparison);
        }

        private void Monitor_KeyEvent(object? sender, EventSourceEventArgs<KeyboardEvent> e) {

            if (State is { } && State.TryNext(e.Data, out var Status) && Status == StateMachineResult.Complete) {
                var args = new TextSequenceEventArgs(e, Sequence);
                Triggered?.Invoke(this, args);
            }

        }
    }

    public class TextSequenceStateMachine : StateMachine<KeyboardEvent, StateMachineResult> {
        public TextClick Sequence { get; }
        public StringComparison Comparison { get; }
        
        public TextSequenceStateMachine(TextClick Sequence, StringComparison Comparison) {
            this.Sequence = Sequence;
            this.Comparison = Comparison;
        }


        protected override IEnumerable<StateMachineResult> Next(State Input) {
            while (true) {
                var Start = Sequence.Text.GetEnumerator();
                Start.MoveNext();

                //Each time that we get an input that is a KeyDown
                while (Input.Current is { } Current) {

                    if (Current?.TextClick?.Text is { } V1 && V1.Length > 0) {
                        var ShouldTrigger = false;

                        var TempResult = StateMachineResult.Rejected;

                        //Add each character to our buffer.
                        foreach (var item in V1) {
                            var DoReset = false;
                            if (item == Start.Current) {
                                TempResult = StateMachineResult.Accepted;

                                if (!Start.MoveNext()) {
                                    TempResult = StateMachineResult.Complete;
                                    ShouldTrigger = true;
                                    DoReset = true;
                                }

                            } else {
                                TempResult = StateMachineResult.Rejected;
                                DoReset = true;
                            }

                            if (DoReset) {
                                Start = Sequence.Text.GetEnumerator();
                                Start.MoveNext();
                            }

                        }

                        if (ShouldTrigger == true) {
                            yield return StateMachineResult.Complete;
                        } else {
                            yield return TempResult;
                        }


                    } else {
                        yield return StateMachineResult.Rejected;
                    }

                }
            }

        }

    }


}
