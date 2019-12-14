using System;
using System.Collections.Generic;
using WindowsInput.Native;

namespace WindowsInput.Events {
    public class ButtonDown : ButtonEvent {

        public ButtonDown(ButtonCode Button) : base(Button, CreateChildren(Button)) { 
            
        }

        private static IEnumerable<IEvent> CreateChildren(ButtonCode Button) {
            yield return new RawInput(new MOUSEINPUT() {
                Flags = Button.ToMouseButtonDownFlags(),
                MouseData = (uint)Button.ToMouseButtonData(),
            });
        }

    }

}
