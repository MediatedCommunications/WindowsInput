using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsInput.Events {
    public class DragStart : AggregateEvent {
        public MouseMove PositionDown { get; private set; }
        public ButtonDown ButtonDown { get; private set; }

        protected override string DebuggerDisplay {
            get {
                var ret = base.DebuggerDisplay;

                if(PositionDown is { } && ButtonDown is { }) {
                    ret = $@"{this.GetType().Name}: Drag {ButtonDown.Button} from {PositionDown.Offset}({PositionDown.X},{PositionDown.Y})";
                }


                return ret;
            }
        }

        public DragStart(MouseMove PositionDown, ButtonDown ButtonDown) {
            this.PositionDown = PositionDown;
            this.ButtonDown = ButtonDown;

            Initialize(PositionDown, ButtonDown);
        }
    }
}
