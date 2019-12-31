// This code is distributed under MIT license.
// Copyright (c) 2015 George Mamaladze
// See license.txt or https://mit-license.org/

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using WindowsInput.Native;
using WindowsInput.Events;

namespace WindowsInput.EventSources {

    public interface IMouseEventSource : IEventSource {
        event EventHandler<EventSourceEventArgs<MouseEvent>> MouseEvent;
        event EventHandler<EventSourceEventArgs<Wait>> Wait;

        event EventHandler<EventSourceEventArgs<MouseMove>> MouseMove;
        event EventHandler<EventSourceEventArgs<ButtonClick>> ButtonClick;
        event EventHandler<EventSourceEventArgs<ButtonDown>> ButtonDown;
        event EventHandler<EventSourceEventArgs<ButtonUp>> ButtonUp;
        event EventHandler<EventSourceEventArgs<ButtonScroll>> ButtonScroll;
        event EventHandler<EventSourceEventArgs<ButtonClickHold>> ButtonClickHold;
        event EventHandler<EventSourceEventArgs<ButtonDoubleClick>> ButtonDoubleClick;

        event EventHandler<EventSourceEventArgs<IReadOnlyCollection<DragStart>>> DragStarted;
        event EventHandler<EventSourceEventArgs<IReadOnlyCollection<DragDrop>>> DragFinished;


    }
}