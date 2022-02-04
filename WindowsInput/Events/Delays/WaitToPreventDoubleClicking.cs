using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WindowsInput.Native;

namespace WindowsInput.Events {
    public class WaitToPreventDoubleClicking : Wait {
        private static readonly TimeSpan ExtraDurationBecauseOfWindowsBug = TimeSpan.FromMilliseconds(13);

        protected override string GetDebuggerDisplay() {
            var ret = $@"{base.GetDebuggerDisplay()} (to prevent double-clicking)";

            return ret;
        }

        public WaitToPreventDoubleClicking() : base(SystemMetrics.Mouse.DoubleClick.Duration.Value + ExtraDurationBecauseOfWindowsBug) { 
        
        }

    }
}
