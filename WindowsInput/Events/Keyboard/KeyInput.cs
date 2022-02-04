// This code is distributed under MIT license. 
// Copyright (c) 2015 George Mamaladze
// See license.txt or https://mit-license.org/

using System;
using System.Collections.Generic;

namespace WindowsInput.Events {
    public class KeyInput : KeyEvent {
        public int ScanCode { get; }
        public KeyStatus Status { get; }

        protected override string GetDebuggerDisplay() {
            var ret = base.GetDebuggerDisplay();

            return ret;
        }

        public KeyInput(KeyCode RawKey, bool? Extended, int ScanCode, KeyStatus Status)  : base(RawKey, Extended){
            this.ScanCode = ScanCode;
            this.Status = Status;


            Initialize(CreateChildren());
        }

        private IEnumerable<IEvent> CreateChildren() {

            if (this.Status == KeyStatus.Pressed) {
                yield return new KeyDown(Key, Extended);
            } else if (this.Status == KeyStatus.Released) {
                yield return new KeyUp(Key, Extended);
            }

        }

    }
}