using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsInput {
    internal static class SWITCHES {
        static SWITCHES() {

            try {
                var value = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ReleaseId", "")?.ToString() ?? "";

                if(long.TryParse(value, out var V)) {
                    ReleaseId = V;
                }
            } catch { 
            }

            if(ReleaseId > 1607) {
                Windows10_AtLeast_v1607_Enabled = true;
            }

        }

        public static long ReleaseId { get; private set; }

        public static bool Windows10_AtLeast_v1607_Enabled { get; private set; }


        

    }
}
