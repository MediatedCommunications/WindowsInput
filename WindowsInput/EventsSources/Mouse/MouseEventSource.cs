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

        public event EventHandler<EventSourceEventArgs<MouseEvent>>? MouseEvent;
        public event EventHandler<EventSourceEventArgs<Wait>>? Wait;

        public event EventHandler<EventSourceEventArgs<MouseMove>>? MouseMove;
        public event EventHandler<EventSourceEventArgs<ButtonClick>>? ButtonClick;
        public event EventHandler<EventSourceEventArgs<ButtonDown>>? ButtonDown;
        public event EventHandler<EventSourceEventArgs<ButtonUp>>? ButtonUp;
        public event EventHandler<EventSourceEventArgs<ButtonScroll>>? ButtonScroll;
        public event EventHandler<EventSourceEventArgs<ButtonClickHold>>? ButtonClickHold;
        public event EventHandler<EventSourceEventArgs<ButtonDoubleClick>>? ButtonDoubleClick;

        public event EventHandler<EventSourceEventArgs<IReadOnlyList<DragStart>>>? DragStarted;
        public event EventHandler<EventSourceEventArgs<IReadOnlyList<DragDrop>>>? DragFinished;

        protected EventSourceEventArgs InvokeMany(MouseEvent Event, object RawData, DateTimeOffset Timestamp) {
            var ret = InvokeMany(
                x => InvokeEvent(x, Event, RawData, Timestamp),

                x => InvokeEvent(x, Event.Wait, RawData, Timestamp),
                x => InvokeEvent(x, Event.Move, RawData, Timestamp),
                x => InvokeEvent(x, Event.ButtonScroll, RawData, Timestamp),
                x => InvokeEvent(x, Event.ButtonDown, RawData, Timestamp),
                x => InvokeEvent(x, Event.DragStart, RawData, Timestamp),
                x => InvokeEvent(x, Event.DragStop, RawData, Timestamp),
                x => InvokeEvent(x, Event.ButtonUp, RawData, Timestamp),

                x => InvokeEvent(x, Event.ButtonClick, RawData, Timestamp),
                x => InvokeEvent(x, Event.ButtonClickHold, RawData, Timestamp),
                x => InvokeEvent(x, Event.ButtonDoubleClick, RawData, Timestamp)
            );

            return ret;
        }


        protected EventSourceEventArgs InvokeEvent(EventSourceEventArgs args, MouseEvent? Data, object RawData, DateTimeOffset Timestamp) => InvokeEvent(args, MouseEvent, Data, RawData, Timestamp);
        protected EventSourceEventArgs InvokeEvent(EventSourceEventArgs args, Wait? Data, object RawData, DateTimeOffset Timestamp) => InvokeEvent(args, Wait, Data, RawData, Timestamp);
        protected EventSourceEventArgs InvokeEvent(EventSourceEventArgs args, MouseMove? Data, object RawData, DateTimeOffset Timestamp) => InvokeEvent(args, MouseMove, Data, RawData, Timestamp);
        protected EventSourceEventArgs InvokeEvent(EventSourceEventArgs args, ButtonScroll? Data, object RawData, DateTimeOffset Timestamp) => InvokeEvent(args, ButtonScroll, Data, RawData, Timestamp);
        protected EventSourceEventArgs InvokeEvent(EventSourceEventArgs args, ButtonDown? Data, object RawData, DateTimeOffset Timestamp) => InvokeEvent(args, ButtonDown, Data, RawData, Timestamp);
        protected EventSourceEventArgs InvokeEvent(EventSourceEventArgs args, IReadOnlyList<DragStart>? Data, object RawData, DateTimeOffset Timestamp) => InvokeEvent(args, DragStarted, Data, RawData, Timestamp);
        protected EventSourceEventArgs InvokeEvent(EventSourceEventArgs args, IReadOnlyList<DragDrop>? Data, object RawData, DateTimeOffset Timestamp) => InvokeEvent(args, DragFinished, Data, RawData, Timestamp);
        protected EventSourceEventArgs InvokeEvent(EventSourceEventArgs args, ButtonUp? Data, object RawData, DateTimeOffset Timestamp) => InvokeEvent(args, ButtonUp, Data, RawData, Timestamp);
        protected EventSourceEventArgs InvokeEvent(EventSourceEventArgs args, ButtonClick? Data, object RawData, DateTimeOffset Timestamp) => InvokeEvent(args, ButtonClick, Data, RawData, Timestamp);
        protected EventSourceEventArgs InvokeEvent(EventSourceEventArgs args, ButtonClickHold? Data, object RawData, DateTimeOffset Timestamp) => InvokeEvent(args, ButtonClickHold, Data, RawData, Timestamp);
        protected EventSourceEventArgs InvokeEvent(EventSourceEventArgs args, ButtonDoubleClick? Data, object RawData, DateTimeOffset Timestamp) => InvokeEvent(args, ButtonDoubleClick, Data, RawData, Timestamp);

    }
}