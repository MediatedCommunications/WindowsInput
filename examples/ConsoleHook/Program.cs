// This code is distributed under MIT license. 
// Copyright (c) 2010-2018 George Mamaladze
// See license.txt or https://mit-license.org/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsInput;
using WindowsInput.Events;
using WindowsInput.Native;

namespace ConsoleHook
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            //Make it so we can see special characters.
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            var selector = new Dictionary<string, Func<Task>>
            {
                {"1. Record and Playback Keyboard Events", ()=>LogEvents.Start(true, false)},
                {"2. Record and Playback Mouse Events", ()=>LogEvents.Start(false, true)},
                {"3. Detect key combinations", DetectChord.Do},
                {"4. Detect key sequences", DetectSequences.Do},
                {"5. Record Multiples", RecordMultiples.Start},
                {"Q. Quit", Exit}
            };

            Func<Task> action = null;

            while (action == null)
            {
                Console.WriteLine("Please select one of these:");
                foreach (var selectorKey in selector.Keys)
                    Console.WriteLine(selectorKey);
                var ch = Console.ReadKey(true).KeyChar;
                action = selector
                    .Where(p => p.Key.StartsWith(ch.ToString()))
                    .Select(p => p.Value).FirstOrDefault();
                if (action == default) {
                    Console.WriteLine();
                }
            }
            await action();

            Console.WriteLine("--------------------------------------------------");

        }


        private static Task Exit()
        {
            return Task.CompletedTask;
        }
    }
}