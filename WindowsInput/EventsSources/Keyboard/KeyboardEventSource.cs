// This code is distributed under MIT license. 
// Copyright (c) 2015 George Mamaladze
// See license.txt or https://mit-license.org/

using System;
using System.Collections.Generic;
using WindowsInput.Native;
using System.Collections.ObjectModel;
using System.Linq;


using WindowsInput.Events;

namespace WindowsInput.Events.Sources {

    public abstract class KeyboardEventSource : HookEventSource, IKeyboardEventSource {

        public event EventHandler<EventSourceEventArgs<KeyboardEvent>> KeyEvent;

        public event EventHandler<EventSourceEventArgs<Wait>> Wait;

        public event EventHandler<EventSourceEventArgs<KeyDown>> KeyDown;
        public event EventHandler<EventSourceEventArgs<TextClick>> TextClick;
        public event EventHandler<EventSourceEventArgs<KeyUp>> KeyUp;

        protected KeyboardEventSourceState State { get; private set; } = new KeyboardEventSourceState();

        protected override void Enable() {
            State = new KeyboardEventSourceState();
            base.Enable();
           
        }

        protected EventSourceEventArgs InvokeMany(KeyboardEvent Event, object RawData, DateTimeOffset Timestamp) {
            var ret = InvokeMany(
                x => InvokeEvent(x, Event, RawData, Timestamp),

                x => InvokeEvent(x, Event.Wait, RawData, Timestamp),

                x => InvokeEvent(x, Event.KeyDown, RawData, Timestamp),
                x => InvokeEvent(x, Event.TextClick, RawData, Timestamp),
                x => InvokeEvent(x, Event.KeyUp, RawData, Timestamp)
            );

            return ret;

        }

        protected EventSourceEventArgs InvokeEvent(EventSourceEventArgs args, KeyboardEvent Data, object RawData, DateTimeOffset Timestamp) => InvokeEvent(args, KeyEvent, Data, RawData, Timestamp);
        protected EventSourceEventArgs InvokeEvent(EventSourceEventArgs args, Wait Data, object RawData, DateTimeOffset Timestamp) => InvokeEvent(args, Wait, Data, RawData, Timestamp);
        protected EventSourceEventArgs InvokeEvent(EventSourceEventArgs args, KeyDown Data, object RawData, DateTimeOffset Timestamp) => InvokeEvent(args, KeyDown, Data, RawData, Timestamp);
        protected EventSourceEventArgs InvokeEvent(EventSourceEventArgs args, TextClick Data, object RawData, DateTimeOffset Timestamp) => InvokeEvent(args, TextClick, Data, RawData, Timestamp);
        protected EventSourceEventArgs InvokeEvent(EventSourceEventArgs args, KeyUp Data, object RawData, DateTimeOffset Timestamp) => InvokeEvent(args, KeyUp, Data, RawData, Timestamp);


    }
}