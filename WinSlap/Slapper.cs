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

        public static void HideTaskview()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", true);
            key.SetValue("ShowTaskViewButton", "0", RegistryValueKind.DWord);
        }

        public static void DoNotGroupTasks()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", true);
            key.SetValue("TaskbarGlomLevel", "2", RegistryValueKind.DWord);
        }

        public static void TaskbarSmallIcons()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", true);
            key.SetValue("TaskbarSmallIcons", "1", RegistryValueKind.DWord);
        }

        public static void DisablePeopleBand()
        {
            Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced\People", true);
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced\People", true);
            key.SetValue("PeopleBand", "0", RegistryValueKind.DWord);
        }

        public static void HideSearch()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Search", true);
            key.SetValue("SearchboxTaskbarMode", "0", RegistryValueKind.DWord);
        }

        public static void HideMeetNow()
        {
            Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer", true);
            Registry.LocalMachine.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer", true);
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer", true);
            RegistryKey key2 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Explorer", true);
            key.SetValue("HideSCAMeetNow", "1", RegistryValueKind.DWord);
            key2.SetValue("HideSCAMeetNow", "1", RegistryValueKind.DWord);
        }

        /*
        // TODO: does not seem to work!
        public static void UpdateMSProducts()
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\WindowsUpdate\Services\7971F918-A847-4430-9279-4A52D1EFE18D", true);
            key.SetValue("RegisteredWithAU", "1", RegistryValueKind.DWord);
        }

        */

        // TODO: not tested
        public static void DisableSharedExperiences()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\CDP", true);
            key.SetValue("CdpSessionUserAuthzPolicy", "0", RegistryValueKind.DWord);
            key.SetValue("RomeSdkChannelUserAuthzPolicy", "0", RegistryValueKind.DWord);
        }

        public static void RemoveCompatibility()
        {
                Registry.ClassesRoot.DeleteSubKey(@"exefile\shellex\ContextMenuHandlers\Compatibility", false);
        }

        public static void DisableCortana()
        {
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\Windows Search", true);
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\WOW6432Node\Policies\Microsoft\Windows\Windows Search", true);

            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\Windows Search", true);
            key.SetValue("AllowCortana", "0", RegistryValueKind.DWord);

            RegistryKey key2 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Policies\Microsoft\Windows\Windows Search", true);
            key2.SetValue("AllowCortana", "0", RegistryValueKind.DWord);
        }

        public static void DisableLLMNR()
        {
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows NT\DNSClient", true);

            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows NT\DNSClient", true);
            key.SetValue("EnableMulticast", "0", RegistryValueKind.DWord);
        }

        public static void DisableSmartNameResolution()
        {
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows NT\DNSClient", true);

            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows NT\DNSClient", true);
            key.SetValue("DisableSmartNameResolution", "1", RegistryValueKind.DWord);

            RegistryKey key2 = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\Dnscache\Parameters", true);
            key2.SetValue("DisableParallelAandAAAA", "1", RegistryValueKind.DWord);
        }

        public static void DisableWPAD()
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\Tcpip\Parameters", true);
            key.SetValue("UseDomainNameDevolution", "0", RegistryValueKind.DWord);
        }

        public static void DisableLockscreenBlur()
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\System", true);
            key.SetValue("DisableAcrylicBackgroundOnLogon", "1", RegistryValueKind.DWord);
        }

        public static void DisableSecurityQuestions()
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\System", true);
            key.SetValue("NoLocalPasswordResetQuestions", "1", RegistryValueKind.DWord);
        }

        public static void DisableBlockPrecisionTrackpad()
        {
            Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\PrecisionTouchPad", true);
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\PrecisionTouchPad", true);
            key.SetValue("AAPThreshold", "0", RegistryValueKind.DWord);
        }

        // note: "Anywhere" actually means disabled
        public static void DisableAppSuggestions()
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer", true);
            key.SetValue("AicEnabled", "Anywhere", RegistryValueKind.String);
        }

        public static void DisableGameDvr()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\GameDVR", true);
            key.SetValue("AppCaptureEnabled", "0", RegistryValueKind.DWord);
            key.SetValue("AudioCaptureEnabled", "0", RegistryValueKind.DWord);
            key.SetValue("CursorCaptureEnabled", "0", RegistryValueKind.DWord);

            RegistryKey key2 = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\GameBar", true);
            key2.SetValue("ShowStartupPanel", "0", RegistryValueKind.DWord);
            key2.SetValue("UseNexusForGameBarEnabled", "0", RegistryValueKind.DWord);
            key2.SetValue("AllowAutoGameMode", "0", RegistryValueKind.DWord);

            RegistryKey key3 = Registry.CurrentUser.OpenSubKey(@"System\GameConfigStore", true);
            key3.SetValue("GameDVR_FSEBehavior", "2", RegistryValueKind.DWord);
            key3.SetValue("GameDVR_Enabled", "0", RegistryValueKind.DWord);

            RegistryKey key4 = Registry.LocalMachine.OpenSubKey(@"Software\Policies\Microsoft\Windows", true);
            key4.SetValue("AllowgameDVR", "0", RegistryValueKind.DWord);

            /* TODO: doesn't work anymore as of 1909
            RegistryKey key5 = Registry.LocalMachine.OpenSubKey(@"System\CurrentControlSet\Services\xbgm", true);
            key5.SetValue("Start", "4", RegistryValueKind.DWord);
            */

            RegistryKey key6 = Registry.LocalMachine.OpenSubKey(@"System\CurrentControlSet\Services\XblAuthManager", true);
            key6.SetValue("Start", "4", RegistryValueKind.DWord);

            RegistryKey key7 = Registry.LocalMachine.OpenSubKey(@"System\CurrentControlSet\Services\XblGameSave", true);
            key7.SetValue("Start", "4", RegistryValueKind.DWord);

            RegistryKey key8 = Registry.LocalMachine.OpenSubKey(@"System\CurrentControlSet\Services\XboxGipSvc", true);
            key8.SetValue("Start", "4", RegistryValueKind.DWord);

            RegistryKey key9 = Registry.LocalMachine.OpenSubKey(@"System\CurrentControlSet\Services\XboxNetApiSvc", true);
            key9.SetValue("Start", "4", RegistryValueKind.DWord);
        }

        public static void DisableHotspot20()
        {
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\WlanSvc\AnqpCache", true);
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\WlanSvc\AnqpCache", true);
            key.SetValue("OsuRegistrationStatus", "0", RegistryValueKind.DWord);
        }

        public static void DisableCloudStates()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", true);
            key.SetValue("NavPaneShowAllCloudStates", "0", RegistryValueKind.DWord);
        }

        public static void NoQuickAccess()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer", true);
            key.SetValue("ShowFrequent", "0", RegistryValueKind.DWord);
        }

        public static void HideSyncNotifications()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", true);
            key.SetValue("ShowSyncProviderNotifications", "0", RegistryValueKind.DWord);
        }

        public static void DisableSharingWizard()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", true);
            key.SetValue("SharingWizardOn", "0", RegistryValueKind.DWord);

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
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", true);
            key.SetValue("HideFileExt", "0", RegistryValueKind.DWord);
        }

        public static void LaunchThisPcFileExplorer()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", true);
            key.SetValue("LaunchTo", "1", RegistryValueKind.DWord);
        }

        public static void DisableInventoryCollection()
        {
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\AppCompat", true);

            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\AppCompat", true);
            key.SetValue("DisableInventory", "1", RegistryValueKind.DWord);
        }

        public static void DisablePreReleaseFeatures()
        {
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\PreviewBuilds", true);

            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\PreviewBuilds", true);
            key.SetValue("AllowBuildPreview", "0", RegistryValueKind.DWord);
        }

        public static void DisableEdgePreload()
        {
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Policies\Microsoft\MicrosoftEdge", true);
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Policies\Microsoft\MicrosoftEdge\Main", true);
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Policies\Microsoft\MicrosoftEdge\TabPreloader", true);

            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\MicrosoftEdge\Main", true);
            key.SetValue("AllowPrelaunch", "0", RegistryValueKind.DWord);

            RegistryKey key2 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\MicrosoftEdge\TabPreloader", true);
            key2.SetValue("AllowTabPreloading", "0", RegistryValueKind.DWord);
        }

        public static void DisableCompatibilityAssistant()
        {
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\AppCompat", true);

            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\AppCompat", true);
            key.SetValue("DisablePCA", "1", RegistryValueKind.DWord);
        }

        public static void DisableDRMInternetAccess()
        {
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Policies\Microsoft\WMDRM", true);

            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\WMDRM", true);
            key.SetValue("DisableOnline", "1", RegistryValueKind.DWord);
        }

        public static void DisableStepsRecorder()
        {
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\AppCompat", true);

            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\AppCompat", true);
            key.SetValue("DisableUAR", "1", RegistryValueKind.DWord);
        }

        public static void UseWin7Volume()
        {
            Registry.LocalMachine.CreateSubKey(@"Software\Microsoft\Windows NT\CurrentVersion\MTCUVC", true);

            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows NT\CurrentVersion\MTCUVC", true);
            key.SetValue("EnableMtcUvc", "0", RegistryValueKind.DWord);
        }

        public static void SetPowerPlanHighPerformance()
        {
            Registry.LocalMachine.CreateSubKey(@"Software\Policies\Microsoft\Power\PowerSettings", true);

            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"Software\Policies\Microsoft\Power\PowerSettings", true);
            key.SetValue("ActivePowerScheme", "8c5e7fda-e8bf-4a96-9a85-a6e23a8c635c", RegistryValueKind.String);
        }

        public static void DisableGetEvenMoreOutOfWindows()
        {
            Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\UserProfileEngagement", true);

            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\UserProfileEngagement", true);
            key.SetValue("ScoobeSystemSettingEnabled", "0", RegistryValueKind.DWord);
        }

        public static void HideOneDriveFileExplorer()
        {
            Registry.ClassesRoot.CreateSubKey(@"CLSID\{018D5C66-4533-4307-9B53-224DE2ED1FE6}", true);
            RegistryKey key = Registry.ClassesRoot.OpenSubKey(@"CLSID\{018D5C66-4533-4307-9B53-224DE2ED1FE6}", true);
            key.SetValue("System.IsPinnedToNameSpaceTree", "0", RegistryValueKind.DWord);

            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\OneDrive", true);
            RegistryKey key2 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\OneDrive", true);
            key2.SetValue("DisableFileSyncNGSC", "1", RegistryValueKind.DWord);

            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\WOW6432Node\Policies\Microsoft\Windows\OneDrive", true);
            RegistryKey key3 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Policies\Microsoft\Windows\OneDrive", true);
            key3.SetValue("DisableFileSyncNGSC", "1", RegistryValueKind.DWord);
        }

        public static void DisableTelemetry()
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\DataCollection", true);
            key.SetValue("AllowTelemetry", "0", RegistryValueKind.DWord);

            RegistryKey key2 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\DataCollection", true);
            key2.SetValue("AllowTelemetry", "0", RegistryValueKind.DWord);

            RegistryKey key3 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Policies\DataCollection", true);
            key3.SetValue("AllowTelemetry", "0", RegistryValueKind.DWord);

            Registry.LocalMachine.CreateSubKey(@"SYSTEM\CurrentControlSet\Control\WMI\Autologger\AutoLogger-Diagtrack-Listener");
            RegistryKey key4 = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\WMI\Autologger\AutoLogger-Diagtrack-Listener", true);
            key4.SetValue("Start", "0", RegistryValueKind.DWord);

            RegistryKey key5 = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\dmwappushservice", true);
            key5.SetValue("Start", "4", RegistryValueKind.DWord);

            RegistryKey key6 = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\DiagTrack", true);
            key6.SetValue("Start", "4", RegistryValueKind.DWord);

            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\AppCompat", true);

            RegistryKey key7 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\AppCompat", true);
            key7.SetValue("AITEnable", "0", RegistryValueKind.DWord);

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
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\OneDrive", true);
            key.SetValue("PreventNetworkTrafficPreUserSignIn", "1", RegistryValueKind.DWord);

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
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\System", true);
            key.SetValue("PublishUserActivities", "0", RegistryValueKind.DWord);

            RegistryKey key2 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\System", true);
            key2.SetValue("EnableActivityFeed", "0", RegistryValueKind.DWord);

            RegistryKey key3 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\System", true);
            key3.SetValue("UploadUserActivities", "0", RegistryValueKind.DWord);
        }

        public static void DisableBackgroundApps()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\BackgroundAccessApplications", true);
            key.SetValue("GlobalUserDisabled", "1", RegistryValueKind.DWord);
        }

        public static void DisableAutomaticAppInstall()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", true);
            key.SetValue("SilentInstalledAppsEnabled", "0", RegistryValueKind.DWord);
            key.SetValue("ContentDeliveryAllowed", "0", RegistryValueKind.DWord);
            key.SetValue("OemPreInstalledAppsEnabled", "0", RegistryValueKind.DWord);
            key.SetValue("PreInstalledAppsEnabled", "0", RegistryValueKind.DWord);
            key.SetValue("PreInstalledAppsEverEnabled", "0", RegistryValueKind.DWord);

            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\CloudContent");
            RegistryKey key2 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\CloudContent", true);
            key2.SetValue("DisableWindowsConsumerFeatures", "1", RegistryValueKind.DWord);
        }

        public static void DisableFeedbackDialogs()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", true);
            key.SetValue("SoftLandingEnabled", "0", RegistryValueKind.DWord);

            RegistryKey key3 = Registry.LocalMachine.OpenSubKey(@"Software\Policies\Microsoft\Windows\CloudContent", true);
            key3.SetValue("DisableSoftLanding", "1", RegistryValueKind.DWord);

            Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Siuf\Rules");
            RegistryKey key2 = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Siuf\Rules", true);
            key2.SetValue("NumberOfSIUFInPeriod", "0", RegistryValueKind.DWord);
            key2.SetValue("PeriodInNanoSeconds", "0", RegistryValueKind.DWord);

            RegistryKey key4 = Registry.LocalMachine.OpenSubKey(@"Software\Policies\Microsoft\Windows\DataCollection", true);
            key4.SetValue("DoNotShowFeedbackNotifications", "1", RegistryValueKind.DWord);

            using (PowerShell ps = PowerShell.Create())
            {
                ps.AddScript("Disable-ScheduledTask -TaskName \"Microsoft\\Windows\\Feedback\\Siuf\\DmClient\"");
                ps.AddScript("Disable-ScheduledTask -TaskName \"Microsoft\\Windows\\Feedback\\Siuf\\DmClientOnScenarioDownload\"");
                ps.Invoke();
            }
        }

        public static void DisableStartMenuSuggestions()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", true);
            key.SetValue("SystemPaneSuggestionsEnabled", "0", RegistryValueKind.DWord);
            key.SetValue("SubscribedContent-338389Enabled", "0", RegistryValueKind.DWord);
            key.SetValue("SubscribedContent-338388Enabled", "0", RegistryValueKind.DWord);
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
            Registry.LocalMachine.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Search");

            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Search", true);
            key.SetValue("BingSearchEnabled", "0", RegistryValueKind.DWord);
            key.SetValue("CortanaConsent", "0", RegistryValueKind.DWord);
            key.SetValue("DisableWebSearch", "1", RegistryValueKind.DWord);

            RegistryKey key4 = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Search", true);
            key4.SetValue("BingSearchEnabled", "0", RegistryValueKind.DWord);
            key4.SetValue("CortanaConsent", "0", RegistryValueKind.DWord);
            key4.SetValue("DisableWebSearch", "1", RegistryValueKind.DWord);

            RegistryKey key2 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Policies\Microsoft\Windows\Windows Search", true);
            key2.SetValue("DisableWebSearch", "1", RegistryValueKind.DWord);
            key2.SetValue("ConnectedSearchUseWeb", "0", RegistryValueKind.DWord);
            key2.SetValue("AllowCloudSearch", "0", RegistryValueKind.DWord);

            RegistryKey key3 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\Windows Search", true);
            key3.SetValue("DisableWebSearch", "1", RegistryValueKind.DWord);
            key3.SetValue("ConnectedSearchUseWeb", "0", RegistryValueKind.DWord);
            key3.SetValue("AllowCloudSearch", "0", RegistryValueKind.DWord);

        }

        public static void DisablePasswordReveal()
        {
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\CredUI", true);
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\CredUI", true);
            key.SetValue("DisablePasswordReveal", "1", RegistryValueKind.DWord);

            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\WOW6432Node\Policies\Microsoft\Windows\CredUI", true);
            RegistryKey key2 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Policies\Microsoft\Windows\CredUI", true);
            key2.SetValue("DisablePasswordReveal", "1", RegistryValueKind.DWord);
        }

        public static void DisableSettingsSync()
        {
            Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Accessibility", true);
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Accessibility", true);
            key.SetValue("Enabled", "0", RegistryValueKind.DWord);

            Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\SettingSync\Groups\BrowserSettings", true);
            RegistryKey key2 = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\SettingSync\Groups\BrowserSettings", true);
            key2.SetValue("Enabled", "0", RegistryValueKind.DWord);

            Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Credentials", true);
            RegistryKey key3 = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Credentials", true);
            key3.SetValue("Enabled", "0", RegistryValueKind.DWord);

            Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Language", true);
            RegistryKey key4 = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Language", true);
            key4.SetValue("Enabled", "0", RegistryValueKind.DWord);

            Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Personalization", true);
            RegistryKey key5 = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Personalization", true);
            key5.SetValue("Enabled", "0", RegistryValueKind.DWord);

            Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Windows", true);
            RegistryKey key6 = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Windows", true);
            key6.SetValue("Enabled", "0", RegistryValueKind.DWord);

            Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\SettingSync\Groups\AppSync", true);
            RegistryKey key7 = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\SettingSync\Groups\AppSync", true);
            key7.SetValue("Enabled", "0", RegistryValueKind.DWord);

            RegistryKey key8 = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\SettingSync", true);
            key8.SetValue("SyncPolicy", "5", RegistryValueKind.DWord);
        }

        public static void DisableStartupSound()
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Authentication\LogonUI\BootAnimation", true);
            key.SetValue("DisableStartupSound", "1", RegistryValueKind.DWord);
        }

        public static void DisableAutostartStartupDelay()
        {
            Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Serialize");
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Serialize", true);
            key.SetValue("Startupdelayinmsec", "0", RegistryValueKind.DWord);
        }

        public static void DisableLocation()
        {
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\LocationAndSensors");
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Sensor\Overrides\{BFA794E4-F964-4FDB-90F6-51056BFE4B44}");

            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\LocationAndSensors", true);
            key.SetValue("DisableLocation", "1", RegistryValueKind.DWord);
            key.SetValue("DisableLocationScripting", "1", RegistryValueKind.DWord);
            key.SetValue("DisableWindowsLocationProvider", "1", RegistryValueKind.DWord);

            RegistryKey key2 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Policies\Microsoft\Windows\LocationAndSensors", true);
            key2.SetValue("DisableLocation", "1", RegistryValueKind.DWord);
            key2.SetValue("DisableLocationScripting", "1", RegistryValueKind.DWord);
            key2.SetValue("DisableWindowsLocationProvider", "1", RegistryValueKind.DWord);

            RegistryKey key3 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\Windows Search", true);
            key3.SetValue("AllowSearchToUseLocation", "0", RegistryValueKind.DWord);

            RegistryKey key4 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Policies\Microsoft\Windows\Windows Search", true);
            key4.SetValue("AllowSearchToUseLocation", "0", RegistryValueKind.DWord);

            RegistryKey key5 = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\lfsvc", true);
            key5.SetValue("Start", "4", RegistryValueKind.DWord);

            RegistryKey key6 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Sensor\Overrides\{BFA794E4-F964-4FDB-90F6-51056BFE4B44}", true);
            key6.SetValue("SensorPermissionState", "0", RegistryValueKind.DWord);
        }

        public static void DisableDefender()
        {
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows Defender\Spynet");
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\WOW6432Node\Policies\Microsoft\Windows Defender\Spynet");

            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows Defender", true);
            key.SetValue("DisableAntiSpyware", "1", RegistryValueKind.DWord);

            RegistryKey key2 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows Defender\Spynet", true);
            key2.SetValue("SpyNetReporting", "0", RegistryValueKind.DWord);
            key2.SetValue("SubmitSamplesConsent", "2", RegistryValueKind.DWord);

            RegistryKey key3 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Policies\Microsoft\Windows Defender", true);
            key3.SetValue("DisableAntiSpyware", "1", RegistryValueKind.DWord);

            RegistryKey key4 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Policies\Microsoft\Windows Defender\Spynet", true);
            key4.SetValue("SpyNetReporting", "0", RegistryValueKind.DWord);
            key4.SetValue("SubmitSamplesConsent", "2", RegistryValueKind.DWord);

            RegistryKey key5 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            key5.DeleteValue("SecurityHealth", false);
        }

        public static void DisableAdvertisingId()
        {
            Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\AdvertisingInfo");
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\AdvertisingInfo", true);
            key.SetValue("Enabled", "0", RegistryValueKind.DWord);

            Registry.LocalMachine.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\AdvertisingInfo");
            RegistryKey key2 = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\AdvertisingInfo", true);
            key2.SetValue("Enabled", "0", RegistryValueKind.DWord);
        }

        public static void DisableMrtReporting()
        {
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Policies\Microsoft\MRT");
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\WOW6432Node\Policies\Microsoft\MRT");

            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\MRT", true);
            key.SetValue("DontReportInfectionInformation", "1", RegistryValueKind.DWord);

            RegistryKey key2 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Policies\Microsoft\MRT", true);
            key2.SetValue("DontReportInfectionInformation", "1", RegistryValueKind.DWord);
        }

        public static void DisableSendingTypingInfo()
        {
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\TabletPC");
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\WOW6432Node\Policies\Microsoft\Windows\TabletPC");

            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Input\TIPC", true);
            key.SetValue("Enabled", "0", RegistryValueKind.DWord);

            RegistryKey key2 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\TabletPC", true);
            key2.SetValue("PreventHandwritingDataSharing", "1", RegistryValueKind.DWord);

            RegistryKey key3 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Policies\Microsoft\Windows\TabletPC", true);
            key3.SetValue("PreventHandwritingDataSharing", "1", RegistryValueKind.DWord);
        }

        public static void DeleteQuicklaunchItems()
        {
            byte[] data = new byte[] { 0xFF };

            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Taskband", true);
            key.SetValue("Favorites", data, RegistryValueKind.Binary);
            key.DeleteValue("FavoritesResolve", false);
        }

        public static void DisablePersonalization()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Personalization\Settings", true);
            key.SetValue("AcceptedPrivacyPolicy", "0", RegistryValueKind.DWord);

            Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\InputPersonalization", true);
            RegistryKey key2 = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\InputPersonalization", true);
            key2.SetValue("RestrictImplicitTextCollection", "1", RegistryValueKind.DWord);
            key2.SetValue("RestrictImplicitInkCollection", "1", RegistryValueKind.DWord);
            key2.SetValue("HarvestContacts", "0", RegistryValueKind.DWord);
        }

        public static void HideLanguageListWebsites()
        {
            Registry.CurrentUser.CreateSubKey(@"Control Panel\International\User Profile");

            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\International\User Profile", true);
            key.SetValue("HttpAcceptLanguageOptOut", "1", RegistryValueKind.DWord);
        }

        public static void DisableMiracast()
        {
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\MiracastReceiver");

            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\MiracastReceiver", true);
            key.SetValue("ConsentToast", "2", RegistryValueKind.DWord);
        }

        public static void DisableBluetoothAdvertisements()
        {
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\PolicyManager\current\device\Bluetooth");

            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\PolicyManager\current\device\Bluetooth", true);
            key.SetValue("AllowAdvertising", "0", RegistryValueKind.DWord);
        }

        public static void DisableClipboardHistory()
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\System", true);
            key.SetValue("AllowClipboardHistory", "0", RegistryValueKind.DWord);

            RegistryKey key2 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Policies\Microsoft\Windows\System", true);
            key2.SetValue("AllowClipboardHistory", "0", RegistryValueKind.DWord);
        }

        public static void DisableClipboardCloudSync()
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\System", true);
            key.SetValue("AllowCrossDeviceClipboard", "0", RegistryValueKind.DWord);

            RegistryKey key2 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Policies\Microsoft\Windows\System", true);
            key2.SetValue("AllowCrossDeviceClipboard", "0", RegistryValueKind.DWord);
        }

        public static void DisableAutomaticSpeechDataUpdates()
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Speech_OneCore\Preferences", true);
            key.SetValue("ModelDownloadAllowed", "0", RegistryValueKind.DWord);

            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Speech");
            RegistryKey key2 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Speech", true);
            key2.SetValue("AllowSpeechModelUpdate", "0", RegistryValueKind.DWord);
        }
        public static void DisableTextMessagesCloudSync()
        {
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\Messaging");
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\WOW6432Node\Policies\Microsoft\Windows\Messaging");

            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\Messaging", true);
            key.SetValue("AllowMessageSync", "0", RegistryValueKind.DWord);

            RegistryKey key2 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Policies\Microsoft\Windows\Messaging", true);
            key2.SetValue("AllowMessageSync", "0", RegistryValueKind.DWord);
        }
        public static void DisableHandwritingErrorReports()
        {
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\HandwritingErrorReports");
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\WOW6432Node\Policies\Microsoft\Windows\HandwritingErrorReports");

            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\HandwritingErrorReports", true);
            key.SetValue("PreventHandwritingErrorReports", "1", RegistryValueKind.DWord);

            RegistryKey key2 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Policies\Microsoft\Windows\HandwritingErrorReports", true);
            key2.SetValue("PreventHandwritingErrorReports", "1", RegistryValueKind.DWord);
        }

        public static void DisableAppDiagnostics()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Privacy", true);
            key.SetValue("TailoredExperiencesWithDiagnosticDataEnabled", "0", RegistryValueKind.DWord);

            RegistryKey key2 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Privacy", true);
            key2.SetValue("TailoredExperiencesWithDiagnosticDataEnabled", "0", RegistryValueKind.DWord);
        }

        public static void DisableSmartScreen()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\AppHost", true);
            key.SetValue("EnableWebContentEvaluation", "0", RegistryValueKind.DWord);

            RegistryKey key2 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer", true);
            key2.SetValue("SmartScreenEnabled", "Off", RegistryValueKind.String);

            RegistryKey key3 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\System", true);
            key3.SetValue("EnableSmartScreen", "0", RegistryValueKind.DWord);
        }

        public static void DisableNotificationOnLockScreen()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Notifications\Settings", true);
            key.SetValue("NOC_GLOBAL_SETTING_ALLOW_TOASTS_ABOVE_LOCK", "0", RegistryValueKind.DWord);
        }

        public static void DisableRemindersAndCallsOnLockScreen()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Notifications\Settings", true);
            key.SetValue("NOC_GLOBAL_SETTING_ALLOW_CRITICAL_TOASTS_ABOVE_LOCK", "0", RegistryValueKind.DWord);
        }

        public static void DisableWelcomeExperience()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", true);
            key.SetValue("SubscribedContent-310093Enabled", "0", RegistryValueKind.DWord);
        }

        public static void DisableAeroShake()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced", true);
            key.SetValue("DisallowShaking", "1", RegistryValueKind.DWord);
        }

        public static void EnableStorageSense()
        {
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\StorageSense");
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\StorageSense", true);
            key.SetValue("AllowStorageSenseGlobal", "1", RegistryValueKind.DWord);
        }

        public static void DisableEdgeFirstRunPage()
        {
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Policies\Microsoft\MicrosoftEdge");
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Policies\Microsoft\MicrosoftEdge\Main");

            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\MicrosoftEdge\Main", true);
            key.SetValue("PreventFirstRunPage", "1", RegistryValueKind.DWord);
        }

        public static void DisableSmartGlass()
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\SmartGlass", true);
            key.SetValue("UserAuthPolicy", "0", RegistryValueKind.DWord);
        }

        public static void DisableInkAppSuggestions()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\SmartGlass", true);
            key.SetValue("PenWorkspaceAppSuggestionsEnabled", "0", RegistryValueKind.DWord);
        }

        public static void DisableExperiments()
        {
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\PolicyManager\current\device\System");

            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\PolicyManager\current\device\System", true);
            key.SetValue("AllowExperimentation", "0", RegistryValueKind.DWord);
        }

        public static void DisableCameraLockScreen()
        {
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\Personalization");

            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\Personalization", true);
            key.SetValue("NoLockScreenCamera", "1", RegistryValueKind.DWord);
        }

        public static void DisableApplicationCompatibilityEngine()
        {
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\AppCompat", true);

            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\AppCompat", true);
            key.SetValue("DisableEngine", "1", RegistryValueKind.DWord);
        }

        public static void DisableWiFiSense()
        {
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\PolicyManager\default\WiFi\AllowWiFiHotSpotReporting");
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\PolicyManager\default\WiFi\AllowAutoConnectToWiFiSenseHotspots");
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\WcmSvc\wifinetworkmanager\config");

            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\PolicyManager\default\WiFi\AllowWiFiHotSpotReporting", true);
            key.SetValue("Value", "0", RegistryValueKind.DWord);

            RegistryKey key2 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\PolicyManager\default\WiFi\AllowAutoConnectToWiFiSenseHotspots", true);
            key2.SetValue("Value", "0", RegistryValueKind.DWord);
            
            RegistryKey key3 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\WcmSvc\wifinetworkmanager\config", true);
            key3.SetValue("AutoConnectAllowedOEM", "0", RegistryValueKind.DWord);
        }

        public static void DisableLockScreenSpotlight()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", true);
            key.SetValue("RotatingLockScreenEnabled", "0", RegistryValueKind.DWord);
            key.SetValue("RotatingLockScreenOverlayEnabled", "0", RegistryValueKind.DWord);
            key.SetValue("SubscribedContent-338387Enabled", "0", RegistryValueKind.DWord);

            RegistryKey key2 = Registry.LocalMachine.OpenSubKey(@"Software\Policies\Microsoft\Windows\CloudContent", true);
            key2.SetValue("DisableWindowsSpotlightFeatures", "1", RegistryValueKind.DWord);
        }

        public static void DisableAutomaticMapsUpdates()
        {
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\Maps");
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\WOW6432Node\Policies\Microsoft\Windows\Maps");

            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SYSTEM\Maps", true);
            key.SetValue("AutoUpdateEnabled", "0", RegistryValueKind.DWord);

            RegistryKey key2 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\Maps", true);
            key2.SetValue("AutoDownloadAndUpdateMapData", "0", RegistryValueKind.DWord);

            RegistryKey key3 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Policies\Microsoft\Windows\Maps", true);
            key3.SetValue("AutoDownloadAndUpdateMapData", "0", RegistryValueKind.DWord);
        }

        public static void DisableErrorReporting()
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\Windows Error Reporting", true);
            key.SetValue("Disabled", "1", RegistryValueKind.DWord);

            using (PowerShell ps = PowerShell.Create())
            {
                ps.AddScript("Disable-ScheduledTask -TaskName \"Microsoft\\Windows\\Windows Error Reporting\\QueueReporting\"");
                ps.Invoke();
            }
        }

        public static void HideNewsAndInterests()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Feeds", true);
            key.SetValue("ShellFeedsTaskbarViewMode", "2", RegistryValueKind.DWord);
        }

        public static void DisableRemoteAssistance()
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Remote Assistance", true);
            key.SetValue("fAllowToGetHelp", "0", RegistryValueKind.DWord);
        }

        public static void UseUtcAsBiosTime()
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\TimeZoneInformation", true);
            key.SetValue("RealTimeIsUniversal", "1", RegistryValueKind.DWord);
        }

        public static void HideNetworkFromLockScreen()
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\System", true);
            key.SetValue("DontDisplayNetworkSelectionUI", "1", RegistryValueKind.DWord);
        }

        public static void DisableStickyKeysPrompt()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Accessibility\StickyKeys", true);
            key.SetValue("Flags", "506", RegistryValueKind.String);
        }

        public static void AddThisPCShortcut()
        {
            Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\HideDesktopIcons\ClassicStartMenu");
            Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\HideDesktopIcons\NewStartPanel");

            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\HideDesktopIcons\ClassicStartMenu", true);
            key.SetValue("{20D04FE0-3AEA-1069-A2D8-08002B30309D}", "0", RegistryValueKind.DWord);

            RegistryKey key2 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\HideDesktopIcons\NewStartPanel", true);
            key2.SetValue("{20D04FE0-3AEA-1069-A2D8-08002B30309D}", "0", RegistryValueKind.DWord);
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

        public static void UpdateStoreApps()
        {
            string commands = Encoding.UTF8.GetString(Resources.updateapps);

            using (PowerShell ps = PowerShell.Create())
            {
                ps.AddScript(commands);
                ps.Invoke();
            }
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

        public static void InstallWinGet()
        {
            String url = "https://github.com/microsoft/winget-cli/releases/download/v1.0.11692/Microsoft.DesktopAppInstaller_8wekyb3d8bbwe.msixbundle";
            using (WebClient client = new WebClient())
            {
                FileInfo file = new FileInfo(MainForm.Tmpfolder + "winget.msixbundle");

                DialogResult result = DialogResult.Retry;
                while (result == DialogResult.Retry)
                {
                    try
                    {
                        client.DownloadFile(new Uri(url), file.FullName);

                        using (PowerShell ps = PowerShell.Create())
                        {
                            ps.AddScript("Add-AppxPackage -Path "+ file.FullName);
                            ps.Invoke();
                            if (ps.HadErrors)
                            {
                                string caption = "Something went wrong...";
                                string errorMessage = "An error occured while trying to install winget.\n\nPlease report this issue on GitHub. Slapping will continue after closing this message, though items selected in Software will likely not be installed.";
                                MessageBox.Show(new Form { TopMost = true }, errorMessage, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }

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

        public static void WinGetInstall(string packageid)
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C winget install --silent --id " + packageid;
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();
        }

        public static void Install7Zip()
        {
            WinGetInstall("7zip.7zip");
        }

        public static void InstallAdobeReaderDC()
        {
            WinGetInstall("Adobe.AdobeAcrobatReaderDC");
        }

        public static void InstallAudacity()
        {
            WinGetInstall("Audacity.Audacity");
        }

        public static void InstallBalenaEtcher()
        {
            WinGetInstall("Balena.Etcher");
        }

        public static void InstallBattleNet()
        {
            WinGetInstall("Blizzard.BattleNet");
        }

        public static void InstallCalibre()
        {
            WinGetInstall("calibre.calibre");
        }

        public static void InstallCPUZ()
        {
            WinGetInstall("CPUID.CPU-Z");
        }

        public static void InstallDiscord()
        {
            WinGetInstall("Discord.Discord");
        }

        public static void InstallDupeGuru()
        {
            WinGetInstall("DupeGuru.DupeGuru");
        }

        public static void InstallEarTrumpet()
        {
            WinGetInstall("File-New-Project.EarTrumpet");
        }

        public static void InstallEpicGamesLauncher()
        {
            WinGetInstall("EpicGames.EpicGamesLauncher");
        }

        public static void InstallEverythingSearch()
        {
            WinGetInstall("voidtools.EverythingLite");
        }

        public static void InstallFlux()
        {
            WinGetInstall("flux.flux");
        }

        public static void InstallFileZilla()
        {
            WinGetInstall("TimKosse.FileZillaClient");
        }

        public static void InstallGIMP()
        {
            WinGetInstall("GIMP.GIMP");
        }

        public static void InstallGPUZ()
        {
            WinGetInstall("TechPowerUp.GPU-Z");
        }

        public static void InstallGit()
        {
            WinGetInstall("Git.Git");
        }

        public static void InstallGoogleChrome()
        {
            WinGetInstall("Google.Chrome");
        }

        public static void InstallHashTab()
        {
            WinGetInstall("Implbits.HashTab");
        }

        public static void InstallInkscape()
        {
            WinGetInstall("Inkscape.Inkscape");
        }

        public static void InstallIrfanview()
        {
            WinGetInstall("IrfanSkiljan.IrfanView");
        }

        public static void InstallJavaRE()
        {
            WinGetInstall("Oracle.JavaRuntimeEnvironment");
        }

        public static void InstallKDEConnect()
        {
            WinGetInstall("KDE.KDEConnect");
        }

        public static void InstallKeePassXC()
        {
            WinGetInstall("KeePassXCTeam.KeePassXC");

            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            key.DeleteValue("KeePassXC", false);
        }

        public static void InstallLeagueOfLegends()
        {
            WinGetInstall("RiotGames.LeagueOfLegends");
        }

        public static void InstallLibreOffice()
        {
            WinGetInstall("LibreOffice.LibreOffice");
        }

        public static void InstallMinecraft()
        {
            WinGetInstall("Mojang.MinecraftLauncher");
        }

        public static void InstallFirefox()
        {
            WinGetInstall("Mozilla.Firefox");
        }

        public static void InstallThunderbird()
        {
            WinGetInstall("Mozilla.Thunderbird");
        }

        public static void InstallNextcloudDesktop()
        {
            WinGetInstall("Nextcloud.NextcloudDesktop");
        }

        public static void InstallNotepadPlusPlus()
        {
            WinGetInstall("Notepad++.Notepad++");
        }

        public static void InstallOBSStudio()
        {
            WinGetInstall("OBSProject.OBSStudio");
        }

        public static void InstallOpenVPNConnect()
        {
            WinGetInstall("OpenVPNTechnologies.OpenVPNConnect");
        }

        public static void InstallOrigin()
        {
            WinGetInstall("ElectronicArts.Origin");
        }

        public static void InstallPowerToys()
        {
            WinGetInstall("Microsoft.PowerToys");
        }

        public static void InstallPuTTY()
        {
            WinGetInstall("PuTTY.PuTTY");
        }

        public static void InstallPython()
        {
            WinGetInstall("Python.Python");
        }

        public static void InstallSkype()
        {
            WinGetInstall("Microsoft.Skype");
        }

        public static void InstallSlack()
        {
            WinGetInstall("SlackTechnologies.Slack");
        }

        public static void InstallSpeccy()
        {
            WinGetInstall("Piriform.Speccy");
        }

        public static void InstallStartIsBack()
        {
            DownloadRun("https://s3.amazonaws.com/startisback/StartIsBackPlusPlus_setup.exe", "StartIsBack_Setup.exe", "/elevated /silent");
        }

        public static void InstallSteam()
        {
            WinGetInstall("Valve.Steam");

            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            key.DeleteValue("Steam", false);
        }

        public static void InstallTeamViewer()
        {
            WinGetInstall("TeamViewer.TeamViewer");
        }

        public static void InstallTeamSpeak()
        {
            WinGetInstall("TeamSpeakSystems.TeamSpeakClient");
        }

        public static void InstallTelegram()
        {
            WinGetInstall("Telegram.TelegramDesktop");
        }

        public static void InstallTwitch()
        {
            WinGetInstall("twitch.twitch");
            File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.Startup) + @"\Twitch.lnk");
        }

        public static void InstallUbisoftConnect()
        {
            WinGetInstall("Ubisoft.Connect");
        }

        public static void InstallVirtualBox()
        {
            WinGetInstall("Oracle.VirtualBox");
        }

        public static void InstallVSCode()
        {
            WinGetInstall("Microsoft.VisualStudioCode");
        }

        public static void InstallVlc()
        {
            WinGetInstall("VideoLAN.VLC");
        }

        public static void InstallWinRAR()
        {
            WinGetInstall("RARLab.WinRAR");
        }

        public static void InstallWinSCP()
        {
            WinGetInstall("WinSCP.WinSCP");
        }

        public static void InstallWindowsTerminal()
        {
            WinGetInstall("Microsoft.WindowsTerminal");
        }

        public static void InstallWireshark()
        {
            WinGetInstall("WiresharkFoundation.Wireshark");
        }

        public static void InstallZoom()
        {
            WinGetInstall("Zoom.Zoom");
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
            string desktoppath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDesktopDirectory);
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

        public static void EnableWSL()
        {
            using (PowerShell ps = PowerShell.Create())
            {
                ps.AddScript("Enable-WindowsOptionalFeature -Online -FeatureName Microsoft-Windows-Subsystem-Linux");
                ps.Invoke();
            }
        }

        public static void UninstallInternetExplorer()
        {
            using (PowerShell ps = PowerShell.Create())
            {
                ps.AddScript("Disable-WindowsOptionalFeature -FeatureName Internet-Explorer-Optional-amd64 –Online");
                ps.Invoke();
            }
        }
    }
}
