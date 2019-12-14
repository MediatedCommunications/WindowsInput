using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsInput.Native {
    public enum GlobalKeyboardMessage {
        None = 0,
        KeyDown = 0x100,
        KeyUp = 0x101,
        SysKeyDown = 0x104,
        SysKeyUp = 0x105,
    }

}
