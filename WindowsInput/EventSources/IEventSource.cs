// This code is distributed under MIT license. 
// Copyright (c) 2015 George Mamaladze
// See license.txt or https://mit-license.org/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using WindowsInput.Native;

namespace WindowsInput.EventSources {
    public interface IEventSource : IDisposable {


        bool Enabled { get; set; }

        void Enable();


        void Disable();

    }
}