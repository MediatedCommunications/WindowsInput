using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace WindowsInput.Events {


    public class ButtonClick : ButtonEvent {

        public ButtonClick(ButtonCode Button) : base(Button, CreateChildren(Button)) {
            
        }

        private static IEnumerable<IEvent> CreateChildren(ButtonCode Button) {
            return new IEvent[] {
                new ButtonDown(Button),
                new ButtonUp(Button),
            };
        }

    }

}
