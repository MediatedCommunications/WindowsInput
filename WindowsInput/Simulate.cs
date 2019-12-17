using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput.Events;

namespace WindowsInput {
    public static class Simulate {

        /// <summary>
        /// Create a new <see cref="EventBuilder"/> to construct a simulation chain.
        /// </summary>
        /// <returns></returns>
        public static EventBuilder Events() {
            return EventBuilder.Create();
        }

        /// <inheritdoc cref="Events(InvokeOptions, IEnumerable{IEvent})"/>
        public static Task<bool> Events(params IEvent[] IEvents) {
            return Events((IEnumerable<IEvent>)IEvents);
        }

        /// <inheritdoc cref="Events(InvokeOptions, IEnumerable{IEvent})"/>
        public static Task<bool> Events(IEnumerable<IEvent> IEvents) {
            return Events(null, IEvents);
        }

        /// <inheritdoc cref="Events(InvokeOptions, IEnumerable{IEvent})"/>
        public static Task<bool> Events(InvokeOptions Options, params IEvent[] IEvents ) {
            return Events(Options, (IEnumerable<IEvent>)IEvents);
        }

        /// <summary>
        /// Immediately simulate a series of input events.
        /// </summary>
        /// <param name="Options">Options that control the execution of the simulation</param>
        /// <param name="IEvents">The list of input events to simulate.</param>
        /// <returns></returns>
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
