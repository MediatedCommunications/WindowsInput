using System;
using System.Collections.Generic;
using WindowsInput.Native;

namespace WindowsInput.Events {

    public abstract class MouseMove : AggregateEvent {
        public int X { get; private set; }
        public int Y { get; private set; }
        public MouseOffset Offset { get; private set; }

        public static MouseMove Create(int X, int Y, MouseOffset Offset) {
            MouseMove ret = Offset switch {
                MouseOffset.None => null,
                MouseOffset.Absolute => new MouseMoveAbsolute(X, Y),
                MouseOffset.Relative => new MouseMoveRelative(X, Y),
                MouseOffset.AbsoluteVirtual => new MouseMoveVirtual(X, Y),
                _ => throw new NotImplementedException()
            };

            return ret;
        }

        protected MouseMove(int X, int Y, MouseOffset Offset) {
            this.X = X;
            this.Y = Y;
            this.Offset = Offset;

            Initialize(CreateChildren());
        }

        private IEnumerable<IEvent> CreateChildren() {
            var NewX = X;
            var NewY = Y;

            if(Offset == MouseOffset.Absolute || Offset == MouseOffset.AbsoluteVirtual) {
                NewX = X * SystemMetrics.Screen.ScaleFactor.Value / SystemMetrics.Screen.Primary.Width.Value;
                NewY = Y * SystemMetrics.Screen.ScaleFactor.Value / SystemMetrics.Screen.Primary.Height.Value;
            }

            yield return new RawInput(new MOUSEINPUT() {
                Point = new POINT() {
                    X = NewX,
                    Y = NewY,
                },
                Flags = Offset.ToMouseFlags(),
            });
        }

        protected override string DebuggerDisplay => $@"{this.GetType().Name}: {Offset}({X},{Y})";

    }

    public class MouseMoveAbsolute : MouseMove {
        public MouseMoveAbsolute(int X, int Y) : base(X, Y, MouseOffset.Absolute) {

        }

    }

    public class MouseMoveRelative : MouseMove {
        public MouseMoveRelative(int X, int Y) : base(X, Y, MouseOffset.Relative) {

        }
    }

    public class MouseMoveVirtual : MouseMove {
        public MouseMoveVirtual(int X, int Y) : base(X, Y, MouseOffset.AbsoluteVirtual) {

        }
    }

}
