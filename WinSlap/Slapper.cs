using WinSlap.Properties;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Management.Automation;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WinSlap
{
    public static class Slapper
    {

        public static void DownloadRun(string url, string filename, string parameters)
        {
            using (WebClient client = new WebClient())
            {
                FileInfo file = new FileInfo(MainForm.Tmpfolder + filename);

                DialogResult result = DialogResult.Retry;
                while (result == DialogResult.Retry)
                {
                    try
                    {
                        client.DownloadFile(new Uri(url), file.FullName);

                        ProcessStartInfo ffi = new ProcessStartInfo(file.FullName, parameters);
                        Process ff = Process.Start(ffi);
                        ff?.WaitForExit();

                        // continue if installation was successful
                        result = DialogResult.Ignore;
                    }
                    catch (WebException)
                    {
                        string caption = "Something went wrong...";
                        string errorMessage = "A WebException occured trying to download from the following URL:\n\n" + url + "\n\nPlease check your network connection and report this issue on GitHub if the error persists";
                        result = MessageBox.Show(errorMessage, caption, MessageBoxButtons.AbortRetryIgnore);
                        if (result == DialogResult.Abort) Application.Exit();
                    }
                }
            }
        }

        public static string GetLatestFtp(string url, string pattern)
        {
            Uri uri = new Uri(url);
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uri);
            request.Method = WebRequestMethods.Ftp.ListDirectory;

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);

            var line = reader.ReadLine();

            while (line != null)
            {
                if (Regex.IsMatch(line, pattern))
                {
                    return url + line;
                }
                line = reader.ReadLine();
            }

            reader.Close();
            response.Close();
            return "";
        }

        public static string GetRedirect(string url)
        {
            Uri uri = new Uri(url);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AllowAutoRedirect = false;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            return response.Headers.Get("Location");
        }

        public static void HideTaskview()
        {
            RegistryKey myKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", true);
            myKey.SetValue("ShowTaskViewButton", "0", RegistryValueKind.DWord);
        }

        public static void DoNotGroupTasks()
        {
            RegistryKey myKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", true);
            myKey.SetValue("TaskbarGlomLevel", "2", RegistryValueKind.DWord);
        }

        public static void TaskbarSmallIcons()
        {
            RegistryKey myKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", true);
            myKey.SetValue("TaskbarSmallIcons", "1", RegistryValueKind.DWord);
        }

        public static void DisablePeopleBand()
        {
            Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced\People", true);
            RegistryKey myKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced\People", true);
            myKey.SetValue("PeopleBand", "0", RegistryValueKind.DWord);
        }

        public static void HideSearch()
        {
            RegistryKey myKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Search", true);
            myKey.SetValue("SearchboxTaskbarMode", "0", RegistryValueKind.DWord);
        }

        /*
        // TODO: does not seem to work!
        public static void UpdateMSProducts()
        {
            RegistryKey myKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\WindowsUpdate\Services\7971F918-A847-4430-9279-4A52D1EFE18D", true);
            myKey.SetValue("RegisteredWithAU", "1", RegistryValueKind.DWord);
        }

        */

        // TODO: not tested
        public static void DisableSharedExperiences()
        {
            RegistryKey myKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\CDP", true);
            myKey.SetValue("CdpSessionUserAuthzPolicy", "0", RegistryValueKind.DWord);
            myKey.SetValue("RomeSdkChannelUserAuthzPolicy", "0", RegistryValueKind.DWord);
        }

        public static void RemoveCompatibility()
        {
                Registry.ClassesRoot.DeleteSubKey(@"exefile\shellex\ContextMenuHandlers\Compatibility", false);
        }

        public static void DisableCortana()
        {
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\Windows Search", true);
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\WOW6432Node\Policies\Microsoft\Windows\Windows Search", true);

            RegistryKey myKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\Windows Search", true);
            myKey.SetValue("AllowCortana", "0", RegistryValueKind.DWord);

            RegistryKey myKey2 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Policies\Microsoft\Windows\Windows Search", true);
            myKey2.SetValue("AllowCortana", "0", RegistryValueKind.DWord);
        }

        public static void DisableLLMNR()
        {
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows NT\DNSClient", true);

            RegistryKey myKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows NT\DNSClient", true);
            myKey.SetValue("EnableMulticast", "0", RegistryValueKind.DWord);
        }

        public static void DisableSmartNameResolution()
        {
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows NT\DNSClient", true);

            RegistryKey myKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows NT\DNSClient", true);
            myKey.SetValue("DisableSmartNameResolution", "1", RegistryValueKind.DWord);

            RegistryKey myKey2 = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\Dnscache\Parameters", true);
            myKey2.SetValue("DisableParallelAandAAAA", "1", RegistryValueKind.DWord);
        }

        public static void DisableWPAD()
        {
            RegistryKey myKey = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\Tcpip\Parameters", true);
            myKey.SetValue("UseDomainNameDevolution", "0", RegistryValueKind.DWord);
        }

        public static void DisableLockscreenBlur()
        {
            RegistryKey myKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\System", true);
            myKey.SetValue("DisableAcrylicBackgroundOnLogon", "1", RegistryValueKind.DWord);
        }

        public static void DisableSecurityQuestions()
        {
            RegistryKey myKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\System", true);
            myKey.SetValue("NoLocalPasswordResetQuestions", "1", RegistryValueKind.DWord);
        }

        public static void DisableBlockPrecisionTrackpad()
        {
            Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\PrecisionTouchPad", true);
            RegistryKey myKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\PrecisionTouchPad", true);
            myKey.SetValue("AAPThreshold", "0", RegistryValueKind.DWord);
        }

        // note: "Anywhere" actually means disabled
        public static void DisableAppSuggestions()
        {
            RegistryKey myKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer", true);
            myKey.SetValue("AicEnabled", "Anywhere", RegistryValueKind.String);
        }

        public static void DisableGameDvr()
        {
            RegistryKey myKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\GameDVR", true);
            myKey.SetValue("AppCaptureEnabled", "0", RegistryValueKind.DWord);
            myKey.SetValue("AudioCaptureEnabled", "0", RegistryValueKind.DWord);
            myKey.SetValue("CursorCaptureEnabled", "0", RegistryValueKind.DWord);

            RegistryKey myKey2 = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\GameBar", true);
            myKey2.SetValue("ShowStartupPanel", "0", RegistryValueKind.DWord);
            myKey2.SetValue("UseNexusForGameBarEnabled", "0", RegistryValueKind.DWord);
            myKey2.SetValue("AllowAutoGameMode", "0", RegistryValueKind.DWord);

            RegistryKey myKey3 = Registry.CurrentUser.OpenSubKey(@"System\GameConfigStore", true);
            myKey3.SetValue("GameDVR_FSEBehavior", "2", RegistryValueKind.DWord);
            myKey3.SetValue("GameDVR_Enabled", "0", RegistryValueKind.DWord);

            RegistryKey myKey4 = Registry.LocalMachine.OpenSubKey(@"Software\Policies\Microsoft\Windows", true);
            myKey4.SetValue("AllowgameDVR", "0", RegistryValueKind.DWord);

            /* TODO: doesn't work anymore as of 1909
            RegistryKey myKey5 = Registry.LocalMachine.OpenSubKey(@"System\CurrentControlSet\Services\xbgm", true);
            myKey5.SetValue("Start", "4", RegistryValueKind.DWord);
            */

            RegistryKey myKey6 = Registry.LocalMachine.OpenSubKey(@"System\CurrentControlSet\Services\XblAuthManager", true);
            myKey6.SetValue("Start", "4", RegistryValueKind.DWord);

            RegistryKey myKey7 = Registry.LocalMachine.OpenSubKey(@"System\CurrentControlSet\Services\XblGameSave", true);
            myKey7.SetValue("Start", "4", RegistryValueKind.DWord);

            RegistryKey myKey8 = Registry.LocalMachine.OpenSubKey(@"System\CurrentControlSet\Services\XboxGipSvc", true);
            myKey8.SetValue("Start", "4", RegistryValueKind.DWord);

            RegistryKey myKey9 = Registry.LocalMachine.OpenSubKey(@"System\CurrentControlSet\Services\XboxNetApiSvc", true);
            myKey9.SetValue("Start", "4", RegistryValueKind.DWord);
        }

        public static void DisableHotspot20()
        {
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\WlanSvc\AnqpCache", true);
            RegistryKey myKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\WlanSvc\AnqpCache", true);
            myKey.SetValue("OsuRegistrationStatus", "0", RegistryValueKind.DWord);
        }

        public static void DisableCloudStates()
        {
            RegistryKey myKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", true);
            myKey.SetValue("NavPaneShowAllCloudStates", "0", RegistryValueKind.DWord);
        }

        public static void NoQuickAccess()
        {
            RegistryKey myKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer", true);
            myKey.SetValue("ShowFrequent", "0", RegistryValueKind.DWord);
        }

        public static void HideSyncNotifications()
        {
            RegistryKey myKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", true);
            myKey.SetValue("ShowSyncProviderNotifications", "0", RegistryValueKind.DWord);
        }

        public static void DisableSharingWizard()
        {
            RegistryKey myKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", true);
            myKey.SetValue("SharingWizardOn", "0", RegistryValueKind.DWord);

            using (PowerShell ps = PowerShell.Create())
            {
                ps.AddScript("New - PSDrive - Name \"HKCR\" - PSProvider \"Registry\" - Root \"HKEY_CLASSES_ROOT\"");
                ps.AddScript("Remove-Item -LiteralPath \"HKCR:\\*\\shellex\\ContextMenuHandlers\\Sharing\" -ErrorAction SilentlyContinue");
                ps.AddScript("Remove-Item -Path \"HKCR:\\Directory\\Background\\shellex\\ContextMenuHandlers\\Sharing\" -ErrorAction SilentlyContinue");
                ps.AddScript("Remove-Item -Path \"HKCR:\\Directory\\shellex\\ContextMenuHandlers\\Sharing\" -ErrorAction SilentlyContinue");
                ps.AddScript("Remove-Item -Path \"HKCR:\\Drive\\shellex\\ContextMenuHandlers\\Sharing\" -ErrorAction SilentlyContinue");
                ps.AddScript("Remove-Item -LiteralPath \"HKCR:\\*\\shellex\\ContextMenuHandlers\\ModernSharing\" -ErrorAction SilentlyContinue");
                ps.Invoke();
            }
        }

        public static void InstallNetFrameworks()
        {
            using (PowerShell ps = PowerShell.Create())
            {
                ps.AddScript("Enable-WindowsOptionalFeature -Online -FeatureName \"NetFx3\" -NoRestart -WarningAction SilentlyContinue");
                ps.Invoke();
            }
        }

        public static void UninstallXPSWriter()
        {
            using (PowerShell ps = PowerShell.Create())
            {
                ps.AddScript("Disable-WindowsOptionalFeature -Online -FeatureName \"Printing - XPSServices - Features\" -NoRestart -WarningAction SilentlyContinue");
                ps.Invoke();
            }
        }

        public static void EnablePhotoViewer()
        {
            string commands = Encoding.UTF8.GetString(Resources.photoviewer);

            using (PowerShell ps = PowerShell.Create())
            {
                ps.AddScript(commands);
                ps.Invoke();
            }
        }


        public static void ShowFilenameExtensions()
        {
            RegistryKey myKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", true);
            myKey.SetValue("HideFileExt", "0", RegistryValueKind.DWord);
        }

        public static void LaunchThisPcFileExplorer()
        {
            RegistryKey myKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", true);
            myKey.SetValue("LaunchTo", "1", RegistryValueKind.DWord);
        }

        public static void DisableInventoryCollection()
        {
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\AppCompat", true);

            RegistryKey myKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\AppCompat", true);
            myKey.SetValue("DisableInventory", "1", RegistryValueKind.DWord);
        }

        public static void DisablePreReleaseFeatures()
        {
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\PreviewBuilds", true);

            RegistryKey myKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\PreviewBuilds", true);
            myKey.SetValue("AllowBuildPreview", "0", RegistryValueKind.DWord);
        }

        public static void DisableEdgePreload()
        {
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Policies\Microsoft\MicrosoftEdge", true);
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Policies\Microsoft\MicrosoftEdge\Main", true);
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Policies\Microsoft\MicrosoftEdge\TabPreloader", true);

            RegistryKey myKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\MicrosoftEdge\Main", true);
            myKey.SetValue("AllowPrelaunch", "0", RegistryValueKind.DWord);

            RegistryKey myKey2 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\MicrosoftEdge\TabPreloader", true);
            myKey2.SetValue("AllowTabPreloading", "0", RegistryValueKind.DWord);
        }

        public static void DisableCompatibilityAssistant()
        {
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\AppCompat", true);

            RegistryKey myKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\AppCompat", true);
            myKey.SetValue("DisablePCA", "1", RegistryValueKind.DWord);
        }

        public static void DisableStepsRecorder()
        {
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\AppCompat", true);

            RegistryKey myKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\AppCompat", true);
            myKey.SetValue("DisableUAR", "1", RegistryValueKind.DWord);
        }

        public static void UseWin7Volume()
        {
            Registry.LocalMachine.CreateSubKey(@"Software\Microsoft\Windows NT\CurrentVersion\MTCUVC", true);

            RegistryKey myKey = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows NT\CurrentVersion\MTCUVC", true);
            myKey.SetValue("EnableMtcUvc", "0", RegistryValueKind.DWord);
        }

        public static void HideOneDriveFileExplorer()
        {
            Registry.ClassesRoot.CreateSubKey(@"CLSID\{018D5C66-4533-4307-9B53-224DE2ED1FE6}", true);
            RegistryKey myKey = Registry.ClassesRoot.OpenSubKey(@"CLSID\{018D5C66-4533-4307-9B53-224DE2ED1FE6}", true);
            myKey.SetValue("System.IsPinnedToNameSpaceTree", "0", RegistryValueKind.DWord);

            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\OneDrive", true);
            RegistryKey myKey2 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\OneDrive", true);
            myKey2.SetValue("DisableFileSyncNGSC", "1", RegistryValueKind.DWord);

            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\WOW6432Node\Policies\Microsoft\Windows\OneDrive", true);
            RegistryKey myKey3 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Policies\Microsoft\Windows\OneDrive", true);
            myKey3.SetValue("DisableFileSyncNGSC", "1", RegistryValueKind.DWord);
        }

        public static void DisableTelemetry()
        {
            RegistryKey myKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\DataCollection", true);
            myKey.SetValue("AllowTelemetry", "0", RegistryValueKind.DWord);

            RegistryKey myKey2 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\DataCollection", true);
            myKey2.SetValue("AllowTelemetry", "0", RegistryValueKind.DWord);

            RegistryKey myKey3 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Policies\DataCollection", true);
            myKey3.SetValue("AllowTelemetry", "0", RegistryValueKind.DWord);

            RegistryKey myKey4 = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\WMI\Autologger\AutoLogger-Diagtrack-Listener", true);
            myKey4.SetValue("Start", "0", RegistryValueKind.DWord);

            RegistryKey myKey5 = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\dmwappushservice", true);
            myKey5.SetValue("Start", "4", RegistryValueKind.DWord);

            RegistryKey myKey6 = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\DiagTrack", true);
            myKey6.SetValue("Start", "4", RegistryValueKind.DWord);

            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\AppCompat", true);

            RegistryKey myKey7 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\AppCompat", true);
            myKey7.SetValue("AITEnable", "0", RegistryValueKind.DWord);

            using (PowerShell ps = PowerShell.Create())
            {
                ps.AddScript("Disable-ScheduledTask -TaskName \"Microsoft\\Windows\\Application Experience\\Microsoft Compatibility Appraiser\"");
                ps.AddScript("Disable-ScheduledTask -TaskName \"Microsoft\\Windows\\Application Experience\\ProgramDataUpdater\"");
                ps.AddScript("Disable-ScheduledTask -TaskName \"Microsoft\\Windows\\Autochk\\Proxy\"");
                ps.AddScript("Disable-ScheduledTask -TaskName \"Microsoft\\Windows\\Customer Experience Improvement Program\\Consolidator\"");
                ps.AddScript("Disable-ScheduledTask -TaskName \"Microsoft\\Windows\\Customer Experience Improvement Program\\UsbCeip\"");
                ps.AddScript("Disable-ScheduledTask -TaskName \"Microsoft\\Windows\\DiskDiagnostic\\Microsoft-Windows-DiskDiagnosticDataCollector\"");
                ps.Invoke();
            }

        }

        public static void UninstallOneDrive()
        {
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\OneDrive");
            RegistryKey myKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\OneDrive", true);
            myKey.SetValue("PreventNetworkTrafficPreUserSignIn", "1", RegistryValueKind.DWord);

            string file1 = @"C:\Windows\SysWOW64\OneDriveSetup.exe";
            string file2 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"Local\Microsoft\OneDrive\Update\OneDriveSetup.exe";

            if (File.Exists(file1))
            {
                ProcessStartInfo cmdsi = new ProcessStartInfo(file1, "/uninstall");
                Process cmd = Process.Start(cmdsi);
                cmd.WaitForExit();
            }
            else if (File.Exists(file2))
            {
                ProcessStartInfo cmdsi = new ProcessStartInfo(file2, "/uninstall");
                Process cmd = Process.Start(cmdsi);
                cmd.WaitForExit();
            }
        }

        public static void DisableActivityHistory()
        {
            RegistryKey myKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\System", true);
            myKey.SetValue("PublishUserActivities", "0", RegistryValueKind.DWord);

            RegistryKey myKey2 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\System", true);
            myKey2.SetValue("EnableActivityFeed", "0", RegistryValueKind.DWord);

            RegistryKey myKey3 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\System", true);
            myKey3.SetValue("UploadUserActivities", "0", RegistryValueKind.DWord);
        }

        public static void DisableBackgroundApps()
        {
            RegistryKey myKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\BackgroundAccessApplications", true);
            myKey.SetValue("GlobalUserDisabled", "1", RegistryValueKind.DWord);
        }

        public static void DisableAutomaticAppInstall()
        {
            RegistryKey myKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", true);
            myKey.SetValue("SilentInstalledAppsEnabled", "0", RegistryValueKind.DWord);
            myKey.SetValue("ContentDeliveryAllowed", "0", RegistryValueKind.DWord);
            myKey.SetValue("OemPreInstalledAppsEnabled", "0", RegistryValueKind.DWord);
            myKey.SetValue("PreInstalledAppsEnabled", "0", RegistryValueKind.DWord);
            myKey.SetValue("PreInstalledAppsEverEnabled", "0", RegistryValueKind.DWord);

            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\CloudContent");
            RegistryKey myKey2 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\CloudContent", true);
            myKey2.SetValue("DisableWindowsConsumerFeatures", "1", RegistryValueKind.DWord);
        }

        public static void DisableFeedbackDialogs()
        {
            RegistryKey myKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", true);
            myKey.SetValue("SoftLandingEnabled", "0", RegistryValueKind.DWord);

            RegistryKey myKey3 = Registry.LocalMachine.OpenSubKey(@"Software\Policies\Microsoft\Windows\CloudContent", true);
            myKey3.SetValue("DisableSoftLanding", "1", RegistryValueKind.DWord);

            Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Siuf\Rules");
            RegistryKey myKey2 = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Siuf\Rules", true);
            myKey2.SetValue("NumberOfSIUFInPeriod", "0", RegistryValueKind.DWord);
            myKey2.SetValue("PeriodInNanoSeconds", "0", RegistryValueKind.DWord);

            RegistryKey myKey4 = Registry.LocalMachine.OpenSubKey(@"Software\Policies\Microsoft\Windows\DataCollection", true);
            myKey4.SetValue("DoNotShowFeedbackNotifications", "1", RegistryValueKind.DWord);

            using (PowerShell ps = PowerShell.Create())
            {
                ps.AddScript("Disable-ScheduledTask -TaskName \"Microsoft\\Windows\\Feedback\\Siuf\\DmClient\"");
                ps.AddScript("Disable-ScheduledTask -TaskName \"Microsoft\\Windows\\Feedback\\Siuf\\DmClientOnScenarioDownload\"");
                ps.Invoke();
            }
        }

        public static void DisableStartMenuSuggestions()
        {
            RegistryKey myKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", true);
            myKey.SetValue("SystemPaneSuggestionsEnabled", "0", RegistryValueKind.DWord);
            myKey.SetValue("SubscribedContent-338389Enabled", "0", RegistryValueKind.DWord);
            myKey.SetValue("SubscribedContent-338388Enabled", "0", RegistryValueKind.DWord);
        }

        public static void DisableTeredo()
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C netsh interface teredo set state disabled";
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();
        }

        public static void DisableISATAP()
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C netsh interface isatap set state disabled";
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();
        }

        public static void DisableBingSearch()
        {
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\Windows Search");
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\WOW6432Node\Policies\Microsoft\Windows\Windows Search");

            RegistryKey myKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Search", true);
            myKey.SetValue("BingSearchEnabled", "0", RegistryValueKind.DWord);
            myKey.SetValue("CortanaConsent", "0", RegistryValueKind.DWord);
            myKey.SetValue("DisableWebSearch", "1", RegistryValueKind.DWord);

            RegistryKey myKey4 = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Search", true);
            myKey4.SetValue("BingSearchEnabled", "0", RegistryValueKind.DWord);
            myKey4.SetValue("CortanaConsent", "0", RegistryValueKind.DWord);
            myKey4.SetValue("DisableWebSearch", "1", RegistryValueKind.DWord);

            RegistryKey myKey2 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Policies\Microsoft\Windows\Windows Search", true);
            myKey2.SetValue("DisableWebSearch", "1", RegistryValueKind.DWord);
            myKey2.SetValue("ConnectedSearchUseWeb", "0", RegistryValueKind.DWord);
            myKey2.SetValue("AllowCloudSearch", "0", RegistryValueKind.DWord);

            RegistryKey myKey3 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\Windows Search", true);
            myKey3.SetValue("DisableWebSearch", "1", RegistryValueKind.DWord);
            myKey3.SetValue("ConnectedSearchUseWeb", "0", RegistryValueKind.DWord);
            myKey3.SetValue("AllowCloudSearch", "0", RegistryValueKind.DWord);

        }

        public static void DisablePasswordReveal()
        {
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\CredUI", true);
            RegistryKey myKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\CredUI", true);
            myKey.SetValue("DisablePasswordReveal", "1", RegistryValueKind.DWord);

            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\WOW6432Node\Policies\Microsoft\Windows\CredUI", true);
            RegistryKey myKey2 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Policies\Microsoft\Windows\CredUI", true);
            myKey2.SetValue("DisablePasswordReveal", "1", RegistryValueKind.DWord);
        }

        public static void DisableSettingsSync()
        {
            Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Accessibility", true);
            RegistryKey myKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Accessibility", true);
            myKey.SetValue("Enabled", "0", RegistryValueKind.DWord);

            Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\SettingSync\Groups\BrowserSettings", true);
            RegistryKey myKey2 = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\SettingSync\Groups\BrowserSettings", true);
            myKey2.SetValue("Enabled", "0", RegistryValueKind.DWord);

            Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Credentials", true);
            RegistryKey myKey3 = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Credentials", true);
            myKey3.SetValue("Enabled", "0", RegistryValueKind.DWord);

            Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Language", true);
            RegistryKey myKey4 = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Language", true);
            myKey4.SetValue("Enabled", "0", RegistryValueKind.DWord);

            Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Personalization", true);
            RegistryKey myKey5 = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Personalization", true);
            myKey5.SetValue("Enabled", "0", RegistryValueKind.DWord);

            Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Windows", true);
            RegistryKey myKey6 = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Windows", true);
            myKey6.SetValue("Enabled", "0", RegistryValueKind.DWord);

            Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\SettingSync\Groups\AppSync", true);
            RegistryKey myKey7 = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\SettingSync\Groups\AppSync", true);
            myKey7.SetValue("Enabled", "0", RegistryValueKind.DWord);

            RegistryKey myKey8 = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\SettingSync", true);
            myKey8.SetValue("SyncPolicy", "5", RegistryValueKind.DWord);
        }

        public static void DisableStartupSound()
        {
            RegistryKey myKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Authentication\LogonUI\BootAnimation", true);
            myKey.SetValue("DisableStartupSound", "1", RegistryValueKind.DWord);
        }

        public static void DisableAutostartStartupDelay()
        {
            Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Serialize");
            RegistryKey myKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Serialize", true);
            myKey.SetValue("Startupdelayinmsec", "0", RegistryValueKind.DWord);
        }

        public static void DisableLocation()
        {
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\LocationAndSensors");

            RegistryKey myKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\LocationAndSensors", true);
            myKey.SetValue("DisableLocation", "1", RegistryValueKind.DWord);
            myKey.SetValue("DisableLocationScripting", "1", RegistryValueKind.DWord);
            myKey.SetValue("DisableWindowsLocationProvider", "1", RegistryValueKind.DWord);

            RegistryKey myKey2 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Policies\Microsoft\Windows\LocationAndSensors", true);
            myKey2.SetValue("DisableLocation", "1", RegistryValueKind.DWord);
            myKey2.SetValue("DisableLocationScripting", "1", RegistryValueKind.DWord);
            myKey2.SetValue("DisableWindowsLocationProvider", "1", RegistryValueKind.DWord);

            RegistryKey myKey3 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\Windows Search", true);
            myKey3.SetValue("AllowSearchToUseLocation", "0", RegistryValueKind.DWord);

            RegistryKey myKey4 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Policies\Microsoft\Windows\Windows Search", true);
            myKey4.SetValue("AllowSearchToUseLocation", "0", RegistryValueKind.DWord);

            RegistryKey myKey5 = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\lfsvc", true);
            myKey5.SetValue("Start", "4", RegistryValueKind.DWord);

            RegistryKey myKey6 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Sensor\Overrides\{BFA794E4-F964-4FDB-90F6-51056BFE4B44}", true);
            myKey6.SetValue("SensorPermissionState", "0", RegistryValueKind.DWord);
        }

        public static void DisableDefender()
        {
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows Defender\Spynet");
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\WOW6432Node\Policies\Microsoft\Windows Defender\Spynet");

            RegistryKey myKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows Defender", true);
            myKey.SetValue("DisableAntiSpyware", "1", RegistryValueKind.DWord);

            RegistryKey myKey2 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows Defender\Spynet", true);
            myKey2.SetValue("SpyNetReporting", "0", RegistryValueKind.DWord);
            myKey2.SetValue("SubmitSamplesConsent", "2", RegistryValueKind.DWord);

            RegistryKey myKey3 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Policies\Microsoft\Windows Defender", true);
            myKey3.SetValue("DisableAntiSpyware", "1", RegistryValueKind.DWord);

            RegistryKey myKey4 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Policies\Microsoft\Windows Defender\Spynet", true);
            myKey4.SetValue("SpyNetReporting", "0", RegistryValueKind.DWord);
            myKey4.SetValue("SubmitSamplesConsent", "2", RegistryValueKind.DWord);

            RegistryKey myKey5 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            myKey5.DeleteValue("SecurityHealth", false);
        }

        public static void DisableAdvertisingId()
        {
            Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\AdvertisingInfo");
            RegistryKey myKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\AdvertisingInfo", true);
            myKey.SetValue("Enabled", "0", RegistryValueKind.DWord);

            Registry.LocalMachine.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\AdvertisingInfo");
            RegistryKey myKey2 = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\AdvertisingInfo", true);
            myKey2.SetValue("Enabled", "0", RegistryValueKind.DWord);
        }

        public static void DisableMrtReporting()
        {
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Policies\Microsoft\MRT");
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\WOW6432Node\Policies\Microsoft\MRT");

            RegistryKey myKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\MRT", true);
            myKey.SetValue("DontReportInfectionInformation", "1", RegistryValueKind.DWord);

            RegistryKey myKey2 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Policies\Microsoft\MRT", true);
            myKey2.SetValue("DontReportInfectionInformation", "1", RegistryValueKind.DWord);
        }

        public static void DisableSendingTypingInfo()
        {
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\TabletPC");
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\WOW6432Node\Policies\Microsoft\Windows\TabletPC");

            RegistryKey myKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Input\TIPC", true);
            myKey.SetValue("Enabled", "0", RegistryValueKind.DWord);

            RegistryKey myKey2 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\TabletPC", true);
            myKey2.SetValue("PreventHandwritingDataSharing", "1", RegistryValueKind.DWord);

            RegistryKey myKey3 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Policies\Microsoft\Windows\TabletPC", true);
            myKey3.SetValue("PreventHandwritingDataSharing", "1", RegistryValueKind.DWord);
        }

        public static void DeleteQuicklaunchItems()
        {
            byte[] data = new byte[] { 0xFF };

            RegistryKey myKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Taskband", true);
            myKey.SetValue("Favorites", data, RegistryValueKind.Binary);
            myKey.DeleteValue("FavoritesResolve", false);
        }

        public static void DisablePersonalization()
        {
            RegistryKey myKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Personalization\Settings", true);
            myKey.SetValue("AcceptedPrivacyPolicy", "0", RegistryValueKind.DWord);

            Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\InputPersonalization", true);
            RegistryKey myKey2 = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\InputPersonalization", true);
            myKey2.SetValue("RestrictImplicitTextCollection", "1", RegistryValueKind.DWord);
            myKey2.SetValue("RestrictImplicitInkCollection", "1", RegistryValueKind.DWord);
            myKey2.SetValue("HarvestContacts", "0", RegistryValueKind.DWord);
        }

        public static void HideLanguageListWebsites()
        {
            Registry.CurrentUser.CreateSubKey(@"Control Panel\International\User Profile");

            RegistryKey myKey = Registry.CurrentUser.OpenSubKey(@"Control Panel\International\User Profile", true);
            myKey.SetValue("HttpAcceptLanguageOptOut", "1", RegistryValueKind.DWord);
        }

        public static void DisableMiracast()
        {
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\MiracastReceiver");

            RegistryKey myKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\MiracastReceiver", true);
            myKey.SetValue("ConsentToast", "2", RegistryValueKind.DWord);
        }

        public static void DisableBluetoothAdvertisements()
        {
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\PolicyManager\current\device\Bluetooth");

            RegistryKey myKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\PolicyManager\current\device\Bluetooth", true);
            myKey.SetValue("AllowAdvertising", "0", RegistryValueKind.DWord);
        }

        public static void DisableClipboardHistory()
        {
            RegistryKey myKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\System", true);
            myKey.SetValue("AllowClipboardHistory", "0", RegistryValueKind.DWord);

            RegistryKey myKey2 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Policies\Microsoft\Windows\System", true);
            myKey2.SetValue("AllowClipboardHistory", "0", RegistryValueKind.DWord);
        }

        public static void DisableClipboardCloudSync()
        {
            RegistryKey myKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\System", true);
            myKey.SetValue("AllowCrossDeviceClipboard", "0", RegistryValueKind.DWord);

            RegistryKey myKey2 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Policies\Microsoft\Windows\System", true);
            myKey2.SetValue("AllowCrossDeviceClipboard", "0", RegistryValueKind.DWord);
        }

        public static void DisableAutomaticSpeechDataUpdates()
        {
            RegistryKey myKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Speech_OneCore\Preferences", true);
            myKey.SetValue("ModelDownloadAllowed", "0", RegistryValueKind.DWord);

            RegistryKey myKey2 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Speech", true);
            myKey2.SetValue("AllowSpeechModelUpdate", "0", RegistryValueKind.DWord);
        }
        public static void DisableTextMessagesCloudSync()
        {
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\Messaging");
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\WOW6432Node\Policies\Microsoft\Windows\Messaging");

            RegistryKey myKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\Messaging", true);
            myKey.SetValue("AllowMessageSync", "0", RegistryValueKind.DWord);

            RegistryKey myKey2 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Policies\Microsoft\Windows\Messaging", true);
            myKey2.SetValue("AllowMessageSync", "0", RegistryValueKind.DWord);
        }
        public static void DisableHandwritingErrorReports()
        {
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\HandwritingErrorReports");
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\WOW6432Node\Policies\Microsoft\Windows\HandwritingErrorReports");

            RegistryKey myKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\HandwritingErrorReports", true);
            myKey.SetValue("PreventHandwritingErrorReports", "1", RegistryValueKind.DWord);

            RegistryKey myKey2 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Policies\Microsoft\Windows\HandwritingErrorReports", true);
            myKey2.SetValue("PreventHandwritingErrorReports", "1", RegistryValueKind.DWord);
        }

        public static void DisableAppDiagnostics()
        {
            RegistryKey myKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Privacy", true);
            myKey.SetValue("TailoredExperiencesWithDiagnosticDataEnabled", "0", RegistryValueKind.DWord);

            RegistryKey myKey2 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Privacy", true);
            myKey2.SetValue("TailoredExperiencesWithDiagnosticDataEnabled", "0", RegistryValueKind.DWord);
        }

        public static void DisableSmartScreen()
        {
            RegistryKey myKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\AppHost", true);
            myKey.SetValue("EnableWebContentEvaluation", "0", RegistryValueKind.DWord);

            RegistryKey myKey2 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer", true);
            myKey2.SetValue("SmartScreenEnabled", "Off", RegistryValueKind.String);

            RegistryKey myKey3 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\System", true);
            myKey3.SetValue("EnableSmartScreen", "0", RegistryValueKind.DWord);
        }

        public static void DisableEdgeFirstRunPage()
        {
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Policies\Microsoft\MicrosoftEdge");
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Policies\Microsoft\MicrosoftEdge\Main");

            RegistryKey myKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\MicrosoftEdge\Main", true);
            myKey.SetValue("PreventFirstRunPage", "1", RegistryValueKind.DWord);
        }

        public static void DisableSmartGlass()
        {
            RegistryKey myKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\SmartGlass", true);
            myKey.SetValue("UserAuthPolicy", "0", RegistryValueKind.DWord);
        }

        public static void DisableInkAppSuggestions()
        {
            RegistryKey myKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\SmartGlass", true);
            myKey.SetValue("PenWorkspaceAppSuggestionsEnabled", "0", RegistryValueKind.DWord);
        }

        public static void DisableExperiments()
        {
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\PolicyManager\current\device\System");

            RegistryKey myKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\PolicyManager\current\device\System", true);
            myKey.SetValue("AllowExperimentation", "0", RegistryValueKind.DWord);
        }

        public static void DisableCameraLockScreen()
        {
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\Personalization");

            RegistryKey myKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\Personalization", true);
            myKey.SetValue("NoLockScreenCamera", "1", RegistryValueKind.DWord);
        }

        public static void DisableApplicationCompatibilityEngine()
        {
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\AppCompat", true);

            RegistryKey myKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\AppCompat", true);
            myKey.SetValue("DisableEngine", "1", RegistryValueKind.DWord);
        }

        public static void DisableWiFiSense()
        {
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\PolicyManager\default\WiFi\AllowWiFiHotSpotReporting");

            RegistryKey myKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\PolicyManager\default\WiFi\AllowWiFiHotSpotReporting", true);
            myKey.SetValue("Value", "0", RegistryValueKind.DWord);

            RegistryKey myKey2 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\PolicyManager\default\WiFi\AllowAutoConnectToWiFiSenseHotspots", true);
            myKey2.SetValue("Value", "0", RegistryValueKind.DWord);
            
            RegistryKey myKey3 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\WcmSvc\wifinetworkmanager\config", true);
            myKey3.SetValue("AutoConnectAllowedOEM", "0", RegistryValueKind.DWord);
        }

        public static void DisableLockScreenSpotlight()
        {
            RegistryKey myKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", true);
            myKey.SetValue("RotatingLockScreenEnabled", "0", RegistryValueKind.DWord);
            myKey.SetValue("RotatingLockScreenOverlayEnabled", "0", RegistryValueKind.DWord);
            myKey.SetValue("SubscribedContent-338387Enabled", "0", RegistryValueKind.DWord);

            RegistryKey myKey2 = Registry.LocalMachine.OpenSubKey(@"Software\Policies\Microsoft\Windows\CloudContent", true);
            myKey2.SetValue("DisableWindowsSpotlightFeatures", "1", RegistryValueKind.DWord);
        }

        public static void DisableAutomaticMapsUpdates()
        {
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\Maps");
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\WOW6432Node\Policies\Microsoft\Windows\Maps");

            RegistryKey myKey = Registry.LocalMachine.OpenSubKey(@"SYSTEM\Maps", true);
            myKey.SetValue("AutoUpdateEnabled", "0", RegistryValueKind.DWord);

            RegistryKey myKey2 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\Maps", true);
            myKey2.SetValue("AutoDownloadAndUpdateMapData", "0", RegistryValueKind.DWord);

            RegistryKey myKey3 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Policies\Microsoft\Windows\Maps", true);
            myKey3.SetValue("AutoDownloadAndUpdateMapData", "0", RegistryValueKind.DWord);
        }

        public static void DisableErrorReporting()
        {
            RegistryKey myKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\Windows Error Reporting", true);
            myKey.SetValue("Disabled", "1", RegistryValueKind.DWord);

            using (PowerShell ps = PowerShell.Create())
            {
                ps.AddScript("Disable-ScheduledTask -TaskName \"Microsoft\\Windows\\Windows Error Reporting\\QueueReporting\"");
                ps.Invoke();
            }
        }

        public static void DisableRemoteAssistance()
        {
            RegistryKey myKey = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Remote Assistance", true);
            myKey.SetValue("fAllowToGetHelp", "0", RegistryValueKind.DWord);
        }

        public static void UseUtcAsBiosTime()
        {
            RegistryKey myKey = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\TimeZoneInformation", true);
            myKey.SetValue("RealTimeIsUniversal", "1", RegistryValueKind.DWord);
        }

        public static void HideNetworkFromLockScreen()
        {
            RegistryKey myKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\System", true);
            myKey.SetValue("DontDisplayNetworkSelectionUI", "1", RegistryValueKind.DWord);
        }

        public static void DisableStickyKeysPrompt()
        {
            RegistryKey myKey = Registry.CurrentUser.OpenSubKey(@"Control Panel\Accessibility\StickyKeys", true);
            myKey.SetValue("Flags", "506", RegistryValueKind.String);
        }

        public static void AddThisPCShortcut()
        {
            Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\HideDesktopIcons\ClassicStartMenu");
            Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\HideDesktopIcons\NewStartPanel");

            RegistryKey myKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\HideDesktopIcons\ClassicStartMenu", true);
            myKey.SetValue("{20D04FE0-3AEA-1069-A2D8-08002B30309D}", "0", RegistryValueKind.DWord);

            RegistryKey myKey2 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\HideDesktopIcons\NewStartPanel", true);
            myKey2.SetValue("{20D04FE0-3AEA-1069-A2D8-08002B30309D}", "0", RegistryValueKind.DWord);
        }

        public static void RemovePreinstalledApps()
        {
            string commands = Encoding.UTF8.GetString(Resources.delapps);

            using (PowerShell ps = PowerShell.Create())
            {
                ps.AddScript(commands);
                ps.Invoke();
            }

            string tempfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\WinSlap\";

            Directory.CreateDirectory(tempfolder);
            File.WriteAllBytes(tempfolder + "startlayout.reg", Resources.startlayout);

            RegistryKey ourKey = Registry.LocalMachine;
            ourKey = ourKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", true);
            ourKey.SetValue("AutoRestartShell", 0);
            
            ourKey.Close();


            var processes = Process.GetProcessesByName("explorer");
            foreach (var process in processes)
            {
                process.Kill();
                process.WaitForExit();
            }

            Process regeditProcess = Process.Start("regedit.exe", "/s " + tempfolder + "startlayout.reg");
            regeditProcess.WaitForExit();

            Process.Start("explorer");

            RegistryKey ourKey2 = Registry.LocalMachine;
            ourKey2 = ourKey2.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", true);
            ourKey2.SetValue("AutoRestartShell", 0);
            ourKey2.Close();
        }

        public static void PreventPreinstallingApps()
        {
            string commands = Encoding.UTF8.GetString(Resources.preventpreinstallingapps);

            using (PowerShell ps = PowerShell.Create())
            {
                ps.AddScript(commands);
                ps.Invoke();
            }
        }

        public static void UnpinPreinstalledApps()
        {
            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\WinSlap\");
            File.WriteAllBytes(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\WinSlap\" + "StartMenuLayout.xml", Resources.StartMenuLayout);

            using (PowerShell ps = PowerShell.Create())
            {
                ps.AddScript(@"Import-StartLayout -LayoutPath '" + Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\WinSlap\" + @"StartMenuLayout.xml'" + @" -MountPath C:\ -verbose");
                ps.Invoke();
            }
        }

        public static void Hide3DObjectsFileExplorer()
        {
                Registry.LocalMachine.DeleteSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\MyComputer\NameSpace\{0DB7E03F-FC29-4DC6-9020-FF41B59E513A}", false);
        }

        public static void InstallFirefox()
        {
            DownloadRun("https://download.mozilla.org/?product=firefox-latest&os=win64&lang=en-US", "Firefox_Setup.exe", "/S");
        }

        public static void InstallThunderbird()
        {
            DownloadRun("https://download.mozilla.org/?product=thunderbird-latest&os=win&lang=en-US", "Thunderbird_Setup.exe", "/S");
        }

        public static void InstallVlc()
        {
            string url = GetLatestFtp("ftp://ftp.halifax.rwth-aachen.de/videolan/vlc/last/win32/", @"win32.exe$");
            DownloadRun(url, "VLC_Setup.exe", "/L=1033 /S");
        }

        public static void InstallTelegram()
        {
            DownloadRun("https://tdesktop.com/win", "Telegram_Setup.exe", "/SP- /VERYSILENT");
        }

        public static void InstallStartIsBack()
        {
            DownloadRun("https://s3.amazonaws.com/startisback/StartIsBackPlusPlus_setup.exe", "StartIsBack_Setup.exe", "/elevated /silent");
        }

        public static void RemoveIntelContextMenu()
        {
            Registry.ClassesRoot.DeleteSubKey(@"Directory\Background\shellex\ContextMenuHandlers\igfxui", false);
            Registry.ClassesRoot.DeleteSubKey(@"Directory\Background\shellex\ContextMenuHandlers\igfxcui", false);
            Registry.ClassesRoot.DeleteSubKey(@"Directory\Background\shellex\ContextMenuHandlers\igfxDTCM", false);
        }

        public static void RemoveNvidiaContextMenu()
        {
            Registry.ClassesRoot.DeleteSubKey(@"Directory\Background\shellex\ContextMenuHandlers\NvCplDesktopContext", false);
        }

        public static void RemoveAmdContextMenu()
        {
            Registry.ClassesRoot.DeleteSubKey(@"Directory\Background\shellex\ContextMenuHandlers\ACE", false);
        }

        public static void RemoveEdgeShortcut()
        {
            string desktoppath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string edgepath = desktoppath + @"\Microsoft Edge.lnk";
            File.Delete(edgepath);
        }

        public static void RemoveFaxPrinter()
        {
            using (PowerShell ps = PowerShell.Create())
            {
                ps.AddScript("Remove-Printer -Name \"Fax\" -ErrorAction SilentlyContinue");
                ps.Invoke();
            }
        }

        public static void RemoveXPSDocumentWriter()
        {
            using (PowerShell ps = PowerShell.Create())
            {
                ps.AddScript("Disable-WindowsOptionalFeature -Online -FeatureName \"Printing-XPSServices-Features\" -NoRestart -WarningAction SilentlyContinue | Out-Null");
                ps.Invoke();
            }
        }
    }
}
