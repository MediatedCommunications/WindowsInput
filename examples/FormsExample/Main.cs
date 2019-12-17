// This code is distributed under MIT license. 
// Copyright (c) 2015 George Mamaladze
// See license.txt or https://mit-license.org/

using System;
using System.ComponentModel;
using System.Windows.Forms;
using WindowsInput.EventSources;

namespace Demo
{
    public partial class Main : Form
    {
        private IKeyboardEventSource m_Keyboard;
        private IMouseEventSource m_Mouse;


        public Main()
        {
            InitializeComponent();
            FormClosing += Main_Closing;

            Subscribe();
        }

        private void Main_Closing(object sender, CancelEventArgs e)
        {
            Unsubscribe();
        }

        private void Subscribe(IKeyboardEventSource Keyboard) {
            this.m_Keyboard?.Dispose();
            this.m_Keyboard = Keyboard;

            if (Keyboard != default) {
                Keyboard.KeyEvent += this.Keyboard_KeyEvent;
            }
        }




        private void Subscribe(IMouseEventSource Mouse) {
            this.m_Mouse?.Dispose();
            this.m_Mouse = Mouse;

            if (Mouse != default) {
                Mouse.MouseEvent += this.Mouse_MouseEvent;
            }
        }

        private void Mouse_MouseEvent(object sender, EventSourceEventArgs<MouseEvent> e) {
            var Notes = "";

            if(checkBoxSuppressMouse.Checked && e.Data.ButtonClick != default && e.Data.ButtonClick.Button == WindowsInput.Events.ButtonCode.Right) {
                e.Next_Event_Enabled = true;

                Notes = "SUPPRESSED!!!";
            }

            if (checkBoxSupressMouseWheel.Checked && e.Data.ButtonScroll != default) {
                e.Next_Event_Enabled = true;
                Notes = "SUPPRESSED!!!";
            }
            Log(e, Notes);

        }

        private void Keyboard_KeyEvent(object sender, EventSourceEventArgs<KeyboardEvent> e) {
            Log(e);
        }

        private void Log<T>(EventSourceEventArgs<T> e, string Notes = "") where T: InputEvent {
            var NewContent = "";
            NewContent += $"{e.Timestamp}: {Notes}\r\n";
            foreach (var item in e.Data.Events) {
                NewContent += $"  {item}\r\n";
            }
            NewContent += "\r\n";

            textBoxLog.Text = NewContent + textBoxLog.Text;

        }

        private void Unsubscribe()
        {
            m_Keyboard?.Dispose();
            m_Keyboard = null;

            m_Mouse?.Dispose();
            m_Mouse = null;

        }

        private void clearLog_Click(object sender, EventArgs e)
        {
            textBoxLog.Clear();
        }

        private void Keyboard_Changed(object sender, EventArgs e) {
            Subscribe();
        }

        private void Mouse_Changed(object sender, EventArgs e) {
            Subscribe();
        }

        private void Subscribe() {
            var Keyboard = default(IKeyboardEventSource);
            if (rbKeyGlobal.Checked) {
                Keyboard = WindowsInput.Capture.Global.Keyboard();
            } else if (rbKeyApp.Checked) {
                Keyboard = WindowsInput.Capture.CurrentThread.Keyboard();
            }

            var Mouse = default(IMouseEventSource);
            if (rbMouseGlobal.Checked) {
                Mouse = WindowsInput.Capture.Global.Mouse();
            } else if (rbMouseApp.Checked) {
                Mouse = WindowsInput.Capture.CurrentThread.Mouse();
            }

            Subscribe(Keyboard);
            Subscribe(Mouse);
        }

    }
}