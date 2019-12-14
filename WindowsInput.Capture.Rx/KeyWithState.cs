// This code is distributed under MIT license. 
// Copyright (c) 2010-2018 George Mamaladze
// See license.txt or https://mit-license.org/

using System;

namespace WindowsInput.Capture.Rx {
    public class KeyWithState : Tuple<KeyCode, KeyboardState>
    {
        public KeyWithState(KeyCode key, KeyboardState state)
            : base(key, state)
        {
        }

        public KeyCode KeyCode
        {
            get { return Item1; }
        }

        public KeyboardState State
        {
            get { return Item2; }
        }

        public bool Matches(Combination combination)
        {
            return
                KeyCode == combination.TriggerKey &&
                State.AreAllDown(combination.Chord);
        }
    }
}