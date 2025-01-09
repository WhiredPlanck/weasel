using Microsoft.Win32;
using System;
using System.Globalization;
using System.IO;
using System.Threading;

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
                catch
                {
                    for (int i = 0; i < 10; i++)
                    {
                        var old = $"{dest}.old.{i}";
                        if (PInvoke.IO.MoveFileEx(dest, old, PInvoke.IO.MoveFileFlags.MOVEFILE_REPLACE_EXISTING))
                        {
                            PInvoke.IO.MoveFileEx(old, null, PInvoke.IO.MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                            break;
                        }
                    }
                    try
                    {
                        File.Copy(src, dest, true);
                    }
                    catch
                    {
                    }
                }
            }

            public static void DeleteFile(string path)
            {
                try
                {
                    File.Delete(path);
                }
                catch
                {
                    for (int i = 0; i < 10; i++)
                    {
                        var old = $"{path}.old.{i}";
                        if (PInvoke.IO.MoveFileEx(path, old, PInvoke.IO.MoveFileFlags.MOVEFILE_REPLACE_EXISTING))
                        {
                            PInvoke.IO.MoveFileEx(old, null, PInvoke.IO.MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                            return;
                        }
                    }
                }
            }
        }
    }
}
