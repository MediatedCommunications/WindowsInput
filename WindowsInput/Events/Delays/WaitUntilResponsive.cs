using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WindowsInput.Native;

namespace WindowsInput.Events {
    public class WaitUntilResponsive : EventBase {
        public IntPtr HWnd { get; private set; }
        public TimeSpan Timeout { get; private set; }

        protected override string DebuggerDisplay => $@"{base.DebuggerDisplay}: Wait up to {Timeout} for {HWnd} to become responsive.";

        public WaitUntilResponsive(IntPtr HWnd, TimeSpan Timeout) {
            this.HWnd = HWnd;
            this.Timeout = Timeout;
        }

        protected override Task<bool> Invoke(InvokeOptions Options) {
            var ret = true;

            var Invole = WindowMessageDispatcher.SendMessageTimeout(HWnd,
                WindowMessage.WM_NULL,
                UIntPtr.Zero,
                IntPtr.Zero,
                SendMessageTimeoutFlags.SMTO_NORMAL,
                (uint)Timeout.TotalMilliseconds,
                out var _);

            if (Invole == IntPtr.Zero) {
                ret = false;
            }

            if (ret == false && Options.Failure.Throw) {
                throw new InvokeDispatcherException();
            }
                
            return Task.FromResult(ret);
        }



    }
}
