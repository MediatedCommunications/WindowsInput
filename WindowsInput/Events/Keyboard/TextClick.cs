using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsInput.Events {
    public class TextClick : AggregateEvent {
        public string Text { get; }
        public IReadOnlyCollection<string> NewLines { get; }

        private static readonly string[] DefaultNewLines = new[] {
            "\r\n",
            "\r",
            "\n",
        };

        public TextClick(IEnumerable<char>? Keys) : this(Keys, DefaultNewLines) {

        }

        public TextClick(IEnumerable<char>? Keys, params string[] NewLines) {
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
            this.NewLines = NewLines.ToList().AsReadOnly();
            Initialize(CreateChildren());
        }

        private IEnumerable<IEvent> CreateChildren() {
            var Lines = Text.Split(NewLines.ToArray(), StringSplitOptions.None);
            for (int i = 0; i < Lines.Length; i++) {
                if (i != 0) {
                    yield return new KeyClick(KeyCode.Enter);
                }

                var Line = Lines[i];

                foreach (var item in Line) {
                    yield return new CharacterClick(item);
                }

            }

        }

        protected override string GetDebuggerDisplay() {
            var ret = $@"{this.GetType().Name}: {Text}";

            return ret;
        }

    }

}
