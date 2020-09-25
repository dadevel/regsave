using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Principal;
using static RegSave.Interop;

namespace RegSave {
    public class Privileges {
        // from https://github.com/Dijji/RepairTasks/blob/97acc1f1806f64f23c978253924836fa7018b5fc/RegKey.cs
        public static void EnableDisablePrivilege(string PrivilegeName, bool EnableDisable) {
            var htok = IntPtr.Zero;
            if (!OpenProcessToken(Process.GetCurrentProcess().Handle, TokenAccessLevels.AdjustPrivileges | TokenAccessLevels.Query, out htok)) {
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
                return;
            }
            var tkp = new TOKEN_PRIVILEGES { PrivilegeCount = 1 };
            LUID luid;
            if (!LookupPrivilegeValue(null, PrivilegeName, out luid)) {
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
                return;
            }
            tkp.Luid = luid;
            tkp.Attributes = (uint)(EnableDisable ? 2 : 0);
            TOKEN_PRIVILEGES prv;
            uint rb;
            if (!AdjustTokenPrivileges(htok, false, ref tkp, 256, out prv, out rb)) {
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
            }
        }
    }
}
