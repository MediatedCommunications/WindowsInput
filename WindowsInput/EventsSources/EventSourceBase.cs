﻿// This code is distributed under MIT license. 
// Copyright (c) 2015 George Mamaladze
// See license.txt or https://mit-license.org/

using System;

namespace WindowsInput.Events.Sources {
    public abstract class EventSourceBase : IEventSource {
        public event EventHandler<EnabledChangedEventArgs>? EnabledChanged;

        private bool __Enabled;
        public bool Enabled {
            get {
                return __Enabled;
            }
            set {
                var NewValue = value;
                if(NewValue != Enabled) {
                    if (NewValue) {
                        NewValue = Enable();
                    } else {
                        Disable();
                    }

                    if (__Enabled != NewValue) {
                        __Enabled = NewValue;

                        EnabledChanged?.Invoke(this, new EnabledChangedEventArgs(Enabled));
                    }

                }
            }
        }

        protected virtual bool Enable() {
            return false;
        }


        protected virtual void Disable() {

        }

        protected virtual void Dispose() {
            Enabled = false;
        }

        void IDisposable.Dispose() {
            Dispose();
        }
    }

}