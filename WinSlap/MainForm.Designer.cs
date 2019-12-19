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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabTweaks = new System.Windows.Forms.TabPage();
            this.checkedListBoxTweaks = new System.Windows.Forms.CheckedListBox();
            this.buttonUncheckTweaks = new System.Windows.Forms.Button();
            this.buttonCheckTweaks = new System.Windows.Forms.Button();
            this.tabAppearance = new System.Windows.Forms.TabPage();
            this.checkedListBoxAppearance = new System.Windows.Forms.CheckedListBox();
            this.buttonUncheckAppearance = new System.Windows.Forms.Button();
            this.buttonCheckAppearance = new System.Windows.Forms.Button();
            this.tabSoftware = new System.Windows.Forms.TabPage();
            this.buttonUncheckSoftware = new System.Windows.Forms.Button();
            this.buttonCheckSoftware = new System.Windows.Forms.Button();
            this.checkedListBoxSoftware = new System.Windows.Forms.CheckedListBox();
            this.tabAdvanced = new System.Windows.Forms.TabPage();
            this.checkedListBoxAdvanced = new System.Windows.Forms.CheckedListBox();
            this.buttonUncheckAdvanced = new System.Windows.Forms.Button();
            this.buttonCheckAdvanced = new System.Windows.Forms.Button();
            this.labelOS = new System.Windows.Forms.Label();
            this.linkGitHub = new System.Windows.Forms.LinkLabel();
            this.tabControl1.SuspendLayout();
            this.tabTweaks.SuspendLayout();
            this.tabAppearance.SuspendLayout();
            this.tabSoftware.SuspendLayout();
            this.tabAdvanced.SuspendLayout();
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
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabTweaks);
            this.tabControl1.Controls.Add(this.tabAppearance);
            this.tabControl1.Controls.Add(this.tabSoftware);
            this.tabControl1.Controls.Add(this.tabAdvanced);
            this.tabControl1.Location = new System.Drawing.Point(-1, 38);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(393, 336);
            this.tabControl1.TabIndex = 6;
            // 
            // tabTweaks
            // 
            this.tabTweaks.Controls.Add(this.checkedListBoxTweaks);
            this.tabTweaks.Controls.Add(this.buttonUncheckTweaks);
            this.tabTweaks.Controls.Add(this.buttonCheckTweaks);
            this.tabTweaks.Cursor = System.Windows.Forms.Cursors.Default;
            this.tabTweaks.Location = new System.Drawing.Point(4, 22);
            this.tabTweaks.Name = "tabTweaks";
            this.tabTweaks.Padding = new System.Windows.Forms.Padding(3);
            this.tabTweaks.Size = new System.Drawing.Size(385, 310);
            this.tabTweaks.TabIndex = 0;
            this.tabTweaks.Text = "Tweaks";
            this.tabTweaks.UseVisualStyleBackColor = true;
            // 
            // checkedListBoxTweaks
            // 
            this.checkedListBoxTweaks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBoxTweaks.CheckOnClick = true;
            this.checkedListBoxTweaks.Cursor = System.Windows.Forms.Cursors.Default;
            this.checkedListBoxTweaks.FormattingEnabled = true;
            this.checkedListBoxTweaks.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkedListBoxTweaks.Items.AddRange(new object[] {
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
            "Disable Background Apps",
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
            "Remove Microsoft XPS Document Writer"});
            this.checkedListBoxTweaks.Location = new System.Drawing.Point(2, 33);
            this.checkedListBoxTweaks.Name = "checkedListBoxTweaks";
            this.checkedListBoxTweaks.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.checkedListBoxTweaks.ScrollAlwaysVisible = true;
            this.checkedListBoxTweaks.Size = new System.Drawing.Size(379, 274);
            this.checkedListBoxTweaks.TabIndex = 3;
            // 
            // buttonUncheckTweaks
            // 
            this.buttonUncheckTweaks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonUncheckTweaks.Location = new System.Drawing.Point(301, 5);
            this.buttonUncheckTweaks.Name = "buttonUncheckTweaks";
            this.buttonUncheckTweaks.Size = new System.Drawing.Size(80, 23);
            this.buttonUncheckTweaks.TabIndex = 5;
            this.buttonUncheckTweaks.Text = "Uncheck all";
            this.buttonUncheckTweaks.UseVisualStyleBackColor = true;
            this.buttonUncheckTweaks.Click += new System.EventHandler(this.ButtonUncheckTweaks_Click);
            // 
            // buttonCheckTweaks
            // 
            this.buttonCheckTweaks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCheckTweaks.Location = new System.Drawing.Point(217, 5);
            this.buttonCheckTweaks.Name = "buttonCheckTweaks";
            this.buttonCheckTweaks.Size = new System.Drawing.Size(78, 23);
            this.buttonCheckTweaks.TabIndex = 4;
            this.buttonCheckTweaks.Text = "Check all";
            this.buttonCheckTweaks.UseVisualStyleBackColor = true;
            this.buttonCheckTweaks.Click += new System.EventHandler(this.ButtonCheckTweaks_Click);
            // 
            // tabAppearance
            // 
            this.tabAppearance.Controls.Add(this.checkedListBoxAppearance);
            this.tabAppearance.Controls.Add(this.buttonUncheckAppearance);
            this.tabAppearance.Controls.Add(this.buttonCheckAppearance);
            this.tabAppearance.Location = new System.Drawing.Point(4, 22);
            this.tabAppearance.Name = "tabAppearance";
            this.tabAppearance.Padding = new System.Windows.Forms.Padding(3);
            this.tabAppearance.Size = new System.Drawing.Size(385, 310);
            this.tabAppearance.TabIndex = 3;
            this.tabAppearance.Text = "Appearance";
            this.tabAppearance.UseVisualStyleBackColor = true;
            // 
            // checkedListBoxAppearance
            // 
            this.checkedListBoxAppearance.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBoxAppearance.CheckOnClick = true;
            this.checkedListBoxAppearance.Cursor = System.Windows.Forms.Cursors.Default;
            this.checkedListBoxAppearance.FormattingEnabled = true;
            this.checkedListBoxAppearance.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkedListBoxAppearance.Items.AddRange(new object[] {
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
            "Disable Lockscreen Blur"});
            this.checkedListBoxAppearance.Location = new System.Drawing.Point(2, 33);
            this.checkedListBoxAppearance.Name = "checkedListBoxAppearance";
            this.checkedListBoxAppearance.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.checkedListBoxAppearance.ScrollAlwaysVisible = true;
            this.checkedListBoxAppearance.Size = new System.Drawing.Size(379, 274);
            this.checkedListBoxAppearance.TabIndex = 6;
            // 
            // buttonUncheckAppearance
            // 
            this.buttonUncheckAppearance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonUncheckAppearance.Location = new System.Drawing.Point(301, 5);
            this.buttonUncheckAppearance.Name = "buttonUncheckAppearance";
            this.buttonUncheckAppearance.Size = new System.Drawing.Size(80, 23);
            this.buttonUncheckAppearance.TabIndex = 8;
            this.buttonUncheckAppearance.Text = "Uncheck all";
            this.buttonUncheckAppearance.UseVisualStyleBackColor = true;
            this.buttonUncheckAppearance.Click += new System.EventHandler(this.ButtonUncheckAppearance_Click);
            // 
            // buttonCheckAppearance
            // 
            this.buttonCheckAppearance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCheckAppearance.Location = new System.Drawing.Point(217, 5);
            this.buttonCheckAppearance.Name = "buttonCheckAppearance";
            this.buttonCheckAppearance.Size = new System.Drawing.Size(78, 23);
            this.buttonCheckAppearance.TabIndex = 7;
            this.buttonCheckAppearance.Text = "Check all";
            this.buttonCheckAppearance.UseVisualStyleBackColor = true;
            this.buttonCheckAppearance.Click += new System.EventHandler(this.ButtonCheckAppearance_Click);
            // 
            // tabSoftware
            // 
            this.tabSoftware.Controls.Add(this.buttonUncheckSoftware);
            this.tabSoftware.Controls.Add(this.buttonCheckSoftware);
            this.tabSoftware.Controls.Add(this.checkedListBoxSoftware);
            this.tabSoftware.Location = new System.Drawing.Point(4, 22);
            this.tabSoftware.Name = "tabSoftware";
            this.tabSoftware.Padding = new System.Windows.Forms.Padding(3);
            this.tabSoftware.Size = new System.Drawing.Size(385, 310);
            this.tabSoftware.TabIndex = 1;
            this.tabSoftware.Text = "Software";
            this.tabSoftware.UseVisualStyleBackColor = true;
            // 
            // buttonUncheckSoftware
            // 
            this.buttonUncheckSoftware.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonUncheckSoftware.Location = new System.Drawing.Point(301, 5);
            this.buttonUncheckSoftware.Name = "buttonUncheckSoftware";
            this.buttonUncheckSoftware.Size = new System.Drawing.Size(80, 23);
            this.buttonUncheckSoftware.TabIndex = 7;
            this.buttonUncheckSoftware.Text = "Uncheck all";
            this.buttonUncheckSoftware.UseVisualStyleBackColor = true;
            this.buttonUncheckSoftware.Click += new System.EventHandler(this.ButtonUncheckSoftware_Click);
            // 
            // buttonCheckSoftware
            // 
            this.buttonCheckSoftware.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCheckSoftware.Location = new System.Drawing.Point(217, 5);
            this.buttonCheckSoftware.Name = "buttonCheckSoftware";
            this.buttonCheckSoftware.Size = new System.Drawing.Size(78, 23);
            this.buttonCheckSoftware.TabIndex = 6;
            this.buttonCheckSoftware.Text = "Check all";
            this.buttonCheckSoftware.UseVisualStyleBackColor = true;
            this.buttonCheckSoftware.Click += new System.EventHandler(this.ButtonCheckSoftware_Click);
            // 
            // checkedListBoxSoftware
            // 
            this.checkedListBoxSoftware.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBoxSoftware.CheckOnClick = true;
            this.checkedListBoxSoftware.FormattingEnabled = true;
            this.checkedListBoxSoftware.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkedListBoxSoftware.Items.AddRange(new object[] {
            "Install Mozilla Firefox",
            "Install Mozilla Thunderbird",
            "Install VLC media player",
            "Install Telegram",
            "Install StartIsBack++"});
            this.checkedListBoxSoftware.Location = new System.Drawing.Point(2, 33);
            this.checkedListBoxSoftware.Name = "checkedListBoxSoftware";
            this.checkedListBoxSoftware.ScrollAlwaysVisible = true;
            this.checkedListBoxSoftware.Size = new System.Drawing.Size(379, 274);
            this.checkedListBoxSoftware.TabIndex = 4;
            // 
            // tabAdvanced
            // 
            this.tabAdvanced.Controls.Add(this.checkedListBoxAdvanced);
            this.tabAdvanced.Controls.Add(this.buttonUncheckAdvanced);
            this.tabAdvanced.Controls.Add(this.buttonCheckAdvanced);
            this.tabAdvanced.Location = new System.Drawing.Point(4, 22);
            this.tabAdvanced.Name = "tabAdvanced";
            this.tabAdvanced.Padding = new System.Windows.Forms.Padding(3);
            this.tabAdvanced.Size = new System.Drawing.Size(385, 310);
            this.tabAdvanced.TabIndex = 2;
            this.tabAdvanced.Text = "Advanced";
            this.tabAdvanced.UseVisualStyleBackColor = true;
            // 
            // checkedListBoxAdvanced
            // 
            this.checkedListBoxAdvanced.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBoxAdvanced.CheckOnClick = true;
            this.checkedListBoxAdvanced.Cursor = System.Windows.Forms.Cursors.Default;
            this.checkedListBoxAdvanced.FormattingEnabled = true;
            this.checkedListBoxAdvanced.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkedListBoxAdvanced.Items.AddRange(new object[] {
            "Precision Trackpad: Disable keyboard block after clicking",
            "Disable Windows Defender",
            "Disable Link-local Multicast Name Resolution",
            "Disable Smart Multi-Homed Name Resolution",
            "Disable Web Proxy Auto-Discovery",
            "Disable Teredo tunneling",
            "Disable Intra-Site Automatic Tunnel Addressing Protocol"});
            this.checkedListBoxAdvanced.Location = new System.Drawing.Point(2, 33);
            this.checkedListBoxAdvanced.Name = "checkedListBoxAdvanced";
            this.checkedListBoxAdvanced.ScrollAlwaysVisible = true;
            this.checkedListBoxAdvanced.Size = new System.Drawing.Size(379, 274);
            this.checkedListBoxAdvanced.TabIndex = 6;
            // 
            // buttonUncheckAdvanced
            // 
            this.buttonUncheckAdvanced.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonUncheckAdvanced.Location = new System.Drawing.Point(301, 5);
            this.buttonUncheckAdvanced.Name = "buttonUncheckAdvanced";
            this.buttonUncheckAdvanced.Size = new System.Drawing.Size(80, 23);
            this.buttonUncheckAdvanced.TabIndex = 8;
            this.buttonUncheckAdvanced.Text = "Uncheck all";
            this.buttonUncheckAdvanced.UseVisualStyleBackColor = true;
            this.buttonUncheckAdvanced.Click += new System.EventHandler(this.ButtonUncheckAdvanced_Click);
            // 
            // buttonCheckAdvanced
            // 
            this.buttonCheckAdvanced.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCheckAdvanced.Location = new System.Drawing.Point(217, 5);
            this.buttonCheckAdvanced.Name = "buttonCheckAdvanced";
            this.buttonCheckAdvanced.Size = new System.Drawing.Size(78, 23);
            this.buttonCheckAdvanced.TabIndex = 7;
            this.buttonCheckAdvanced.Text = "Check all";
            this.buttonCheckAdvanced.UseVisualStyleBackColor = true;
            this.buttonCheckAdvanced.Click += new System.EventHandler(this.ButtonCheckAdvanced_Click);
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
            this.linkGitHub.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkGitHub_LinkClicked);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 412);
            this.Controls.Add(this.linkGitHub);
            this.Controls.Add(this.labelOS);
            this.Controls.Add(this.parameternotice);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.buttonSlap);
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WinSlap 1.2";
            this.Load += new System.EventHandler(this.WinSlap_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabTweaks.ResumeLayout(false);
            this.tabAppearance.ResumeLayout(false);
            this.tabSoftware.ResumeLayout(false);
            this.tabAdvanced.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonSlap;
        private System.Windows.Forms.Label parameternotice;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabTweaks;
        private System.Windows.Forms.CheckedListBox checkedListBoxTweaks;
        private System.Windows.Forms.TabPage tabSoftware;
        private System.Windows.Forms.CheckedListBox checkedListBoxSoftware;
        private System.Windows.Forms.Button buttonUncheckTweaks;
        private System.Windows.Forms.Button buttonCheckTweaks;
        private System.Windows.Forms.Button buttonUncheckSoftware;
        private System.Windows.Forms.Button buttonCheckSoftware;
        private System.Windows.Forms.TabPage tabAdvanced;
        private System.Windows.Forms.CheckedListBox checkedListBoxAdvanced;
        private System.Windows.Forms.Button buttonUncheckAdvanced;
        private System.Windows.Forms.Button buttonCheckAdvanced;
        private System.Windows.Forms.TabPage tabAppearance;
        private System.Windows.Forms.CheckedListBox checkedListBoxAppearance;
        private System.Windows.Forms.Button buttonUncheckAppearance;
        private System.Windows.Forms.Button buttonCheckAppearance;
        private System.Windows.Forms.Label labelOS;
        private System.Windows.Forms.LinkLabel linkGitHub;
    }
}