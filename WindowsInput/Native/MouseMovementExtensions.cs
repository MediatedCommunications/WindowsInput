using System;
using WindowsInput.Events;

namespace WindowsInput.Native {
    public static class MouseMovementExtensions {
        public static MouseFlag ToMouseFlags(this MouseOffset This) {
            var ret = default(MouseFlag);
            switch (This) {
                case MouseOffset.Relative:
                    ret = MouseFlag.Move | MouseFlag.Relative;
                    break;
                case MouseOffset.Absolute:
                    ret = MouseFlag.Move | MouseFlag.Absolute;
                    break;
                case MouseOffset.AbsoluteVirtual:
                    ret = MouseFlag.Move | MouseFlag.Absolute | MouseFlag.VirtualDesk;
                    break;
                default:
                    throw new InvalidCastException($@"Unable to convert {This} to a valid {nameof(MouseFlag)}.  Accepted values are {MouseOffset.Relative}, {MouseOffset.Absolute}, and {MouseOffset.AbsoluteVirtual}.");
            }

            return ret;
        }
    }

}
