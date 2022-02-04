using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput.Native;

namespace WindowsInput.Events.Sources {

    public class EventSourceEventArgs {
        public bool Next_Hook_Enabled { get; set; } = true;
        public bool Next_Event_Enabled { get; set; } = true;
        public DateTimeOffset Timestamp { get; }

        public EventSourceEventArgs(DateTimeOffset Timestamp) {
            this.Timestamp = Timestamp;
        }

        public EventSourceEventArgs(int Timestamp) {
            this.Timestamp = BootTime.Value.AddMilliseconds(Timestamp);
        }

        public static EventSourceEventArgs<TData> Create<TData>(DateTimeOffset Timestamp, TData Data, object RawData) {
            return new EventSourceEventArgs<TData>(Timestamp, Data, RawData);
        }

        public static EventSourceEventArgs<TData> Create<TData>(int Timestamp, TData Data, object RawData) {
            return new EventSourceEventArgs<TData>(Timestamp, Data, RawData);
        }

    }

    public class EventSourceEventArgs<TData> : EventSourceEventArgs { 
    
        public TData Data { get; }
        public object RawData { get; }

        public EventSourceEventArgs(DateTimeOffset Timestamp, TData Data, object RawData) : base(Timestamp) {
            this.Data = Data;
            this.RawData = RawData;
        }

        public EventSourceEventArgs(int Timestamp, TData Data, object RawData) : base(Timestamp) {
            this.Data = Data;
            this.RawData = RawData;
        }

    }

}
