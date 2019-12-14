using System.Collections.Generic;
using System.Diagnostics;

namespace WindowsInput.Events {
    public class ButtonDoubleClick : ButtonEvent {

        public ButtonDoubleClick(ButtonCode Button) : base(Button, CreateChildren(Button)) {
            
        }

        private static IEnumerable<IEvent> CreateChildren(ButtonCode Button) {
            return new IEvent[] {
                new ButtonClick(Button),
                new ButtonClick(Button),
            };
        }


    }

}
