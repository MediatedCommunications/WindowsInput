using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsInput.Events {
    public class KeyPressData {

        public char KeyChar { get; }

        public KeyPressData(char Key) {
            this.KeyChar = Key;
        }

    }
}
