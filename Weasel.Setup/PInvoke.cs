using System;
using System.Runtime.InteropServices;

namespace Weasel.Setup
{
    internal class PInvoke
    {
        public static partial class Input
        {
            [Flags]
            public enum ILOT
            {
                /// <summary>Same as ILOT_DISABLED.</summary>
                ILOT_UNINSTALL = 0x00000001,
                /// <summary>Sets the specified layout or tip as a default item.</summary>
                ILOT_DEFPROFILE = 0x00000002,
                /// <summary>Changes the setting of .Default.</summary>
                ILOT_DEFUSER4 = 0x00000004,
                /// <summary>Unused.</summary>
                ILOT_SYSLOCALE = 0x00000008,
                /// <summary>Unused.</summary>
                ILOT_NOLOCALETOENUMERATE = 0x00000010,
                /// <summary>The setting is saved but is not applied to the current session.</summary>
                ILOT_NOAPPLYTOCURRENTSESSION = 0x00000020,
                /// <summary>Disables all of the current keyboard layouts and text services.</summary>
                ILOT_CLEANINSTALL = 0x00000040,
                /// <summary>Disables the specified keyboard layout and text service.</summary>
                ILOT_DISABLED = 0x00000080,
            }

            [DllImport("input.dll", SetLastError = false, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool InstallLayoutOrTip([In, MarshalAs(UnmanagedType.LPWStr)] string psz, [In] ILOT dwFlags);
        }

        public static partial class IO
        {
            [Flags]
            public enum MoveFileFlags
            {
                MOVEFILE_REPLACE_EXISTING = 0x1,
                MOVEFILE_DELAY_UNTIL_REBOOT = 0x4
            }


            [DllImport("kernel32.dll", EntryPoint = "MoveFileExW", SetLastError = true, CharSet = CharSet.Unicode)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool MoveFileEx(string src, string dest, MoveFileFlags flags);
        }

        public static partial class HIDPI
        {
            [Flags]
            public enum PROCESS_DPI_AWARENESS
            {
                /// <summary>
                /// DPI unaware. This app does not scale for DPI changes and is always assumed to have a scale factor of 100% (96 DPI). It will
                /// be automatically scaled by the system on any other DPI setting.
                /// </summary>
                PROCESS_DPI_UNAWARE = 0,

                /// <summary>
                /// System DPI aware. This app does not scale for DPI changes. It will query for the DPI once and use that value for the
                /// lifetime of the app. If the DPI changes, the app will not adjust to the new DPI value. It will be automatically scaled up or
                /// down by the system when the DPI changes from the system value.
                /// </summary>
                PROCESS_SYSTEM_DPI_AWARE,

                /// <summary>
                /// Per monitor DPI aware. This app checks for the DPI when it is created and adjusts the scale factor whenever the DPI changes.
                /// These applications are not automatically scaled by the system.
                /// </summary>
                PROCESS_PER_MONITOR_DPI_AWARE,
            }

            [DllImport("Shcore.dll", SetLastError = true)]
            public static extern int SetProcessDpiAwareness(PROCESS_DPI_AWARENESS type);
        }
    }
}
