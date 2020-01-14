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

    public interface IKeyboardEventSource : IEventSource {

        event EventHandler<EventSourceEventArgs<KeyboardEvent>> KeyEvent;

        event EventHandler<EventSourceEventArgs<Wait>> Wait;

        event EventHandler<EventSourceEventArgs<KeyDown>> KeyDown;
        event EventHandler<EventSourceEventArgs<TextClick>> TextClick;
        event EventHandler<EventSourceEventArgs<KeyUp>> KeyUp;

    }
}