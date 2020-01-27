using System.Runtime.InteropServices;

namespace WindowsInput.Native
{
    /// <summary>
    ///     The <see cref="MouseStruct" /> structure contains information about a mouse input event.
    /// </summary>
    /// <remarks>
    ///     See full documentation at http://globalmousekeyhook.codeplex.com/wikipage?title=MouseStruct
    /// </remarks>
    [StructLayout(LayoutKind.Explicit)]
    public struct MouseStruct
    {
        /// <summary>
        ///     Specifies a Point structure that contains the X- and Y-coordinates of the cursor, in screen coordinates.
        /// </summary>
        [FieldOffset(0x00)] public POINT Point;

        [FieldOffset(0x0A)] public MouseData MouseData;
        [FieldOffset(0x0A)] public short MouseDataValue;

        /// <summary>
        ///     Returns a Timestamp associated with the input, in System Ticks.
        /// </summary>
        [FieldOffset(0x10)] public int Timestamp;
    }

    public enum MouseData : short {
        None = 0,
        XButton1_Click = 1,
        XButton2_Click = 2,
        Scroll_Away = 120,
        Scroll_Toward = -120,
    }

}