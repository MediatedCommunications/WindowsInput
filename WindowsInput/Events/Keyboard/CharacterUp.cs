using System;
using System.Collections.Generic;
using WindowsInput.Native;

namespace WindowsInput.Events {
    public class CharacterUp : CharacterEvent {

        public CharacterUp(char Text) : base(Text, CreateChildren(Text)) {
            
        }

        private static IEnumerable<IEvent> CreateChildren(char Text) {
            yield return new RawInput(new KEYBDINPUT() {
                ScanCode = Text,
                Flags = (KeyboardFlag.Unicode | KeyboardFlag.KeyUp),
            });
        }

    }

}
