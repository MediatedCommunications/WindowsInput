using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput.Native;

namespace WindowsInput.Events {

    public class RawInputAggregate : RawInput {
        public IReadOnlyCollection<IEvent> Sources { get; }

        public RawInputAggregate(IList<IEvent> Sources, IList<INPUT> Data) : base(Data) {
            this.Sources = new System.Collections.ObjectModel.ReadOnlyCollection<IEvent>(Sources);
        }

        protected override string GetDebuggerDisplay() {
            var ret = $@"{base.GetDebuggerDisplay()} ({Sources.Count} Children)";

            if (Sources.Count == 1) {
                ret = $@"{Sources.First()}";
            }

            return ret;
        }
    }

    public class RawInput : EventBase {
        public IReadOnlyCollection<INPUT> Data { get; }

        protected override string GetDebuggerDisplay() { 
            var ret = $@"{base.GetDebuggerDisplay()}: See '{nameof(Data)}' Property";

            return ret;
        }

        public RawInput(params INPUT[] Data) {
            this.Data = Data;
        }

        public RawInput(IEnumerable<INPUT> Data) {
            this.Data = Data.ToArray();
        }

        public RawInput(MOUSEINPUT Data) {
            this.Data = new[]{ 
                new INPUT() {
                    Type = INPUTTYPE.Mouse,
                    Data = new INPUTUNION() {
                        Mouse = Data
                    }
                }
            };
        }

        public RawInput(KEYBDINPUT Data) {
            this.Data = new[]{
                new INPUT() {
                    Type = INPUTTYPE.Keyboard,
                    Data = new INPUTUNION() {
                        Keyboard = Data
                    }
                }
            };
        }

        public RawInput(HARDWAREINPUT Data) {
            this.Data = this.Data = new[]{
                new INPUT() {
                    Type = INPUTTYPE.Hardware,
                    Data = new INPUTUNION() {
                        Hardware = Data
                    }
                }
            };
        }

        protected override Task<bool> Invoke(InvokeOptions Options) {
            var ret = true;

            if (!Options.Cancellation.Token.IsCancellationRequested) {

                var AData = Data.ToArray();
                var Results = INPUTDispatcher.SendInput(AData);

                if (Results != AData.Length) {
                    ret = false;

                    if (Options.Failure.Throw) {
                        throw new InvokeDispatcherException();
                    }

                }


            } else {
                if (Options.Cancellation.ThowWhenCanceled) {
                    Options.Cancellation.Token.ThrowIfCancellationRequested();
                }
            }

            return Task.FromResult(ret);
        }
    }



}
