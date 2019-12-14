using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using WindowsInput.Events;

namespace WindowsInput.Events {
    
    public partial class EventBuilder {
        public EventBuilder Add(IEvent Value) {
            if (Value != null) {
                this.Events.Add(Value);
            }

            return this;
        }


        public EventBuilder Hold(KeyCode Key, bool? Extended = default) {
            return Add(new KeyDown(Key, Extended));
        }

        public EventBuilder Release(KeyCode Key, bool? Extended = default) {
            return Add(new KeyUp(Key, Extended));
        }

        public EventBuilder Click(KeyCode Key, bool? Extended = default) {
            return Add(new KeyClick(Key, Extended));
        }

        public EventBuilder Click(char Key) {
            return Add(new CharacterClick(Key));
        }

        public EventBuilder Click(params KeyCode[] Keys) {
            return Add(new CombinationClick(Keys));
        }

        public EventBuilder Click(IEnumerable<char> Keys, IEvent BetweenCharacters = default) {
            return Add(new TextClick(Keys, BetweenCharacters));
        }

        public EventBuilder Hold(ButtonCode Button) {
            return Add(new ButtonDown(Button));
        }

        public EventBuilder Release(ButtonCode Button) {
            return Add(new ButtonUp(Button));
        }

        public EventBuilder Click(ButtonCode Button) {
            return Add(new ButtonClick(Button));
        }

        public EventBuilder DoubleClick(ButtonCode Button) {
            return Add(new ButtonDoubleClick(Button));
        }

        public EventBuilder Scroll(ButtonCode Button, int Offset) {
            return Add(new ButtonScroll(Button, Offset));
        }

        public EventBuilder Scroll(ButtonCode Button, ButtonScrollDirection Direction) {
            return Add(new ButtonScroll(Button, Direction));
        }

        public EventBuilder Scroll(ButtonCode Button, ButtonScrollDirection Direction, int Offset) {
            return Add(new ButtonScroll(Button, Direction, Offset));
        }

        public EventBuilder MoveTo(int x, int y) {
            return Add(new MouseMoveAbsolute(x, y));
        }

        public EventBuilder MoveBy(int x, int y) {
            return Add(new MouseMoveRelative(x, y));
        }

        public EventBuilder MoveToVirtual(int x, int y) {
            return Add(new MouseMoveVirtual(x, y));
        }

        public EventBuilder Move(int x, int y, MouseOffset offset) {
            return Add(MouseMove.Create(x, y, offset));
        }

        public EventBuilder Wait(TimeSpan Duration) {
            return Add(new Wait(Duration));
        }

        public EventBuilder Wait(double DurationInMs) {
            return Add(new Wait(DurationInMs));
        }

        public EventBuilder WaitUntilResponsive(IntPtr hWnd, TimeSpan Timeout) {
            return Add(new WaitUntilResponsive(hWnd, Timeout));
        }

        public EventBuilder WaitToPreventDoubleClicking() {
            return Add(new WaitToPreventDoubleClicking());
        }

        public EventBuilder DragDrop(MouseMove Start, ButtonCode Button, MouseMove Stop) {
            return Add(new DragDrop(Start, Button, Stop));
        }


    }

}
