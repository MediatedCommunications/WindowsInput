using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WindowsInput.Native {
    public class MouseNativeMethods {

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out POINT lpPoint);

    }
}
