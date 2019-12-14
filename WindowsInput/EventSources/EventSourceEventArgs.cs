using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput.Native;

namespace WindowsInput.EventSources {

    public class EventSourceEventArgs {
        public bool Handled { get; set; }
        public DateTimeOffset Timestamp { get; private set; }

        public EventSourceEventArgs(DateTimeOffset Timestamp, bool Handled = false) {
            this.Timestamp = Timestamp;
            this.Handled = Handled;
        }

        public EventSourceEventArgs(int Timestamp, bool Handled = false) {
            this.Timestamp = BootTime.Value.AddMilliseconds(Timestamp);
            this.Handled = Handled;
        }

        public static EventSourceEventArgs<TData> Create<TData>(DateTimeOffset Timestamp, bool Handled, TData Data) {
            return new EventSourceEventArgs<TData>(Timestamp, Handled, Data);
        }

        public static EventSourceEventArgs<TData> Create<TData>(int Timestamp, bool Handled, TData Data) {
            return new EventSourceEventArgs<TData>(Timestamp, Handled, Data);
        }

    }

    public class EventSourceEventArgs<TData> : EventSourceEventArgs { 
    
        public TData Data { get; private set; }

        public EventSourceEventArgs(DateTimeOffset Timestamp, bool Handled, TData Data) : base(Timestamp, Handled) {
            this.Data = Data;
        }

        public EventSourceEventArgs(int Timestamp, bool Handled, TData Data) : base(Timestamp, Handled) {
            this.Data = Data;
        }


    }

}
