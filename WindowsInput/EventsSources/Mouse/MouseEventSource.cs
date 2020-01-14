// This code is distributed under MIT license.
// Copyright (c) 2015 George Mamaladze
// See license.txt or https://mit-license.org/

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using WindowsInput.Native;
using WindowsInput.Events;

namespace WindowsInput.Events.Sources {

    public abstract class MouseEventSource : HookEventSource, IMouseEventSource {
        protected MouseEventSourceState State { get; private set; } = new MouseEventSourceState();

        protected override void Enable() {
            this.State = new MouseEventSourceState();
            base.Enable();
        }

        protected MouseEventSource() {

        }

        public event EventHandler<EventSourceEventArgs<MouseEvent>> MouseEvent;
        public event EventHandler<EventSourceEventArgs<Wait>> Wait;

        public event EventHandler<EventSourceEventArgs<MouseMove>> MouseMove;
        public event EventHandler<EventSourceEventArgs<ButtonClick>> ButtonClick;
        public event EventHandler<EventSourceEventArgs<ButtonDown>> ButtonDown;
        public event EventHandler<EventSourceEventArgs<ButtonUp>> ButtonUp;
        public event EventHandler<EventSourceEventArgs<ButtonScroll>> ButtonScroll;
        public event EventHandler<EventSourceEventArgs<ButtonClickHold>> ButtonClickHold;
        public event EventHandler<EventSourceEventArgs<ButtonDoubleClick>> ButtonDoubleClick;

        public event EventHandler<EventSourceEventArgs<IReadOnlyCollection<DragStart>>> DragStarted;
        public event EventHandler<EventSourceEventArgs<IReadOnlyCollection<DragDrop>>> DragFinished;

        protected EventSourceEventArgs InvokeMany(MouseEvent Event, DateTimeOffset Timestamp) {
            var ret = InvokeMany(
                x => InvokeEvent(x, Event, Timestamp),

                x => InvokeEvent(x, Event.Wait, Timestamp),
                x => InvokeEvent(x, Event.Move, Timestamp),
                x => InvokeEvent(x, Event.ButtonScroll, Timestamp),
                x => InvokeEvent(x, Event.ButtonDown, Timestamp),
                x => InvokeEvent(x, Event.DragStart, Timestamp),
                x => InvokeEvent(x, Event.DragStop, Timestamp),
                x => InvokeEvent(x, Event.ButtonUp, Timestamp),

                x => InvokeEvent(x, Event.ButtonClick, Timestamp),
                x => InvokeEvent(x, Event.ButtonClickHold, Timestamp),
                x => InvokeEvent(x, Event.ButtonDoubleClick, Timestamp)
            );

            return ret;
        }


        protected EventSourceEventArgs InvokeEvent(EventSourceEventArgs args, MouseEvent Data, DateTimeOffset Timestamp) => InvokeEvent(args, MouseEvent, Data, Timestamp);
        protected EventSourceEventArgs InvokeEvent(EventSourceEventArgs args, Wait Data, DateTimeOffset Timestamp) => InvokeEvent(args, Wait, Data, Timestamp);
        protected EventSourceEventArgs InvokeEvent(EventSourceEventArgs args, MouseMove Data, DateTimeOffset Timestamp) => InvokeEvent(args, MouseMove, Data, Timestamp);
        protected EventSourceEventArgs InvokeEvent(EventSourceEventArgs args, ButtonScroll Data, DateTimeOffset Timestamp) => InvokeEvent(args, ButtonScroll, Data, Timestamp);
        protected EventSourceEventArgs InvokeEvent(EventSourceEventArgs args, ButtonDown Data, DateTimeOffset Timestamp) => InvokeEvent(args, ButtonDown, Data, Timestamp);
        protected EventSourceEventArgs InvokeEvent(EventSourceEventArgs args, IReadOnlyCollection<DragStart> Data, DateTimeOffset Timestamp) => InvokeEvent(args, DragStarted, Data, Timestamp);
        protected EventSourceEventArgs InvokeEvent(EventSourceEventArgs args, IReadOnlyCollection<DragDrop> Data, DateTimeOffset Timestamp) => InvokeEvent(args, DragFinished, Data, Timestamp);
        protected EventSourceEventArgs InvokeEvent(EventSourceEventArgs args, ButtonUp Data, DateTimeOffset Timestamp) => InvokeEvent(args, ButtonUp, Data, Timestamp);
        protected EventSourceEventArgs InvokeEvent(EventSourceEventArgs args, ButtonClick Data, DateTimeOffset Timestamp) => InvokeEvent(args, ButtonClick, Data, Timestamp);
        protected EventSourceEventArgs InvokeEvent(EventSourceEventArgs args, ButtonClickHold Data, DateTimeOffset Timestamp) => InvokeEvent(args, ButtonClickHold, Data, Timestamp);
        protected EventSourceEventArgs InvokeEvent(EventSourceEventArgs args, ButtonDoubleClick Data, DateTimeOffset Timestamp) => InvokeEvent(args, ButtonDoubleClick, Data, Timestamp);




    }
}