using System.Collections.Generic;
using WindowsInput.Native;

namespace WindowsInput.Events {
    public class ButtonUp : ButtonEvent {

        public ButtonUp(ButtonCode Button) : base(Button, CreateChildren(Button)) {

        }

        private static IEnumerable<IEvent> CreateChildren(ButtonCode Button) {
            return new IEvent[] {
                new RawInput(new MOUSEINPUT(){
                    Flags = Button.ToMouseButtonUpFlags(),
                    MouseData = (uint) Button.ToMouseButtonData(),
                })
            };
        }


    }

}
