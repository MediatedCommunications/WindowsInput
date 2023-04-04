using System.Runtime.InteropServices;

namespace WindowsInput.Native {
    internal static class CallbackDataExtensions {
        public static GlobalKeyboardEventSourceCallbackData ToGlobalKeyboardEventSourceCallbackData(this CallbackData data) {
            var Message = (GlobalKeyboardMessage)data.WParam;
            var keyboardHookStruct = Marshal.PtrToStructure<KeyboardHookStruct>(data.LParam);

            var ret = new GlobalKeyboardEventSourceCallbackData() {
                Message = Message,
                Data = keyboardHookStruct,
            };

            return ret;
        }

        internal static GlobalMouseEventSourceCallbackData ToGlobalMouseEventSourceCallbackData(this CallbackData data) {
            var wParam = data.WParam;
            var lParam = data.LParam;

            var marshalledMouseStruct = Marshal.PtrToStructure<MouseStruct>(lParam);

            var ret = new GlobalMouseEventSourceCallbackData() {
                Message = (WindowMessage)wParam,
                Data = marshalledMouseStruct,
            };

            return ret;
        }

        internal static CurrentThreadMouseEventSourceCallbackData ToCurrentThreadMouseEventSourceCallbackData(this CallbackData data) {
            var wParam = data.WParam;
            var lParam = data.LParam;

            var MouseData1 = Marshal.PtrToStructure<MOUSEHOOKSTRUCTEX>(lParam);

            var Message = (WindowMessage)wParam;

            var ret = new CurrentThreadMouseEventSourceCallbackData() {
                Message = Message,
                Data = MouseData1,
            };

            return ret;

        }

    }



}