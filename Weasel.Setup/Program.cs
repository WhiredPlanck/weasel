﻿using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Weasel.Setup
{
    internal class Program
    {
        private static bool IsRunAsAdmin()
        {
            var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        private static void RunAsAdmin(string arg)
        {
            var info = new ProcessStartInfo
            {
                FileName = Application.ExecutablePath,
                Arguments = arg,
                Verb = "RunAs",
                UseShellExecute = true,
            };
            Process.Start(info);
        }

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var appCulture = Utils.App.CultureInfo;
            Thread.CurrentThread.CurrentCulture = appCulture;
            Thread.CurrentThread.CurrentUICulture = appCulture;

            Run(string.Join(" ", args));
        }

        private static readonly string WEASEL_PROG_REG_KEY = @"SOFTWARE\Rime\Weasel";
        private static readonly string WEASEL_UPDATE_REG_KEY = $@"{WEASEL_PROG_REG_KEY}\Updates";

        private static void Run(string arg)
        {
            try
            {
                if (arg.StartsWith("/userdir:")) // 设置用户目录
                {
                    var dir = arg.Substring(arg.IndexOf(':') + 1);
                    if (!string.IsNullOrEmpty(arg))
                    {
                        Utils.Reg.SetValue(Registry.CurrentUser, WEASEL_PROG_REG_KEY, "RimeUserDir", dir);
                    }
                    return;
                }
                else
                {
                    switch (arg) // 无需管理员权限的操作
                    {
                        case "/ls": // 简体中文
                            Utils.Reg.SetValue(Registry.CurrentUser, WEASEL_PROG_REG_KEY, "Language", "chs");
                            return;
                        case "/lt": // 繁体中文
                            Utils.Reg.SetValue(Registry.CurrentUser, WEASEL_PROG_REG_KEY, "Language", "cht");
                            return;
                        case "/le": // 英语
                            Utils.Reg.SetValue(Registry.CurrentUser, WEASEL_PROG_REG_KEY, "Language", "eng");
                            return;
                        case "/eu": // 启用更新
                            Utils.Reg.SetValue(Registry.CurrentUser, WEASEL_UPDATE_REG_KEY, "CheckForUpdates", 1);
                            return;
                        case "/du": // 禁用更新
                            Utils.Reg.SetValue(Registry.CurrentUser, WEASEL_UPDATE_REG_KEY, "CheckForUpdates", 0);
                            return;
                        case "/toggleime":
                            Utils.Reg.SetValue(Registry.CurrentUser, WEASEL_PROG_REG_KEY, "ToggleImeOnOpenClose", 1);
                            return;
                        case "/toggleascii":
                            Utils.Reg.SetValue(Registry.CurrentUser, WEASEL_PROG_REG_KEY, "ToggleImeOnOpenClose", 0);
                            return;
                        case "/testing": // 测试通道
                            Utils.Reg.SetValue(Registry.CurrentUser, WEASEL_PROG_REG_KEY, "UpdateChannel", "testing");
                            return;
                        case "/release": // 正式通道
                            Utils.Reg.SetValue(Registry.CurrentUser, WEASEL_PROG_REG_KEY, "UpdateChannel", "release");
                            return;
                        default: // 需要管理员权限的操作
                            if (!IsRunAsAdmin())
                            {
                                RunAsAdmin(arg);
                                Application.Exit();
                                return;
                            }
                            switch (arg)
                            {
                                case "/u": // 卸载
                                    Setup.Uninstall(true);
                                    return;
                                case "/s": // 简体中文安装
                                    Setup.NormalInstall(false);
                                    return;
                                case "/t": // 繁体中文安装
                                    Setup.NormalInstall(true);
                                    return;
                                default:
                                    break;
                            }
                            break;
                    }
                }
                // 自定义安装
                CustomInstall(arg == "/i");
            }
            catch (Exception ex)
            {
                MessageBox.Show(Localization.Resources.STR_ERROR, ex.Message);
            }
        }

        private static void CustomInstall(bool isInstalling)
        {
            var isSilentMode = isInstalling;
            var isInstalled = Setup.IsWeaselInstalled;

            var isHant = Convert.ToBoolean(
                Utils.Reg.GetValue(Registry.CurrentUser, WEASEL_PROG_REG_KEY, "Hant", 0)
            );
            var userDir = Convert.ToString(
                Utils.Reg.GetValue(Registry.CurrentUser, WEASEL_PROG_REG_KEY, "RimeUserDir", string.Empty)
            );

            if (!isSilentMode)
            {
                var dialog = new SetupOptionDialog
                {
                    IsInstalled = isInstalled,
                    IsHant = isHant,
                    UserDir = userDir
                };
                if (DialogResult.OK == dialog.ShowDialog())
                {
                    isInstalled = dialog.IsInstalled;
                    isHant = dialog.IsHant;
                    userDir = dialog.UserDir;

                    Utils.Reg.SetValue(Registry.CurrentUser, WEASEL_PROG_REG_KEY, "RimeUserDir", userDir);
                    Utils.Reg.SetValue(Registry.CurrentUser, WEASEL_PROG_REG_KEY, "Hant", isHant ? 1 : 0);
                }
                else
                {
                    if (!isInstalling)
                    {
                        Application.Exit();
                        return;
                    }
                }
            }
            if (!isInstalled)
            {
                Setup.NormalInstall(isHant, isSilentMode);
            }
            else
            {
                var installDir = Path.GetDirectoryName(Application.ExecutablePath); ;
                Task.Run(() =>
                {
                    ExecProcess(Path.Combine(installDir, "WeaselServer.exe"), "/q");
                    Task.Delay(500);

                    ExecProcess(Path.Combine(installDir, "WeaselServer.exe"), string.Empty);
                    Task.Delay(500);

                    ExecProcess(Path.Combine(installDir, "WeaselDeployer.exe"), "/deploy");
                });
                MessageBox.Show(Localization.Resources.STR_UF_CHANGE_SUCCESS);
            }
        }

        private static void ExecProcess(string path, string args)
        {
            var info = new ProcessStartInfo
            {
                FileName = path,
                Arguments = args,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Normal,
            };
            Process.Start(info);
        }
    }
}