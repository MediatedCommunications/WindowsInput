// This code is distributed under MIT license.
// Copyright (c) 2015 George Mamaladze
// See license.txt or https://mit-license.org/

using System.Collections.Generic;
using System.Linq;
using WindowsInput.Events;

namespace WindowsInput.EventSources {

    public class MouseEvent : InputEvent {


        public MouseEvent(Wait Wait, MouseMove Move, ButtonScroll ButtonScroll, ButtonDown ButtonDown, ButtonUp ButtonUp, ButtonClick ButtonClick, ButtonClickHold ButtonClickHold, ButtonDoubleClick ButtonDoubleClick, IReadOnlyCollection<DragStart> DragStarted, IReadOnlyCollection<DragDrop> DragFinished) {
            this.Wait = Wait;
            this.Move = Move;
            this.ButtonScroll = ButtonScroll;
            this.ButtonDown = ButtonDown;
            this.ButtonUp = ButtonUp;
            this.ButtonClick = ButtonClick;
            this.ButtonClickHold = ButtonClickHold;
            this.ButtonDoubleClick = ButtonDoubleClick;

            this.DragStart = DragStarted;
            this.DragStop = DragFinished;

            {
                var PotentialEvents = new List<IEvent>();
                PotentialEvents.Add(Wait);
                PotentialEvents.Add(Move);
                PotentialEvents.Add(ButtonScroll);
                PotentialEvents.Add(ButtonDown);
                PotentialEvents.Add(ButtonUp);
                PotentialEvents.Add(ButtonClick);
                PotentialEvents.Add(ButtonClickHold);
                PotentialEvents.Add(ButtonDoubleClick);
                if (DragStart is { }) {
                    foreach (var item in DragStart) {
                        PotentialEvents.Add(item);
                    }
                }

                if (DragStop is { }) {
                    foreach (var item in DragStop) {
                        PotentialEvents.Add(item);
                    }
                }
                PotentialEvents = PotentialEvents.Where(x => x is { }).ToList();
                this.Events = PotentialEvents.AsReadOnly();

            }

        }

        public Wait Wait { get; private set; }
        public MouseMove Move { get; private set; }
        public ButtonDown ButtonDown { get; private set; }
        public ButtonUp ButtonUp { get; private set; }
        public ButtonClick ButtonClick { get; private set; }
        public ButtonClickHold ButtonClickHold { get; private set; }
        public ButtonDoubleClick ButtonDoubleClick { get; private set; }
        public ButtonScroll ButtonScroll { get; private set; }

        public IReadOnlyCollection<DragStart> DragStart { get; private set; }
        public IReadOnlyCollection<DragDrop> DragStop { get; private set; }

    }
}