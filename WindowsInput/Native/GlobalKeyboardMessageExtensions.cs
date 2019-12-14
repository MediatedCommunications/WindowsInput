namespace WindowsInput.Native {
    public static class GlobalKeyboardMessageExtensions {
        public static bool IsKeyDown(this GlobalKeyboardMessage This) {
            return This == GlobalKeyboardMessage.KeyDown || This == GlobalKeyboardMessage.SysKeyDown;
        }

        public static bool IsKeyUp(this GlobalKeyboardMessage This) {
            return This == GlobalKeyboardMessage.KeyUp || This == GlobalKeyboardMessage.SysKeyUp;
        }
    }

}
