// This code is distributed under MIT license. 
// Copyright (c) 2015 George Mamaladze
// See license.txt or https://mit-license.org/

using System.Collections.Generic;
using System.Linq;
using WindowsInput.Events;

namespace WindowsInput.Events.Sources {
    public class KeyboardEvent : InputEvent {

        public KeyboardEvent(
            Wait? Wait, 
            KeyDown? KeyDown, 
            TextClick? TextClick, 
            KeyUp? KeyUp
            ) : base(
                new List<IEvent?>() {
                    Wait,
                    KeyDown,
                    TextClick,
                    KeyUp
                }) {

            this.Wait = Wait;
            this.KeyDown = KeyDown;
            this.TextClick = TextClick;
            this.KeyUp = KeyUp;

        }

        public Wait? Wait { get; }
        public KeyDown? KeyDown { get; }
        public KeyUp? KeyUp { get; }
        public TextClick? TextClick { get; }

    }
}
