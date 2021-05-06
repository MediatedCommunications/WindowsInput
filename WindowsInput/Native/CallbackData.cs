using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WindowsInput.Events;
using WindowsInput.Events.Sources;

namespace WindowsInput.Native {
    public struct CallbackData {
        public CallbackData(IntPtr wParam, IntPtr lParam) {
            WParam = wParam;
            LParam = lParam;
        }

        public IntPtr WParam { get; }

        public IntPtr LParam { get; }
    }

    public struct GlobalKeyboardEventSourceCallbackData {
        public GlobalKeyboardMessage Message { get; set; }
        public KeyboardHookStruct Data { get; set; }
    }

    public struct CurrentThreadMouseEventSourceCallbackData {
        public WindowMessage Message { get; set; }
        public MOUSEHOOKSTRUCTEX Data { get; set; }
    }

    public struct GlobalMouseEventSourceCallbackData {
        public WindowMessage Message { get; set; }
        public MouseStruct Data { get; set; }
    }

    public static class MouseCallbackDataExtensions {




        internal static EventSourceEventArgs<MouseInput> ToGlobalMouseEventArgs(this CallbackData data) {

            var Data = data.ToGlobalMouseEventSourceCallbackData();

            return ToMouseEventArgs(Data);
        }

        /// <summary>
        ///     Creates <see cref="MouseInput" /> from relevant mouse data.
        /// </summary>
        /// <param name="wParam">First Windows Message parameter.</param>
        /// <param name="mouseInfo">A MouseStruct containing information from which to construct MouseEventExtArgs.</param>
        /// <returns>A new MouseEventExtArgs object.</returns>
        private static EventSourceEventArgs<MouseInput> ToMouseEventArgs(GlobalMouseEventSourceCallbackData data) {
            var button = ButtonCode.None;
            short mouseDelta = 0;

            var isMouseButtonDown = false;
            var isMouseButtonUp = false;

            switch (data.Message) {
                case WindowMessage.WM_LBUTTONDOWN:
                    isMouseButtonDown = true;
                    button = ButtonCode.Left;
                    break;
                case WindowMessage.WM_LBUTTONUP:
                    isMouseButtonUp = true;
                    button = ButtonCode.Left;
                    break;
                case WindowMessage.WM_LBUTTONDBLCLK:
                    isMouseButtonDown = true;
                    button = ButtonCode.Left;
                    break;
                case WindowMessage.WM_RBUTTONDOWN:
                    isMouseButtonDown = true;
                    button = ButtonCode.Right;
                    break;
                case WindowMessage.WM_RBUTTONUP:
                    isMouseButtonUp = true;
                    button = ButtonCode.Right;
                    break;
                case WindowMessage.WM_RBUTTONDBLCLK:
                    isMouseButtonDown = true;
                    button = ButtonCode.Right;
                    break;
                case WindowMessage.WM_MBUTTONDOWN:
                    isMouseButtonDown = true;
                    button = ButtonCode.Middle;
                    break;
                case WindowMessage.WM_MBUTTONUP:
                    isMouseButtonUp = true;
                    button = ButtonCode.Middle;
                    break;
                case WindowMessage.WM_MBUTTONDBLCLK:
                    isMouseButtonDown = true;
                    button = ButtonCode.Middle;
                    break;
                case WindowMessage.WM_MOUSEWHEEL_V:
                    button = ButtonCode.VScroll;
                    mouseDelta = data.Data.MouseDataValue;
                    break;
                case WindowMessage.WM_XBUTTONDOWN:
                    button = data.Data.MouseData == MouseData.XButton1_Click
                        ? ButtonCode.XButton1
                        : ButtonCode.XButton2;
                    isMouseButtonDown = true;
                    break;

                case WindowMessage.WM_XBUTTONUP:
                    button = data.Data.MouseData == MouseData.XButton1_Click
                        ? ButtonCode.XButton1
                        : ButtonCode.XButton2;
                    isMouseButtonUp = true;
                    break;

                case WindowMessage.WM_XBUTTONDBLCLK:
                    isMouseButtonDown = true;
                    button = data.Data.MouseData == MouseData.XButton1_Click
                        ? ButtonCode.XButton1
                        : ButtonCode.XButton2;
                    break;

                case WindowMessage.WM_MOUSEWHEEL_H:
                    button = ButtonCode.HScroll;
                    mouseDelta = data.Data.MouseDataValue;
                    break;
            }

            var Status = ButtonStatusValue.Compute(isMouseButtonDown, isMouseButtonUp, mouseDelta);

            var ret = EventSourceEventArgs.Create(data.Data.Timestamp, new MouseInput(button, data.Data.Point, mouseDelta, Status), data);

            return ret;
        }

    }



}