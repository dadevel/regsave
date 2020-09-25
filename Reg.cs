using System;
using static RegSave.Interop;

namespace RegSave {
    internal class Reg {
        public static UIntPtr HKEY_LOCAL_MACHINE = new UIntPtr(0x80000002u);
        public static int KEY_READ = 0x20019;
        public static int KEY_ALL_ACCESS = 0xF003F;
        public static int REG_OPTION_OPEN_LINK = 0x0008;
        public static int REG_OPTION_BACKUP_RESTORE = 0x0004;
        public static int KEY_QUERY_VALUE = 0x1;

        public static void ExportRegKey(string key, string outFile) {
            var hKey = UIntPtr.Zero;
            RegOpenKeyEx(HKEY_LOCAL_MACHINE, key, REG_OPTION_BACKUP_RESTORE | REG_OPTION_OPEN_LINK, KEY_ALL_ACCESS, out hKey);
            RegSaveKey(hKey, outFile, IntPtr.Zero);
            RegCloseKey(hKey);
        }
    }
}
