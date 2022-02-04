using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace WindowsInput.Events {

    public class Wait : EventBase {
        public TimeSpan Duration { get; }

        public Wait(TimeSpan Duration) {
            this.Duration = Duration;
        }

        public Wait(double DurationInMs) {
            this.Duration = TimeSpan.FromMilliseconds(DurationInMs);
        }

        protected override string GetDebuggerDisplay() { 
            var ret = $@"{base.GetDebuggerDisplay()}: {Duration}";

            return ret;
        }

        protected override async Task<bool> Invoke(InvokeOptions Options) {
            var ret = true;
            try {
                await Task.Delay(Duration, Options.Cancellation.Token)
                    .DefaultAwait()
                    ;
            } catch(OperationCanceledException) {
                ret = false;

                if (Options.Cancellation.ThowWhenCanceled) {
                    Options.Cancellation.Token.ThrowIfCancellationRequested();
                }

            } catch {
                ret = false;

                if (Options.Failure.Throw) {
                    throw;
                }

            }

            return ret;
        }
    }

}
