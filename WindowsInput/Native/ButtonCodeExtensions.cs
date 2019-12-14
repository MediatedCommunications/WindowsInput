using System;

using WindowsInput.Events;

namespace WindowsInput.Native {
    public static class ButtonCodeExtensions {
        public static MouseFlag ToMouseWheel(this ButtonCode This) {
            var ret = default(MouseFlag);

            switch (This) {
                case ButtonCode.HScroll:
                    ret = MouseFlag.HorizontalWheel;
                    break;
                case ButtonCode.VScroll:
                    ret = MouseFlag.VerticalWheel;
                    break;

                default:
                    throw new InvalidCastException($@"Unable to convert {This} to a Wheel.  Valid values are {MouseFlag.HorizontalWheel} and {MouseFlag.VerticalWheel}");
            }
            return ret;
        }


        public static MouseFlag ToMouseButtonDownFlags(this ButtonCode This) {
            var ret = default(MouseFlag);
            switch (This) {
                case ButtonCode.Left:
                    ret = MouseFlag.LeftDown;
                    break;
                case ButtonCode.Right:
                    ret = MouseFlag.RightDown;
                    break;
                case ButtonCode.Middle:
                    ret = MouseFlag.MiddleDown;
                    break;
                case ButtonCode.XButton1:
                    ret = MouseFlag.XDown;
                    break;
                case ButtonCode.XButton2:
                    ret = MouseFlag.XDown;
                    break;
                default:
                    throw new InvalidCastException($@"Unable to convert {This} to a valid 'Down' state.  Valid inputs are: {ButtonCode.Left}, {ButtonCode.Right}, {ButtonCode.Middle}, {ButtonCode.XButton1}, and {ButtonCode.XButton2}.");
            }

            return ret;
        }

        public static MouseFlag ToMouseButtonUpFlags(this ButtonCode This) {
            var ret = default(MouseFlag);
            switch (This) {
                case ButtonCode.Left:
                    ret = MouseFlag.LeftUp;
                    break;
                case ButtonCode.Right:
                    ret = MouseFlag.RightUp;
                    break;
                case ButtonCode.Middle:
                    ret = MouseFlag.MiddleUp;
                    break;
                case ButtonCode.XButton1:
                    ret = MouseFlag.XUp;
                    break;
                case ButtonCode.XButton2:
                    ret = MouseFlag.XUp;
                    break;
                default:
                    throw new InvalidCastException($@"Unable to convert {This} to a valid 'Up' state.  Valid inputs are: {ButtonCode.Left}, {ButtonCode.Right}, {ButtonCode.Middle}, {ButtonCode.XButton1}, and {ButtonCode.XButton2}.");
            }

            return ret;
        }


        public static uint ToMouseButtonData(this ButtonCode This) {
            var ret = default(uint);

            if (This == ButtonCode.XButton1) {
                ret = 1;
            } else if (This == ButtonCode.XButton2) {
                ret = 2;
            }

            return ret;
        }

    }

}
