using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput.Events;

namespace WindowsInput.Events.Sources {

    public abstract class StateMachine<TInput, TOutput> {
        public StateMachine() {
            Reset();
        }

        private State? CurrentState;
        private Action<TInput>? CurrentStateSetter;
        private IEnumerator<TOutput>? Elements;

        public void Reset() {
            CurrentState = new State(out CurrentStateSetter);
            Elements = Next(CurrentState).GetEnumerator();
        }

        public bool TryNext(TInput Input, out TOutput? Value) {
            var ret = false;
            Value = default;
            
            if (Elements is { }) {
                CurrentStateSetter?.Invoke(Input);

                if (Elements.MoveNext()) {
                    Value = Elements.Current;
                    ret = true;
                } else {
                    Elements = default;
                    CurrentState = default;
                    CurrentStateSetter = default;
                }
            }

            return ret;
        }

        public TOutput? Next(TInput Input) {
            var ret = default(TOutput?);

            if (TryNext(Input, out var tret)) {
                ret = tret;
            }

            return ret;
        }

        public class State {
            public TInput? Current { get; private set; }

            public State(out Action<TInput> Setter) {
                Setter = x => Current = x;
            }

            public State() { 
            
            }
           

        }

        protected abstract IEnumerable<TOutput> Next(State Item);


    }





}
