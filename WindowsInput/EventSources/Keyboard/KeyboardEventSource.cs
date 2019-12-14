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

    public abstract class KeyboardEventSource : EventSource, IKeyboardEventSource {

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

        protected bool InvokeEvent(KeyboardEvent Data, DateTimeOffset Timestamp) => InvokeEvent(KeyEvent, Data, Timestamp);
        protected bool InvokeEvent(Wait Data, DateTimeOffset Timestamp) => InvokeEvent(Wait, Data, Timestamp);
        protected bool InvokeEvent(KeyDown Data, DateTimeOffset Timestamp) => InvokeEvent(KeyDown, Data, Timestamp);
        protected bool InvokeEvent(TextClick Data, DateTimeOffset Timestamp) => InvokeEvent(TextClick, Data, Timestamp);
        protected bool InvokeEvent(KeyUp Data, DateTimeOffset Timestamp) => InvokeEvent(KeyUp, Data, Timestamp);


    }
}