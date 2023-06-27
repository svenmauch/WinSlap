namespace WinSlap
{
    sealed partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonSlap = new System.Windows.Forms.Button();
            this.parameternotice = new System.Windows.Forms.Label();
            this.tabControlWin11 = new System.Windows.Forms.TabControl();
            this.tabTweaks = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.checkedListBoxWin11Tweaks = new System.Windows.Forms.CheckedListBox();
            this.buttonWin11UncheckTweaks = new System.Windows.Forms.Button();
            this.buttonWin11CheckTweaks = new System.Windows.Forms.Button();
            this.tabAppearance = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.checkedListBoxWin11Appearance = new System.Windows.Forms.CheckedListBox();
            this.buttonWin11UncheckAppearance = new System.Windows.Forms.Button();
            this.buttonWin11CheckAppearance = new System.Windows.Forms.Button();
            this.tabSoftware = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonWin11UncheckSoftware = new System.Windows.Forms.Button();
            this.buttonWin11CheckSoftware = new System.Windows.Forms.Button();
            this.checkedListBoxWin11Software = new System.Windows.Forms.CheckedListBox();
            this.tabWin11Advanced = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.checkedListBoxWin11Advanced = new System.Windows.Forms.CheckedListBox();
            this.buttonWin11UncheckAdvanced = new System.Windows.Forms.Button();
            this.buttonWin11CheckAdvanced = new System.Windows.Forms.Button();
            this.labelOS = new System.Windows.Forms.Label();
            this.linkGitHub = new System.Windows.Forms.LinkLabel();
            this.tabControlWin10 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.checkedListBoxWin10Tweaks = new System.Windows.Forms.CheckedListBox();
            this.buttonWin10UncheckTweaks = new System.Windows.Forms.Button();
            this.buttonWin10CheckTweaks = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.checkedListBoxWin10Appearance = new System.Windows.Forms.CheckedListBox();
            this.buttonWin10UncheackAppearance = new System.Windows.Forms.Button();
            this.buttonWin10CheckAppearance = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.buttonWin10UncheckSoftware = new System.Windows.Forms.Button();
            this.buttonWin10CheckSoftware = new System.Windows.Forms.Button();
            this.checkedListBoxWin10Software = new System.Windows.Forms.CheckedListBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.label8 = new System.Windows.Forms.Label();
            this.checkedListBoxWin10Advanced = new System.Windows.Forms.CheckedListBox();
            this.buttonWin10UncheckAdvanced = new System.Windows.Forms.Button();
            this.buttonWin10CheckAdvanced = new System.Windows.Forms.Button();
            this.tabControlWin11.SuspendLayout();
            this.tabTweaks.SuspendLayout();
            this.tabAppearance.SuspendLayout();
            this.tabSoftware.SuspendLayout();
            this.tabWin11Advanced.SuspendLayout();
            this.tabControlWin10.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonSlap
            // 
            this.buttonSlap.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonSlap.Location = new System.Drawing.Point(138, 379);
            this.buttonSlap.Name = "buttonSlap";
            this.buttonSlap.Size = new System.Drawing.Size(113, 23);
            this.buttonSlap.TabIndex = 1;
            this.buttonSlap.Text = "Slap!";
            this.buttonSlap.UseVisualStyleBackColor = true;
            this.buttonSlap.Click += new System.EventHandler(this.ButtonSlap_Click);
            // 
            // parameternotice
            // 
            this.parameternotice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.parameternotice.AutoSize = true;
            this.parameternotice.ForeColor = System.Drawing.Color.Red;
            this.parameternotice.Location = new System.Drawing.Point(13, 383);
            this.parameternotice.Name = "parameternotice";
            this.parameternotice.Size = new System.Drawing.Size(89, 13);
            this.parameternotice.TabIndex = 4;
            this.parameternotice.Text = "Parameter Notice";
            this.parameternotice.Visible = false;
            // 
            // tabControlWin11
            // 
            this.tabControlWin11.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlWin11.Controls.Add(this.tabTweaks);
            this.tabControlWin11.Controls.Add(this.tabAppearance);
            this.tabControlWin11.Controls.Add(this.tabSoftware);
            this.tabControlWin11.Controls.Add(this.tabWin11Advanced);
            this.tabControlWin11.Location = new System.Drawing.Point(-1, 38);
            this.tabControlWin11.Name = "tabControlWin11";
            this.tabControlWin11.SelectedIndex = 0;
            this.tabControlWin11.Size = new System.Drawing.Size(393, 336);
            this.tabControlWin11.TabIndex = 6;
            // 
            // tabTweaks
            // 
            this.tabTweaks.Controls.Add(this.label1);
            this.tabTweaks.Controls.Add(this.checkedListBoxWin11Tweaks);
            this.tabTweaks.Controls.Add(this.buttonWin11UncheckTweaks);
            this.tabTweaks.Controls.Add(this.buttonWin11CheckTweaks);
            this.tabTweaks.Cursor = System.Windows.Forms.Cursors.Default;
            this.tabTweaks.Location = new System.Drawing.Point(4, 22);
            this.tabTweaks.Name = "tabTweaks";
            this.tabTweaks.Padding = new System.Windows.Forms.Padding(3);
            this.tabTweaks.Size = new System.Drawing.Size(385, 310);
            this.tabTweaks.TabIndex = 0;
            this.tabTweaks.Text = "Tweaks";
            this.tabTweaks.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.label1.Location = new System.Drawing.Point(9, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Win11";
            this.label1.Visible = false;
            // 
            // checkedListBoxWin11Tweaks
            // 
            this.checkedListBoxWin11Tweaks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBoxWin11Tweaks.CheckOnClick = true;
            this.checkedListBoxWin11Tweaks.Cursor = System.Windows.Forms.Cursors.Default;
            this.checkedListBoxWin11Tweaks.FormattingEnabled = true;
            this.checkedListBoxWin11Tweaks.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkedListBoxWin11Tweaks.Items.AddRange(new object[] {
            "Disable Shared Experiences",
            "Disable Cortana",
            "Disable Game DVR and Game Bar",
            "Disable Hotspot 2.0",
            "Don\'t include frequently used folders in Quick access",
            "Don\'t show sync provider notifications",
            "Disable Sharing Wizard",
            "Show \'This PC\' when launching File Explorer",
            "Disable Telemetry",
            "Uninstall OneDrive",
            "Disable Activity History",
            "Disable automatically installing Apps",
            "Disable Feedback dialogs",
            "Disable Start Menu suggestions",
            "Disable Bing search",
            "Disable password reveal button",
            "Disable settings sync",
            "Disable startup sound",
            "Disable autostart startup delay",
            "Disable location",
            "Disable Advertising ID",
            "Disable Malware Removal Tool data reporting",
            "Disable sending typing info to Microsoft",
            "Disable Personalization",
            "Hide language list from websites",
            "Disable Miracast",
            "Disable App Diagnostics",
            "Disable Wi-Fi Sense",
            "Disable lock screen Spotlight",
            "Disable automatic maps updates",
            "Disable error reporting",
            "Disable Remote Assistance",
            "Use UTC as BIOS time",
            "Hide network from lock screen",
            "Disable sticky keys prompt",
            "Hide 3D Objects from File Explorer",
            "Remove preinstalled apps except Photos, Calculator, Store",
            "Update Windows Store apps",
            "Prevent preinstalling apps for new users",
            "Unpin preinstalled apps",
            "Disable Smart Screen",
            "Disable Smart Glass",
            "Remove Intel Control Panel from context menus",
            "Remove NVIDIA Control Panel from context menus",
            "Remove AMD Control Panel from context menus",
            "Disable suggested apps in Windows Ink Workspace",
            "Disable experiments by Microsoft",
            "Disable Inventory Collection",
            "Disable Steps Recorder",
            "Disable Application Compatibility Engine",
            "Disable pre-release features and settings",
            "Disable camera on lock screen",
            "Disable Microsoft Edge first run page",
            "Disable Microsoft Edge preload",
            "Install .NET Framework 2.0, 3.0 and 3.5",
            "Enable Windows Photo Viewer",
            "Uninstall Microsoft XPS Document Writer",
            "Disable security questions for local accounts",
            "Disable app suggestions (e.g. use Edge instead of Firefox)",
            "Remove default Fax printer",
            "Remove Microsoft XPS Document Writer",
            "Disable clipboard history",
            "Disable cloud sync of clipboard history",
            "Disable automatic update of speech data",
            "Disable handwriting error reports",
            "Disable cloud sync of text messages",
            "Disable Bluetooth advertisements",
            "Disable Windows Media DRM internet access",
            "Disable Get even more out of Windows screen",
            "Set power plan to high performance",
            "Disable notifications on the lock screen",
            "Disable reminders and incoming VoIP calls on the lock screen",
            "Disable Windows welcome experience",
            "Disable suggestions in timeline",
            "Disable typing insights",
            "Disable spell checker",
            "Disable text suggestions on the software keyboard",
            "Disable SafeSearch",
            "Disable suggested content in settings app",
            "Disable automatic login after finishing updates",
            "Disable Windows Defender submitting sample files"});
            this.checkedListBoxWin11Tweaks.Location = new System.Drawing.Point(2, 33);
            this.checkedListBoxWin11Tweaks.Name = "checkedListBoxWin11Tweaks";
            this.checkedListBoxWin11Tweaks.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.checkedListBoxWin11Tweaks.ScrollAlwaysVisible = true;
            this.checkedListBoxWin11Tweaks.Size = new System.Drawing.Size(379, 274);
            this.checkedListBoxWin11Tweaks.TabIndex = 3;
            // 
            // buttonWin11UncheckTweaks
            // 
            this.buttonWin11UncheckTweaks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonWin11UncheckTweaks.Location = new System.Drawing.Point(301, 5);
            this.buttonWin11UncheckTweaks.Name = "buttonWin11UncheckTweaks";
            this.buttonWin11UncheckTweaks.Size = new System.Drawing.Size(80, 23);
            this.buttonWin11UncheckTweaks.TabIndex = 5;
            this.buttonWin11UncheckTweaks.Text = "Uncheck all";
            this.buttonWin11UncheckTweaks.UseVisualStyleBackColor = true;
            this.buttonWin11UncheckTweaks.Click += new System.EventHandler(this.ButtonUncheckTweaks_Click);
            // 
            // buttonWin11CheckTweaks
            // 
            this.buttonWin11CheckTweaks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonWin11CheckTweaks.Location = new System.Drawing.Point(217, 5);
            this.buttonWin11CheckTweaks.Name = "buttonWin11CheckTweaks";
            this.buttonWin11CheckTweaks.Size = new System.Drawing.Size(78, 23);
            this.buttonWin11CheckTweaks.TabIndex = 4;
            this.buttonWin11CheckTweaks.Text = "Check all";
            this.buttonWin11CheckTweaks.UseVisualStyleBackColor = true;
            this.buttonWin11CheckTweaks.Click += new System.EventHandler(this.ButtonCheckTweaks_Click);
            // 
            // tabAppearance
            // 
            this.tabAppearance.Controls.Add(this.label2);
            this.tabAppearance.Controls.Add(this.checkedListBoxWin11Appearance);
            this.tabAppearance.Controls.Add(this.buttonWin11UncheckAppearance);
            this.tabAppearance.Controls.Add(this.buttonWin11CheckAppearance);
            this.tabAppearance.Location = new System.Drawing.Point(4, 22);
            this.tabAppearance.Name = "tabAppearance";
            this.tabAppearance.Padding = new System.Windows.Forms.Padding(3);
            this.tabAppearance.Size = new System.Drawing.Size(385, 310);
            this.tabAppearance.TabIndex = 3;
            this.tabAppearance.Text = "Appearance";
            this.tabAppearance.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.label2.Location = new System.Drawing.Point(9, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Win11";
            this.label2.Visible = false;
            // 
            // checkedListBoxWin11Appearance
            // 
            this.checkedListBoxWin11Appearance.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBoxWin11Appearance.CheckOnClick = true;
            this.checkedListBoxWin11Appearance.Cursor = System.Windows.Forms.Cursors.Default;
            this.checkedListBoxWin11Appearance.FormattingEnabled = true;
            this.checkedListBoxWin11Appearance.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkedListBoxWin11Appearance.Items.AddRange(new object[] {
            "Add This PC shortcut to desktop",
            "Small taskbar icons",
            "Don\'t group tasks in taskbar",
            "Hide Taskview button in taskbar",
            "Hide People button in taskbar",
            "Hide search bar in taskbar",
            "Remove compatibility item from context menu",
            "Hide OneDrive Cloud states in File Explorer",
            "Always show file name extensions",
            "Remove OneDrive from File Explorer",
            "Delete quicklaunch items",
            "Remove Microsoft Edge desktop shortcut",
            "Use Windows 10 ribbon bar in Windows Explorer"});
            this.checkedListBoxWin11Appearance.Location = new System.Drawing.Point(2, 33);
            this.checkedListBoxWin11Appearance.Name = "checkedListBoxWin11Appearance";
            this.checkedListBoxWin11Appearance.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.checkedListBoxWin11Appearance.ScrollAlwaysVisible = true;
            this.checkedListBoxWin11Appearance.Size = new System.Drawing.Size(379, 274);
            this.checkedListBoxWin11Appearance.TabIndex = 6;
            // 
            // buttonWin11UncheckAppearance
            // 
            this.buttonWin11UncheckAppearance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonWin11UncheckAppearance.Location = new System.Drawing.Point(301, 5);
            this.buttonWin11UncheckAppearance.Name = "buttonWin11UncheckAppearance";
            this.buttonWin11UncheckAppearance.Size = new System.Drawing.Size(80, 23);
            this.buttonWin11UncheckAppearance.TabIndex = 8;
            this.buttonWin11UncheckAppearance.Text = "Uncheck all";
            this.buttonWin11UncheckAppearance.UseVisualStyleBackColor = true;
            this.buttonWin11UncheckAppearance.Click += new System.EventHandler(this.ButtonUncheckAppearance_Click);
            // 
            // buttonWin11CheckAppearance
            // 
            this.buttonWin11CheckAppearance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonWin11CheckAppearance.Location = new System.Drawing.Point(217, 5);
            this.buttonWin11CheckAppearance.Name = "buttonWin11CheckAppearance";
            this.buttonWin11CheckAppearance.Size = new System.Drawing.Size(78, 23);
            this.buttonWin11CheckAppearance.TabIndex = 7;
            this.buttonWin11CheckAppearance.Text = "Check all";
            this.buttonWin11CheckAppearance.UseVisualStyleBackColor = true;
            this.buttonWin11CheckAppearance.Click += new System.EventHandler(this.ButtonCheckAppearance_Click);
            // 
            // tabSoftware
            // 
            this.tabSoftware.Controls.Add(this.label3);
            this.tabSoftware.Controls.Add(this.buttonWin11UncheckSoftware);
            this.tabSoftware.Controls.Add(this.buttonWin11CheckSoftware);
            this.tabSoftware.Controls.Add(this.checkedListBoxWin11Software);
            this.tabSoftware.Location = new System.Drawing.Point(4, 22);
            this.tabSoftware.Name = "tabSoftware";
            this.tabSoftware.Padding = new System.Windows.Forms.Padding(3);
            this.tabSoftware.Size = new System.Drawing.Size(385, 310);
            this.tabSoftware.TabIndex = 1;
            this.tabSoftware.Text = "Software";
            this.tabSoftware.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.label3.Location = new System.Drawing.Point(9, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Win11";
            this.label3.Visible = false;
            // 
            // buttonWin11UncheckSoftware
            // 
            this.buttonWin11UncheckSoftware.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonWin11UncheckSoftware.Location = new System.Drawing.Point(301, 5);
            this.buttonWin11UncheckSoftware.Name = "buttonWin11UncheckSoftware";
            this.buttonWin11UncheckSoftware.Size = new System.Drawing.Size(80, 23);
            this.buttonWin11UncheckSoftware.TabIndex = 7;
            this.buttonWin11UncheckSoftware.Text = "Uncheck all";
            this.buttonWin11UncheckSoftware.UseVisualStyleBackColor = true;
            this.buttonWin11UncheckSoftware.Click += new System.EventHandler(this.ButtonUncheckSoftware_Click);
            // 
            // buttonWin11CheckSoftware
            // 
            this.buttonWin11CheckSoftware.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonWin11CheckSoftware.Location = new System.Drawing.Point(217, 5);
            this.buttonWin11CheckSoftware.Name = "buttonWin11CheckSoftware";
            this.buttonWin11CheckSoftware.Size = new System.Drawing.Size(78, 23);
            this.buttonWin11CheckSoftware.TabIndex = 6;
            this.buttonWin11CheckSoftware.Text = "Check all";
            this.buttonWin11CheckSoftware.UseVisualStyleBackColor = true;
            this.buttonWin11CheckSoftware.Click += new System.EventHandler(this.ButtonCheckSoftware_Click);
            // 
            // checkedListBoxWin11Software
            // 
            this.checkedListBoxWin11Software.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBoxWin11Software.CheckOnClick = true;
            this.checkedListBoxWin11Software.FormattingEnabled = true;
            this.checkedListBoxWin11Software.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkedListBoxWin11Software.Items.AddRange(new object[] {
            "Install 7Zip",
            "Install Adobe Acrobat Reader DC",
            "Install Audacity",
            "Install BalenaEtcher",
            "Install calibre",
            "Install CPU-Z",
            "Install Discord",
            "Install DupeGuru",
            "Install EarTrumpet",
            "Install Epic Games Launcher",
            "Install FileZilla",
            "Install GIMP",
            "Install GPU-Z",
            "Install Git",
            "Install Google Chrome",
            "Install Inkscape",
            "Install Irfanview",
            "Install Java Runtime Environment",
            "Install KeePassXC",
            "Install LibreOffice",
            "Install Minecraft",
            "Install Mozilla Firefox",
            "Install Mozilla Thunderbird",
            "Install Nextcloud Desktop",
            "Install Notepad++",
            "Install OBS Studio",
            "Install OpenHashTab",
            "Install OpenVPN Connect",
            "Install PowerToys",
            "Install PuTTY",
            "Install Python 3.11",
            "Install Slack",
            "Install Speccy",
            "Install Steam",
            "Install TeamViewer",
            "Install TeamSpeak",
            "Install Telegram",
            "Install Ubisoft Connect",
            "Install VirtualBox",
            "Install VLC media player",
            "Install WinRAR",
            "Install WinSCP",
            "Install Wireguard",
            "Install Wireshark",
            "Install Zoom"});
            this.checkedListBoxWin11Software.Location = new System.Drawing.Point(2, 33);
            this.checkedListBoxWin11Software.Name = "checkedListBoxWin11Software";
            this.checkedListBoxWin11Software.ScrollAlwaysVisible = true;
            this.checkedListBoxWin11Software.Size = new System.Drawing.Size(379, 274);
            this.checkedListBoxWin11Software.TabIndex = 4;
            // 
            // tabWin11Advanced
            // 
            this.tabWin11Advanced.Controls.Add(this.label4);
            this.tabWin11Advanced.Controls.Add(this.checkedListBoxWin11Advanced);
            this.tabWin11Advanced.Controls.Add(this.buttonWin11UncheckAdvanced);
            this.tabWin11Advanced.Controls.Add(this.buttonWin11CheckAdvanced);
            this.tabWin11Advanced.Location = new System.Drawing.Point(4, 22);
            this.tabWin11Advanced.Name = "tabWin11Advanced";
            this.tabWin11Advanced.Padding = new System.Windows.Forms.Padding(3);
            this.tabWin11Advanced.Size = new System.Drawing.Size(385, 310);
            this.tabWin11Advanced.TabIndex = 2;
            this.tabWin11Advanced.Text = "Advanced";
            this.tabWin11Advanced.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.label4.Location = new System.Drawing.Point(9, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Win11";
            this.label4.Visible = false;
            // 
            // checkedListBoxWin11Advanced
            // 
            this.checkedListBoxWin11Advanced.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBoxWin11Advanced.CheckOnClick = true;
            this.checkedListBoxWin11Advanced.Cursor = System.Windows.Forms.Cursors.Default;
            this.checkedListBoxWin11Advanced.FormattingEnabled = true;
            this.checkedListBoxWin11Advanced.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkedListBoxWin11Advanced.Items.AddRange(new object[] {
            "Disable Background Apps",
            "Precision Trackpad: Disable keyboard block after clicking",
            "Disable Windows Defender",
            "Disable Link-local Multicast Name Resolution",
            "Disable Smart Multi-Homed Name Resolution",
            "Disable Web Proxy Auto-Discovery",
            "Disable Teredo tunneling",
            "Disable Intra-Site Automatic Tunnel Addressing Protocol",
            "Enable Windows Subsystem for Linux",
            "Uninstall Internet Explorer",
            "Enable Storage Sense",
            "Disable fast startup",
            "Disable mouse pointer acceleration"});
            this.checkedListBoxWin11Advanced.Location = new System.Drawing.Point(2, 33);
            this.checkedListBoxWin11Advanced.Name = "checkedListBoxWin11Advanced";
            this.checkedListBoxWin11Advanced.ScrollAlwaysVisible = true;
            this.checkedListBoxWin11Advanced.Size = new System.Drawing.Size(379, 274);
            this.checkedListBoxWin11Advanced.TabIndex = 6;
            // 
            // buttonWin11UncheckAdvanced
            // 
            this.buttonWin11UncheckAdvanced.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonWin11UncheckAdvanced.Location = new System.Drawing.Point(301, 5);
            this.buttonWin11UncheckAdvanced.Name = "buttonWin11UncheckAdvanced";
            this.buttonWin11UncheckAdvanced.Size = new System.Drawing.Size(80, 23);
            this.buttonWin11UncheckAdvanced.TabIndex = 8;
            this.buttonWin11UncheckAdvanced.Text = "Uncheck all";
            this.buttonWin11UncheckAdvanced.UseVisualStyleBackColor = true;
            this.buttonWin11UncheckAdvanced.Click += new System.EventHandler(this.ButtonUncheckAdvanced_Click);
            // 
            // buttonWin11CheckAdvanced
            // 
            this.buttonWin11CheckAdvanced.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonWin11CheckAdvanced.Location = new System.Drawing.Point(217, 5);
            this.buttonWin11CheckAdvanced.Name = "buttonWin11CheckAdvanced";
            this.buttonWin11CheckAdvanced.Size = new System.Drawing.Size(78, 23);
            this.buttonWin11CheckAdvanced.TabIndex = 7;
            this.buttonWin11CheckAdvanced.Text = "Check all";
            this.buttonWin11CheckAdvanced.UseVisualStyleBackColor = true;
            this.buttonWin11CheckAdvanced.Click += new System.EventHandler(this.ButtonCheckAdvanced_Click);
            // 
            // labelOS
            // 
            this.labelOS.AutoSize = true;
            this.labelOS.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelOS.Location = new System.Drawing.Point(12, 9);
            this.labelOS.Name = "labelOS";
            this.labelOS.Size = new System.Drawing.Size(136, 18);
            this.labelOS.TabIndex = 7;
            this.labelOS.Text = "Windows 10 (????)";
            // 
            // linkGitHub
            // 
            this.linkGitHub.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkGitHub.AutoSize = true;
            this.linkGitHub.Location = new System.Drawing.Point(286, 13);
            this.linkGitHub.Name = "linkGitHub";
            this.linkGitHub.Size = new System.Drawing.Size(98, 13);
            this.linkGitHub.TabIndex = 8;
            this.linkGitHub.TabStop = true;
            this.linkGitHub.Text = "WinSlap on GitHub";
            this.linkGitHub.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(225)))));
            this.linkGitHub.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkGitHub_LinkClicked);
            // 
            // tabControlWin10
            // 
            this.tabControlWin10.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlWin10.Controls.Add(this.tabPage1);
            this.tabControlWin10.Controls.Add(this.tabPage2);
            this.tabControlWin10.Controls.Add(this.tabPage3);
            this.tabControlWin10.Controls.Add(this.tabPage4);
            this.tabControlWin10.Location = new System.Drawing.Point(-1, 38);
            this.tabControlWin10.Name = "tabControlWin10";
            this.tabControlWin10.SelectedIndex = 0;
            this.tabControlWin10.Size = new System.Drawing.Size(393, 336);
            this.tabControlWin10.TabIndex = 9;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.checkedListBoxWin10Tweaks);
            this.tabPage1.Controls.Add(this.buttonWin10UncheckTweaks);
            this.tabPage1.Controls.Add(this.buttonWin10CheckTweaks);
            this.tabPage1.Cursor = System.Windows.Forms.Cursors.Default;
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(385, 310);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Tweaks";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.DarkViolet;
            this.label5.Location = new System.Drawing.Point(9, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Win10";
            this.label5.Visible = false;
            // 
            // checkedListBoxWin10Tweaks
            // 
            this.checkedListBoxWin10Tweaks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBoxWin10Tweaks.CheckOnClick = true;
            this.checkedListBoxWin10Tweaks.Cursor = System.Windows.Forms.Cursors.Default;
            this.checkedListBoxWin10Tweaks.FormattingEnabled = true;
            this.checkedListBoxWin10Tweaks.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkedListBoxWin10Tweaks.Items.AddRange(new object[] {
            "Disable Shared Experiences",
            "Disable Cortana",
            "Disable Game DVR and Game Bar",
            "Disable Hotspot 2.0",
            "Don\'t include frequently used folders in Quick access",
            "Don\'t show sync provider notifications",
            "Disable Sharing Wizard",
            "Show \'This PC\' when launching File Explorer",
            "Disable Telemetry",
            "Uninstall OneDrive",
            "Disable Activity History",
            "Disable automatically installing Apps",
            "Disable Feedback dialogs",
            "Disable Start Menu suggestions",
            "Disable Bing search",
            "Disable password reveal button",
            "Disable settings sync",
            "Disable startup sound",
            "Disable autostart startup delay",
            "Disable location",
            "Disable Advertising ID",
            "Disable Malware Removal Tool data reporting",
            "Disable sending typing info to Microsoft",
            "Disable Personalization",
            "Hide language list from websites",
            "Disable Miracast",
            "Disable App Diagnostics",
            "Disable Wi-Fi Sense",
            "Disable lock screen Spotlight",
            "Disable automatic maps updates",
            "Disable error reporting",
            "Disable Remote Assistance",
            "Use UTC as BIOS time",
            "Hide network from lock screen",
            "Disable sticky keys prompt",
            "Hide 3D Objects from File Explorer",
            "Remove preinstalled apps except Photos, Calculator, Store",
            "Update Windows Store apps",
            "Prevent preinstalling apps for new users",
            "Unpin preinstalled apps",
            "Disable Smart Screen",
            "Disable Smart Glass",
            "Remove Intel Control Panel from context menus",
            "Remove NVIDIA Control Panel from context menus",
            "Remove AMD Control Panel from context menus",
            "Disable suggested apps in Windows Ink Workspace",
            "Disable experiments by Microsoft",
            "Disable Inventory Collection",
            "Disable Steps Recorder",
            "Disable Application Compatibility Engine",
            "Disable pre-release features and settings",
            "Disable camera on lock screen",
            "Disable Microsoft Edge first run page",
            "Disable Microsoft Edge preload",
            "Install .NET Framework 2.0, 3.0 and 3.5",
            "Enable Windows Photo Viewer",
            "Uninstall Microsoft XPS Document Writer",
            "Disable security questions for local accounts",
            "Disable app suggestions (e.g. use Edge instead of Firefox)",
            "Remove default Fax printer",
            "Remove Microsoft XPS Document Writer",
            "Disable clipboard history",
            "Disable cloud sync of clipboard history",
            "Disable automatic update of speech data",
            "Disable handwriting error reports",
            "Disable cloud sync of text messages",
            "Disable Bluetooth advertisements",
            "Disable Windows Media DRM internet access",
            "Disable Get even more out of Windows screen",
            "Set power plan to high performance",
            "Disable notifications on the lock screen",
            "Disable reminders and incoming VoIP calls on the lock screen",
            "Disable Windows welcome experience",
            "Disable Aero Shake",
            "Disable suggestions in timeline",
            "Disable typing insights",
            "Disable spell checker",
            "Disable text suggestions on the software keyboard",
            "Disable SafeSearch",
            "Disable suggested content in settings app",
            "Disable automatic login after finishing updates",
            "Disable Windows Defender submitting sample files"});
            this.checkedListBoxWin10Tweaks.Location = new System.Drawing.Point(2, 33);
            this.checkedListBoxWin10Tweaks.Name = "checkedListBoxWin10Tweaks";
            this.checkedListBoxWin10Tweaks.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.checkedListBoxWin10Tweaks.ScrollAlwaysVisible = true;
            this.checkedListBoxWin10Tweaks.Size = new System.Drawing.Size(379, 274);
            this.checkedListBoxWin10Tweaks.TabIndex = 3;
            // 
            // buttonWin10UncheckTweaks
            // 
            this.buttonWin10UncheckTweaks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonWin10UncheckTweaks.Location = new System.Drawing.Point(301, 5);
            this.buttonWin10UncheckTweaks.Name = "buttonWin10UncheckTweaks";
            this.buttonWin10UncheckTweaks.Size = new System.Drawing.Size(80, 23);
            this.buttonWin10UncheckTweaks.TabIndex = 5;
            this.buttonWin10UncheckTweaks.Text = "Uncheck all";
            this.buttonWin10UncheckTweaks.UseVisualStyleBackColor = true;
            this.buttonWin10UncheckTweaks.Click += new System.EventHandler(this.buttonWin10UncheckTweaks_Click);
            // 
            // buttonWin10CheckTweaks
            // 
            this.buttonWin10CheckTweaks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonWin10CheckTweaks.Location = new System.Drawing.Point(217, 5);
            this.buttonWin10CheckTweaks.Name = "buttonWin10CheckTweaks";
            this.buttonWin10CheckTweaks.Size = new System.Drawing.Size(78, 23);
            this.buttonWin10CheckTweaks.TabIndex = 4;
            this.buttonWin10CheckTweaks.Text = "Check all";
            this.buttonWin10CheckTweaks.UseVisualStyleBackColor = true;
            this.buttonWin10CheckTweaks.Click += new System.EventHandler(this.buttonWin10CheckTweaks_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.checkedListBoxWin10Appearance);
            this.tabPage2.Controls.Add(this.buttonWin10UncheackAppearance);
            this.tabPage2.Controls.Add(this.buttonWin10CheckAppearance);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(385, 310);
            this.tabPage2.TabIndex = 3;
            this.tabPage2.Text = "Appearance";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.DarkViolet;
            this.label6.Location = new System.Drawing.Point(9, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Win10";
            this.label6.Visible = false;
            // 
            // checkedListBoxWin10Appearance
            // 
            this.checkedListBoxWin10Appearance.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBoxWin10Appearance.CheckOnClick = true;
            this.checkedListBoxWin10Appearance.Cursor = System.Windows.Forms.Cursors.Default;
            this.checkedListBoxWin10Appearance.FormattingEnabled = true;
            this.checkedListBoxWin10Appearance.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkedListBoxWin10Appearance.Items.AddRange(new object[] {
            "Add This PC shortcut to desktop",
            "Small taskbar icons",
            "Don\'t group tasks in taskbar",
            "Hide Taskview button in taskbar",
            "Hide People button in taskbar",
            "Hide search bar in taskbar",
            "Remove compatibility item from context menu",
            "Hide OneDrive Cloud states in File Explorer",
            "Always show file name extensions",
            "Remove OneDrive from File Explorer",
            "Delete quicklaunch items",
            "Use Windows 7 volume control",
            "Remove Microsoft Edge desktop shortcut",
            "Disable Lockscreen Blur",
            "Hide Meet Now icon in taskbar",
            "Hide News and interests in taskbar"});
            this.checkedListBoxWin10Appearance.Location = new System.Drawing.Point(2, 33);
            this.checkedListBoxWin10Appearance.Name = "checkedListBoxWin10Appearance";
            this.checkedListBoxWin10Appearance.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.checkedListBoxWin10Appearance.ScrollAlwaysVisible = true;
            this.checkedListBoxWin10Appearance.Size = new System.Drawing.Size(379, 274);
            this.checkedListBoxWin10Appearance.TabIndex = 6;
            // 
            // buttonWin10UncheackAppearance
            // 
            this.buttonWin10UncheackAppearance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonWin10UncheackAppearance.Location = new System.Drawing.Point(301, 5);
            this.buttonWin10UncheackAppearance.Name = "buttonWin10UncheackAppearance";
            this.buttonWin10UncheackAppearance.Size = new System.Drawing.Size(80, 23);
            this.buttonWin10UncheackAppearance.TabIndex = 8;
            this.buttonWin10UncheackAppearance.Text = "Uncheck all";
            this.buttonWin10UncheackAppearance.UseVisualStyleBackColor = true;
            this.buttonWin10UncheackAppearance.Click += new System.EventHandler(this.buttonWin10UncheackAppearance_Click);
            // 
            // buttonWin10CheckAppearance
            // 
            this.buttonWin10CheckAppearance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonWin10CheckAppearance.Location = new System.Drawing.Point(217, 5);
            this.buttonWin10CheckAppearance.Name = "buttonWin10CheckAppearance";
            this.buttonWin10CheckAppearance.Size = new System.Drawing.Size(78, 23);
            this.buttonWin10CheckAppearance.TabIndex = 7;
            this.buttonWin10CheckAppearance.Text = "Check all";
            this.buttonWin10CheckAppearance.UseVisualStyleBackColor = true;
            this.buttonWin10CheckAppearance.Click += new System.EventHandler(this.buttonWin10CheckAppearance_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label7);
            this.tabPage3.Controls.Add(this.buttonWin10UncheckSoftware);
            this.tabPage3.Controls.Add(this.buttonWin10CheckSoftware);
            this.tabPage3.Controls.Add(this.checkedListBoxWin10Software);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(385, 310);
            this.tabPage3.TabIndex = 1;
            this.tabPage3.Text = "Software";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.DarkViolet;
            this.label7.Location = new System.Drawing.Point(9, 10);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Win10";
            this.label7.Visible = false;
            // 
            // buttonWin10UncheckSoftware
            // 
            this.buttonWin10UncheckSoftware.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonWin10UncheckSoftware.Location = new System.Drawing.Point(301, 5);
            this.buttonWin10UncheckSoftware.Name = "buttonWin10UncheckSoftware";
            this.buttonWin10UncheckSoftware.Size = new System.Drawing.Size(80, 23);
            this.buttonWin10UncheckSoftware.TabIndex = 7;
            this.buttonWin10UncheckSoftware.Text = "Uncheck all";
            this.buttonWin10UncheckSoftware.UseVisualStyleBackColor = true;
            this.buttonWin10UncheckSoftware.Click += new System.EventHandler(this.buttonWin10UncheckSoftware_Click);
            // 
            // buttonWin10CheckSoftware
            // 
            this.buttonWin10CheckSoftware.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonWin10CheckSoftware.Location = new System.Drawing.Point(217, 5);
            this.buttonWin10CheckSoftware.Name = "buttonWin10CheckSoftware";
            this.buttonWin10CheckSoftware.Size = new System.Drawing.Size(78, 23);
            this.buttonWin10CheckSoftware.TabIndex = 6;
            this.buttonWin10CheckSoftware.Text = "Check all";
            this.buttonWin10CheckSoftware.UseVisualStyleBackColor = true;
            this.buttonWin10CheckSoftware.Click += new System.EventHandler(this.buttonWin10CheckSoftware_Click);
            // 
            // checkedListBoxWin10Software
            // 
            this.checkedListBoxWin10Software.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBoxWin10Software.CheckOnClick = true;
            this.checkedListBoxWin10Software.FormattingEnabled = true;
            this.checkedListBoxWin10Software.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkedListBoxWin10Software.Items.AddRange(new object[] {
            "Install 7Zip",
            "Install Adobe Acrobat Reader DC",
            "Install Audacity",
            "Install BalenaEtcher",
            "Install calibre",
            "Install CPU-Z",
            "Install Discord",
            "Install DupeGuru",
            "Install EarTrumpet",
            "Install Epic Games Launcher",
            "Install FileZilla",
            "Install GIMP",
            "Install GPU-Z",
            "Install Git",
            "Install Google Chrome",
            "Install Inkscape",
            "Install Irfanview",
            "Install Java Runtime Environment",
            "Install KeePassXC",
            "Install LibreOffice",
            "Install Minecraft",
            "Install Mozilla Firefox",
            "Install Mozilla Thunderbird",
            "Install Nextcloud Desktop",
            "Install Notepad++",
            "Install OBS Studio",
            "Install OpenHashTab",
            "Install OpenVPN Connect",
            "Install PowerToys",
            "Install PuTTY",
            "Install Python 3.11",
            "Install Slack",
            "Install Speccy",
            "Install Steam",
            "Install TeamViewer",
            "Install TeamSpeak",
            "Install Telegram",
            "Install Ubisoft Connect",
            "Install VirtualBox",
            "Install VLC media player",
            "Install WinRAR",
            "Install WinSCP",
            "Install Windows Terminal",
            "Install Wireguard",
            "Install Wireshark",
            "Install Zoom"});
            this.checkedListBoxWin10Software.Location = new System.Drawing.Point(2, 33);
            this.checkedListBoxWin10Software.Name = "checkedListBoxWin10Software";
            this.checkedListBoxWin10Software.ScrollAlwaysVisible = true;
            this.checkedListBoxWin10Software.Size = new System.Drawing.Size(379, 274);
            this.checkedListBoxWin10Software.TabIndex = 4;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.label8);
            this.tabPage4.Controls.Add(this.checkedListBoxWin10Advanced);
            this.tabPage4.Controls.Add(this.buttonWin10UncheckAdvanced);
            this.tabPage4.Controls.Add(this.buttonWin10CheckAdvanced);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(385, 310);
            this.tabPage4.TabIndex = 2;
            this.tabPage4.Text = "Advanced";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.DarkViolet;
            this.label8.Location = new System.Drawing.Point(9, 10);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "Win10";
            this.label8.Visible = false;
            // 
            // checkedListBoxWin10Advanced
            // 
            this.checkedListBoxWin10Advanced.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBoxWin10Advanced.CheckOnClick = true;
            this.checkedListBoxWin10Advanced.Cursor = System.Windows.Forms.Cursors.Default;
            this.checkedListBoxWin10Advanced.FormattingEnabled = true;
            this.checkedListBoxWin10Advanced.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkedListBoxWin10Advanced.Items.AddRange(new object[] {
            "Disable Background Apps",
            "Precision Trackpad: Disable keyboard block after clicking",
            "Disable Windows Defender",
            "Disable Link-local Multicast Name Resolution",
            "Disable Smart Multi-Homed Name Resolution",
            "Disable Web Proxy Auto-Discovery",
            "Disable Teredo tunneling",
            "Disable Intra-Site Automatic Tunnel Addressing Protocol",
            "Enable Windows Subsystem for Linux",
            "Uninstall Internet Explorer",
            "Enable Storage Sense",
            "Disable fast startup",
            "Disable mouse pointer acceleration"});
            this.checkedListBoxWin10Advanced.Location = new System.Drawing.Point(2, 33);
            this.checkedListBoxWin10Advanced.Name = "checkedListBoxWin10Advanced";
            this.checkedListBoxWin10Advanced.ScrollAlwaysVisible = true;
            this.checkedListBoxWin10Advanced.Size = new System.Drawing.Size(379, 274);
            this.checkedListBoxWin10Advanced.TabIndex = 6;
            // 
            // buttonWin10UncheckAdvanced
            // 
            this.buttonWin10UncheckAdvanced.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonWin10UncheckAdvanced.Location = new System.Drawing.Point(301, 5);
            this.buttonWin10UncheckAdvanced.Name = "buttonWin10UncheckAdvanced";
            this.buttonWin10UncheckAdvanced.Size = new System.Drawing.Size(80, 23);
            this.buttonWin10UncheckAdvanced.TabIndex = 8;
            this.buttonWin10UncheckAdvanced.Text = "Uncheck all";
            this.buttonWin10UncheckAdvanced.UseVisualStyleBackColor = true;
            this.buttonWin10UncheckAdvanced.Click += new System.EventHandler(this.buttonWin10UncheckAdvanced_Click);
            // 
            // buttonWin10CheckAdvanced
            // 
            this.buttonWin10CheckAdvanced.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonWin10CheckAdvanced.Location = new System.Drawing.Point(217, 5);
            this.buttonWin10CheckAdvanced.Name = "buttonWin10CheckAdvanced";
            this.buttonWin10CheckAdvanced.Size = new System.Drawing.Size(78, 23);
            this.buttonWin10CheckAdvanced.TabIndex = 7;
            this.buttonWin10CheckAdvanced.Text = "Check all";
            this.buttonWin10CheckAdvanced.UseVisualStyleBackColor = true;
            this.buttonWin10CheckAdvanced.Click += new System.EventHandler(this.buttonWin10CheckAdvanced_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 412);
            this.Controls.Add(this.linkGitHub);
            this.Controls.Add(this.labelOS);
            this.Controls.Add(this.parameternotice);
            this.Controls.Add(this.buttonSlap);
            this.Controls.Add(this.tabControlWin10);
            this.Controls.Add(this.tabControlWin11);
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WinSlap 1.7";
            this.Load += new System.EventHandler(this.WinSlap_Load);
            this.tabControlWin11.ResumeLayout(false);
            this.tabTweaks.ResumeLayout(false);
            this.tabTweaks.PerformLayout();
            this.tabAppearance.ResumeLayout(false);
            this.tabAppearance.PerformLayout();
            this.tabSoftware.ResumeLayout(false);
            this.tabSoftware.PerformLayout();
            this.tabWin11Advanced.ResumeLayout(false);
            this.tabWin11Advanced.PerformLayout();
            this.tabControlWin10.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonSlap;
        private System.Windows.Forms.Label parameternotice;
        private System.Windows.Forms.TabControl tabControlWin11;
        private System.Windows.Forms.TabPage tabTweaks;
        private System.Windows.Forms.CheckedListBox checkedListBoxWin11Tweaks;
        private System.Windows.Forms.TabPage tabSoftware;
        private System.Windows.Forms.CheckedListBox checkedListBoxWin11Software;
        private System.Windows.Forms.Button buttonWin11UncheckTweaks;
        private System.Windows.Forms.Button buttonWin11CheckTweaks;
        private System.Windows.Forms.Button buttonWin11UncheckSoftware;
        private System.Windows.Forms.Button buttonWin11CheckSoftware;
        private System.Windows.Forms.TabPage tabWin11Advanced;
        private System.Windows.Forms.CheckedListBox checkedListBoxWin11Advanced;
        private System.Windows.Forms.Button buttonWin11UncheckAdvanced;
        private System.Windows.Forms.Button buttonWin11CheckAdvanced;
        private System.Windows.Forms.TabPage tabAppearance;
        private System.Windows.Forms.CheckedListBox checkedListBoxWin11Appearance;
        private System.Windows.Forms.Button buttonWin11UncheckAppearance;
        private System.Windows.Forms.Button buttonWin11CheckAppearance;
        private System.Windows.Forms.Label labelOS;
        private System.Windows.Forms.LinkLabel linkGitHub;
        private System.Windows.Forms.TabControl tabControlWin10;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.CheckedListBox checkedListBoxWin10Tweaks;
        private System.Windows.Forms.Button buttonWin10UncheckTweaks;
        private System.Windows.Forms.Button buttonWin10CheckTweaks;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.CheckedListBox checkedListBoxWin10Appearance;
        private System.Windows.Forms.Button buttonWin10UncheackAppearance;
        private System.Windows.Forms.Button buttonWin10CheckAppearance;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button buttonWin10UncheckSoftware;
        private System.Windows.Forms.Button buttonWin10CheckSoftware;
        private System.Windows.Forms.CheckedListBox checkedListBoxWin10Software;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.CheckedListBox checkedListBoxWin10Advanced;
        private System.Windows.Forms.Button buttonWin10UncheckAdvanced;
        private System.Windows.Forms.Button buttonWin10CheckAdvanced;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
    }
}