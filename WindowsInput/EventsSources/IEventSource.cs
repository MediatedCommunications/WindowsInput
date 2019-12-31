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
        event EventHandler<EnabledChangedEventArgs> EnabledChanged;

        bool Enabled { get; set; }

    }

    public static partial class EventSourceExtensions {
        public static SuspendEventSource Suspend(this IEventSource This) {
            return new SuspendEventSource(This);
        }
    }


    public class EnabledChangedEventArgs : EventArgs {
        public EnabledChangedEventArgs(bool NewValue){
            this.NewValue = NewValue;
        }

        public bool NewValue { get; private set; }
    }

}