using Microsoft.Win32;
using System;
using System.Globalization;
using System.IO;
using System.Threading;

namespace Weasel.Setup
{
    internal class Utils
    {
        public static class App
        {
            public static CultureInfo CultureInfo
            {
                get
                {
                    var lang = (string)Registry.CurrentUser.GetValue(@"Software\Rime\Weasel\Language", string.Empty);
                    if (!string.IsNullOrEmpty(lang))
                    {
                        switch (lang)
                        {
                            case "chs":
                                return new CultureInfo("zh-Hans");
                            case "cht":
                                return new CultureInfo("zh-Hant");
                            default:
                                return new CultureInfo("en-US");
                        }
                    }
                    else
                    {
                        var current = Thread.CurrentThread.CurrentUICulture.Name;
                        if (current.StartsWith("zh"))
                        {
                            if (current.EndsWith("HK") || current.EndsWith("MO") ||
                                current.EndsWith("TW") || current.EndsWith("Hant"))
                            {
                                return new CultureInfo("zh-Hant");
                            }
                            else
                            {
                                return new CultureInfo("zh-Hans");
                            }
                        }
                        else
                        {
                            return new CultureInfo("en-US");
                        }
                    }
                }
            }

            public static void SetProcessApiAwareness()
            {
                if (Environment.OSVersion.Version >= new Version(6, 3, 9600)) // Windows 8.1
                {
                    PInvoke.HIDPI.SetProcessDpiAwareness(PInvoke.HIDPI.PROCESS_DPI_AWARENESS.PROCESS_PER_MONITOR_DPI_AWARE);
                }
            }
        }

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
