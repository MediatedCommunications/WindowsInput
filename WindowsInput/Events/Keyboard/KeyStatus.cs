using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsInput.Events {
    public enum KeyStatus {
        NotPresent,
        Pressed,
        Released
    }

    public static class KeyStatusValue {
        public static KeyStatus Compute(bool isKeyDown, bool isKeyUp) {
            var ret = KeyStatus.NotPresent;
            if (isKeyDown) {
                ret = KeyStatus.Pressed;
            } else if (isKeyUp) {
                ret = KeyStatus.Released;
            }
            return ret;
        }
    }

}
