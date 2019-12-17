// This code is distributed under MIT license. 
// Copyright (c) 2010-2018 George Mamaladze
// See license.txt or https://mit-license.org/

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WindowsInput;
using WindowsInput.EventSources;

namespace ConsoleHook
{
    internal class DetectSequences
    {
        public static Task Do()
        {
            using (var Keyboard = WindowsInput.Capture.Global.KeyboardAsync()) {

                var Listener = new WindowsInput.EventSources.TextSequenceEventSource(Keyboard, new WindowsInput.Events.TextClick("aaa"));
                Listener.Triggered += (x, y) => Listener_Triggered(Keyboard, x, y); ;
                Listener.Enabled = true;

                Console.WriteLine("The keyboard is now listening for sequences.  Try typing 'aaa' in notepad.");

                Console.WriteLine("Press enter to quit...");
                Console.ReadLine();

            }

                
            return Task.CompletedTask;
        }



        private static async void Listener_Triggered(IKeyboardEventSource Keyboard, object sender, WindowsInput.EventSources.TextSequenceEventArgs e) {
            e.Input.Next_Hook_Enabled = false;

            var ToSend = WindowsInput.Simulate.Events();
            for (int i = 1; i < e.Sequence.Text.Length; i++) {
                ToSend.Click(WindowsInput.Events.KeyCode.Backspace);
            }

            ToSend.Click("Always ask albert!");

            //We suspend keyboard events because we don't want to accidently trigger a recursive loop if our
            //sending text actually had 'aaa' in it.
            using (Keyboard.Suspend()) {
                await ToSend.Invoke();
            }
        }

    }
}