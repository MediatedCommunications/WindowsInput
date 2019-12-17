using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput.Native;

namespace WindowsInput.EventSources {

    public class EventSourceEventArgs {
        public bool Next_Hook_Enabled { get; set; } = true;
        public bool Next_Event_Enabled { get; set; } = true;
        public DateTimeOffset Timestamp { get; private set; }

        public EventSourceEventArgs(DateTimeOffset Timestamp) {
            this.Timestamp = Timestamp;
        }

        public EventSourceEventArgs(int Timestamp) {
            this.Timestamp = BootTime.Value.AddMilliseconds(Timestamp);
        }

        public static EventSourceEventArgs<TData> Create<TData>(DateTimeOffset Timestamp, TData Data) {
            return new EventSourceEventArgs<TData>(Timestamp, Data);
        }

        public static EventSourceEventArgs<TData> Create<TData>(int Timestamp, TData Data) {
            return new EventSourceEventArgs<TData>(Timestamp, Data);
        }

    }

    public class EventSourceEventArgs<TData> : EventSourceEventArgs { 
    
        public TData Data { get; private set; }

        public EventSourceEventArgs(DateTimeOffset Timestamp, TData Data) : base(Timestamp) {
            this.Data = Data;
        }

        public EventSourceEventArgs(int Timestamp, TData Data) : base(Timestamp) {
            this.Data = Data;
        }


    }

}
