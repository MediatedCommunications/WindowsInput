﻿using System;
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
            return Events(default, IEvents);
        }

        /// <inheritdoc cref="Events(InvokeOptions, IEnumerable{IEvent})"/>
        public static Task<bool> Events(InvokeOptions? Options, params IEvent[] IEvents) {
            return Events(Options, (IEnumerable<IEvent>)IEvents);
        }

        /// <summary>
        /// Immediately simulate a series of input events.
        /// </summary>
        /// <param name="Options">Options that control the execution of the simulation</param>
        /// <param name="IEvents">The list of input events to simulate.</param>
        /// <returns></returns>
        public static async Task<bool> Events(InvokeOptions? Options, IEnumerable<IEvent> IEvents) {
            Options ??= new InvokeOptions();
            var ret = true;

            var EventList = new List<IEvent>();

            if (IEvents is { }) {
                EventList.AddRange(IEvents);
            }

            EventList = EventList.SendInput_Flatten(Options.SendInput);
            


            foreach (var item in EventList) {
                if (Options.Cancellation.Token.IsCancellationRequested) {
                    if (Options.Cancellation.ThowWhenCanceled) {
                        Options.Cancellation.Token.ThrowIfCancellationRequested();
                    } else {
                        break;
                    }
                }

                if (item is { }) {

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



        private static List<IEvent> SendInput_Flatten(this IEnumerable<IEvent> This, SendInputOptions Options) {
            var MaxItemsPerBatch = Math.Max(1, Options.BatchSize);
            var BatchDelay = Options.BatchDelay > TimeSpan.Zero ? Options.BatchDelay : TimeSpan.Zero;
            var BatchDelayItem = BatchDelay> TimeSpan.Zero ? new Wait(BatchDelay) : default;
            
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

            foreach (var CurrentEvent in This) {
                if (CurrentEvent is IEnumerable<IEvent> E) {
                    var RecursiveChildren = E.RecursiveChildren().ToList();
                    if (RecursiveChildren.TrueForAll(x=> x is RawInput)) {
                        CurrentEvents.Add(CurrentEvent);

                        foreach (var RecursiveChild in RecursiveChildren.OfType<RawInput>().Select(x => x.Data)) {
                            CurrentInputs.Add(RecursiveChild);

                            if(CurrentInputs.Count >= MaxItemsPerBatch) {
                                WrapUp();
                                CurrentEvents.Add(CurrentEvent);

                                if(BatchDelayItem is { }) {
                                    Linear.Add(BatchDelayItem);
                                }

                            }
                        }
                        

                    } else {
                        WrapUp();
                        Linear.Add(CurrentEvent);
                    }
                } else {
                    WrapUp();
                    Linear.Add(CurrentEvent);
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
