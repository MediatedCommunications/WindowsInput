using System;
using System.Collections.Generic;
using WindowsInput.Native;

namespace WindowsInput.Events {

    public class ButtonScroll : ButtonEvent {
        public int Offset { get; private set; }

        protected override string DebuggerDisplay {
            get {
                var ret = $@"{base.DebuggerDisplay} {Offset}";
                if (Button == ButtonCode.HScroll || Button == ButtonCode.VScroll) {


                    var DirectionString = "";
                    {
                        if (Button == ButtonCode.HScroll) {
                            if(Offset > 0) {
                                DirectionString = nameof(ButtonScrollDirection.Left);
                            } else if(Offset < 0) {
                                DirectionString = nameof(ButtonScrollDirection.Right);
                            }
                        } else if (Button == ButtonCode.VScroll) {
                            if (Offset > 0) {
                                DirectionString = nameof(ButtonScrollDirection.Up);
                            } else if (Offset < 0) {
                                DirectionString = nameof(ButtonScrollDirection.Down);
                            }
                        }
                    }

                    var OffsetString = "";
                    if(!string.IsNullOrWhiteSpace(DirectionString)){
                        var AbsOffset = Math.Abs(Offset);
                        if (AbsOffset != DefaultOffset && AbsOffset != 0) {
                            OffsetString = $@" {AbsOffset}";
                        }
                    }


                    if (!string.IsNullOrWhiteSpace(DirectionString)) {
                        ret = $@"{this.GetType().Name}: {DirectionString}{OffsetString}";
                    }
                    
                }

                return ret;
            }
        }

        public static int DefaultOffset { get; } = 120;


        public ButtonScroll(ButtonCode Button, ButtonScrollDirection Direction) : this(Button, Direction, DefaultOffset) {

        }

        public ButtonScroll(ButtonCode Button, ButtonScrollDirection Direction, int Offset) : this(Button, (int)Direction * Offset) {

        }

        public ButtonScroll(ButtonCode Button, int Offset) : base(Button, CreateChildren(Button, Offset)) {
            this.Offset = Offset;
        }

        private static IEnumerable<IEvent> CreateChildren(ButtonCode Button, int Offset) {
            var Flags = Button.ToMouseWheel();

            yield return new RawInput(new MOUSEINPUT() {
                Flags = Flags,
                MouseData = (uint)Offset

            });

        }

    }

}
