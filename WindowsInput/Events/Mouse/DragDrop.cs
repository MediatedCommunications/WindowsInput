using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsInput.Events {
    public class DragDrop : AggregateEvent {
        public DragStart Start { get; }
        public DragStop Stop { get; }

        protected override string GetDebuggerDisplay() {

            var ret = base.GetDebuggerDisplay();

            if (Start.PositionDown is { } && Stop.PositionUp is { } && Start.ButtonDown?.Button == Stop.ButtonUp?.Button && Start.ButtonDown is { }) {
                ret = $@"{this.GetType().Name}: Drag {Start.ButtonDown.Button} from {Start.PositionDown.Offset}({Start.PositionDown.X},{Start.PositionDown.Y}) to {Stop.PositionUp.Offset}({Stop.PositionUp.X},{Stop.PositionUp.Y})";
            }

            return ret;

        }


        public DragDrop(MouseMove Start, MouseMove Stop, ButtonCode Button) {
            this.Start = new DragStart(Start, new ButtonDown(Button));
            this.Stop = new DragStop(Stop, new ButtonUp(Button));

            Initialize(this.Start, this.Stop);
        }

        public DragDrop(DragStart Start, DragStop Stop) {
            this.Start = Start;
            this.Stop = Stop;

            Initialize(this.Start, this.Stop);
        }

    }
}
