using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace WinSlap
{
    public sealed partial class MainForm : Form
    {
        public static readonly string Tmpfolder = Path.GetTempPath() + @"WinSlap\";
        private bool _restart = true;

        public MainForm(string[] args)
        {
            const string strNoRestart = "norestart";
            if (args.Any(strNoRestart.Contains)) { this._restart = false; }
            InitializeComponent();
            MinimumSize = new System.Drawing.Size(332, 173);
        }

        private void ButtonSlap_Click(object sender, EventArgs e)
        {
            if (ModifierKeys == Keys.Shift)
            {
                _restart = false;
            }

            if (checkedListBoxTweaks.CheckedItems.Count == 0 && checkedListBoxSoftware.CheckedItems.Count == 0 && checkedListBoxAdvanced.CheckedItems.Count == 0 && checkedListBoxAppearance.CheckedItems.Count == 0)
            {
                string caption = "Notice";
                string errorMessage = "No items selected.";
                MessageBox.Show(errorMessage, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ShowDisclaimer()) return;

            Hide();
            SlapForm slapform = new SlapForm();
            slapform.Show();

            ApplySlaps(slapform);

            if (_restart) RestartNow();
            else
            {
                slapform.Hide();
                string caption = "Slapping finished!";
                string errorMessage = "This Notice is shown because of the NoRestart parameter.";
                MessageBox.Show(errorMessage, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                Environment.Exit(0);
            }

        }

        // ToDo: combine methods?
        private void ApplySlaps(SlapForm slapform)
        {
            SlapTweaks(slapform);
            SlapAppearance(slapform);
            SlapSoftware(slapform);
            SlapAdvanced(slapform);
        }

        private void ApplySlap(SlapForm slapform, string boxcontent)
        {
            try
            {
                switch (boxcontent)
                {
                    case "Disable Shared Experiences":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.DisableSharedExperiences();
                        break;
                    case "Disable Cortana":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.DisableCortana();
                        break;
                    case "Disable Game DVR and Game Bar":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.DisableGameDvr();
                        break;
                    case "Disable Hotspot 2.0":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.DisableHotspot20();
                        break;
                    case "Don't include frequently used folders in Quick access":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.NoQuickAccess();
                        break;
                    case "Don't show sync provider notifications":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.HideSyncNotifications();
                        break;
                    case "Disable Sharing Wizard":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.DisableSharingWizard();
                        break;
                    case "Show 'This PC' when launching File Explorer":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.LaunchThisPcFileExplorer();
                        break;
                    case "Disable Telemetry":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.DisableTelemetry();
                        break;
                    case "Uninstall OneDrive":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.UninstallOneDrive();
                        break;
                    case "Disable Activity History":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.DisableActivityHistory();
                        break;
                    case "Disable Background Apps":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.DisableBackgroundApps();
                        break;
                    case "Disable automatically installing Apps":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.DisableAutomaticAppInstall();
                        break;
                    case "Disable Feedback dialogs":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.DisableFeedbackDialogs();
                        break;
                    case "Disable Start Menu suggestions":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.DisableStartMenuSuggestions();
                        break;
                    case "Disable Bing search":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.DisableBingSearch();
                        break;
                    case "Disable password reveal button":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.DisablePasswordReveal();
                        break;
                    case "Disable settings sync":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.DisableSettingsSync();
                        break;
                    case "Disable startup sound":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.DisableStartupSound();
                        break;
                    case "Disable autostart startup delay":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.DisableAutostartStartupDelay();
                        break;
                    case "Disable location":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.DisableLocation();
                        break;
                    case "Disable Advertising ID":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.DisableAdvertisingId();
                        break;
                    case "Disable Malware Removal Tool data reporting":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.DisableMrtReporting();
                        break;
                    case "Disable sending typing info to Microsoft":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.DisableSendingTypingInfo();
                        break;
                    case "Disable Personalization":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.DisablePersonalization();
                        break;
                    case "Hide language list from websites":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.HideLanguageListWebsites();
                        break;
                    case "Disable Miracast":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.DisableMiracast();
                        break;
                    case "Disable App Diagnostics":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.DisableAppDiagnostics();
                        break;
                    case "Disable Wi-Fi Sense":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.DisableWiFiSense();
                        break;
                    case "Disable lock screen Spotlight":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.DisableLockScreenSpotlight();
                        break;
                    case "Disable automatic maps updates":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.DisableAutomaticMapsUpdates();
                        break;
                    case "Disable error reporting":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.DisableErrorReporting();
                        break;
                    case "Disable Remote Assistance":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.DisableRemoteAssistance();
                        break;
                    case "Use UTC as BIOS time":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.UseUtcAsBiosTime();
                        break;
                    case "Hide network from lock screen":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.HideNetworkFromLockScreen();
                        break;
                    case "Disable sticky keys prompt":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.DisableStickyKeysPrompt();
                        break;
                    case "Hide 3D Objects from File Explorer":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.Hide3DObjectsFileExplorer();
                        break;
                    case "Remove preinstalled apps except Photos, Calculator, Store":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.RemovePreinstalledApps();
                        break;
                    case "Prevent preinstalling apps for new users":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.PreventPreinstallingApps();
                        break;
                    case "Unpin preinstalled apps":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.UnpinPreinstalledApps();
                        break;
                    case "Disable Smart Screen":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.DisableSmartScreen();
                        break;
                    case "Disable Smart Glass":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.DisableSmartGlass();
                        break;
                    case "Remove Intel Control Panel from context menus":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.RemoveIntelContextMenu();
                        break;
                    case "Remove NVIDIA Control Panel from context menus":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.RemoveNvidiaContextMenu();
                        break;
                    case "Remove AMD Control Panel from context menus":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.RemoveAmdContextMenu();
                        break;
                    case "Disable suggested apps in Windows Ink Workspace":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.DisableInkAppSuggestions();
                        break;
                    case "Disable experiments by Microsoft":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.DisableExperiments();
                        break;
                    case "Disable Inventory Collection":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.DisableInventoryCollection();
                        break;
                    case "Disable Steps Recorder":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.DisableStepsRecorder();
                        break;
                    case "Disable Application Compatibility Engine":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.DisableCompatibilityAssistant();
                        break;
                    case "Disable pre-release features and settings":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.DisablePreReleaseFeatures();
                        break;
                    case "Disable camera on lock screen":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.DisableCameraLockScreen();
                        break;
                    case "Disable Microsoft Edge first run page":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.DisableEdgeFirstRunPage();
                        break;
                    case "Disable Microsoft Edge preload":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.DisableEdgePreload();
                        break;
                    case "Install .NET Framework 2.0, 3.0 and 3.5":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.InstallNetFrameworks();
                        break;
                    case "Enable Windows Photo Viewer":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.EnablePhotoViewer();
                        break;
                    case "Uninstall Microsoft XPS Document Writer":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.UninstallXPSWriter();
                        break;
                    case "Disable security questions for local accounts":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.DisableSecurityQuestions();
                        break;
                    case "Disable app suggestions (e.g. use Edge instead of Firefox)":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.DisableAppSuggestions();
                        break;
                    case "Remove default Fax printer":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.RemoveFaxPrinter();
                        break;
                    case "Remove Microsoft XPS Document Writer":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.RemoveXPSDocumentWriter();
                        break;
                    case "Add This PC shortcut to desktop":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.AddThisPCShortcut();
                        break;
                    case "Small taskbar icons":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.TaskbarSmallIcons();
                        break;
                    case "Don't group tasks in taskbar":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.DoNotGroupTasks();
                        break;
                    case "Hide Taskview button in taskbar":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.HideTaskview();
                        break;
                    case "Hide People button in taskbar":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.DisablePeopleBand();
                        break;
                    case "Hide search bar in taskbar":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.HideSearch();
                        break;
                    case "Remove compatibility item from context menu":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.RemoveCompatibility();
                        break;
                    case "Hide OneDrive Cloud states in File Explorer":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.DisableCloudStates();
                        break;
                    case "Always show file name extensions":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.ShowFilenameExtensions();
                        break;
                    case "Remove OneDrive from File Explorer":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.HideOneDriveFileExplorer();
                        break;
                    case "Delete quicklaunch items":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.DeleteQuicklaunchItems();
                        break;
                    case "Use Windows 7 volume control":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.UseWin7Volume();
                        break;
                    case "Remove Microsoft Edge desktop shortcut":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.RemoveEdgeShortcut();
                        break;
                    case "Disable Lockscreen Blur":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.DisableLockscreenBlur();
                        break;
                    case "Install Mozilla Firefox":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.InstallFirefox();
                        break;
                    case "Install Mozilla Thunderbird":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.InstallThunderbird();
                        break;
                    case "Install VLC media player":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.InstallVlc();
                        break;
                    case "Install Telegram":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.InstallTelegram();
                        break;
                    case "Install StartIsBack++":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.InstallStartIsBack();
                        break;
                    case "Precision Trackpad: Disable keyboard block after clicking":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.DisableBlockPrecisionTrackpad();
                        break;
                    case "Disable Windows Defender":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.DisableDefender();
                        break;
                    case "Disable Link-local Multicast Name Resolution":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.DisableLLMNR();
                        break;
                    case "Disable Smart Multi-Homed Name Resolution":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.DisableSmartNameResolution();
                        break;
                    case "Disable Web Proxy Auto-Discovery":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.DisableWPAD();
                        break;
                    case "Disable Teredo tunneling":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.DisableTeredo();
                        break;
                    case "Disable Intra-Site Automatic Tunnel Addressing Protocol":
                        slapform.SetCurrentOp(boxcontent);
                        Slapper.DisableISATAP();
                        break;
                    default:
                        slapform.Hide();
                        string caption = "Something went wrong...";
                        string errorMessage = "Item not found (" + boxcontent + ")\nPlease report this issue at https://github.com/svenmauch/WinSlap";
                        MessageBox.Show(errorMessage, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Environment.Exit(1);
                        break;
                }
            }

            catch (NullReferenceException ex)
            {
                slapform.Hide();
                string caption = "Something went wrong...";
                string errorMessage = "NullReferenceException in \"" + boxcontent + "\"\nPlease report this issue at https://github.com/svenmauch/WinSlap" + "\n\n" + ex;
                MessageBox.Show(errorMessage, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            }
        }

        private void SlapTweaks(SlapForm slapform)
        {
            for (int x = 0; x <= checkedListBoxTweaks.CheckedItems.Count; x++)
            {
                if (x == checkedListBoxTweaks.CheckedItems.Count)
                {
                    return;
                }
                string boxcontent = checkedListBoxTweaks.CheckedItems[x].ToString();
                ApplySlap(slapform, boxcontent);
            }
        }

        private void SlapAppearance(SlapForm slapform)
        {
            for (int x = 0; x <= checkedListBoxAppearance.CheckedItems.Count; x++)
            {
                if (x == checkedListBoxAppearance.CheckedItems.Count)
                {
                    return;
                }
                string boxcontent = checkedListBoxAppearance.CheckedItems[x].ToString();
                ApplySlap(slapform, boxcontent);
            }
        }

        private void SlapSoftware(SlapForm slapform)
        {
            for (int x = 0; x <= checkedListBoxSoftware.CheckedItems.Count; x++)
            {
                if (x == checkedListBoxSoftware.CheckedItems.Count)
                {
                    return;
                }
                string boxcontent = checkedListBoxSoftware.CheckedItems[x].ToString();
                ApplySlap(slapform, boxcontent);
            }
        }

        private void SlapAdvanced(SlapForm slapform)
        {
            for (int x = 0; x <= checkedListBoxAdvanced.CheckedItems.Count; x++)
            {
                if (x == checkedListBoxAdvanced.CheckedItems.Count)
                {
                    return;
                }
                string boxcontent = checkedListBoxAdvanced.CheckedItems[x].ToString();
                ApplySlap(slapform, boxcontent);
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

        private static Boolean ShowDisclaimer()
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

        private void linkGitHub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/svenmauch/WinSlap");
        }
    }
}
