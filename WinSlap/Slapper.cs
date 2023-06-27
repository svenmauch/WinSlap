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
                        string errorMessage = $"A WebException occured trying to download from the following URL:\n\n{url}\n\nPlease check your network connection and report this issue on GitHub if the error persists";
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

        public enum RegistryHive
        {
            HKCU,
            HKLM,
            HKCR
        }

        private static void UpdateRegistryKey(RegistryHive registryHive, string keyPath, string valueName, string valueData, RegistryValueKind valueKind)
        {
            try
            {
                RegistryKey baseKey = null;
                switch (registryHive)
                {
                    case RegistryHive.HKCU:
                        baseKey = Registry.CurrentUser;
                        break;
                    case RegistryHive.HKLM:
                        baseKey = Registry.LocalMachine;
                        break;
                    case RegistryHive.HKCR:
                        baseKey = Registry.ClassesRoot;
                        break;
                }

                using (RegistryKey key = baseKey.CreateSubKey(keyPath))
                {
                    if (key != null)
                    {
                        key.SetValue(valueName, valueData, valueKind);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        public static void HideTaskview()
        {
            UpdateRegistryKey(RegistryHive.HKCU, @"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "ShowTaskViewButton", "0", RegistryValueKind.DWord);
        }

        public static void DoNotGroupTasks()
        {
            UpdateRegistryKey(RegistryHive.HKCU, @"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "TaskbarGlomLevel", "2", RegistryValueKind.DWord);
        }

        public static void TaskbarSmallIcons()
        {
            UpdateRegistryKey(RegistryHive.HKCU, @"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "TaskbarSmallIcons", "1", RegistryValueKind.DWord);
        }

        public static void DisablePeopleBand()
        {
            UpdateRegistryKey(RegistryHive.HKCU, @"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced\People", "PeopleBand", "0", RegistryValueKind.DWord);
        }

        public static void HideSearch()
        {
            UpdateRegistryKey(RegistryHive.HKCU, @"Software\Microsoft\Windows\CurrentVersion\Search", "SearchboxTaskbarMode", "0", RegistryValueKind.DWord);
        }

        public static void HideMeetNow()
        {
            UpdateRegistryKey(RegistryHive.HKCU, @"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer", "HideSCAMeetNow", "1", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKLM, @"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer", "HideSCAMeetNow", "1", RegistryValueKind.DWord);
        }

        /*
        // TODO: does not seem to work!
        public static void UpdateMSProducts()
        {
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\Microsoft\Windows\CurrentVersion\WindowsUpdate\Services\7971F918-A847-4430-9279-4A52D1EFE18D", "###", "###", RegistryValueKind.DWord);
            key.SetValue("RegisteredWithAU", "1", RegistryValueKind.DWord);
        }

        */

        // TODO: not tested
        public static void DisableSharedExperiences()
        {
            UpdateRegistryKey(RegistryHive.HKCU, @"Software\Microsoft\Windows\CurrentVersion\CDP", "CdpSessionUserAuthzPolicy", "0", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKCU, @"Software\Microsoft\Windows\CurrentVersion\CDP", "RomeSdkChannelUserAuthzPolicy", "0", RegistryValueKind.DWord);
        }

        public static void RemoveCompatibility()
        {
            Registry.ClassesRoot.DeleteSubKey(@"exefile\shellex\ContextMenuHandlers\Compatibility", false);
        }

        public static void DisableCortana()
        {
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\Policies\Microsoft\Windows\Windows Search", "AllowCortana", "0", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\WOW6432Node\Policies\Microsoft\Windows\Windows Search", "AllowCortana", "0", RegistryValueKind.DWord);
        }

        public static void DisableLLMNR()
        {
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\Policies\Microsoft\Windows NT\DNSClient", "EnableMulticast", "0", RegistryValueKind.DWord);
        }

        public static void DisableSmartNameResolution()
        {
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\Policies\Microsoft\Windows NT\DNSClient", "DisableSmartNameResolution", "1", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKLM, @"SYSTEM\CurrentControlSet\Services\Dnscache\Parameters", "DisableParallelAandAAAA", "1", RegistryValueKind.DWord);
        }

        public static void DisableWPAD()
        {
            UpdateRegistryKey(RegistryHive.HKLM, @"SYSTEM\CurrentControlSet\Services\Tcpip\Parameters", "UseDomainNameDevolution", "0", RegistryValueKind.DWord);
        }

        public static void DisableLockscreenBlur()
        {
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\Policies\Microsoft\Windows\System", "DisableAcrylicBackgroundOnLogon", "1", RegistryValueKind.DWord);
        }

        public static void DisableSecurityQuestions()
        {
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\Policies\Microsoft\Windows\System", "NoLocalPasswordResetQuestions", "1", RegistryValueKind.DWord);
        }

        public static void DisableBlockPrecisionTrackpad()
        {
            UpdateRegistryKey(RegistryHive.HKCU, @"SOFTWARE\Microsoft\Windows\CurrentVersion\PrecisionTouchPad", "AAPThreshold", "0", RegistryValueKind.DWord);
        }

        // note: "Anywhere" actually means disabled
        public static void DisableAppSuggestions()
        {
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer", "AicEnabled", "Anywhere", RegistryValueKind.String);
        }

        public static void DisableGameDvr()
        {
            UpdateRegistryKey(RegistryHive.HKCU, @"SOFTWARE\Microsoft\Windows\CurrentVersion\GameDVR", "AppCaptureEnabled", "0", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKCU, @"SOFTWARE\Microsoft\Windows\CurrentVersion\GameDVR", "AudioCaptureEnabled", "0", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKCU, @"SOFTWARE\Microsoft\Windows\CurrentVersion\GameDVR", "CursorCaptureEnabled", "0", RegistryValueKind.DWord);

            UpdateRegistryKey(RegistryHive.HKCU, @"Software\Microsoft\GameBar", "ShowStartupPanel", "0", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKCU, @"Software\Microsoft\GameBar", "UseNexusForGameBarEnabled", "0", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKCU, @"Software\Microsoft\GameBar", "AllowAutoGameMode", "0", RegistryValueKind.DWord);

            UpdateRegistryKey(RegistryHive.HKCU, @"System\GameConfigStore", "GameDVR_FSEBehavior", "2", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKCU, @"System\GameConfigStore", "GameDVR_Enabled", "0", RegistryValueKind.DWord);

            UpdateRegistryKey(RegistryHive.HKLM, @"Software\Policies\Microsoft\Windows", "AllowgameDVR", "0", RegistryValueKind.DWord);

            /* TODO: doesn't work anymore as of 1909
            UpdateRegistryKey(RegistryHive.HKLM, @"System\CurrentControlSet\Services\xbgm", "###", "###", RegistryValueKind.DWord);
            key5.SetValue("Start", "4", RegistryValueKind.DWord);
            */

            UpdateRegistryKey(RegistryHive.HKLM, @"System\CurrentControlSet\Services\XblAuthManager", "Start", "4", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKLM, @"System\CurrentControlSet\Services\XblGameSave", "Start", "4", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKLM, @"System\CurrentControlSet\Services\XboxGipSvc", "Start", "4", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKLM, @"System\CurrentControlSet\Services\XboxNetApiSvc", "Start", "4", RegistryValueKind.DWord);
        }

        public static void DisableHotspot20()
        {
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\Microsoft\WlanSvc\AnqpCache", "OsuRegistrationStatus", "0", RegistryValueKind.DWord);
        }

        public static void DisableCloudStates()
        {
            UpdateRegistryKey(RegistryHive.HKCU, @"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "NavPaneShowAllCloudStates", "0", RegistryValueKind.DWord);
        }

        public static void NoQuickAccess()
        {
            UpdateRegistryKey(RegistryHive.HKCU, @"Software\Microsoft\Windows\CurrentVersion\Explorer", "ShowFrequent", "0", RegistryValueKind.DWord);
        }

        public static void HideSyncNotifications()
        {
            UpdateRegistryKey(RegistryHive.HKCU, @"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "ShowSyncProviderNotifications", "0", RegistryValueKind.DWord);
        }

        public static void DisableSharingWizard()
        {
            UpdateRegistryKey(RegistryHive.HKCU, @"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "SharingWizardOn", "0", RegistryValueKind.DWord);

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
            UpdateRegistryKey(RegistryHive.HKCU, @"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "HideFileExt", "0", RegistryValueKind.DWord);
        }

        public static void LaunchThisPcFileExplorer()
        {
            UpdateRegistryKey(RegistryHive.HKCU, @"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "LaunchTo", "1", RegistryValueKind.DWord);
        }

        public static void DisableInventoryCollection()
        {
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\Policies\Microsoft\Windows\AppCompat", "DisableInventory", "1", RegistryValueKind.DWord);
        }

        public static void DisablePreReleaseFeatures()
        {
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\Policies\Microsoft\Windows\PreviewBuilds", "AllowBuildPreview", "0", RegistryValueKind.DWord);
        }

        public static void DisableEdgePreload()
        {
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\Policies\Microsoft\MicrosoftEdge\Main", "AllowPrelaunch", "0", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\Policies\Microsoft\MicrosoftEdge\TabPreloader", "AllowTabPreloading", "0", RegistryValueKind.DWord);
        }

        public static void DisableCompatibilityAssistant()
        {
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\Policies\Microsoft\Windows\AppCompat", "DisablePCA", "1", RegistryValueKind.DWord);
        }

        public static void DisableDRMInternetAccess()
        {
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\Policies\Microsoft\WMDRM", "DisableOnline", "1", RegistryValueKind.DWord);
        }

        public static void DisableStepsRecorder()
        {
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\Policies\Microsoft\Windows\AppCompat", "DisableUAR", "1", RegistryValueKind.DWord);
        }

        public static void UseWin7Volume()
        {
            UpdateRegistryKey(RegistryHive.HKLM, @"Software\Microsoft\Windows NT\CurrentVersion\MTCUVC", "EnableMtcUvc", "0", RegistryValueKind.DWord);
        }

        public static void SetPowerPlanHighPerformance()
        {
            UpdateRegistryKey(RegistryHive.HKLM, @"Software\Policies\Microsoft\Power\PowerSettings", "ActivePowerScheme", "8c5e7fda-e8bf-4a96-9a85-a6e23a8c635c", RegistryValueKind.String);
        }

        public static void DisableGetEvenMoreOutOfWindows()
        {
            UpdateRegistryKey(RegistryHive.HKCU, @"SOFTWARE\Microsoft\Windows\CurrentVersion\UserProfileEngagement", "ScoobeSystemSettingEnabled", "0", RegistryValueKind.DWord);
        }

        public static void HideOneDriveFileExplorer()
        {
            UpdateRegistryKey(RegistryHive.HKCR, @"CLSID\{018D5C66-4533-4307-9B53-224DE2ED1FE6}", "System.IsPinnedToNameSpaceTree", "0", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\Policies\Microsoft\Windows\OneDrive", "DisableFileSyncNGSC", "1", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\WOW6432Node\Policies\Microsoft\Windows\OneDrive", "DisableFileSyncNGSC", "1", RegistryValueKind.DWord);
        }

        public static void DisableTelemetry()
        {
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\Policies\Microsoft\Windows\DataCollection", "AllowTelemetry", "0", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\DataCollection", "AllowTelemetry", "0", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Policies\DataCollection", "AllowTelemetry", "0", RegistryValueKind.DWord);

            UpdateRegistryKey(RegistryHive.HKLM, @"SYSTEM\CurrentControlSet\Control\WMI\Autologger\AutoLogger-Diagtrack-Listener", "Start", "0", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKLM, @"SYSTEM\CurrentControlSet\Services\dmwappushservice", "Start", "4", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKLM, @"SYSTEM\CurrentControlSet\Services\DiagTrack", "Start", "4", RegistryValueKind.DWord);

            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\Policies\Microsoft\Windows\AppCompat", "AITEnable", "0", RegistryValueKind.DWord);

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
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\Microsoft\OneDrive", "PreventNetworkTrafficPreUserSignIn", "1", RegistryValueKind.DWord);

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
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\Policies\Microsoft\Windows\System", "PublishUserActivities", "0", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\Policies\Microsoft\Windows\System", "EnableActivityFeed", "0", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\Policies\Microsoft\Windows\System", "UploadUserActivities", "0", RegistryValueKind.DWord);
        }

        public static void DisableBackgroundApps()
        {
            UpdateRegistryKey(RegistryHive.HKCU, @"Software\Microsoft\Windows\CurrentVersion\BackgroundAccessApplications", "GlobalUserDisabled", "1", RegistryValueKind.DWord);
        }

        public static void DisableAutomaticAppInstall()
        {
            UpdateRegistryKey(RegistryHive.HKCU, @"Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "SilentInstalledAppsEnabled", "0", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKCU, @"Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "ContentDeliveryAllowed", "0", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKCU, @"Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "OemPreInstalledAppsEnabled", "0", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKCU, @"Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "PreInstalledAppsEnabled", "0", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKCU, @"Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "PreInstalledAppsEverEnabled", "0", RegistryValueKind.DWord);

            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\Policies\Microsoft\Windows\CloudContent", "DisableWindowsConsumerFeatures", "1", RegistryValueKind.DWord);
        }

        public static void DisableFeedbackDialogs()
        {
            UpdateRegistryKey(RegistryHive.HKCU, @"Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "SoftLandingEnabled", "0", RegistryValueKind.DWord);

            UpdateRegistryKey(RegistryHive.HKLM, @"Software\Policies\Microsoft\Windows\CloudContent", "DisableSoftLanding", "1", RegistryValueKind.DWord);

            UpdateRegistryKey(RegistryHive.HKCU, @"Software\Microsoft\Siuf\Rules", "NumberOfSIUFInPeriod", "0", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKCU, @"Software\Microsoft\Siuf\Rules", "PeriodInNanoSeconds", "0", RegistryValueKind.DWord);

            UpdateRegistryKey(RegistryHive.HKLM, @"Software\Policies\Microsoft\Windows\DataCollection", "DoNotShowFeedbackNotifications", "1", RegistryValueKind.DWord);

            using (PowerShell ps = PowerShell.Create())
            {
                ps.AddScript("Disable-ScheduledTask -TaskName \"Microsoft\\Windows\\Feedback\\Siuf\\DmClient\"");
                ps.AddScript("Disable-ScheduledTask -TaskName \"Microsoft\\Windows\\Feedback\\Siuf\\DmClientOnScenarioDownload\"");
                ps.Invoke();
            }
        }

        public static void DisableStartMenuSuggestions()
        {
            UpdateRegistryKey(RegistryHive.HKCU, @"Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "SystemPaneSuggestionsEnabled", "0", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKCU, @"Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "SubscribedContent-338389Enabled", "0", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKCU, @"Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "SubscribedContent-338388Enabled", "0", RegistryValueKind.DWord);
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
            UpdateRegistryKey(RegistryHive.HKCU, @"Software\Microsoft\Windows\CurrentVersion\Search", "BingSearchEnabled", "0", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKCU, @"Software\Microsoft\Windows\CurrentVersion\Search", "CortanaConsent", "0", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKCU, @"Software\Microsoft\Windows\CurrentVersion\Search", "DisableWebSearch", "1", RegistryValueKind.DWord);

            UpdateRegistryKey(RegistryHive.HKLM, @"Software\Microsoft\Windows\CurrentVersion\Search", "BingSearchEnabled", "0", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKLM, @"Software\Microsoft\Windows\CurrentVersion\Search", "CortanaConsent", "0", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKLM, @"Software\Microsoft\Windows\CurrentVersion\Search", "DisableWebSearch", "1", RegistryValueKind.DWord);

            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\WOW6432Node\Policies\Microsoft\Windows\Windows Search", "DisableWebSearch", "1", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\WOW6432Node\Policies\Microsoft\Windows\Windows Search", "ConnectedSearchUseWeb", "0", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\WOW6432Node\Policies\Microsoft\Windows\Windows Search", "AllowCloudSearch", "0", RegistryValueKind.DWord);

            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\Policies\Microsoft\Windows\Windows Search", "DisableWebSearch", "1", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\Policies\Microsoft\Windows\Windows Search", "ConnectedSearchUseWeb", "0", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\Policies\Microsoft\Windows\Windows Search", "AllowCloudSearch", "0", RegistryValueKind.DWord);
        }

        public static void DisablePasswordReveal()
        {
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\Policies\Microsoft\Windows\CredUI", "DisablePasswordReveal", "1", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\WOW6432Node\Policies\Microsoft\Windows\CredUI", "DisablePasswordReveal", "1", RegistryValueKind.DWord);
        }

        public static void DisableSettingsSync()
        {
            UpdateRegistryKey(RegistryHive.HKCU, @"Software\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Accessibility", "Enabled", "0", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKCU, @"Software\Microsoft\Windows\CurrentVersion\SettingSync\Groups\BrowserSettings", "Enabled", "0", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKCU, @"Software\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Credentials", "Enabled", "0", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKCU, @"Software\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Language", "Enabled", "0", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKCU, @"Software\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Personalization", "Enabled", "0", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKCU, @"Software\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Windows", "Enabled", "0", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKCU, @"Software\Microsoft\Windows\CurrentVersion\SettingSync\Groups\AppSync", "Enabled", "0", RegistryValueKind.DWord);

            UpdateRegistryKey(RegistryHive.HKCU, @"Software\Microsoft\Windows\CurrentVersion\SettingSync", "SyncPolicy", "5", RegistryValueKind.DWord);
        }

        public static void DisableStartupSound()
        {
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\Microsoft\Windows\CurrentVersion\Authentication\LogonUI\BootAnimation", "DisableStartupSound", "1", RegistryValueKind.DWord);
        }

        public static void DisableAutostartStartupDelay()
        {
            UpdateRegistryKey(RegistryHive.HKCU, @"Software\Microsoft\Windows\CurrentVersion\Explorer\Serialize", "Startupdelayinmsec", "0", RegistryValueKind.DWord);
        }

        public static void DisableLocation()
        {
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\Policies\Microsoft\Windows\LocationAndSensors", "DisableLocation", "1", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\Policies\Microsoft\Windows\LocationAndSensors", "DisableLocationScripting", "1", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\Policies\Microsoft\Windows\LocationAndSensors", "DisableWindowsLocationProvider", "1", RegistryValueKind.DWord);

            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\WOW6432Node\Policies\Microsoft\Windows\LocationAndSensors", "DisableLocation", "1", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\WOW6432Node\Policies\Microsoft\Windows\LocationAndSensors", "DisableLocationScripting", "1", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\WOW6432Node\Policies\Microsoft\Windows\LocationAndSensors", "DisableWindowsLocationProvider", "1", RegistryValueKind.DWord);

            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\Policies\Microsoft\Windows\Windows Search", "AllowSearchToUseLocation", "0", RegistryValueKind.DWord);

            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\WOW6432Node\Policies\Microsoft\Windows\Windows Search", "AllowSearchToUseLocation", "0", RegistryValueKind.DWord);

            UpdateRegistryKey(RegistryHive.HKLM, @"SYSTEM\CurrentControlSet\Services\lfsvc", "Start", "4", RegistryValueKind.DWord);

            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Sensor\Overrides\{BFA794E4-F964-4FDB-90F6-51056BFE4B44}", "SensorPermissionState", "0", RegistryValueKind.DWord);
        }

        public static void DisableDefender()
        {
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\Policies\Microsoft\Windows Defender", "DisableAntiSpyware", "1", RegistryValueKind.DWord);

            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\Policies\Microsoft\Windows Defender\Spynet", "SpyNetReporting", "0", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\Policies\Microsoft\Windows Defender\Spynet", "SubmitSamplesConsent", "2", RegistryValueKind.DWord);

            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\WOW6432Node\Policies\Microsoft\Windows Defender", "DisableAntiSpyware", "1", RegistryValueKind.DWord);

            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\WOW6432Node\Policies\Microsoft\Windows Defender\Spynet", "SpyNetReporting", "0", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\WOW6432Node\Policies\Microsoft\Windows Defender\Spynet", "SubmitSamplesConsent", "2", RegistryValueKind.DWord);

            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            key.DeleteValue("SecurityHealth", false);
        }

        public static void DisableAdvertisingId()
        {
            UpdateRegistryKey(RegistryHive.HKCU, @"Software\Microsoft\Windows\CurrentVersion\AdvertisingInfo", "Enabled", "0", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKLM, @"Software\Microsoft\Windows\CurrentVersion\AdvertisingInfo", "Enabled", "0", RegistryValueKind.DWord);
        }

        public static void DisableMrtReporting()
        {
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\Policies\Microsoft\MRT", "DontReportInfectionInformation", "1", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\WOW6432Node\Policies\Microsoft\MRT", "DontReportInfectionInformation", "1", RegistryValueKind.DWord);
        }

        public static void DisableSendingTypingInfo()
        {
            UpdateRegistryKey(RegistryHive.HKCU, @"Software\Microsoft\Input\TIPC", "Enabled", "0", RegistryValueKind.DWord);

            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\Policies\Microsoft\Windows\TabletPC", "PreventHandwritingDataSharing", "1", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\WOW6432Node\Policies\Microsoft\Windows\TabletPC", "PreventHandwritingDataSharing", "1", RegistryValueKind.DWord);
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
            UpdateRegistryKey(RegistryHive.HKCU, @"Software\Microsoft\Personalization\Settings", "AcceptedPrivacyPolicy", "0", RegistryValueKind.DWord);

            UpdateRegistryKey(RegistryHive.HKCU, @"Software\Microsoft\InputPersonalization", "RestrictImplicitTextCollection", "1", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKCU, @"Software\Microsoft\InputPersonalization", "RestrictImplicitInkCollection", "1", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKCU, @"Software\Microsoft\InputPersonalization", "HarvestContacts", "0", RegistryValueKind.DWord);
        }

        public static void HideLanguageListWebsites()
        {
            UpdateRegistryKey(RegistryHive.HKCU, @"Control Panel\International\User Profile", "HttpAcceptLanguageOptOut", "1", RegistryValueKind.DWord);
        }

        public static void DisableMiracast()
        {
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\Microsoft\MiracastReceiver", "ConsentToast", "2", RegistryValueKind.DWord);
        }

        public static void DisableBluetoothAdvertisements()
        {
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\Microsoft\PolicyManager\current\device\Bluetooth", "AllowAdvertising", "0", RegistryValueKind.DWord);
        }

        public static void DisableClipboardHistory()
        {
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\Policies\Microsoft\Windows\System", "AllowClipboardHistory", "0", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\WOW6432Node\Policies\Microsoft\Windows\System", "AllowClipboardHistory", "0", RegistryValueKind.DWord);
        }

        public static void DisableClipboardCloudSync()
        {
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\Policies\Microsoft\Windows\System", "AllowCrossDeviceClipboard", "0", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\WOW6432Node\Policies\Microsoft\Windows\System", "AllowCrossDeviceClipboard", "0", RegistryValueKind.DWord);
        }

        public static void DisableAutomaticSpeechDataUpdates()
        {
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\Microsoft\Speech_OneCore\Preferences", "ModelDownloadAllowed", "0", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\Policies\Microsoft\Speech", "AllowSpeechModelUpdate", "0", RegistryValueKind.DWord);
        }
        public static void DisableTextMessagesCloudSync()
        {
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\Policies\Microsoft\Windows\Messaging", "AllowMessageSync", "0", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\WOW6432Node\Policies\Microsoft\Windows\Messaging", "AllowMessageSync", "0", RegistryValueKind.DWord);
        }
        public static void DisableHandwritingErrorReports()
        {
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\Policies\Microsoft\Windows\HandwritingErrorReports", "PreventHandwritingErrorReports", "1", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\WOW6432Node\Policies\Microsoft\Windows\HandwritingErrorReports", "PreventHandwritingErrorReports", "1", RegistryValueKind.DWord);
        }

        public static void DisableAppDiagnostics()
        {
            UpdateRegistryKey(RegistryHive.HKCU, @"SOFTWARE\Microsoft\Windows\CurrentVersion\Privacy", "TailoredExperiencesWithDiagnosticDataEnabled", "0", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\Microsoft\Windows\CurrentVersion\Privacy", "TailoredExperiencesWithDiagnosticDataEnabled", "0", RegistryValueKind.DWord);
        }

        public static void DisableSmartScreen()
        {
            UpdateRegistryKey(RegistryHive.HKCU, @"SOFTWARE\Microsoft\Windows\CurrentVersion\AppHost", "EnableWebContentEvaluation", "0", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer", "SmartScreenEnabled", "Off", RegistryValueKind.String);
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\Policies\Microsoft\Windows\System", "EnableSmartScreen", "0", RegistryValueKind.DWord);
        }

        public static void DisableNotificationOnLockScreen()
        {
            UpdateRegistryKey(RegistryHive.HKCU, @"SOFTWARE\Microsoft\Windows\CurrentVersion\Notifications\Settings", "NOC_GLOBAL_SETTING_ALLOW_TOASTS_ABOVE_LOCK", "0", RegistryValueKind.DWord);
        }

        public static void DisableRemindersAndCallsOnLockScreen()
        {
            UpdateRegistryKey(RegistryHive.HKCU, @"SOFTWARE\Microsoft\Windows\CurrentVersion\Notifications\Settings", "NOC_GLOBAL_SETTING_ALLOW_CRITICAL_TOASTS_ABOVE_LOCK", "0", RegistryValueKind.DWord);
        }

        public static void DisableWelcomeExperience()
        {
            UpdateRegistryKey(RegistryHive.HKCU, @"SOFTWARE\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "SubscribedContent-310093Enabled", "0", RegistryValueKind.DWord);
        }

        public static void DisableAeroShake()
        {
            UpdateRegistryKey(RegistryHive.HKCU, @"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "DisallowShaking", "1", RegistryValueKind.DWord);
        }

        public static void DisableTimelineSuggestions()
        {
            UpdateRegistryKey(RegistryHive.HKCU, @"SOFTWARE\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "SubscribedContent-353698Enabled", "0", RegistryValueKind.DWord);
        }

        public static void DisableTypingInsights()
        {
            UpdateRegistryKey(RegistryHive.HKCU, @"SOFTWARE\Microsoft\Input\Settings", "InsightsEnabled", "0", RegistryValueKind.DWord);
        }

        public static void DisableSpellChecker()
        {
            UpdateRegistryKey(RegistryHive.HKCU, @"SOFTWARE\Microsoft\TabletTip\1.7", "EnableAutocorrection", "0", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKCU, @"SOFTWARE\Microsoft\TabletTip\1.7", "EnableSpellchecking", "0", RegistryValueKind.DWord);
        }

        public static void DisableTextSuggestions()
        {
            UpdateRegistryKey(RegistryHive.HKCU, @"SOFTWARE\Microsoft\TabletTip\1.7", "EnableTextPrediction", "0", RegistryValueKind.DWord);
        }

        public static void DisableSafeSearch()
        {
            UpdateRegistryKey(RegistryHive.HKCU, @"SOFTWARE\Microsoft\Windows\CurrentVersion\SearchSettings", "SafeSearchMode", "0", RegistryValueKind.DWord);
        }

        public static void UseWin10RibbonExplorer()
        {
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\Microsoft\Windows\CurrentVersion\Shell Extensions\Blocked", "{e2bf9676-5f8f-435c-97eb-11607a5bedf7}", "", RegistryValueKind.String);
        }

        public static void DisableSuggestedContentInSettings()
        {
            UpdateRegistryKey(RegistryHive.HKCU, @"SOFTWARE\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "SubscribedContent-338393Enabled", "0", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKCU, @"SOFTWARE\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "SubscribedContent-353694Enabled", "0", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKCU, @"SOFTWARE\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "SubscribedContent-353696Enabled", "0", RegistryValueKind.DWord);
        }

        public static void EnableStorageSense()
        {
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\Policies\Microsoft\Windows\StorageSense", "AllowStorageSenseGlobal", "1", RegistryValueKind.DWord);
        }

        public static void DisableAutoLoginAfterUpdates()
        {
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System", "DisableAutomaticRestartSignOn", "1", RegistryValueKind.DWord);
        }

        public static void DisableEdgeFirstRunPage()
        {
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\Policies\Microsoft\MicrosoftEdge\Main", "PreventFirstRunPage", "1", RegistryValueKind.DWord);
        }

        public static void DisableSmartGlass()
        {
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\Microsoft\Windows\CurrentVersion\SmartGlass", "UserAuthPolicy", "0", RegistryValueKind.DWord);
        }

        public static void DisableInkAppSuggestions()
        {
            UpdateRegistryKey(RegistryHive.HKCU, @"SOFTWARE\Microsoft\Windows\CurrentVersion\SmartGlass", "PenWorkspaceAppSuggestionsEnabled", "0", RegistryValueKind.DWord);
        }

        public static void DisableExperiments()
        {
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\Microsoft\PolicyManager\current\device\System", "AllowExperimentation", "0", RegistryValueKind.DWord);
        }

        public static void DisableCameraLockScreen()
        {
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\Policies\Microsoft\Windows\Personalization", "NoLockScreenCamera", "1", RegistryValueKind.DWord);
        }

        public static void DisableApplicationCompatibilityEngine()
        {
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\Policies\Microsoft\Windows\AppCompat", "DisableEngine", "1", RegistryValueKind.DWord);
        }

        public static void DisableWiFiSense()
        {
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\Microsoft\PolicyManager\default\WiFi\AllowWiFiHotSpotReporting", "Value", "0", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\Microsoft\PolicyManager\default\WiFi\AllowAutoConnectToWiFiSenseHotspots", "Value", "0", RegistryValueKind.DWord);

            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\Microsoft\WcmSvc\wifinetworkmanager\config", "AutoConnectAllowedOEM", "0", RegistryValueKind.DWord);
        }

        public static void DisableLockScreenSpotlight()
        {
            UpdateRegistryKey(RegistryHive.HKCU, @"Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "RotatingLockScreenEnabled", "0", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKCU, @"Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "RotatingLockScreenOverlayEnabled", "0", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKCU, @"Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "SubscribedContent-338387Enabled", "0", RegistryValueKind.DWord);

            UpdateRegistryKey(RegistryHive.HKLM, @"Software\Policies\Microsoft\Windows\CloudContent", "DisableWindowsSpotlightFeatures", "1", RegistryValueKind.DWord);
        }

        public static void DisableAutomaticMapsUpdates()
        {
            UpdateRegistryKey(RegistryHive.HKLM, @"SYSTEM\Maps", "AutoUpdateEnabled", "0", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\Policies\Microsoft\Windows\Maps", "AutoDownloadAndUpdateMapData", "0", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\WOW6432Node\Policies\Microsoft\Windows\Maps", "AutoDownloadAndUpdateMapData", "0", RegistryValueKind.DWord);
        }

        public static void DisableErrorReporting()
        {
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\Microsoft\Windows\Windows Error Reporting", "Disabled", "1", RegistryValueKind.DWord);

            using (PowerShell ps = PowerShell.Create())
            {
                ps.AddScript("Disable-ScheduledTask -TaskName \"Microsoft\\Windows\\Windows Error Reporting\\QueueReporting\"");
                ps.Invoke();
            }
        }

        public static void HideNewsAndInterests()
        {
            UpdateRegistryKey(RegistryHive.HKCU, @"Software\Microsoft\Windows\CurrentVersion\Feeds", "ShellFeedsTaskbarViewMode", "2", RegistryValueKind.DWord);
        }

        public static void DisableDefenderSampleFiles()
        {
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\Policies\Microsoft\Windows Defender\Spynet", "SubmitSamplesConsent", "2", RegistryValueKind.DWord);
        }

        public static void DisableMousePointerAcceleration()
        {
            UpdateRegistryKey(RegistryHive.HKCU, @"Control Panel\Mouse", "MouseSpeed", "0", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKCU, @"Control Panel\Mouse", "MouseThreshold1", "0", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKCU, @"Control Panel\Mouse", "MouseThreshold2", "0", RegistryValueKind.DWord);
        }

        public static void DisableFastStartup()
        {
            UpdateRegistryKey(RegistryHive.HKLM, @"SYSTEM\CurrentControlSet\Control\Session Manager\Power", "HiberbootEnabled", "1", RegistryValueKind.DWord);
        }

        public static void DisableRemoteAssistance()
        {
            UpdateRegistryKey(RegistryHive.HKLM, @"SYSTEM\CurrentControlSet\Control\Remote Assistance", "fAllowToGetHelp", "0", RegistryValueKind.DWord);
        }

        public static void UseUtcAsBiosTime()
        {
            UpdateRegistryKey(RegistryHive.HKLM, @"SYSTEM\CurrentControlSet\Control\TimeZoneInformation", "RealTimeIsUniversal", "1", RegistryValueKind.DWord);
        }

        public static void HideNetworkFromLockScreen()
        {
            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\Policies\Microsoft\Windows\System", "DontDisplayNetworkSelectionUI", "1", RegistryValueKind.DWord);
        }

        public static void DisableStickyKeysPrompt()
        {
            UpdateRegistryKey(RegistryHive.HKCU, @"Control Panel\Accessibility\StickyKeys", "Flags", "506", RegistryValueKind.String);
        }

        public static void AddThisPCShortcut()
        {
            UpdateRegistryKey(RegistryHive.HKCU, @"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\HideDesktopIcons\ClassicStartMenu", "{20D04FE0-3AEA-1069-A2D8-08002B30309D}", "0", RegistryValueKind.DWord);
            UpdateRegistryKey(RegistryHive.HKCU, @"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\HideDesktopIcons\NewStartPanel", "{20D04FE0-3AEA-1069-A2D8-08002B30309D}", "0", RegistryValueKind.DWord);
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

            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", "AutoRestartShell", "0", RegistryValueKind.DWord);

            var processes = Process.GetProcessesByName("explorer");
            foreach (var process in processes)
            {
                process.Kill();
                process.WaitForExit();
            }

            Process regeditProcess = Process.Start("regedit.exe", "/s " + tempfolder + "startlayout.reg");
            regeditProcess.WaitForExit();

            Process.Start("explorer");

            UpdateRegistryKey(RegistryHive.HKLM, @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", "AutoRestartShell", "1", RegistryValueKind.DWord);
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
            String url1 = "https://www.nuget.org/api/v2/package/Microsoft.UI.Xaml/2.7.3";
            String url2 = "https://github.com/microsoft/winget-cli/releases/download/v1.4.11071/Microsoft.DesktopAppInstaller_8wekyb3d8bbwe.msixbundle";
            using (WebClient client = new WebClient())
            {
                FileInfo file1 = new FileInfo(MainForm.Tmpfolder + "microsoft.ui.xaml.zip");
                FileInfo file2 = new FileInfo(MainForm.Tmpfolder + "winget.msixbundle");

                DialogResult result = DialogResult.Retry;
                while (result == DialogResult.Retry)
                {
                    try
                    {
                        client.DownloadFile(new Uri(url1), file1.FullName);
                        client.DownloadFile(new Uri(url2), file2.FullName);

                        System.IO.Compression.ZipFile.ExtractToDirectory(file1.FullName, MainForm.Tmpfolder + "microsoft-ui-xaml");

                        using (PowerShell ps = PowerShell.Create())
                        {
                            ps.AddScript("Add-AppxPackage -Path https://aka.ms/Microsoft.VCLibs.x64.14.00.Desktop.appx");
                            ps.AddScript("Add-AppxPackage -Path " + MainForm.Tmpfolder + @"microsoft-ui-xaml\tools\AppX\x64\Release\Microsoft.UI.Xaml.2.7.appx");
                            ps.AddScript("Add-AppxPackage -Path " + file2.FullName);
                            ps.Invoke();
                            if (ps.HadErrors)
                            {
                                using (StreamWriter sw = new StreamWriter("WinSlap-Error.log", true))  // creates or appends to existing file
                                {
                                    foreach (ErrorRecord error in ps.Streams.Error)
                                    {
                                        sw.WriteLine(error.ToString());
                                    }
                                }
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
                        string errorMessage = $"A WebException occured trying to download a required file.\n\nPlease check your network connection and report this issue on GitHub if the error persists";
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
            startInfo.Arguments = "/C winget install --silent --accept-package-agreements --accept-source-agreements --id " + packageid;
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();
            if (process.ExitCode != 0)
            {
                string caption = "Something went wrong...";
                string errorMessage = $"WinSlap failed to install the following software using winget: {packageid}\n\nError code: {process.ExitCode}\n\nPlease report this issue on GitHub if the error persists.";
                MessageBox.Show(new Form { TopMost = true }, errorMessage, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void Install7Zip()
        {
            WinGetInstall("7zip.7zip");
        }

        public static void InstallAdobeReaderDC()
        {
            WinGetInstall("Adobe.Acrobat.Reader.64-bit");
        }

        public static void InstallAudacity()
        {
            WinGetInstall("Audacity.Audacity");
        }

        public static void InstallBalenaEtcher()
        {
            WinGetInstall("Balena.Etcher");
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
            WinGetInstall("TimKosse.FileZilla.Client");
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

        public static void InstallOpenHashTab()
        {
            WinGetInstall("namazso.OpenHashTab");
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

        public static void InstallKeePassXC()
        {
            WinGetInstall("KeePassXCTeam.KeePassXC");

            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            key.DeleteValue("KeePassXC", false);
        }

        public static void InstallLibreOffice()
        {
            WinGetInstall("TheDocumentFoundation.LibreOffice");
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
            WinGetInstall("Python.Python.3");
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

        public static void InstallWireguard()
        {
            WinGetInstall("Wireguard.Wireguard");
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
