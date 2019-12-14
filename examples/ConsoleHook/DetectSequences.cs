// This code is distributed under MIT license. 
// Copyright (c) 2010-2018 George Mamaladze
// See license.txt or https://mit-license.org/

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WindowsInput;

namespace ConsoleHook
{
    internal class DetectSequences
    {
        public static Task Do()
        {
            Console.WriteLine("NOT IMPLEMENTED!");
            return Task.CompletedTask;
            /*
            var map = new Dictionary<Sequence, Action>
            {
                {Sequence.FromString("Control+Z,B"), Console.WriteLine},
                {Sequence.FromString("Control+Z,Z"), Console.WriteLine},
                //{Sequence.FromString("Escape,Escape,Escape"), quit}
            };

            Console.WriteLine("Detecting following combinations:");
            foreach (var sequence in map.Keys)
                Console.WriteLine("\t{0}", sequence);
            Console.WriteLine("Press 3 x ESC (three times) to exit.");

            //Actual loop
            Hook.GlobalKeyboard.OnSequence(map);

    */
        }
    }
}