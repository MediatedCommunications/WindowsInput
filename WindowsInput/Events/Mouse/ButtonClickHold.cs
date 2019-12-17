using System.Collections.Generic;
using System.Diagnostics;

namespace WindowsInput.Events {
    public class ButtonClickHold : ButtonEvent {

        public ButtonClickHold(ButtonCode Button) : base(Button, CreateChildren(Button)) {
            
        }

        private static IEnumerable<IEvent> CreateChildren(ButtonCode Button) {
            return new IEvent[] {
                new ButtonClick(Button),
                new ButtonDown(Button),
            };
        }

    }

}
