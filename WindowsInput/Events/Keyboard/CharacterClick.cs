using System.Collections.Generic;
using System.Linq;

namespace WindowsInput.Events {

    public class CharacterClick : CharacterEvent {

        public CharacterClick(char Text) : base(Text, CreateChildren(Text)) {

        }

        private static IEnumerable<IEvent> CreateChildren(char Text) {
            return new IEvent[] {
                new CharacterDown(Text),
                new CharacterUp(Text),
            };
        }


    }

}
