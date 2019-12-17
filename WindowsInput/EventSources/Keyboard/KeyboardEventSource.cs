// This code is distributed under MIT license. 
// Copyright (c) 2015 George Mamaladze
// See license.txt or https://mit-license.org/

using System;
using System.Collections.Generic;
using WindowsInput.Native;
using System.Collections.ObjectModel;
using System.Linq;


using WindowsInput.Events;

namespace WindowsInput.EventSources {

    public abstract class KeyboardEventSource : HookEventSource, IKeyboardEventSource {

        public event EventHandler<EventSourceEventArgs<KeyboardEvent>> KeyEvent;

        public event EventHandler<EventSourceEventArgs<Wait>> Wait;

        public event EventHandler<EventSourceEventArgs<KeyDown>> KeyDown;
        public event EventHandler<EventSourceEventArgs<TextClick>> TextClick;
        public event EventHandler<EventSourceEventArgs<KeyUp>> KeyUp;

        protected KeyboardEventSourceState State { get; private set; } = new KeyboardEventSourceState();

        protected override void EnableInternal() {
            base.EnableInternal();

            State = new KeyboardEventSourceState();
        }

        protected EventSourceEventArgs InvokeMany(KeyboardEvent Event, DateTimeOffset Timestamp) {
            var ret = InvokeMany(
                x => InvokeEvent(x, Event, Timestamp),

                x => InvokeEvent(x, Event.Wait, Timestamp),

                x => InvokeEvent(x, Event.KeyDown, Timestamp),
                x => InvokeEvent(x, Event.TextClick, Timestamp),
                x => InvokeEvent(x, Event.KeyUp, Timestamp)
            );

            return ret;

        }

        protected EventSourceEventArgs InvokeEvent(EventSourceEventArgs args, KeyboardEvent Data, DateTimeOffset Timestamp) => InvokeEvent(args, KeyEvent, Data, Timestamp);
        protected EventSourceEventArgs InvokeEvent(EventSourceEventArgs args, Wait Data, DateTimeOffset Timestamp) => InvokeEvent(args, Wait, Data, Timestamp);
        protected EventSourceEventArgs InvokeEvent(EventSourceEventArgs args, KeyDown Data, DateTimeOffset Timestamp) => InvokeEvent(args, KeyDown, Data, Timestamp);
        protected EventSourceEventArgs InvokeEvent(EventSourceEventArgs args, TextClick Data, DateTimeOffset Timestamp) => InvokeEvent(args, TextClick, Data, Timestamp);
        protected EventSourceEventArgs InvokeEvent(EventSourceEventArgs args, KeyUp Data, DateTimeOffset Timestamp) => InvokeEvent(args, KeyUp, Data, Timestamp);


    }
}