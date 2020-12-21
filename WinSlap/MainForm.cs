using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace WinSlap
{
    public sealed partial class MainForm : Form
    {
        BackgroundWorker backgroundWorker1 = new BackgroundWorker();
        private SlapForm SlapForm;

        public static readonly string Tmpfolder = Path.GetTempPath() + @"WinSlap\";
        private bool _restart = true;

        public MainForm(string[] args)
        {
            const string strNoRestart = "norestart";
            if (args.Any(strNoRestart.Contains)) { this._restart = false; }
            InitializeComponent();
            MinimumSize = new System.Drawing.Size(332, 173);

            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker1.RunWorkerCompleted+=new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
            backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);
        }

        void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            SlapForm.CurrentJobText = (String)e.UserState;
            SlapForm.PercentFinished = e.ProgressPercentage;
        }

        // ToDo: implement cancel check
        void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            object[] arrayTweaks = checkedListBoxTweaks.CheckedItems.OfType<object>().ToArray();
            object[] arrayAppearance = checkedListBoxAppearance.CheckedItems.OfType<object>().ToArray();
            object[] arraySoftware = checkedListBoxSoftware.CheckedItems.OfType<object>().ToArray();
            object[] arrayAdvanced = checkedListBoxAdvanced.CheckedItems.OfType<object>().ToArray();

            object[] items = arrayTweaks.Concat(arrayAppearance).Concat(arraySoftware).Concat(arrayAdvanced).ToArray();

            int totalCheckedItems = checkedListBoxTweaks.CheckedItems.Count + checkedListBoxSoftware.CheckedItems.Count + checkedListBoxAdvanced.CheckedItems.Count + checkedListBoxAppearance.CheckedItems.Count;
            int totalItemsDone = 0;
            double progresspercent;
            int progress = 0;

            for (int x = 0; x <= items.Length - 1; x++)
            {
                string boxcontent = items[x].ToString();
                backgroundWorker1.ReportProgress(progress, boxcontent);
                ApplySlap(boxcontent);
                totalItemsDone++;
                progresspercent = (double)totalItemsDone / (double)totalCheckedItems;
                progress = (int)Math.Ceiling(progresspercent * 100);
                backgroundWorker1.ReportProgress(progress);
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                MessageBox.Show("You've cancelled the slapping! :(");
            }
            else
            {
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
        }

        private void ButtonSlap_Click(object sender, EventArgs e)
        {
            if (ModifierKeys == Keys.Shift) _restart = false;

            int totalCheckedItems = checkedListBoxTweaks.CheckedItems.Count + checkedListBoxSoftware.CheckedItems.Count + checkedListBoxAdvanced.CheckedItems.Count + checkedListBoxAppearance.CheckedItems.Count;
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

            backgroundWorker1.RunWorkerAsync(300); //300 gives a total of 3 seconds pause
        }

        private void ApplySlap(string boxcontent)
        {
            if (checkedListBoxSoftware.CheckedItems.Count != 0)
            {
                Slapper.InstallWinGet();
            }
            try
            {
                switch (boxcontent)
                {
                    case "Disable Shared Experiences":
                        Slapper.DisableSharedExperiences();
                        break;
                    case "Disable Cortana":
                        Slapper.DisableCortana();
                        break;
                    case "Disable Game DVR and Game Bar":
                        Slapper.DisableGameDvr();
                        break;
                    case "Disable Hotspot 2.0":
                        Slapper.DisableHotspot20();
                        break;
                    case "Don't include frequently used folders in Quick access":
                        Slapper.NoQuickAccess();
                        break;
                    case "Don't show sync provider notifications":
                        Slapper.HideSyncNotifications();
                        break;
                    case "Disable Sharing Wizard":
                        Slapper.DisableSharingWizard();
                        break;
                    case "Show 'This PC' when launching File Explorer":
                        Slapper.LaunchThisPcFileExplorer();
                        break;
                    case "Disable Telemetry":
                        Slapper.DisableTelemetry();
                        break;
                    case "Uninstall OneDrive":
                        Slapper.UninstallOneDrive();
                        break;
                    case "Disable Activity History":
                        Slapper.DisableActivityHistory();
                        break;
                    case "Disable automatically installing Apps":
                        Slapper.DisableAutomaticAppInstall();
                        break;
                    case "Disable Feedback dialogs":
                        Slapper.DisableFeedbackDialogs();
                        break;
                    case "Disable Start Menu suggestions":
                        Slapper.DisableStartMenuSuggestions();
                        break;
                    case "Disable Bing search":
                        Slapper.DisableBingSearch();
                        break;
                    case "Disable password reveal button":
                        Slapper.DisablePasswordReveal();
                        break;
                    case "Disable settings sync":
                        Slapper.DisableSettingsSync();
                        break;
                    case "Disable startup sound":
                        Slapper.DisableStartupSound();
                        break;
                    case "Disable autostart startup delay":
                        Slapper.DisableAutostartStartupDelay();
                        break;
                    case "Disable location":
                        Slapper.DisableLocation();
                        break;
                    case "Disable Advertising ID":
                        Slapper.DisableAdvertisingId();
                        break;
                    case "Disable Malware Removal Tool data reporting":
                        Slapper.DisableMrtReporting();
                        break;
                    case "Disable sending typing info to Microsoft":
                        Slapper.DisableSendingTypingInfo();
                        break;
                    case "Disable Personalization":
                        Slapper.DisablePersonalization();
                        break;
                    case "Hide language list from websites":
                        Slapper.HideLanguageListWebsites();
                        break;
                    case "Disable Miracast":
                        Slapper.DisableMiracast();
                        break;
                    case "Disable App Diagnostics":
                        Slapper.DisableAppDiagnostics();
                        break;
                    case "Disable Wi-Fi Sense":
                        Slapper.DisableWiFiSense();
                        break;
                    case "Disable lock screen Spotlight":
                        Slapper.DisableLockScreenSpotlight();
                        break;
                    case "Disable automatic maps updates":
                        Slapper.DisableAutomaticMapsUpdates();
                        break;
                    case "Disable error reporting":
                        Slapper.DisableErrorReporting();
                        break;
                    case "Disable Remote Assistance":
                        Slapper.DisableRemoteAssistance();
                        break;
                    case "Use UTC as BIOS time":
                        Slapper.UseUtcAsBiosTime();
                        break;
                    case "Hide network from lock screen":
                        Slapper.HideNetworkFromLockScreen();
                        break;
                    case "Disable sticky keys prompt":
                        Slapper.DisableStickyKeysPrompt();
                        break;
                    case "Hide 3D Objects from File Explorer":
                        Slapper.Hide3DObjectsFileExplorer();
                        break;
                    case "Remove preinstalled apps except Photos, Calculator, Store":
                        Slapper.RemovePreinstalledApps();
                        break;
                    case "Update Windows Store apps":
                        Slapper.UpdateStoreApps();
                        break;
                    case "Prevent preinstalling apps for new users":
                        Slapper.PreventPreinstallingApps();
                        break;
                    case "Unpin preinstalled apps":
                        Slapper.UnpinPreinstalledApps();
                        break;
                    case "Disable Smart Screen":
                        Slapper.DisableSmartScreen();
                        break;
                    case "Disable Smart Glass":
                        Slapper.DisableSmartGlass();
                        break;
                    case "Remove Intel Control Panel from context menus":
                        Slapper.RemoveIntelContextMenu();
                        break;
                    case "Remove NVIDIA Control Panel from context menus":
                        Slapper.RemoveNvidiaContextMenu();
                        break;
                    case "Remove AMD Control Panel from context menus":
                        Slapper.RemoveAmdContextMenu();
                        break;
                    case "Disable suggested apps in Windows Ink Workspace":
                        Slapper.DisableInkAppSuggestions();
                        break;
                    case "Disable experiments by Microsoft":
                        Slapper.DisableExperiments();
                        break;
                    case "Disable Inventory Collection":
                        Slapper.DisableInventoryCollection();
                        break;
                    case "Disable Steps Recorder":
                        Slapper.DisableStepsRecorder();
                        break;
                    case "Disable Application Compatibility Engine":
                        Slapper.DisableCompatibilityAssistant();
                        break;
                    case "Disable pre-release features and settings":
                        Slapper.DisablePreReleaseFeatures();
                        break;
                    case "Disable camera on lock screen":
                        Slapper.DisableCameraLockScreen();
                        break;
                    case "Disable Microsoft Edge first run page":
                        Slapper.DisableEdgeFirstRunPage();
                        break;
                    case "Disable Microsoft Edge preload":
                        Slapper.DisableEdgePreload();
                        break;
                    case "Install .NET Framework 2.0, 3.0 and 3.5":
                        Slapper.InstallNetFrameworks();
                        break;
                    case "Enable Windows Photo Viewer":
                        Slapper.EnablePhotoViewer();
                        break;
                    case "Uninstall Microsoft XPS Document Writer":
                        Slapper.UninstallXPSWriter();
                        break;
                    case "Disable security questions for local accounts":
                        Slapper.DisableSecurityQuestions();
                        break;
                    case "Disable app suggestions (e.g. use Edge instead of Firefox)":
                        Slapper.DisableAppSuggestions();
                        break;
                    case "Remove default Fax printer":
                        Slapper.RemoveFaxPrinter();
                        break;
                    case "Remove Microsoft XPS Document Writer":
                        Slapper.RemoveXPSDocumentWriter();
                        break;
                    case "Disable clipboard history":
                        Slapper.DisableClipboardHistory();
                        break;
                    case "Disable cloud sync of clipboard history":
                        Slapper.DisableClipboardCloudSync();
                        break;
                    case "Disable automatic update of speech data":
                        Slapper.DisableAutomaticSpeechDataUpdates();
                        break;
                    case "Disable handwriting error reports":
                        Slapper.DisableHandwritingErrorReports();
                        break;
                    case "Disable cloud sync of text messages":
                        Slapper.DisableTextMessagesCloudSync();
                        break;
                    case "Disable Bluetooth advertisements":
                        Slapper.DisableBluetoothAdvertisements();
                        break;
                    case "Add This PC shortcut to desktop":
                        Slapper.AddThisPCShortcut();
                        break;
                    case "Small taskbar icons":
                        Slapper.TaskbarSmallIcons();
                        break;
                    case "Don't group tasks in taskbar":
                        Slapper.DoNotGroupTasks();
                        break;
                    case "Hide Taskview button in taskbar":
                        Slapper.HideTaskview();
                        break;
                    case "Hide People button in taskbar":
                        Slapper.DisablePeopleBand();
                        break;
                    case "Hide search bar in taskbar":
                        Slapper.HideSearch();
                        break;
                    case "Remove compatibility item from context menu":
                        Slapper.RemoveCompatibility();
                        break;
                    case "Hide OneDrive Cloud states in File Explorer":
                        Slapper.DisableCloudStates();
                        break;
                    case "Always show file name extensions":
                        Slapper.ShowFilenameExtensions();
                        break;
                    case "Remove OneDrive from File Explorer":
                        Slapper.HideOneDriveFileExplorer();
                        break;
                    case "Delete quicklaunch items":
                        Slapper.DeleteQuicklaunchItems();
                        break;
                    case "Use Windows 7 volume control":
                        Slapper.UseWin7Volume();
                        break;
                    case "Remove Microsoft Edge desktop shortcut":
                        Slapper.RemoveEdgeShortcut();
                        break;
                    case "Disable Lockscreen Blur":
                        Slapper.DisableLockscreenBlur();
                        break;
                    case "Hide Meet Now icon in taskbar":
                        Slapper.HideMeetNow();
                        break;
                    case "Install 7Zip":
                        Slapper.Install7Zip();
                        break;
                    case "Install Adobe Acrobat Reader DC":
                        Slapper.InstallAdobeReaderDC();
                        break;
                    case "Install Audacity":
                        Slapper.InstallAudacity();
                        break;
                    case "Install BalenaEtcher":
                        Slapper.InstallBalenaEtcher();
                        break;
                    case "Install Battle.Net":
                        Slapper.InstallBattleNet();
                        break;
                    case "Install calibre":
                        Slapper.InstallCalibre();
                        break;
                    case "Install CPU-Z":
                        Slapper.InstallCPUZ();
                        break;
                    case "Install Discord":
                        Slapper.InstallDiscord();
                        break;
                    case "Install DupeGuru":
                        Slapper.InstallDupeGuru();
                        break;
                    case "Install EarTrumpet":
                        Slapper.InstallEarTrumpet();
                        break;
                    case "Install Epic Games Launcher":
                        Slapper.InstallEpicGamesLauncher();
                        break;
                    case "Install Everything Search":
                        Slapper.InstallEverythingSearch();
                        break;
                    case "Install f.lux":
                        Slapper.InstallFlux();
                        break;
                    case "Install FileZilla":
                        Slapper.InstallFileZilla();
                        break;
                    case "Install GIMP":
                        Slapper.InstallGIMP();
                        break;
                    case "Install GPU-Z":
                        Slapper.InstallGPUZ();
                        break;
                    case "Install Git":
                        Slapper.InstallGit();
                        break;
                    case "Install Google Chrome":
                        Slapper.InstallGoogleChrome();
                        break;
                    case "Install HashTab":
                        Slapper.InstallHashTab();
                        break;
                    case "Install Inkscape":
                        Slapper.InstallInkscape();
                        break;
                    case "Install Irfanview":
                        Slapper.InstallIrfanview();
                        break;
                    case "Install Java Runtime Environment":
                        Slapper.InstallJavaRE();
                        break;
                    case "Install KDE Connect":
                        Slapper.InstallKDEConnect();
                        break;
                    case "Install KeePassXC":
                        Slapper.InstallKeePassXC();
                        break;
                    case "Install League Of Legends":
                        Slapper.InstallLeagueOfLegends();
                        break;
                    case "Install LibreOffice":
                        Slapper.InstallLibreOffice();
                        break;
                    case "Install Minecraft":
                        Slapper.InstallMinecraft();
                        break;
                    case "Install Mozilla Firefox":
                        Slapper.InstallFirefox();
                        break;
                    case "Install Mozilla Thunderbird":
                        Slapper.InstallThunderbird();
                        break;
                    case "Install Nextcloud Desktop":
                        Slapper.InstallNextcloudDesktop();
                        break;
                    case "Install Notepad++":
                        Slapper.InstallNotepadPlusPlus();
                        break;
                    case "Install OBS Studio":
                        Slapper.InstallOBSStudio();
                        break;
                    case "Install OpenVPN Connect":
                        Slapper.InstallOpenVPNConnect();
                        break;
                    case "Install Origin":
                        Slapper.InstallOrigin();
                        break;
                    case "Install PowerToys":
                        Slapper.InstallPowerToys();
                        break;
                    case "Install PuTTY":
                        Slapper.InstallPuTTY();
                        break;
                    case "Install Python":
                        Slapper.InstallPython();
                        break;
                    case "Install Skype":
                        Slapper.InstallSkype();
                        break;
                    case "Install Slack":
                        Slapper.InstallSlack();
                        break;
                    case "Install Speccy":
                        Slapper.InstallSpeccy();
                        break;
                    case "Install Spotify":
                        Slapper.InstallSpotify();
                        break;
                    case "Install StartIsBack++":
                        Slapper.InstallStartIsBack();
                        break;
                    case "Install Steam":
                        Slapper.InstallSteam();
                        break;
                    case "Install TeamViewer":
                        Slapper.InstallTeamViewer();
                        break;
                    case "Install TeamSpeak":
                        Slapper.InstallTeamSpeak();
                        break;
                    case "Install Telegram":
                        Slapper.InstallTelegram();
                        break;
                    case "Install Twitch":
                        Slapper.InstallTwitch();
                        break;
                    case "Install Ubisoft Connect":
                        Slapper.InstallUbisoftConnect();
                        break;
                    case "Install VirtualBox":
                        Slapper.InstallVirtualBox();
                        break;
                    case "Install Visual Studio Code":
                        Slapper.InstallVSCode();
                        break;
                    case "Install VLC media player":
                        Slapper.InstallVlc();
                        break;
                    case "Install WinRAR":
                        Slapper.InstallWinRAR();
                        break;
                    case "Install WinSCP":
                        Slapper.InstallWinSCP();
                        break;
                    case "Install Windows Terminal":
                        Slapper.InstallWindowsTerminal();
                        break;
                    case "Install Wireshark":
                        Slapper.InstallWireshark();
                        break;
                    case "Install Zoom":
                        Slapper.InstallZoom();
                        break;
                    case "Disable Background Apps":
                        Slapper.DisableBackgroundApps();
                        break;
                    case "Precision Trackpad: Disable keyboard block after clicking":
                        Slapper.DisableBlockPrecisionTrackpad();
                        break;
                    case "Disable Windows Defender":
                        Slapper.DisableDefender();
                        break;
                    case "Disable Link-local Multicast Name Resolution":
                        Slapper.DisableLLMNR();
                        break;
                    case "Disable Smart Multi-Homed Name Resolution":
                        Slapper.DisableSmartNameResolution();
                        break;
                    case "Disable Web Proxy Auto-Discovery":
                        Slapper.DisableWPAD();
                        break;
                    case "Disable Teredo tunneling":
                        Slapper.DisableTeredo();
                        break;
                    case "Disable Intra-Site Automatic Tunnel Addressing Protocol":
                        Slapper.DisableISATAP();
                        break;
                    case "Enable Windows Subsystem for Linux":
                        Slapper.EnableWSL();
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

            string win10release = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ReleaseId", "").ToString();
            labelOS.Text = "Windows 10 (" + win10release + ")";
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
            CheckAll(checkedListBoxTweaks, false);
        }

        private void ButtonCheckTweaks_Click(object sender, EventArgs e)
        {
            CheckAll(checkedListBoxTweaks, true);
        }

        private void ButtonCheckSoftware_Click(object sender, EventArgs e)
        {
            CheckAll(checkedListBoxSoftware, true);
        }

        private void ButtonUncheckSoftware_Click(object sender, EventArgs e)
        {
            CheckAll(checkedListBoxSoftware, false);
        }

        private void ButtonCheckAdvanced_Click(object sender, EventArgs e)
        {
            CheckAll(checkedListBoxAdvanced, true);
        }

        private void ButtonUncheckAdvanced_Click(object sender, EventArgs e)
        {
            CheckAll(checkedListBoxAdvanced, false);
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
            CheckAll(checkedListBoxAppearance, true);
        }

        private void ButtonUncheckAppearance_Click(object sender, EventArgs e)
        {
            CheckAll(checkedListBoxAppearance, false);
        }

        private void LinkGitHub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/svenmauch/WinSlap");
        }
    }
}
