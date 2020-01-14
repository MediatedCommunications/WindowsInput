// This code is distributed under MIT license. 
// Copyright (c) 2015 George Mamaladze
// See license.txt or https://mit-license.org/

using System.Collections.Generic;
using System.Linq;
using WindowsInput.Events;

namespace WindowsInput.Events.Sources {
    public class KeyboardEvent : InputEvent {

        public KeyboardEvent(Wait Wait, KeyDown KeyDown, TextClick TextClick, KeyUp KeyUp) {
            this.Wait = Wait;
            this.KeyDown = KeyDown;
            this.TextClick = TextClick;
            this.KeyUp = KeyUp;

            {
                var PotentialEvents = new List<IEvent>();
                PotentialEvents.Add(Wait);
                PotentialEvents.Add(KeyDown);
                PotentialEvents.Add(TextClick);
                PotentialEvents.Add(KeyUp);

                PotentialEvents = PotentialEvents.Where(x => x is { }).ToList();
                this.Events = PotentialEvents.AsReadOnly();

            }


        }

        public Wait Wait { get; private set; }
        public KeyDown KeyDown { get; private set; }
        public KeyUp KeyUp { get; private set; }
        public TextClick TextClick { get; private set; }

    }
}
