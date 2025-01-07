using Microsoft.Win32;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;
using TSF.InteropTypes;

namespace Weasel.Setup
{
    internal class Utils
    {
        public static class Reg
        {
            public static void SetValue(RegistryKey baseKey, string subKeyName, string valueName, object value)
            {
                using (var reg = baseKey.CreateSubKey(subKeyName))
                {
                    reg.SetValue(valueName, value);
                }
            }

            public static object GetValue(RegistryKey baseKey, string subKeyName, string valueName, object defaultValue)
            {
                using (var reg = baseKey.CreateSubKey(subKeyName))
                {
                    return reg.GetValue(valueName, defaultValue);
                }
            }
        }

        public static class Path
        {
            public static string LogPath
            {
                get
                {
                    var path = Environment.ExpandEnvironmentVariables(@"%TEMP%\rime.weasel");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    return path;
                }
            }
        }

        public static class IO
        {
            public static void CopyFile(string src, string dest)
            {
                try
                {
                    File.Copy(src, dest, true);
                }
                finally
                {
                    if (!File.Exists(dest))
                    {
                        var i = 0;
                        while (i < dest.Length)
                        {
                            var old = $"{dest}.old.{i}";
                            if (PInvoke.IO.MoveFileEx(dest, old, PInvoke.IO.MoveFileFlags.MOVEFILE_REPLACE_EXISTING))
                            {
                                PInvoke.IO.MoveFileEx(old, null, PInvoke.IO.MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                                break;
                            }
                        }
                        File.Copy(src, dest, true);
                    }
                }
            }

            public static void DeleteFile(string path)
            {
                try
                {
                    File.Delete(path);
                }
                finally
                {
                    if (File.Exists(path))
                    {
                        var i = 0;
                        while (i < path.Length)
                        {
                            var old = $"{path}.old.{i}";
                            if (PInvoke.IO.MoveFileEx(path, old, PInvoke.IO.MoveFileFlags.MOVEFILE_REPLACE_EXISTING))
                            {
                                PInvoke.IO.MoveFileEx(old, null, PInvoke.IO.MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}
