using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinSlap
{
    public sealed partial class MainForm : Form
    {
        private SlapForm SlapForm;

        public static readonly string Tmpfolder = Path.GetTempPath() + @"WinSlap\";
        private bool _restart = true;

        public MainForm(string[] args)
        {
            const string strNoRestart = "norestart";
            if (args.Any(strNoRestart.Contains)) { this._restart = false; }
            InitializeComponent();
            MinimumSize = new System.Drawing.Size(332, 173);
        }

        private object[] GetSelectedItems(CheckedListBox tweaks, CheckedListBox appearance, CheckedListBox software, CheckedListBox advanced)
        {
            return tweaks.CheckedItems.OfType<object>()
                .Concat(appearance.CheckedItems.OfType<object>())
                .Concat(software.CheckedItems.OfType<object>())
                .Concat(advanced.CheckedItems.OfType<object>())
                .ToArray();
        }

        private int CountTotalCheckedItems(CheckedListBox tweaks, CheckedListBox appearance, CheckedListBox software, CheckedListBox advanced)
        {
            return tweaks.CheckedItems.Count
                + software.CheckedItems.Count
                + advanced.CheckedItems.Count
                + appearance.CheckedItems.Count;
        }

        // ToDo: implement cancel check
        private async Task DoWorkAsync(object[] items, int totalCheckedItems, IProgress<ProgressReport> progress)
        {
            int totalItemsDone = 0;

            try
            {
                for (int x = 0; x <= items.Length - 1; x++)
                {
                    string boxcontent = items[x].ToString();
                    await ApplySlapAsync(boxcontent);
                    totalItemsDone++;
                    double progresspercent = totalItemsDone / totalCheckedItems;
                    int percent = (int)Math.Ceiling(progresspercent * 100);
                    progress?.Report(new ProgressReport { PercentComplete = percent, CurrentJob = boxcontent });
                }
            }
            catch (Exception ex)
            {
                string caption = "Something went wrong...";
                string errorMessage = "Exception during slapping process.\n\n" + ex + "\n\nPlease report this issue on GitHub. Slapping will continue after closing this message.";
                MessageBox.Show(new Form { TopMost = true }, errorMessage, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public async void StartWork()
        {
            var progressIndicator = new Progress<ProgressReport>(report =>
            {
                SlapForm.CurrentJobText = report.CurrentJob;
                SlapForm.PercentFinished = report.PercentComplete;
            });
            object[] items;
            int totalCheckedItems;

            if (Globals.winmajor == "11")
            {
                items = GetSelectedItems(checkedListBoxWin11Tweaks, checkedListBoxWin11Appearance, checkedListBoxWin11Software, checkedListBoxWin11Advanced);
                totalCheckedItems = CountTotalCheckedItems(checkedListBoxWin11Tweaks, checkedListBoxWin11Software, checkedListBoxWin11Advanced, checkedListBoxWin11Appearance);
            }
            else
            {
                items = GetSelectedItems(checkedListBoxWin10Tweaks, checkedListBoxWin10Appearance, checkedListBoxWin10Software, checkedListBoxWin10Advanced);
                totalCheckedItems = CountTotalCheckedItems(checkedListBoxWin10Tweaks, checkedListBoxWin10Software, checkedListBoxWin10Advanced, checkedListBoxWin10Appearance);
            }

            await DoWorkAsync(items, totalCheckedItems, progressIndicator);

            if (_restart) RestartNow();
            else
            {
                SlapForm.Dispose();
                string caption = "Slapping finished!";
                string errorMessage = "This message is shown because you are skipping the reboot.\n(shift-klick or norestart argument)";
                MessageBox.Show(errorMessage, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                Environment.Exit(0);
            }
        }

        private void ButtonSlap_Click(object sender, EventArgs e)
        {
            if (ModifierKeys == Keys.Shift) _restart = false;

            int totalCheckedItems;
            if (Globals.winmajor == "11")
            {
                totalCheckedItems = CountTotalCheckedItems(checkedListBoxWin11Tweaks, checkedListBoxWin11Software, checkedListBoxWin11Advanced, checkedListBoxWin11Appearance);
            }
            else
            {
                totalCheckedItems = CountTotalCheckedItems(checkedListBoxWin10Tweaks, checkedListBoxWin10Software, checkedListBoxWin10Advanced, checkedListBoxWin10Appearance);
            }

            if (totalCheckedItems == 0)
            {
                string caption = "Notice";
                string errorMessage = "No items selected.";
                MessageBox.Show(errorMessage, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ShowDisclaimer()) return;

            Hide();
            SlapForm = new SlapForm();
            SlapForm.Show();

            StartWork();

            if (checkedListBoxWin10Software.CheckedItems.Count != 0 || checkedListBoxWin11Software.CheckedItems.Count != 0)
            {
                Process process1 = new Process();
                ProcessStartInfo startInfo1 = new ProcessStartInfo();
                startInfo1.WindowStyle = ProcessWindowStyle.Hidden;
                startInfo1.FileName = "cmd.exe";
                startInfo1.Arguments = "/C where winget";
                process1.StartInfo = startInfo1;
                process1.Start();
                process1.WaitForExit();
                if (process1.ExitCode != 0)
                {
                    Slapper.InstallWinGet();
                }

                Process process2 = new Process();
                ProcessStartInfo startInfo2 = new ProcessStartInfo();
                startInfo2.WindowStyle = ProcessWindowStyle.Hidden;
                startInfo2.FileName = "cmd.exe";
                startInfo2.Arguments = "/C winget --version";
                startInfo2.UseShellExecute = false;
                startInfo2.RedirectStandardOutput = true;
                process2.StartInfo = startInfo2;
                process2.Start();
                string output2 = process2.StandardOutput.ReadToEnd();
                process2.WaitForExit();
                if (output2.Contains("v1.0") || output2.Contains("v1.1") || output2.Contains("v1.2"))
                {
                    Slapper.InstallWinGet();
                }
            }
        }

        private async Task ApplySlapAsync(string boxcontent)
        {
            try
            {
                switch (boxcontent)
                {
                    case "Remove preinstalled apps except Photos, Calculator, Store":
                        await Task.Run(() => Slapper.RemovePreinstalledApps());
                        break;
                    case "Disable Shared Experiences":
                        await Task.Run(() => Slapper.DisableSharedExperiences());
                        break;
                    case "Disable Cortana":
                        await Task.Run(() => Slapper.DisableCortana());
                        break;
                    case "Disable Game DVR and Game Bar":
                        await Task.Run(() => Slapper.DisableGameDvr());
                        break;
                    case "Disable Hotspot 2.0":
                        await Task.Run(() => Slapper.DisableHotspot20());
                        break;
                    case "Don't include frequently used folders in Quick access":
                        await Task.Run(() => Slapper.NoQuickAccess());
                        break;
                    case "Don't show sync provider notifications":
                        await Task.Run(() => Slapper.HideSyncNotifications());
                        break;
                    case "Disable Sharing Wizard":
                        await Task.Run(() => Slapper.DisableSharingWizard());
                        break;
                    case "Show 'This PC' when launching File Explorer":
                        await Task.Run(() => Slapper.LaunchThisPcFileExplorer());
                        break;
                    case "Disable Telemetry":
                        await Task.Run(() => Slapper.DisableTelemetry());
                        break;
                    case "Uninstall OneDrive":
                        await Task.Run(() => Slapper.UninstallOneDrive());
                        break;
                    case "Disable Activity History":
                        await Task.Run(() => Slapper.DisableActivityHistory());
                        break;
                    case "Disable automatically installing Apps":
                        await Task.Run(() => Slapper.DisableAutomaticAppInstall());
                        break;
                    case "Disable Feedback dialogs":
                        await Task.Run(() => Slapper.DisableFeedbackDialogs());
                        break;
                    case "Disable Start Menu suggestions":
                        await Task.Run(() => Slapper.DisableStartMenuSuggestions());
                        break;
                    case "Disable Bing search":
                        await Task.Run(() => Slapper.DisableBingSearch());
                        break;
                    case "Disable password reveal button":
                        await Task.Run(() => Slapper.DisablePasswordReveal());
                        break;
                    case "Disable settings sync":
                        await Task.Run(() => Slapper.DisableSettingsSync());
                        break;
                    case "Disable startup sound":
                        await Task.Run(() => Slapper.DisableStartupSound());
                        break;
                    case "Disable autostart startup delay":
                        await Task.Run(() => Slapper.DisableAutostartStartupDelay());
                        break;
                    case "Disable location":
                        await Task.Run(() => Slapper.DisableLocation());
                        break;
                    case "Disable Advertising ID":
                        await Task.Run(() => Slapper.DisableAdvertisingId());
                        break;
                    case "Disable Malware Removal Tool data reporting":
                        await Task.Run(() => Slapper.DisableMrtReporting());
                        break;
                    case "Disable sending typing info to Microsoft":
                        await Task.Run(() => Slapper.DisableSendingTypingInfo());
                        break;
                    case "Disable Personalization":
                        await Task.Run(() => Slapper.DisablePersonalization());
                        break;
                    case "Hide language list from websites":
                        await Task.Run(() => Slapper.HideLanguageListWebsites());
                        break;
                    case "Disable Miracast":
                        await Task.Run(() => Slapper.DisableMiracast());
                        break;
                    case "Disable App Diagnostics":
                        await Task.Run(() => Slapper.DisableAppDiagnostics());
                        break;
                    case "Disable Wi-Fi Sense":
                        await Task.Run(() => Slapper.DisableWiFiSense());
                        break;
                    case "Disable lock screen Spotlight":
                        await Task.Run(() => Slapper.DisableLockScreenSpotlight());
                        break;
                    case "Disable automatic maps updates":
                        await Task.Run(() => Slapper.DisableAutomaticMapsUpdates());
                        break;
                    case "Disable error reporting":
                        await Task.Run(() => Slapper.DisableErrorReporting());
                        break;
                    case "Disable Remote Assistance":
                        await Task.Run(() => Slapper.DisableRemoteAssistance());
                        break;
                    case "Use UTC as BIOS time":
                        await Task.Run(() => Slapper.UseUtcAsBiosTime());
                        break;
                    case "Hide network from lock screen":
                        await Task.Run(() => Slapper.HideNetworkFromLockScreen());
                        break;
                    case "Disable sticky keys prompt":
                        await Task.Run(() => Slapper.DisableStickyKeysPrompt());
                        break;
                    case "Hide 3D Objects from File Explorer":
                        await Task.Run(() => Slapper.Hide3DObjectsFileExplorer());
                        break;
                    case "Prevent preinstalling apps for new users":
                        await Task.Run(() => Slapper.PreventPreinstallingApps());
                        break;
                    case "Unpin preinstalled apps":
                        await Task.Run(() => Slapper.UnpinPreinstalledApps());
                        break;
                    case "Disable Smart Screen":
                        await Task.Run(() => Slapper.DisableSmartScreen());
                        break;
                    case "Disable Smart Glass":
                        await Task.Run(() => Slapper.DisableSmartGlass());
                        break;
                    case "Remove Intel Control Panel from context menus":
                        await Task.Run(() => Slapper.RemoveIntelContextMenu());
                        break;
                    case "Remove NVIDIA Control Panel from context menus":
                        await Task.Run(() => Slapper.RemoveNvidiaContextMenu());
                        break;
                    case "Remove AMD Control Panel from context menus":
                        await Task.Run(() => Slapper.RemoveAmdContextMenu());
                        break;
                    case "Disable suggested apps in Windows Ink Workspace":
                        await Task.Run(() => Slapper.DisableInkAppSuggestions());
                        break;
                    case "Disable experiments by Microsoft":
                        await Task.Run(() => Slapper.DisableExperiments());
                        break;
                    case "Disable Inventory Collection":
                        await Task.Run(() => Slapper.DisableInventoryCollection());
                        break;
                    case "Disable Steps Recorder":
                        await Task.Run(() => Slapper.DisableStepsRecorder());
                        break;
                    case "Disable Application Compatibility Engine":
                        await Task.Run(() => Slapper.DisableCompatibilityAssistant());
                        break;
                    case "Disable pre-release features and settings":
                        await Task.Run(() => Slapper.DisablePreReleaseFeatures());
                        break;
                    case "Disable camera on lock screen":
                        await Task.Run(() => Slapper.DisableCameraLockScreen());
                        break;
                    case "Disable Microsoft Edge first run page":
                        await Task.Run(() => Slapper.DisableEdgeFirstRunPage());
                        break;
                    case "Disable Microsoft Edge preload":
                        await Task.Run(() => Slapper.DisableEdgePreload());
                        break;
                    case "Install .NET Framework 2.0, 3.0 and 3.5":
                        await Task.Run(() => Slapper.InstallNetFrameworks());
                        break;
                    case "Update Windows Store apps":
                        await Task.Run(() => Slapper.UpdateStoreApps());
                        break;
                    case "Enable Windows Photo Viewer":
                        await Task.Run(() => Slapper.EnablePhotoViewer());
                        break;
                    case "Uninstall Microsoft XPS Document Writer":
                        await Task.Run(() => Slapper.UninstallXPSWriter());
                        break;
                    case "Disable security questions for local accounts":
                        await Task.Run(() => Slapper.DisableSecurityQuestions());
                        break;
                    case "Disable app suggestions (e.g. use Edge instead of Firefox)":
                        await Task.Run(() => Slapper.DisableAppSuggestions());
                        break;
                    case "Remove default Fax printer":
                        await Task.Run(() => Slapper.RemoveFaxPrinter());
                        break;
                    case "Remove Microsoft XPS Document Writer":
                        await Task.Run(() => Slapper.RemoveXPSDocumentWriter());
                        break;
                    case "Disable clipboard history":
                        await Task.Run(() => Slapper.DisableClipboardHistory());
                        break;
                    case "Disable cloud sync of clipboard history":
                        await Task.Run(() => Slapper.DisableClipboardCloudSync());
                        break;
                    case "Disable automatic update of speech data":
                        await Task.Run(() => Slapper.DisableAutomaticSpeechDataUpdates());
                        break;
                    case "Disable handwriting error reports":
                        await Task.Run(() => Slapper.DisableHandwritingErrorReports());
                        break;
                    case "Disable cloud sync of text messages":
                        await Task.Run(() => Slapper.DisableTextMessagesCloudSync());
                        break;
                    case "Disable Bluetooth advertisements":
                        await Task.Run(() => Slapper.DisableBluetoothAdvertisements());
                        break;
                    case "Disable Windows Media DRM internet access":
                        await Task.Run(() => Slapper.DisableDRMInternetAccess());
                        break;
                    case "Disable Get even more out of Windows screen":
                        await Task.Run(() => Slapper.DisableGetEvenMoreOutOfWindows());
                        break;
                    case "Set power plan to high performance":
                        await Task.Run(() => Slapper.SetPowerPlanHighPerformance());
                        break;
                    case "Add This PC shortcut to desktop":
                        await Task.Run(() => Slapper.AddThisPCShortcut());
                        break;
                    case "Small taskbar icons":
                        await Task.Run(() => Slapper.TaskbarSmallIcons());
                        break;
                    case "Don't group tasks in taskbar":
                        await Task.Run(() => Slapper.DoNotGroupTasks());
                        break;
                    case "Hide Taskview button in taskbar":
                        await Task.Run(() => Slapper.HideTaskview());
                        break;
                    case "Hide People button in taskbar":
                        await Task.Run(() => Slapper.DisablePeopleBand());
                        break;
                    case "Hide search bar in taskbar":
                        await Task.Run(() => Slapper.HideSearch());
                        break;
                    case "Remove compatibility item from context menu":
                        await Task.Run(() => Slapper.RemoveCompatibility());
                        break;
                    case "Hide OneDrive Cloud states in File Explorer":
                        await Task.Run(() => Slapper.DisableCloudStates());
                        break;
                    case "Always show file name extensions":
                        await Task.Run(() => Slapper.ShowFilenameExtensions());
                        break;
                    case "Remove OneDrive from File Explorer":
                        await Task.Run(() => Slapper.HideOneDriveFileExplorer());
                        break;
                    case "Delete quicklaunch items":
                        await Task.Run(() => Slapper.DeleteQuicklaunchItems());
                        break;
                    case "Use Windows 7 volume control":
                        await Task.Run(() => Slapper.UseWin7Volume());
                        break;
                    case "Remove Microsoft Edge desktop shortcut":
                        await Task.Run(() => Slapper.RemoveEdgeShortcut());
                        break;
                    case "Disable Lockscreen Blur":
                        await Task.Run(() => Slapper.DisableLockscreenBlur());
                        break;
                    case "Hide Meet Now icon in taskbar":
                        await Task.Run(() => Slapper.HideMeetNow());
                        break;
                    case "Hide News and interests in taskbar":
                        await Task.Run(() => Slapper.HideNewsAndInterests());
                        break;
                    case "Disable notifications on the lock screen":
                        await Task.Run(() => Slapper.DisableNotificationOnLockScreen());
                        break;
                    case "Disable reminders and incoming VoIP calls on the lock screen":
                        await Task.Run(() => Slapper.DisableRemindersAndCallsOnLockScreen());
                        break;
                    case "Disable Windows welcome experience":
                        await Task.Run(() => Slapper.DisableWelcomeExperience());
                        break;
                    case "Disable Aero Shake":
                        await Task.Run(() => Slapper.DisableAeroShake());
                        break;
                    case "Disable suggestions in timeline":
                        await Task.Run(() => Slapper.DisableTimelineSuggestions());
                        break;
                    case "Disable typing insights":
                        await Task.Run(() => Slapper.DisableTypingInsights());
                        break;
                    case "Disable spell checker":
                        await Task.Run(() => Slapper.DisableSpellChecker());
                        break;
                    case "Disable text suggestions on the software keyboard":
                        await Task.Run(() => Slapper.DisableTextSuggestions());
                        break;
                    case "Disable SafeSearch":
                        await Task.Run(() => Slapper.DisableSafeSearch());
                        break;
                    case "Disable suggested content in settings app":
                        await Task.Run(() => Slapper.DisableSuggestedContentInSettings());
                        break;
                    case "Disable automatic login after finishing updates":
                        await Task.Run(() => Slapper.DisableAutoLoginAfterUpdates());
                        break;
                    case "Disable Windows Defender submitting sample files":
                        await Task.Run(() => Slapper.DisableDefenderSampleFiles());
                        break;
                    case "Use Windows 10 ribbon bar in Windows Explorer":
                        await Task.Run(() => Slapper.UseWin10RibbonExplorer());
                        break;
                    case "Install 7Zip":
                        await Task.Run(() => Slapper.Install7Zip());
                        break;
                    case "Install Adobe Acrobat Reader DC":
                        await Task.Run(() => Slapper.InstallAdobeReaderDC());
                        break;
                    case "Install Audacity":
                        await Task.Run(() => Slapper.InstallAudacity());
                        break;
                    case "Install BalenaEtcher":
                        await Task.Run(() => Slapper.InstallBalenaEtcher());
                        break;
                    case "Install calibre":
                        await Task.Run(() => Slapper.InstallCalibre());
                        break;
                    case "Install CPU-Z":
                        await Task.Run(() => Slapper.InstallCPUZ());
                        break;
                    case "Install Discord":
                        await Task.Run(() => Slapper.InstallDiscord());
                        break;
                    case "Install DupeGuru":
                        await Task.Run(() => Slapper.InstallDupeGuru());
                        break;
                    case "Install EarTrumpet":
                        await Task.Run(() => Slapper.InstallEarTrumpet());
                        break;
                    case "Install Epic Games Launcher":
                        await Task.Run(() => Slapper.InstallEpicGamesLauncher());
                        break;
                    case "Install Everything Search":
                        await Task.Run(() => Slapper.InstallEverythingSearch());
                        break;
                    case "Install f.lux":
                        await Task.Run(() => Slapper.InstallFlux());
                        break;
                    case "Install GIMP":
                        await Task.Run(() => Slapper.InstallGIMP());
                        break;
                    case "Install GPU-Z":
                        await Task.Run(() => Slapper.InstallGPUZ());
                        break;
                    case "Install Git":
                        await Task.Run(() => Slapper.InstallGit());
                        break;
                    case "Install Google Chrome":
                        await Task.Run(() => Slapper.InstallGoogleChrome());
                        break;
                    case "Install Inkscape":
                        await Task.Run(() => Slapper.InstallInkscape());
                        break;
                    case "Install Irfanview":
                        await Task.Run(() => Slapper.InstallIrfanview());
                        break;
                    case "Install Java Runtime Environment":
                        await Task.Run(() => Slapper.InstallJavaRE());
                        break;
                    case "Install KeePassXC":
                        await Task.Run(() => Slapper.InstallKeePassXC());
                        break;
                    case "Install LibreOffice":
                        await Task.Run(() => Slapper.InstallLibreOffice());
                        break;
                    case "Install Minecraft":
                        await Task.Run(() => Slapper.InstallMinecraft());
                        break;
                    case "Install Mozilla Firefox":
                        await Task.Run(() => Slapper.InstallFirefox());
                        break;
                    case "Install Mozilla Thunderbird":
                        await Task.Run(() => Slapper.InstallThunderbird());
                        break;
                    case "Install Nextcloud Desktop":
                        await Task.Run(() => Slapper.InstallNextcloudDesktop());
                        break;
                    case "Install Notepad++":
                        await Task.Run(() => Slapper.InstallNotepadPlusPlus());
                        break;
                    case "Install OBS Studio":
                        await Task.Run(() => Slapper.InstallOBSStudio());
                        break;
                    case "Install OpenHashTab":
                        await Task.Run(() => Slapper.InstallOpenHashTab());
                        break;
                    case "Install OpenVPN Connect":
                        await Task.Run(() => Slapper.InstallOpenVPNConnect());
                        break;
                    case "Install PowerToys":
                        await Task.Run(() => Slapper.InstallPowerToys());
                        break;
                    case "Install PuTTY":
                        await Task.Run(() => Slapper.InstallPuTTY());
                        break;
                    case "Install Python 3.11":
                        await Task.Run(() => Slapper.InstallPython311());
                        break;
                    case "Install Skype":
                        await Task.Run(() => Slapper.InstallSkype());
                        break;
                    case "Install Slack":
                        await Task.Run(() => Slapper.InstallSlack());
                        break;
                    case "Install Speccy":
                        await Task.Run(() => Slapper.InstallSpeccy());
                        break;
                    case "Install Steam":
                        await Task.Run(() => Slapper.InstallSteam());
                        break;
                    case "Install TeamViewer":
                        await Task.Run(() => Slapper.InstallTeamViewer());
                        break;
                    case "Install TeamSpeak":
                        await Task.Run(() => Slapper.InstallTeamSpeak());
                        break;
                    case "Install Telegram":
                        await Task.Run(() => Slapper.InstallTelegram());
                        break;
                    case "Install Ubisoft Connect":
                        await Task.Run(() => Slapper.InstallUbisoftConnect());
                        break;
                    case "Install VirtualBox":
                        await Task.Run(() => Slapper.InstallVirtualBox());
                        break;
                    case "Install Visual Studio Code":
                        await Task.Run(() => Slapper.InstallVSCode());
                        break;
                    case "Install VLC media player":
                        await Task.Run(() => Slapper.InstallVlc());
                        break;
                    case "Install WinRAR":
                        await Task.Run(() => Slapper.InstallWinRAR());
                        break;
                    case "Install WinSCP":
                        await Task.Run(() => Slapper.InstallWinSCP());
                        break;
                    case "Install Windows Terminal":
                        await Task.Run(() => Slapper.InstallWindowsTerminal());
                        break;
                    case "Install Wireguard":
                        await Task.Run(() => Slapper.InstallWireguard());
                        break;
                    case "Install Wireshark":
                        await Task.Run(() => Slapper.InstallWireshark());
                        break;
                    case "Install Zoom":
                        await Task.Run(() => Slapper.InstallZoom());
                        break;
                    case "Disable Background Apps":
                        await Task.Run(() => Slapper.DisableBackgroundApps());
                        break;
                    case "Precision Trackpad: Disable keyboard block after clicking":
                        await Task.Run(() => Slapper.DisableBlockPrecisionTrackpad());
                        break;
                    case "Disable Windows Defender":
                        await Task.Run(() => Slapper.DisableDefender());
                        break;
                    case "Disable Link-local Multicast Name Resolution":
                        await Task.Run(() => Slapper.DisableLLMNR());
                        break;
                    case "Disable Smart Multi-Homed Name Resolution":
                        await Task.Run(() => Slapper.DisableSmartNameResolution());
                        break;
                    case "Disable Web Proxy Auto-Discovery":
                        await Task.Run(() => Slapper.DisableWPAD());
                        break;
                    case "Disable Teredo tunneling":
                        await Task.Run(() => Slapper.DisableTeredo());
                        break;
                    case "Disable Intra-Site Automatic Tunnel Addressing Protocol":
                        await Task.Run(() => Slapper.DisableISATAP());
                        break;
                    case "Enable Windows Subsystem for Linux":
                        await Task.Run(() => Slapper.EnableWSL());
                        break;
                    case "Uninstall Internet Explorer":
                        await Task.Run(() => Slapper.UninstallInternetExplorer());
                        break;
                    case "Enable Storage Sense":
                        await Task.Run(() => Slapper.EnableStorageSense());
                        break;
                    case "Disable fast startup":
                        await Task.Run(() => Slapper.DisableFastStartup());
                        break;
                    case "Disable mouse pointer acceleration":
                        await Task.Run(() => Slapper.DisableMousePointerAcceleration());
                        break;
                    default:
                        string caption = "Something went wrong...";
                        string errorMessage = "Item not found (" + boxcontent + ")\n\nPlease report this issue on GitHub. Slapping will continue after closing this message.";
                        MessageBox.Show(new Form { TopMost = true }, errorMessage, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }

            catch (NullReferenceException ex)
            {
                string caption = "Something went wrong...";
                string errorMessage = "NullReferenceException in \"" + boxcontent + "\"\n\n" + ex + "\n\nPlease report this issue on GitHub. Slapping will continue after closing this message.";
                MessageBox.Show(new Form { TopMost = true }, errorMessage, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void WinSlap_Load(object sender, EventArgs e)
        {
            InitParameterNotice();

            AppDomain.CurrentDomain.ProcessExit += OnProcessExit;
            Directory.CreateDirectory(Tmpfolder);

            labelOS.Text = $"Windows {Globals.winmajor} ({Globals.winrelease})";

            if (Globals.winmajor == "11")
            {
                tabControlWin11.Visible = true;
                tabControlWin10.Visible = false;
            } else
            {
                tabControlWin11.Visible = false;
                tabControlWin10.Visible = true;
            }
        }

        private static void RestartNow()
        {
            ProcessStartInfo proc = new ProcessStartInfo
            {
                FileName = "cmd",
                WindowStyle = ProcessWindowStyle.Hidden,
                Arguments = "/C shutdown -f -r -t 0"
            };
            Process.Start(proc);
        }

        private void OnProcessExit(object sender, EventArgs e)
        {
            Directory.Delete(Tmpfolder, true);
        }

        private void InitParameterNotice()
        {
            parameternotice.Text = "";
            if (!_restart)
            {
                parameternotice.Visible = true;
                parameternotice.Text += " NoRestart";
            }
        }

        private static bool ShowDisclaimer()
        {
            string caption = "Important";
            string disclaimer = "- All changes are made at your own risk.\n" +
                    "- There is no easy way to revert the changes.\n" +
                    "- Your PC will restart immediately after the changes have been made.\n \n" +
                    "Are you ready to slap?";

            if (MessageBox.Show(disclaimer, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                return true;
            }
            return false;
        }

        private void ButtonUncheckTweaks_Click(object sender, EventArgs e)
        {
            CheckAll(checkedListBoxWin11Tweaks, false);
        }

        private void ButtonCheckTweaks_Click(object sender, EventArgs e)
        {
            CheckAll(checkedListBoxWin11Tweaks, true);
        }

        private void ButtonCheckSoftware_Click(object sender, EventArgs e)
        {
            CheckAll(checkedListBoxWin11Software, true);
        }

        private void ButtonUncheckSoftware_Click(object sender, EventArgs e)
        {
            CheckAll(checkedListBoxWin11Software, false);
        }

        private void ButtonCheckAdvanced_Click(object sender, EventArgs e)
        {
            CheckAll(checkedListBoxWin11Advanced, true);
        }

        private void ButtonUncheckAdvanced_Click(object sender, EventArgs e)
        {
            CheckAll(checkedListBoxWin11Advanced, false);
        }

        private void CheckAll(CheckedListBox list, bool check)
        {
            for (int i = 0; i < list.Items.Count; i++)
            {
                list.SetItemChecked(i, check);
            }
        }

        private void ButtonCheckAppearance_Click(object sender, EventArgs e)
        {
            CheckAll(checkedListBoxWin11Appearance, true);
        }

        private void ButtonUncheckAppearance_Click(object sender, EventArgs e)
        {
            CheckAll(checkedListBoxWin11Appearance, false);
        }

        private void LinkGitHub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/svenmauch/WinSlap");
        }

        private void buttonWin10CheckTweaks_Click(object sender, EventArgs e)
        {
            CheckAll(checkedListBoxWin10Tweaks, true);
        }

        private void buttonWin10UncheckTweaks_Click(object sender, EventArgs e)
        {
            CheckAll(checkedListBoxWin10Tweaks, false);
        }

        private void buttonWin10CheckAppearance_Click(object sender, EventArgs e)
        {
            CheckAll(checkedListBoxWin10Appearance, true);
        }

        private void buttonWin10UncheackAppearance_Click(object sender, EventArgs e)
        {
            CheckAll(checkedListBoxWin10Appearance, false);
        }

        private void buttonWin10CheckSoftware_Click(object sender, EventArgs e)
        {
            CheckAll(checkedListBoxWin10Software, true);
        }

        private void buttonWin10UncheckSoftware_Click(object sender, EventArgs e)
        {
            CheckAll(checkedListBoxWin10Software, false);
        }

        private void buttonWin10CheckAdvanced_Click(object sender, EventArgs e)
        {
            CheckAll(checkedListBoxWin10Advanced, true);
        }

        private void buttonWin10UncheckAdvanced_Click(object sender, EventArgs e)
        {
            CheckAll(checkedListBoxWin10Advanced, false);
        }
    }
}
