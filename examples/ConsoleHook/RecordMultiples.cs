using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput;
using WindowsInput.Events.Sources;

namespace ConsoleHook {
    public static class RecordMultiples {

        public static async Task Start() {
            Console.WriteLine("Recording from two sources");

            var Keyboards = new List<IKeyboardEventSource>();
            for (int i = 0; i < 1; i++) {
                Keyboards.Add(Capture.Global.KeyboardAsync());
            }

            await Task.Delay(1000 * 60 * 10);

            foreach (var item in Keyboards) {
                item?.Dispose();
            }

            
        }

    }
}
