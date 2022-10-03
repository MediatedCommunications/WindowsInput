using System;
using System.Collections.Generic;
using WindowsInput.Events;

namespace WindowsInput.Events.Sources {

    public class KeyChordEventArgs : EventArgs {
        public EventSourceEventArgs<KeyboardEvent> Input { get; }
        public ChordClick Chord { get; }

        public KeyChordEventArgs(EventSourceEventArgs<KeyboardEvent> Input, ChordClick Chord) { 
            this.Input = Input;
            this.Chord = Chord;
        }

    }


    public class KeyChordEventSource : KeyboardMonitoringEventSource {
        public event EventHandler<KeyChordEventArgs>? Triggered;

        public ChordClick Chord { get; }

        public KeyChordEventSource(IKeyboardEventSource Monitor, ChordClick Chord) : base(Monitor) {
            this.Chord = Chord;
            
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

        private ChordEventSourceStateMachine? State;
        protected override void Reset() {
            base.Reset();
            State = new ChordEventSourceStateMachine(Chord);
        }

        private void Monitor_KeyEvent(object? sender, EventSourceEventArgs<KeyboardEvent> e) {
            if(State is { } && State.TryNext(e.Data, out var Status) && Status == StateMachineResult.Complete) {
                var args = new KeyChordEventArgs(e, Chord);
                Triggered?.Invoke(this, args);
            }
        }
    }

    public class ChordEventSourceStateMachine : StateMachine<KeyboardEvent, StateMachineResult> {
        public ChordClick Chord { get; }
        public int MaxSequentialTriggers { get; }

        public ChordEventSourceStateMachine(ChordClick Chord) : this(Chord, 1) {

        }

        public ChordEventSourceStateMachine(ChordClick Chord, int MaxSequentialTriggers) {
            this.Chord = Chord;
            this.MaxSequentialTriggers = MaxSequentialTriggers;
        }

        protected override IEnumerable<StateMachineResult> Next(State Input) {
            var Expected = new HashSet<KeyCode>(Chord.Keys);

            var SequentialTriggers = 0;

            while (true) {
                var Down = new HashSet<KeyCode>();

                //Each time that we get an input that is a KeyDown
                while (Input.Current is { } Current) {

                    System.Diagnostics.Debug.WriteLine(Input.Current);

                    if (Current.KeyDown is { } V1) {
                        var Key = PotentiallyNormalize(V1.Key, Expected);

                        Down.Add(Key);
                        if (Expected.IsSubsetOf(Down)) {
                            SequentialTriggers += 1;
                            if(SequentialTriggers <= MaxSequentialTriggers) {
                                yield return StateMachineResult.Complete;
                            } else {
                                yield return StateMachineResult.Rejected;
                            }
                        } else {
                            SequentialTriggers = 0;
                            yield return StateMachineResult.Rejected;
                        }

                    } else if (Current.KeyUp is { } V2) {
                        var Key = PotentiallyNormalize(V2.Key, Expected);

                        Down.Remove(Key);
                        SequentialTriggers = 0;
                        yield return StateMachineResult.Rejected;
                    } else {
                        SequentialTriggers = 0;
                        yield return StateMachineResult.Rejected;
                    }

                }
            }
        }

        private KeyCode PotentiallyNormalize(KeyCode key, HashSet<KeyCode> expected) {
            var ret = key;

            var Normalized = key.Normalize();
            if (expected.Contains(Normalized)) {
                ret = Normalized;
            }


            return ret;
            
        }
    }

}
