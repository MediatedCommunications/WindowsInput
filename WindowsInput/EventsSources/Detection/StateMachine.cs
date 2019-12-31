using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput.Events;

namespace WindowsInput.EventSources {

    public class StateMachine<TInput, TOutput> {
        public StateMachine() {
            Init(x => Next(x));
        }

        public StateMachine(Func<Value, IEnumerable<TOutput>> NextSource) {
            Init(NextSource);
        }

        private void Init(Func<Value, IEnumerable<TOutput>> NextSource) {
            this.NextSource = NextSource;
            Reset();
        }
        private Func<Value, IEnumerable<TOutput>> NextSource;



        private Value Adder;
        private IEnumerator<TOutput> Elements;

        public void Reset() {
            Adder = new Value();
            Elements = NextSource(Adder).GetEnumerator();
        }

        public TOutput Next(TInput Input) {
            Adder.Current = Input;
            Elements.MoveNext();

            var ret = Elements.Current;

            return ret;
        }

        public class Value {
            public TInput Current { get; set; }
        }

        protected virtual IEnumerable<TOutput> Next(Value Item) {
            yield break;
        }


    }





}
