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


    public class SuspendEventSource : IDisposable {

        protected IEventSource Target { get; private set; }
        protected bool Original { get; private set; }

        public SuspendEventSource(IEventSource Target) {
            this.Target = Target;

            this.Original = Target.Enabled;
            Target.Enabled = false;
        }

        public void Dispose() {
            Target.Enabled = Original;
        }
    }

    public static partial class EventSourceExtensions {
        public static SuspendEventSource Suspend(this IEventSource This) {
            return new SuspendEventSource(This);
        }
    }


    public abstract class EventSourceBase : IEventSource {
        public event EventHandler<EnabledChangedEventArgs> EnabledChanged;

        private bool __Enabled;
        public bool Enabled {
            get {
                return __Enabled;
            }
            set {
                if(value != Enabled) {
                    if (value) {
                        Enable();
                    } else {
                        Disable();
                    }
                    __Enabled = value;
                }
            }
        }

        protected virtual void Enable() { 
        }


        protected virtual void Disable() {

        }

        protected virtual void Dispose() {
            
        }

        void IDisposable.Dispose() {
            Dispose();
        }
    }


    public class EnabledChangedEventArgs : EventArgs {
        public EnabledChangedEventArgs(bool NewValue){
            this.NewValue = NewValue;
        }

        public bool NewValue { get; private set; }
    }

}