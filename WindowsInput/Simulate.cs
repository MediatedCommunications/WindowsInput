using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput.Events;

namespace WindowsInput {
    public static class Simulate {

        public static EventBuilder Events() {
            return EventBuilder.Create();
        }


        public static Task<bool> Events(params IEvent[] IEvents) {
            return Events((IEnumerable<IEvent>)IEvents);
        }

        public static Task<bool> Events(IEnumerable<IEvent> IEvents) {
            return Events(null, IEvents);
        }

        public static Task<bool> Events(InvokeOptions Options, params IEvent[] IEvents ) {
            return Events(Options, (IEnumerable<IEvent>)IEvents);
        }

        public static async Task<bool> Events(InvokeOptions Options, IEnumerable<IEvent> IEvents) {
            Options = Options ?? new InvokeOptions();

            var ret = true;

            if (IEvents != null) {
                foreach (var item in IEvents) {
                    if (Options.Cancellation.Token.IsCancellationRequested) {
                        if (Options.Cancellation.ThowWhenCanceled) {
                            Options.Cancellation.Token.ThrowIfCancellationRequested();
                        } else {
                            break;
                        }
                    }

                    if (item != default) {

                        try {
                            var tret = await item.Invoke(Options)
                                .DefaultAwait()
                                ;

                            if (tret == false) {
                                throw new InvokeFailedException();
                            }

                        } catch {
                            ret = false;

                            if (Options.Failure.Throw) {
                                throw;
                            } else if (!Options.Failure.Continue) {
                                break;
                            }

                        }

                    }

                }
            }

            return ret;

        }
    }
}
