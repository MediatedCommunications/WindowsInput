using System;

namespace WindowsInput.Native {
    public static class BootTime {
        public static DateTimeOffset Value { get; private set; }

        static BootTime() {
            Value = DateTimeOffset.Now.AddMilliseconds(-System.Environment.TickCount);
        }

    }

}
