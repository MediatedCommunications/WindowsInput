using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput;
using WindowsInput.Events.Sources;

namespace ConsoleHook {
    public static class DisallowKeys {

        public static async Task Start() {
            Console.WriteLine("Disallowing the 'W' key on the keyboard for 10 seconds");

            var Keyboards = new List<IKeyboardEventSource>();
            for (int i = 0; i < 1; i++) {
                Keyboards.Add(Capture.Global.KeyboardAsync());
            }

            foreach (var item in Keyboards) {
                item.KeyEvent += Item_KeyEvent;
            }

            await Task.Delay(1000 * 10);

            foreach (var item in Keyboards) {
                item?.Dispose();
            }

            
        }

        private static void Item_KeyEvent(object sender, EventSourceEventArgs<KeyboardEvent> e) {
            if (e.Data.KeyDown?.Key == WindowsInput.Events.KeyCode.W || e.Data.KeyUp?.Key == WindowsInput.Events.KeyCode.W) {
                //Tell the OS to ignore this key
                e.Next_Hook_Enabled = false;
            }
        }

    }
}
