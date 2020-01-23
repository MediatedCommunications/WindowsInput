// This code is distributed under MIT license. 
// Copyright (c) 2010-2018 George Mamaladze
// See license.txt or https://mit-license.org/

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WindowsInput;
using WindowsInput.Events;
using WindowsInput.Events.Sources;

namespace ConsoleHook
{
    internal class LogEvents {

        private static void QueueAndLog(EventBuilder This, IEvent Value) {
            This.Add(Value);

            Console.WriteLine(Value);
        }

        private static void QueueAndLog(EventBuilder This, KeyboardEvent Value, string Prefix = "  ") {

            This
                .Add(Value.Wait)
                .Add(Value.KeyDown)
                .Add(Value.KeyUp)
                ;
            
            var ToShow = new List<IEvent>() {
                Value.Wait,
                Value.KeyDown,
                Value.KeyUp,
                Value.TextClick,
            };

            Show(ToShow);
        }

        private static void Show(IEnumerable<IEvent> ToShow) {

            var Data = (
                from x in ToShow
                let Text = x?.ToString() ?? ""
                let Parts = Text.Split(new[] { ' ' }, 2)
                let Part1 = Parts.Length >= 1 ? Parts[0] : ""
                let Part2 = Parts.Length >= 2 ? Parts[1] : ""
                select new[] {
                    Part1,
                    Part2
                }).ToList();

            var ColumnWidth = (from x in Data from y in x select y.Length).Max() + 1;
            var Rows = (from x in Data select x.Length).Max();

            for (int y = 0; y < Rows; y++) {
                for (int x = 0; x < Data.Count; x++) {
                    Console.Write($@"{Data[x][y],-20}");
                }
                Console.WriteLine();
            }
            Console.WriteLine();


            
        }

        private static void QueueAndLog(EventBuilder This, MouseEvent Value, string Prefix = "  ") {
            This
                .Add(Value.Wait)
                .Add(Value.Move)
                .Add(Value.ButtonScroll)
                .Add(Value.ButtonDown)
                .Add(Value.ButtonUp)
                ;

            var ToShow = new List<IEvent>() {
                Value.Wait,
                Value.Move,
                Value.ButtonScroll,
                Value.ButtonDown,
                Value.ButtonUp,
                Value.ButtonClick,
                Value.ButtonDoubleClick,
            };

            if(Value.DragStart != default) {
                ToShow.AddRange(Value.DragStart);
            }

            if (Value.DragStop != default) {
                ToShow.AddRange(Value.DragStop);
            }

            Show(ToShow);
                

        }


        private static EventBuilder CollectActionsFor(int DurationInSections, bool Keyboard = false, bool Mouse = true) {
            var ret = new EventBuilder();

            var ToDispose = new List<IDisposable>();
            
            if(Keyboard) {
                var GlobalKeyboard = Capture.Global.KeyboardAsync();
                GlobalKeyboard.KeyEvent += (x, y) => QueueAndLog(ret, y.Data);
                ToDispose.Add(GlobalKeyboard);
            }

            if(Mouse) {
                var GlobalMouse = Capture.Global.MouseAsync();
                GlobalMouse.MouseEvent += (x, y) => QueueAndLog(ret, y.Data);
                ToDispose.Add(GlobalMouse);
            }


            for (int i = DurationInSections; i > 0; i--) {
                //Console.WriteLine($@"Recording for {i} more seconds...");
                System.Threading.Thread.Sleep(1000);
            }


            foreach (var item in ToDispose) {
                item.Dispose();
            }

            return ret;

        }



        public static async Task Start(bool Keyboard, bool Mouse)
        {
            var RecordDuration = 10000;
            var DelayDuration = 3;

            Console.WriteLine($@"For the next {RecordDuration} seconds, the following events will be recorded then played back:");
            Console.WriteLine($@"Keyboard: {Keyboard}");
            Console.WriteLine($@"Mouse:    {Mouse}");

            for (int i = DelayDuration; i >= 0; i--) {
                Console.WriteLine($@"Recording starts in {i} seconds...");
                await Task.Delay(1000);
            }

            var Actions = CollectActionsFor(RecordDuration, Keyboard, Mouse);

            Console.WriteLine("------------------------");
            Console.WriteLine();
            Console.WriteLine($@"A total of {Actions.Events.Count} events were record.");
            Console.WriteLine();

            Console.WriteLine("Press Enter to begin playback...");
            Console.ReadLine();
            Console.WriteLine();

            for (int i = DelayDuration; i >= 0; i--) {
                Console.WriteLine($@"Playback starts in {i} seconds...");
                await Task.Delay(1000);
            }

            await Actions.Invoke();

        }
    }
}