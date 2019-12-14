using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput.Native;

namespace WindowsInput.Events {

    public class RawInput : EventBase {
        public INPUT Data { get; private set; }

        protected override string DebuggerDisplay => $@"{base.DebuggerDisplay}: See '{nameof(Data)}' Property";

        public RawInput(INPUT Data) {
            this.Data = Data;
        }

        public RawInput(MOUSEINPUT Data) {
            this.Data = new INPUT() {
                Type = INPUTTYPE.Mouse,
                Data = new INPUTUNION() {
                    Mouse = Data
                },
            };
        }

        public RawInput(KEYBDINPUT Data) {
            this.Data = new INPUT() {
                Type = INPUTTYPE.Keyboard,
                Data = new INPUTUNION() {
                    Keyboard = Data
                },
            };
        }

        public RawInput(HARDWAREINPUT Data) {
            this.Data = new INPUT() {
                Type = INPUTTYPE.Hardware,
                Data = new INPUTUNION() {
                    Hardware = Data
                },
            };
        }

        protected override Task<bool> Invoke(InvokeOptions Options) {
            var ret = true;

            if (!Options.Cancellation.Token.IsCancellationRequested) {

                var Results = INPUTDispatcher.SendInput(Data);

                if (Results != 1) {
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
