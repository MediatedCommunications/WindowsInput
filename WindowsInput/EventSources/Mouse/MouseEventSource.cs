// This code is distributed under MIT license.
// Copyright (c) 2015 George Mamaladze
// See license.txt or https://mit-license.org/

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using WindowsInput.Native;
using WindowsInput.Events;

namespace WindowsInput.EventSources {

    public abstract class MouseEventSource : EventSource, IMouseEventSource {
        protected MouseEventSourceState State { get; private set; } = new MouseEventSourceState();

        protected override void EnableInternal() {
            base.EnableInternal();

            this.State = new MouseEventSourceState();
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
        public event EventHandler<EventSourceEventArgs<ButtonDoubleClick>> ButtonDoubleClick;

        public event EventHandler<EventSourceEventArgs<IReadOnlyCollection<DragStart>>> DragStarted;
        public event EventHandler<EventSourceEventArgs<IReadOnlyCollection<DragDrop>>> DragFinished;

        protected override bool Callback(CallbackData data) {
            var e = GetEventArgs(data);

            var Events = State.GetEventArgs(e);
            var Handled = InvokeEvent(
                () => InvokeEvent(Events.Data, e.Timestamp),

                () => InvokeEvent(Events.Data.Wait, e.Timestamp),
                () => InvokeEvent(Events.Data.Move, e.Timestamp),
                () => InvokeEvent(Events.Data.ButtonScroll, e.Timestamp),
                () => InvokeEvent(Events.Data.ButtonDown, e.Timestamp),
                () => InvokeEvent(Events.Data.DragStart, e.Timestamp),
                () => InvokeEvent(Events.Data.DragStop, e.Timestamp),
                () => InvokeEvent(Events.Data.ButtonUp, e.Timestamp),
                
                () => InvokeEvent(Events.Data.ButtonClick, e.Timestamp),
                () => InvokeEvent(Events.Data.ButtonDoubleClick, e.Timestamp)
                );

            return !e.Handled;
        }

        protected abstract EventSourceEventArgs<MouseInput> GetEventArgs(CallbackData data);



        protected bool InvokeEvent(MouseEvent Data, DateTimeOffset Timestamp) => InvokeEvent(MouseEvent, Data, Timestamp);
        protected bool InvokeEvent(Wait Data, DateTimeOffset Timestamp) => InvokeEvent(Wait, Data, Timestamp);
        protected bool InvokeEvent(MouseMove Data, DateTimeOffset Timestamp) => InvokeEvent(MouseMove, Data, Timestamp);
        protected bool InvokeEvent(ButtonScroll Data, DateTimeOffset Timestamp) => InvokeEvent(ButtonScroll, Data, Timestamp);
        protected bool InvokeEvent(ButtonDown Data, DateTimeOffset Timestamp) => InvokeEvent(ButtonDown, Data, Timestamp);
        protected bool InvokeEvent(IReadOnlyCollection<DragStart> Data, DateTimeOffset Timestamp) => InvokeEvent(DragStarted, Data, Timestamp);
        protected bool InvokeEvent(IReadOnlyCollection<DragDrop> Data, DateTimeOffset Timestamp) => InvokeEvent(DragFinished, Data, Timestamp);
        protected bool InvokeEvent(ButtonUp Data, DateTimeOffset Timestamp) => InvokeEvent(ButtonUp, Data, Timestamp);
        protected bool InvokeEvent(ButtonClick Data, DateTimeOffset Timestamp) => InvokeEvent(ButtonClick, Data, Timestamp);
        protected bool InvokeEvent(ButtonDoubleClick Data, DateTimeOffset Timestamp) => InvokeEvent(ButtonDoubleClick, Data, Timestamp);




    }
}