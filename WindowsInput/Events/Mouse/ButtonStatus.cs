namespace WindowsInput.Events {
    public enum ButtonStatus {
        NotPresent,
        Pressed,
        Released,
        Scrolled
    }

    public static class ButtonStatusValue {
        public static ButtonStatus Compute(bool isKeyDown, bool isKeyUp, int delta) {
            var ret = ButtonStatus.NotPresent;
            if (isKeyDown) {
                ret = ButtonStatus.Pressed;
            } else if (isKeyUp) {
                ret = ButtonStatus.Released;
            } else if (delta != 0) {
                ret = ButtonStatus.Scrolled;
            }

            return ret;
        }
    }

}
