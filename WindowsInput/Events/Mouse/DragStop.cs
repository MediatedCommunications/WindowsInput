using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsInput.Events {
    public class DragStop : AggregateEvent {
        public MouseMove PositionUp { get; private set; }
        public ButtonUp ButtonUp { get; private set; }

        protected override string DebuggerDisplay {
            get {
                var ret = base.DebuggerDisplay;

                if (PositionUp is { } && ButtonUp is { }) {
                    ret = $@"{this.GetType().Name}: Drag {ButtonUp.Button} to {PositionUp.Offset}({PositionUp.X},{PositionUp.Y})";
                }

                return ret;
            }
        }

        public DragStop(MouseMove PositionUp, ButtonUp ButtonUp) {
            this.PositionUp = PositionUp;
            this.ButtonUp = ButtonUp;

            Initialize(PositionUp, ButtonUp);
        }
    }
}
