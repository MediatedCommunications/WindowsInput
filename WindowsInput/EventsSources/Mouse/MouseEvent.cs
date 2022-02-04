// This code is distributed under MIT license.
// Copyright (c) 2015 George Mamaladze
// See license.txt or https://mit-license.org/

using System.Collections.Generic;
using System.Linq;
using WindowsInput.Events;

namespace WindowsInput.Events.Sources {

    public class MouseEvent : InputEvent {


        public MouseEvent(
            Wait? Wait, 
            MouseMove? Move, 
            ButtonScroll? ButtonScroll, 
            ButtonDown? ButtonDown, 
            ButtonUp? ButtonUp, 
            ButtonClick? ButtonClick, 
            ButtonClickHold? ButtonClickHold, 
            ButtonDoubleClick? ButtonDoubleClick, 
            IReadOnlyList<DragStart>? DragStart, 
            IReadOnlyList<DragDrop>? DragStop) : base(
                new List<IEvent?>() {
                    Wait,
                    Move,
                    ButtonScroll,
                    ButtonDown,
                    ButtonUp,
                    ButtonClick,
                    ButtonClickHold,
                    ButtonDoubleClick,
                    DragStart,
                    DragStop,
                }) {
            
            this.Wait = Wait;
            this.Move = Move;
            this.ButtonScroll = ButtonScroll;
            this.ButtonDown = ButtonDown;
            this.ButtonUp = ButtonUp;
            this.ButtonClick = ButtonClick;
            this.ButtonClickHold = ButtonClickHold;
            this.ButtonDoubleClick = ButtonDoubleClick;

            this.DragStart = DragStart;
            this.DragStop = DragStop;


        }

        public Wait? Wait { get; }
        public MouseMove? Move { get; }
        public ButtonDown? ButtonDown { get; }
        public ButtonUp? ButtonUp { get; }
        public ButtonClick? ButtonClick { get; }
        public ButtonClickHold? ButtonClickHold { get; }
        public ButtonDoubleClick? ButtonDoubleClick { get; }
        public ButtonScroll? ButtonScroll { get; }

        public IReadOnlyList<DragStart>? DragStart { get; }
        public IReadOnlyList<DragDrop>? DragStop { get; }

    }
}