using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsInput.Events {

    //
    // Summary:
    //     Specifies constants that define which mouse button was pressed.
    public enum ButtonCode {
        None,
        Left,
        Right,
        Middle,
        XButton1,
        XButton2,
        HScroll,
        VScroll
    }



    /*
    //
    // Summary:
    //     Specifies constants that define which mouse button was pressed.
    [Flags]
    public enum MouseButtons {
        //
        // Summary:
        //     The left mouse button was pressed.
        Left = 0x100000,
        //
        // Summary:
        //     No mouse button was pressed.
        None = 0x0,
        //
        // Summary:
        //     The right mouse button was pressed.
        Right = 0x200000,
        //
        // Summary:
        //     The middle mouse button was pressed.
        Middle = 0x400000,
        //
        // Summary:
        //     The first XButton was pressed.
        XButton1 = 0x800000,
        //
        // Summary:
        //     The second XButton was pressed.
        XButton2 = 0x1000000
    }
    */
}
