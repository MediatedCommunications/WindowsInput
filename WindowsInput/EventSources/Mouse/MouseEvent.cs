// This code is distributed under MIT license.
// Copyright (c) 2015 George Mamaladze
// See license.txt or https://mit-license.org/

using System.Collections.Generic;

using WindowsInput.Events;

namespace WindowsInput.EventSources {
    public class MouseEvent {
        public MouseEvent(Wait Wait, MouseMove Move, ButtonScroll ButtonScroll, ButtonDown ButtonDown, ButtonUp ButtonUp, ButtonClick ButtonClick, ButtonDoubleClick ButtonDoubleClick, IReadOnlyCollection<DragStart> DragStarted, IReadOnlyCollection<DragDrop> DragFinished) {
            this.Wait = Wait;
            this.Move = Move;
            this.ButtonScroll = ButtonScroll;
            this.ButtonDown = ButtonDown;
            this.ButtonUp = ButtonUp;
            this.ButtonClick = ButtonClick;
            this.ButtonDoubleClick = ButtonDoubleClick;

            this.DragStart = DragStarted;
            this.DragStop = DragFinished;

        }

        public Wait Wait { get; private set; }
        public MouseMove Move { get; private set; }
        public ButtonDown ButtonDown { get; private set; }
        public ButtonUp ButtonUp { get; private set; }
        public ButtonClick ButtonClick { get; private set; }
        public ButtonDoubleClick ButtonDoubleClick { get; private set; }
        public ButtonScroll ButtonScroll { get; private set; }

        public IReadOnlyCollection<DragStart> DragStart { get; private set; }
        public IReadOnlyCollection<DragDrop> DragStop { get; private set; }

    }
}