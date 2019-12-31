// This code is distributed under MIT license. 
// Copyright (c) 2015 George Mamaladze
// See license.txt or https://mit-license.org/

using System;

namespace WindowsInput.EventSources {
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

                    EnabledChanged?.Invoke(this, new EnabledChangedEventArgs(Enabled));

                }
            }
        }

        protected virtual void Enable() { 
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