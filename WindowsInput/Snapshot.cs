namespace WindowsInput {
    public static class Snapshot {
        /// <summary>
        /// Get a one-time read on where the mouse position is.
        /// </summary>
        public static WindowsInput.Events.MouseMoveAbsolute? Mouse() {
            var ret = default(WindowsInput.Events.MouseMoveAbsolute?);

            if(WindowsInput.Native.MouseNativeMethods.GetCursorPos(out var Point)){
                ret = new Events.MouseMoveAbsolute(Point.X, Point.Y);
            }

            return ret;
        }
    }

}
