﻿// This code is distributed under MIT license. 
// Copyright (c) 2010-2018 George Mamaladze
// See license.txt or https://mit-license.org/

using System;
using System.Collections.Generic;
using WindowsInput;

namespace ConsoleHook {

    public static class Hook {
        public static IKeyboardEvents GlobalKeyboard { get; } = WindowsInput.Capture.Global.Keyboard();
        public static IMouseEvents GlobalMouse { get; } = WindowsInput.Capture.Global.Mouse();
    }

    internal class QuickStart {
        public void Do() {
            //1. Define key combinations
            var undo = Combination.FromString("Control+Z");
            //Using builder method
            var undo2 = Combination.TriggeredBy(KeyCode.Z).With(KeyCode.ControlModifier);
            var fullScreen = Combination.FromString("Shift+Alt+Enter");

            //2. Define actions
            Action actionUndo = DoSomething;
            Action actionFullScreen = () => { Console.WriteLine("You Pressed FULL SCREEN"); };

            void DoSomething() {
                Console.WriteLine("You pressed UNDO");
            }

            //3. Assign actions to key combinations
            var assignment = new Dictionary<Combination, Action>
            {
                {undo, actionUndo},
                {fullScreen, actionFullScreen}
            };

            //4. Install listener
            Hook.GlobalKeyboard.OnCombination(assignment);
        }

        public void DoCompact() {
            void DoSomething() {
                Console.WriteLine("You pressed UNDO");
            }

            Hook.GlobalKeyboard.OnCombination(new Dictionary<Combination, Action>
            {
                {Combination.FromString("Control+Z"), DoSomething},
                {Combination.FromString("Shift+Alt+Enter"), () => { Console.WriteLine("You Pressed FULL SCREEN"); }}
            });
        }

        public void DoSequence() {
            var exitVim = Sequence.FromString("Shift+Z,Z");
            var rename = Sequence.FromString("Control+R,R");
            var exitReally = Sequence.FromString("Escape,Escape,Escape");

            var assignment = new Dictionary<Sequence, Action>
            {
                {exitVim, () => Console.WriteLine("No!")},
                {rename, () => Console.WriteLine("rename2")},
                {exitReally, () => Console.WriteLine("Ok.")}
            };

            Hook.GlobalKeyboard.OnSequence(assignment);
        }
    }
}