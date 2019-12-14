// This code is distributed under MIT license. 
// Copyright (c) 2015 George Mamaladze
// See license.txt or https://mit-license.org/

using WindowsInput.Events;

namespace WindowsInput.EventSources {
    public class KeyboardEvent {

        public KeyboardEvent(Wait Wait, KeyDown KeyDown, KeyUp KeyUp, TextClick TextClick) {
            this.Wait = Wait;
            this.KeyDown = KeyDown;
            this.KeyUp = KeyUp;
            this.TextClick = TextClick;
        }

        public Wait Wait { get; private set; }
        public KeyDown KeyDown { get; private set; }
        public KeyUp KeyUp { get; private set; }
        public TextClick TextClick { get; private set; }
    }
}
