using System;
using System.Collections.Generic;
using WindowsInput.Native;


namespace WindowsInput.Events {
    public class KeyDown : KeyEvent {

        public KeyDown(KeyCode Key, bool? Extended = default) : base(Key, Extended) {
            Initialize(CreateChildren());
        }

        private IEnumerable<IEvent> CreateChildren() {
            yield return new RawInput(new KEYBDINPUT() {
                KeyCode = Key,
                ScanCode = (ushort)(NativeMethods.MapVirtualKey((ushort)Key, 0) & 0xFFU),
                Flags = (this.Extended
                    ? KeyboardFlag.ExtendedKey
                    : KeyboardFlag.None
                    ),
            });
        }

    }

}
