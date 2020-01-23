using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput.Events;
using WindowsInput.Native;

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
        public static Task<bool> Events(InvokeOptions Options, params IEvent[] IEvents) {
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

            var EventList = new List<IEvent>();

            if(IEvents != default) {
                EventList.AddRange(IEvents);
            }

            if (Options.SendInput.Aggregate) {
                EventList = EventList.SendInput_Flatten();
            }


            foreach (var item in EventList) {
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

            return ret;

        }



        private static List<IEvent> SendInput_Flatten(this IEnumerable<IEvent> This) {
            var Linear = new List<IEvent>();

            var CurrentEvents = new List<IEvent>();
            var CurrentInputs = new List<IReadOnlyCollection<INPUT>>();

            void WrapUp() {
                if(CurrentEvents.Count > 0) {
                    var InputList = CurrentInputs.SelectMany(x => x).ToArray();

                    Linear.Add(new RawInputAggregate(CurrentEvents, InputList));
                    CurrentEvents = new List<IEvent>();
                    CurrentInputs = new List<IReadOnlyCollection<INPUT>>();
                }
            }

            foreach (var item in This) {
                if (item is IEnumerable<IEvent> E) {
                    var RecursiveChildren = E.RecursiveChildren().ToList();
                    if (RecursiveChildren.TrueForAll(x=> x is RawInput)) {
                        CurrentEvents.Add(item);
                        CurrentInputs.AddRange(RecursiveChildren.OfType<RawInput>().Select(x => x.Data));
                    } else {
                        WrapUp();
                        Linear.Add(item);
                    }
                } else {
                    WrapUp();
                    Linear.Add(item);
                }
            }

            WrapUp();


            return Linear;
        }

        private static IEnumerable<IEvent> RecursiveChildren(this IEnumerable<IEvent> This) {

            foreach (var child in This) {
                if(child is IEnumerable<IEvent> E) {
                    foreach (var descendant in E.RecursiveChildren()) {
                        yield return descendant;
                    }

                } else {
                    yield return child;
                }
            }

        }

    }



}
