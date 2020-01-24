using System;

namespace WindowsInput.Events.Sources {
    [Flags]
    public enum ToUnicodeExFlags : uint {
        None                     = 0,
        Menu                     = 0x0001,
        Unknown                  = 0x0002,
        DoNotChangeKeyboardState = 0x0004 //This flag is only present on Windows10 v1607 and above
    }
}