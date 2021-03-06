﻿using System;
using System.Collections.Generic;
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

    public static class MouseCallbackDataExtensions {


        internal static EventSourceEventArgs<MouseInput> ToGlobalMouseEventArgs(this CallbackData data) {
            var wParam = data.WParam;
            var lParam = data.LParam;

            var marshalledMouseStruct = Marshal.PtrToStructure<MouseStruct>(lParam);

            var TM = Marshal.PtrToStructure<MOUSEINPUT>(lParam);

            return ToMouseEventArgs(wParam, marshalledMouseStruct);
        }

        /// <summary>
        ///     Creates <see cref="MouseInput" /> from relevant mouse data.
        /// </summary>
        /// <param name="wParam">First Windows Message parameter.</param>
        /// <param name="mouseInfo">A MouseStruct containing information from which to construct MouseEventExtArgs.</param>
        /// <returns>A new MouseEventExtArgs object.</returns>
        private static EventSourceEventArgs<MouseInput> ToMouseEventArgs(IntPtr wParam, MouseStruct mouseInfo) {
            var button = ButtonCode.None;
            short mouseDelta = 0;

            var isMouseButtonDown = false;
            var isMouseButtonUp = false;

            switch ((WindowMessage)wParam) {
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
                    mouseDelta = mouseInfo.MouseDataValue;
                    break;
                case WindowMessage.WM_XBUTTONDOWN:
                    button = mouseInfo.MouseData == MouseData.XButton1_Click
                        ? ButtonCode.XButton1
                        : ButtonCode.XButton2;
                    isMouseButtonDown = true;
                    break;

                case WindowMessage.WM_XBUTTONUP:
                    button = mouseInfo.MouseData == MouseData.XButton1_Click
                        ? ButtonCode.XButton1
                        : ButtonCode.XButton2;
                    isMouseButtonUp = true;
                    break;

                case WindowMessage.WM_XBUTTONDBLCLK:
                    isMouseButtonDown = true;
                    button = mouseInfo.MouseData == MouseData.XButton1_Click
                        ? ButtonCode.XButton1
                        : ButtonCode.XButton2;
                    break;

                case WindowMessage.WM_MOUSEWHEEL_H:
                    button = ButtonCode.HScroll;
                    mouseDelta = mouseInfo.MouseDataValue;
                    break;
            }

            var Status = ButtonStatusValue.Compute(isMouseButtonDown, isMouseButtonUp, mouseDelta);

            var ret = EventSourceEventArgs.Create(mouseInfo.Timestamp, new MouseInput(button, mouseInfo.Point, mouseDelta, Status));

            return ret;
        }

    }



}