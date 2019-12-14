using System;

namespace WindowsInput.Native {
    [Flags]
    public enum KeyboardHookStructFlags : int {
        None = 0b_0000_000,
        Extended = 0b_0000_0001,
        Injected_FromLower = 0b_0000_0010,
        Reserved2 = 0b_0000_0100,
        Reserved3 = 0b_0000_1000,
        Injected = 0b_0001_0000,
        Alt = 0b_0010_0000,
        Reserved6 = 0b_0100_0000,
        Pressed = 0b_1000_0000,
    }

}
