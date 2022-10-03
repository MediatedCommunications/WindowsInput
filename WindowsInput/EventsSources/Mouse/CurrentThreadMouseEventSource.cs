// This code is distributed under MIT license. 
// Copyright (c) 2015 George Mamaladze
// See license.txt or https://mit-license.org/

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using WindowsInput.Events;
using WindowsInput.Native;

namespace WindowsInput.Events.Sources {
    public class CurrentThreadMouseEventSource : MouseEventSource {

        protected override HookHandle? Subscribe() {

            return HookHandle.Create(
                HookType.AppMouse,
                HookProcedure,
                IntPtr.Zero,
                ThreadNativeMethods.GetCurrentThreadId());

        }

        protected override bool Callback(CallbackData data) {
            var Timestamp = DateTimeOffset.UtcNow;

            var NewData = data.ToCurrentThreadMouseEventSourceCallbackData();

            var Button = default(ButtonCode);
            var Location = default(POINT);
            var Scroll = default(int);
            var Status = default(ButtonStatus);

            Location.X = NewData.Data.Point.X;
            Location.Y = NewData.Data.Point.Y;

            switch (NewData.Message) {
                case WindowMessage.WM_MOUSEMOVE:
                case WindowMessage.WM_MOUSEMOVE_NC:
                    break;

                case WindowMessage.WM_LBUTTONDOWN:
                case WindowMessage.WM_LBUTTONDOWN_NC:
                    Button = ButtonCode.Left;
                    Status = ButtonStatus.Pressed;
                    break;

                case WindowMessage.WM_RBUTTONDOWN:
                case WindowMessage.WM_RBUTTONDOWN_NC:
                    Button = ButtonCode.Right;
                    Status = ButtonStatus.Pressed;
                    break;

                case WindowMessage.WM_MBUTTONDOWN:
                case WindowMessage.WM_MBUTTONDOWN_NC:
                    Button = ButtonCode.Middle;
                    Status = ButtonStatus.Pressed;
                    break;

                case WindowMessage.WM_LBUTTONUP:
                case WindowMessage.WM_LBUTTONUP_NC:
                    Button = ButtonCode.Left;
                    Status = ButtonStatus.Released;
                    break;

                case WindowMessage.WM_RBUTTONUP:
                case WindowMessage.WM_RBUTTONUP_NC:
                    Button = ButtonCode.Right;
                    Status = ButtonStatus.Released;
                    break;

                case WindowMessage.WM_MBUTTONUP:
                case WindowMessage.WM_MBUTTONUP_NC:
                    Button = ButtonCode.Middle;
                    Status = ButtonStatus.Released;
                    break;

                case WindowMessage.WM_LBUTTONDBLCLK:
                case WindowMessage.WM_LBUTTONDBLCLK_NC:
                    Button = ButtonCode.Left;
                    Status = ButtonStatus.Pressed;
                    break;

                case WindowMessage.WM_RBUTTONDBLCLK:
                case WindowMessage.WM_RBUTTONDBLCLK_NC:
                    Button = ButtonCode.Right;
                    Status = ButtonStatus.Pressed;
                    break;

                case WindowMessage.WM_MBUTTONDBLCLK:
                case WindowMessage.WM_MBUTTONDBLCLK_NC:
                    Button = ButtonCode.Middle;
                    Status = ButtonStatus.Pressed;
                    break;

                case WindowMessage.WM_MOUSEWHEEL_H:
                    Button = ButtonCode.HScroll;
                    Status = ButtonStatus.Scrolled;
                    Scroll = NewData.Data.MouseData.HiWord;
                    break;
                case WindowMessage.WM_MOUSEWHEEL_V:
                    Button = ButtonCode.VScroll;
                    Status = ButtonStatus.Scrolled;
                    Scroll = NewData.Data.MouseData.HiWord;
                    break;

                case WindowMessage.WM_XBUTTONDOWN:
                case WindowMessage.WM_XBUTTONDOWN_NC:
                    Button = XButton(NewData.Data);
                    Status = ButtonStatus.Pressed;
                    break;

                case WindowMessage.WM_XBUTTONUP:
                case WindowMessage.WM_XBUTTONUP_NC:
                    Button = XButton(NewData.Data);
                    Status = ButtonStatus.Released;
                    break;

                case WindowMessage.WM_XBUTTONDBLCLK:
                case WindowMessage.WM_XBUTTONDBLCLK_NC:
                    Button = XButton(NewData.Data);
                    Status = ButtonStatus.Pressed;
                    break;

                default:
                    break;
            }

            var e = new EventSourceEventArgs<MouseInput>(Timestamp, new MouseInput(Button, Location, Scroll, Status), NewData);
            var Events = State.GetEventArgs(e);
            var ret = InvokeMany(Events.Data, NewData, Events.Timestamp);

            return ret.Next_Hook_Enabled;
        }

        private ButtonCode XButton(MOUSEHOOKSTRUCTEX Value) {
            var ret = ButtonCode.None;
            if (Value.MouseData.HiWord == 1) {
                ret = ButtonCode.XButton1;
            } else if (Value.MouseData.HiWord == 2) {
                ret = ButtonCode.XButton2;
            }

            return ret;
        }

        
    }
}