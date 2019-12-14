// This code is distributed under MIT license. 
// Copyright (c) 2010-2018 George Mamaladze
// See license.txt or https://mit-license.org/


namespace WindowsInput.Capture.Rx {
    public static class KeysExtensions
    {
        public static KeyEvent Up(this KeyCode key)
        {
            return new KeyEvent(key, KeyEventKind.Up);
        }

        public static KeyEvent Down(this KeyCode key)
        {
            return new KeyEvent(key, KeyEventKind.Down);
        }

    }
}