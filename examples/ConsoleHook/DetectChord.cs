// This code is distributed under MIT license. 
// Copyright (c) 2010-2018 George Mamaladze
// See license.txt or https://mit-license.org/

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WindowsInput;
using WindowsInput.Events;
using WindowsInput.EventSources;

namespace ConsoleHook
{
    internal class DetectChord
    {

        public static Task Do() {
            using (var Keyboard = WindowsInput.Capture.Global.KeyboardAsync()) {

                var Listener = new WindowsInput.EventSources.KeyChordEventSource(Keyboard, new ChordClick(KeyCode.Control, KeyCode.Alt, KeyCode.Shift));
                Listener.Triggered += (x, y) => Listener_Triggered(Keyboard, x, y);
                Listener.Reset_On_Parent_EnabledChanged = false;
                Listener.Enabled = true;

                Console.WriteLine("The keyboard is now listening for chords.  Try typing 'CONTROL+ALT+SHIFT' in notepad.");

                Console.WriteLine("Press enter to quit...");
                Console.ReadLine();

            }


            return Task.CompletedTask;

        }

        private static async void Listener_Triggered(IKeyboardEventSource Keyboard, object sender, WindowsInput.EventSources.KeyChordEventArgs e) {
            var ToSend = WindowsInput.Simulate.Events();
            
            ToSend.Click("You pressed the magic keys.");

            using (Keyboard.Suspend()) {
                await ToSend.Invoke();
            }


        }

    }
}