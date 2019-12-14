using System.Collections.Generic;
using System.Text;

namespace WindowsInput.Events {
    public class TextClick : AggregateEvent {
        public string Text { get; private set; }
        public IEvent BetweenCharacters { get; private set; }

        public TextClick(IEnumerable<char> Keys, IEvent BetweenCharacters = default) {
            var Value = "";
            {
                
                if (Keys is string V2) {
                    Value = V2;
                } else if (Keys != null) {
                    var SB = new StringBuilder();
                    if (Keys != null) {
                        foreach (var item in Keys) {
                            SB.Append(item);
                        }
                    }
                    Value = SB.ToString();
                }

            }

            this.Text = Value;
            this.BetweenCharacters = BetweenCharacters;

            Initialize(CreateChildren());
        }

        private IEnumerable<IEvent> CreateChildren() {
            if(Text?.Length > 0) {
                var First = true;
                foreach (var item in Text) {

                    if (!First && BetweenCharacters != null) {
                        yield return BetweenCharacters;
                    }

                    yield return new CharacterClick(item);

                    First = false;
                }
            }

        }

        protected override string DebuggerDisplay => $@"{this.GetType().Name}: {Text}";

    }

}
