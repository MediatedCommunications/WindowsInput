// This code is distributed under MIT license. 
// Copyright (c) 2010-2018 George Mamaladze
// See license.txt or https://mit-license.org/


namespace WindowsInput.Capture.Rx {
    public struct KeyEvent
    {
        public KeyEvent(KeyCode keyCode, KeyEventKind kind = KeyEventKind.Down)
        {
            KeyCode = keyCode;
            Kind = kind;
        }

        public KeyEventKind Kind { get; }
        public KeyCode KeyCode { get; private set; }

        public override string ToString()
        {
            return string.Format("{0}-{1}", KeyCode, Kind);
        }

        public KeyEvent ApplyMofidifers(KeyboardState state)
        {
            if (state.IsDown(KeyCode.LControl) || state.IsDown(KeyCode.RControl)) {
                KeyCode |= KeyCode.ControlModifier;
            }

            if (state.IsDown(KeyCode.LShift) || state.IsDown(KeyCode.RShift)) {
                KeyCode |= KeyCode.ShiftModifier;
            }

            if (state.IsDown(KeyCode.LMenu) || state.IsDown(KeyCode.RMenu)) {
                KeyCode |= KeyCode.AltModifier;
            }

            return this;
        }
    }
}