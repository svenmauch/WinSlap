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

        private void SlapTweaks(SlapForm slapping)
        {
            for (int x = 0; x <= checkedListBoxTweaks.CheckedItems.Count; x++)
            {
                if (x == checkedListBoxTweaks.CheckedItems.Count)
                {
                    return;
                }
                string boxcontent = checkedListBoxTweaks.CheckedItems[x].ToString();
                try
                {
                    switch (boxcontent)
                    {
                        case "Disable Shared Experiences":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.DisableSharedExperiences();
                            break;
                        case "Disable Cortana":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.DisableCortana();
                            break;
                        case "Disable Game DVR and Game Bar":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.DisableGameDvr();
                            break;
                        case "Disable Hotspot 2.0":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.DisableHotspot20();
                            break;
                        case "Don't include frequently used folders in Quick access":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.NoQuickAccess();
                            break;
                        case "Don't show sync provider notifications":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.HideSyncNotifications();
                            break;
                        case "Disable Sharing Wizard":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.DisableSharingWizard();
                            break;
                        case "Show 'This PC' when launching File Explorer":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.LaunchThisPcFileExplorer();
                            break;
                        case "Disable Telemetry":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.DisableTelemetry();
                            break;
                        case "Uninstall OneDrive":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.UninstallOneDrive();
                            break;
                        case "Disable Activity History":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.DisableActivityHistory();
                            break;
                        case "Disable Background Apps":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.DisableBackgroundApps();
                            break;
                        case "Disable automatically installing Apps":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.DisableAutomaticAppInstall();
                            break;
                        case "Disable Feedback dialogs":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.DisableFeedbackDialogs();
                            break;
                        case "Disable Start Menu suggestions":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.DisableStartMenuSuggestions();
                            break;
                        case "Disable Bing search":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.DisableBingSearch();
                            break;
                        case "Disable password reveal button":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.DisablePasswordReveal();
                            break;
                        case "Disable settings sync":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.DisableSettingsSync();
                            break;
                        case "Disable startup sound":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.DisableStartupSound();
                            break;
                        case "Disable autostart startup delay":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.DisableAutostartStartupDelay();
                            break;
                        case "Disable location":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.DisableLocation();
                            break;
                        case "Disable Advertising ID":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.DisableAdvertisingId();
                            break;
                        case "Disable Malware Removal Tool data reporting":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.DisableMrtReporting();
                            break;
                        case "Disable sending typing info to Microsoft":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.DisableSendingTypingInfo();
                            break;
                        case "Disable Personalization":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.DisablePersonalization();
                            break;
                        case "Hide language list from websites":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.HideLanguageListWebsites();
                            break;
                        case "Disable Miracast":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.DisableMiracast();
                            break;
                        case "Disable App Diagnostics":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.DisableAppDiagnostics();
                            break;
                        case "Disable Wi-Fi Sense":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.DisableWiFiSense();
                            break;
                        case "Disable lock screen Spotlight":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.DisableLockScreenSpotlight();
                            break;
                        case "Disable automatic maps updates":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.DisableAutomaticMapsUpdates();
                            break;
                        case "Disable error reporting":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.DisableErrorReporting();
                            break;
                        case "Disable Remote Assistance":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.DisableRemoteAssistance();
                            break;
                        case "Use UTC as BIOS time":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.UseUtcAsBiosTime();
                            break;
                        case "Hide network from lock screen":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.HideNetworkFromLockScreen();
                            break;
                        case "Disable sticky keys prompt":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.DisableStickyKeysPrompt();
                            break;
                        case "Hide 3D Objects from File Explorer":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.Hide3DObjectsFileExplorer();
                            break;
                        case "Remove preinstalled apps except Photos, Calculator, Store":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.RemovePreinstalledApps();
                            break;
                        case "Prevent preinstalling apps for new users":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.PreventPreinstallingApps();
                            break;
                        case "Unpin preinstalled apps":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.UnpinPreinstalledApps();
                            break;
                        case "Disable Smart Screen":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.DisableSmartScreen();
                            break;
                        case "Disable Smart Glass":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.DisableSmartGlass();
                            break;
                        case "Remove Intel Control Panel from context menus":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.RemoveIntelContextMenu();
                            break;
                        case "Remove NVIDIA Control Panel from context menus":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.RemoveNvidiaContextMenu();
                            break;
                        case "Remove AMD Control Panel from context menus":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.RemoveAmdContextMenu();
                            break;
                        case "Disable suggested apps in Windows Ink Workspace":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.DisableInkAppSuggestions();
                            break;
                        case "Disable experiments by Microsoft":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.DisableExperiments();
                            break;
                        case "Disable Inventory Collection":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.DisableInventoryCollection();
                            break;
                        case "Disable Steps Recorder":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.DisableStepsRecorder();
                            break;
                        case "Disable Application Compatibility Engine":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.DisableCompatibilityAssistant();
                            break;
                        case "Disable pre-release features and settings":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.DisablePreReleaseFeatures();
                            break;
                        case "Disable camera on lock screen":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.DisableCameraLockScreen();
                            break;
                        case "Disable Microsoft Edge first run page":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.DisableEdgeFirstRunPage();
                            break;
                        case "Disable Microsoft Edge preload":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.DisableEdgePreload();
                            break;
                        case "Install .NET Framework 2.0, 3.0 and 3.5":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.InstallNetFrameworks();
                            break;
                        case "Enable Windows Photo Viewer":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.EnablePhotoViewer();
                            break;
                        case "Uninstall Microsoft XPS Document Writer":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.UninstallXPSWriter();
                            break;
                        case "Disable security questions for local accounts":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.DisableSecurityQuestions();
                            break;
                        case "Disable app suggestions (e.g. use Edge instead of Firefox)":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.DisableAppSuggestions();
                            break;
                        case "Remove default Fax printer":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.RemoveFaxPrinter();
                            break;
                        case "Remove Microsoft XPS Document Writer":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.RemoveXPSDocumentWriter();
                            break;
                        default:
                            slapping.Hide();
                            string caption = "Something went wrong...";
                            string errorMessage = "Item not found (" + boxcontent + ")\nPlease report this issue at https://github.com/svenmauch/WinSlap";
                            MessageBox.Show(errorMessage, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Environment.Exit(1);
                            break;
                    }
                }

                catch (NullReferenceException ex)
                {
                    slapping.Hide();
                    string caption = "Something went wrong...";
                    string errorMessage = "NullReferenceException in \"" + boxcontent + "\"\nPlease report this issue at https://github.com/svenmauch/WinSlap" + "\n\n" + ex;
                    MessageBox.Show(errorMessage, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(1);
                }
            }
        }

        private void SlapAppearance(SlapForm slapping)
        {
            for (int x = 0; x <= checkedListBoxAppearance.CheckedItems.Count; x++)
            {
                if (x == checkedListBoxAppearance.CheckedItems.Count)
                {
                    return;
                }
                string boxcontent = checkedListBoxAppearance.CheckedItems[x].ToString();
                try
                {
                    switch (boxcontent)
                    {
                        case "Add This PC shortcut to desktop":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.AddThisPCShortcut();
                            break;
                        case "Small taskbar icons":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.TaskbarSmallIcons();
                            break;
                        case "Don't group tasks in taskbar":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.DoNotGroupTasks();
                            break;
                        case "Hide Taskview button in taskbar":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.HideTaskview();
                            break;
                        case "Hide People button in taskbar":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.DisablePeopleBand();
                            break;
                        case "Hide search bar in taskbar":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.HideSearch();
                            break;
                        case "Remove compatibility item from context menu":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.RemoveCompatibility();
                            break;
                        case "Hide OneDrive Cloud states in File Explorer":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.DisableCloudStates();
                            break;
                        case "Always show file name extensions":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.ShowFilenameExtensions();
                            break;
                        case "Remove OneDrive from File Explorer":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.HideOneDriveFileExplorer();
                            break;
                        case "Delete quicklaunch items":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.DeleteQuicklaunchItems();
                            break;
                        case "Use Windows 7 volume control":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.UseWin7Volume();
                            break;
                        case "Remove Microsoft Edge desktop shortcut":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.RemoveEdgeShortcut();
                            break;
                        case "Disable Lockscreen Blur":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.DisableLockscreenBlur();
                            break;
                        default:
                            slapping.Hide();
                            string caption = "Something went wrong...";
                            string errorMessage = "Item not found in Appearance\nPlease report this issue at https://github.com/svenmauch/WinSlap";
                            MessageBox.Show(errorMessage, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Environment.Exit(1);
                            break;
                    }
                }
                catch (NullReferenceException)
                {
                    slapping.Hide();
                    string caption = "Something went wrong...";
                    string errorMessage = "NullReferenceException in \"" + boxcontent + "\"\nPlease report this issue at https://github.com/svenmauch/WinSlap";
                    MessageBox.Show(errorMessage, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(1);
                }
            }
        }

        private void SlapSoftware(SlapForm slapping)
        {
            for (int x = 0; x <= checkedListBoxSoftware.CheckedItems.Count; x++)
            {
                if (x == checkedListBoxSoftware.CheckedItems.Count)
                {
                    return;
                }
                string boxcontent = checkedListBoxSoftware.CheckedItems[x].ToString();
                try
                {
                    switch (boxcontent)
                    {
                        case "Install Mozilla Firefox":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.InstallFirefox();
                            break;
                        case "Install Mozilla Thunderbird":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.InstallThunderbird();
                            break;
                        case "Install VLC media player":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.InstallVlc();
                            break;
                        case "Install Telegram":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.InstallTelegram();
                            break;
                        case "Install StartIsBack++":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.InstallStartIsBack();
                            break;
                        case "Disable Application Compatibility Engine":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.DisableApplicationCompatibilityEngine();
                            break;
                        default:
                            slapping.Hide();
                            string caption = "Something went wrong...";
                            string errorMessage = "Item not found in Software\nPlease report this issue at https://github.com/svenmauch/WinSlap";
                            MessageBox.Show(errorMessage, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Environment.Exit(1);
                            break;
                    }
                }
                catch (NullReferenceException)
                {
                    slapping.Hide();
                    string caption = "Something went wrong...";
                    string errorMessage = "NullReferenceException in \"" + boxcontent + "\"\nPlease report this issue at https://github.com/svenmauch/WinSlap";
                    MessageBox.Show(errorMessage, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(1);
                }
            }
        }

        private void SlapAdvanced(SlapForm slapping)
        {
            for (int x = 0; x <= checkedListBoxAdvanced.CheckedItems.Count; x++)
            {
                if (x == checkedListBoxAdvanced.CheckedItems.Count)
                {
                    return;
                }
                string boxcontent = checkedListBoxAdvanced.CheckedItems[x].ToString();
                try
                {
                    switch (boxcontent)
                    {
                        case "Precision Trackpad: Disable keyboard block after clicking":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.InstallFirefox();
                            break;
                        case "Disable Windows Defender":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.DisableDefender();
                            break;
                        case "Disable Link-local Multicast Name Resolution":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.DisableLLMNR();
                            break;
                        case "Disable Smart Multi-Homed Name Resolution":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.DisableSmartNameResolution();
                            break;
                        case "Disable Web Proxy Auto-Discovery":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.DisableWPAD();
                            break;
                        case "Disable Teredo tunneling":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.DisableTeredo();
                            break;
                        case "Disable Intra-Site Automatic Tunnel Addressing Protocol":
                            slapping.SetCurrentOp(boxcontent);
                            Slapper.DisableISATAP();
                            break;
                        default:
                            slapping.Hide();
                            string caption = "Something went wrong...";
                            string errorMessage = "Item not found in Advanced\nPlease report this issue at https://github.com/svenmauch/WinSlap";
                            MessageBox.Show(errorMessage, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Environment.Exit(1);
                            break;
                    }
                }
                catch (NullReferenceException)
                {
                    slapping.Hide();
                    string caption = "Something went wrong...";
                    string errorMessage = "NullReferenceException in \"" + boxcontent + "\"\nPlease report this issue at https://github.com/svenmauch/WinSlap";
                    MessageBox.Show(errorMessage, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(1);
                }
            }
        }

        private void WinSlap_Load(object sender, EventArgs e)
        {
            InitParameterNotice();

            AppDomain.CurrentDomain.ProcessExit += OnProcessExit;
            Directory.CreateDirectory(Tmpfolder);

            labelOS.Text = "Windows 10 (" + Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ReleaseId", "") + ")";
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
