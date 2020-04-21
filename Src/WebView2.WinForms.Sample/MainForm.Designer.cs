namespace MtrDev.WebView2.WinForms.Sample
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveScreenshotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getDocumentTitleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getBrowserVersionAfterCreationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getBrowserVersionBeforeCreationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.injectScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addInitializeScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeInitializeScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.postMessageStringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.postMessageJSONToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.subscribeToCDPEventToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.callCDPMethodToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.addCOMObjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.openDevToolsWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeWebViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeWebViewAndCleanupUserDataFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createWebViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createNewWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createNewThreadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.processToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.browserProcessInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.crashBrowserProcessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.blockedDomainsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setUserAgentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toggleJavaScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toggleWebMessagingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toggleFullscreenAllowedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toggleStatusBarEnabledToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toggleDevToolsEnabledToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toggleBlockImagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.javaScriptDialogsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.useDefaultScriptDialogsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.useCustomScriptDialogsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.useDeferredScriptDialogsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.completeDeferredScriptDialogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toggleContextMenusEnabledToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toggleRemoteObjectsAllowedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toggleZoomControlEnabledToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toggleVisibilityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getWebViewBoundsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.webViewAreaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemViewArea25 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemViewArea50 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemViewArea75 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemViewArea100 = new System.Windows.Forms.ToolStripMenuItem();
            this.webViewZoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemZoomhalf = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemZoomOne = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemZoomTwo = new System.Windows.Forms.ToolStripMenuItem();
            this.setFocusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reverseTabInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toggleTabHandlingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scenerioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.webMessagingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.remoteObjectsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eventMonitorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.navigationToolBar = new MtrDev.WebView2.WinForms.Sample.Controls.NavigationToolBar();
            this.menuStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.scriptToolStripMenuItem,
            this.windowToolStripMenuItem,
            this.processToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.scenerioToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1056, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveScreenshotToolStripMenuItem,
            this.getDocumentTitleToolStripMenuItem,
            this.getBrowserVersionAfterCreationToolStripMenuItem,
            this.getBrowserVersionBeforeCreationToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // saveScreenshotToolStripMenuItem
            // 
            this.saveScreenshotToolStripMenuItem.Name = "saveScreenshotToolStripMenuItem";
            this.saveScreenshotToolStripMenuItem.Size = new System.Drawing.Size(332, 26);
            this.saveScreenshotToolStripMenuItem.Text = "Save Screenshot";
            this.saveScreenshotToolStripMenuItem.Click += new System.EventHandler(this.saveScreenshotToolStripMenuItem_Click);
            // 
            // getDocumentTitleToolStripMenuItem
            // 
            this.getDocumentTitleToolStripMenuItem.Name = "getDocumentTitleToolStripMenuItem";
            this.getDocumentTitleToolStripMenuItem.Size = new System.Drawing.Size(332, 26);
            this.getDocumentTitleToolStripMenuItem.Text = "Get Document Title";
            this.getDocumentTitleToolStripMenuItem.Click += new System.EventHandler(this.getDocumentTitleToolStripMenuItem_Click);
            // 
            // getBrowserVersionAfterCreationToolStripMenuItem
            // 
            this.getBrowserVersionAfterCreationToolStripMenuItem.Name = "getBrowserVersionAfterCreationToolStripMenuItem";
            this.getBrowserVersionAfterCreationToolStripMenuItem.Size = new System.Drawing.Size(332, 26);
            this.getBrowserVersionAfterCreationToolStripMenuItem.Text = "Get Browser Version After Creation";
            this.getBrowserVersionAfterCreationToolStripMenuItem.Click += new System.EventHandler(this.getBrowserVersionAfterCreationToolStripMenuItem_Click);
            // 
            // getBrowserVersionBeforeCreationToolStripMenuItem
            // 
            this.getBrowserVersionBeforeCreationToolStripMenuItem.Name = "getBrowserVersionBeforeCreationToolStripMenuItem";
            this.getBrowserVersionBeforeCreationToolStripMenuItem.Size = new System.Drawing.Size(332, 26);
            this.getBrowserVersionBeforeCreationToolStripMenuItem.Text = "Get Browser Version Before Creation";
            this.getBrowserVersionBeforeCreationToolStripMenuItem.Click += new System.EventHandler(this.getBrowserVersionBeforeCreationToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(332, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // scriptToolStripMenuItem
            // 
            this.scriptToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.injectScriptToolStripMenuItem,
            this.addInitializeScriptToolStripMenuItem,
            this.removeInitializeScriptToolStripMenuItem,
            this.toolStripSeparator1,
            this.postMessageStringToolStripMenuItem,
            this.postMessageJSONToolStripMenuItem,
            this.toolStripSeparator2,
            this.subscribeToCDPEventToolStripMenuItem,
            this.callCDPMethodToolStripMenuItem,
            this.toolStripSeparator3,
            this.addCOMObjectToolStripMenuItem,
            this.toolStripSeparator4,
            this.openDevToolsWindowToolStripMenuItem});
            this.scriptToolStripMenuItem.Name = "scriptToolStripMenuItem";
            this.scriptToolStripMenuItem.Size = new System.Drawing.Size(61, 24);
            this.scriptToolStripMenuItem.Text = "&Script";
            // 
            // injectScriptToolStripMenuItem
            // 
            this.injectScriptToolStripMenuItem.Name = "injectScriptToolStripMenuItem";
            this.injectScriptToolStripMenuItem.Size = new System.Drawing.Size(252, 26);
            this.injectScriptToolStripMenuItem.Text = "Inject Script";
            this.injectScriptToolStripMenuItem.Click += new System.EventHandler(this.injectScriptToolStripMenuItem_Click);
            // 
            // addInitializeScriptToolStripMenuItem
            // 
            this.addInitializeScriptToolStripMenuItem.Name = "addInitializeScriptToolStripMenuItem";
            this.addInitializeScriptToolStripMenuItem.Size = new System.Drawing.Size(252, 26);
            this.addInitializeScriptToolStripMenuItem.Text = "Add Initialize Script";
            this.addInitializeScriptToolStripMenuItem.Click += new System.EventHandler(this.addInitializeScriptToolStripMenuItem_Click);
            // 
            // removeInitializeScriptToolStripMenuItem
            // 
            this.removeInitializeScriptToolStripMenuItem.Name = "removeInitializeScriptToolStripMenuItem";
            this.removeInitializeScriptToolStripMenuItem.Size = new System.Drawing.Size(252, 26);
            this.removeInitializeScriptToolStripMenuItem.Text = "Remove Initialize Script";
            this.removeInitializeScriptToolStripMenuItem.Click += new System.EventHandler(this.removeInitializeScriptToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(249, 6);
            // 
            // postMessageStringToolStripMenuItem
            // 
            this.postMessageStringToolStripMenuItem.Name = "postMessageStringToolStripMenuItem";
            this.postMessageStringToolStripMenuItem.Size = new System.Drawing.Size(252, 26);
            this.postMessageStringToolStripMenuItem.Text = "Post Message String";
            this.postMessageStringToolStripMenuItem.Click += new System.EventHandler(this.postMessageStringToolStripMenuItem_Click);
            // 
            // postMessageJSONToolStripMenuItem
            // 
            this.postMessageJSONToolStripMenuItem.Name = "postMessageJSONToolStripMenuItem";
            this.postMessageJSONToolStripMenuItem.Size = new System.Drawing.Size(252, 26);
            this.postMessageJSONToolStripMenuItem.Text = "Post Message JSON";
            this.postMessageJSONToolStripMenuItem.Click += new System.EventHandler(this.postMessageJSONToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(249, 6);
            // 
            // subscribeToCDPEventToolStripMenuItem
            // 
            this.subscribeToCDPEventToolStripMenuItem.Name = "subscribeToCDPEventToolStripMenuItem";
            this.subscribeToCDPEventToolStripMenuItem.Size = new System.Drawing.Size(252, 26);
            this.subscribeToCDPEventToolStripMenuItem.Text = "Subscribe to CDP event";
            this.subscribeToCDPEventToolStripMenuItem.Click += new System.EventHandler(this.subscribeToCDPEventToolStripMenuItem_Click);
            // 
            // callCDPMethodToolStripMenuItem
            // 
            this.callCDPMethodToolStripMenuItem.Name = "callCDPMethodToolStripMenuItem";
            this.callCDPMethodToolStripMenuItem.Size = new System.Drawing.Size(252, 26);
            this.callCDPMethodToolStripMenuItem.Text = "Call CDP method";
            this.callCDPMethodToolStripMenuItem.Click += new System.EventHandler(this.callCDPMethodToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(249, 6);
            // 
            // addCOMObjectToolStripMenuItem
            // 
            this.addCOMObjectToolStripMenuItem.Name = "addCOMObjectToolStripMenuItem";
            this.addCOMObjectToolStripMenuItem.Size = new System.Drawing.Size(252, 26);
            this.addCOMObjectToolStripMenuItem.Text = "Add COM object";
            this.addCOMObjectToolStripMenuItem.Click += new System.EventHandler(this.addCOMObjectToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(249, 6);
            // 
            // openDevToolsWindowToolStripMenuItem
            // 
            this.openDevToolsWindowToolStripMenuItem.Name = "openDevToolsWindowToolStripMenuItem";
            this.openDevToolsWindowToolStripMenuItem.Size = new System.Drawing.Size(252, 26);
            this.openDevToolsWindowToolStripMenuItem.Text = "Open DevTools Window";
            this.openDevToolsWindowToolStripMenuItem.Click += new System.EventHandler(this.openDevToolsWindowToolStripMenuItem_Click);
            // 
            // windowToolStripMenuItem
            // 
            this.windowToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeWebViewToolStripMenuItem,
            this.closeWebViewAndCleanupUserDataFolderToolStripMenuItem,
            this.createWebViewToolStripMenuItem,
            this.createNewWindowToolStripMenuItem,
            this.createNewThreadToolStripMenuItem});
            this.windowToolStripMenuItem.Name = "windowToolStripMenuItem";
            this.windowToolStripMenuItem.Size = new System.Drawing.Size(78, 24);
            this.windowToolStripMenuItem.Text = "&Window";
            // 
            // closeWebViewToolStripMenuItem
            // 
            this.closeWebViewToolStripMenuItem.Name = "closeWebViewToolStripMenuItem";
            this.closeWebViewToolStripMenuItem.Size = new System.Drawing.Size(388, 26);
            this.closeWebViewToolStripMenuItem.Text = "Close WebView";
            this.closeWebViewToolStripMenuItem.Click += new System.EventHandler(this.closeWebViewToolStripMenuItem_Click);
            // 
            // closeWebViewAndCleanupUserDataFolderToolStripMenuItem
            // 
            this.closeWebViewAndCleanupUserDataFolderToolStripMenuItem.Name = "closeWebViewAndCleanupUserDataFolderToolStripMenuItem";
            this.closeWebViewAndCleanupUserDataFolderToolStripMenuItem.Size = new System.Drawing.Size(388, 26);
            this.closeWebViewAndCleanupUserDataFolderToolStripMenuItem.Text = "Close WebView and cleanup user data folder";
            this.closeWebViewAndCleanupUserDataFolderToolStripMenuItem.Click += new System.EventHandler(this.closeWebViewAndCleanupUserDataFolderToolStripMenuItem_Click);
            // 
            // createWebViewToolStripMenuItem
            // 
            this.createWebViewToolStripMenuItem.Name = "createWebViewToolStripMenuItem";
            this.createWebViewToolStripMenuItem.Size = new System.Drawing.Size(388, 26);
            this.createWebViewToolStripMenuItem.Text = "Create WebView";
            this.createWebViewToolStripMenuItem.Click += new System.EventHandler(this.createWebViewToolStripMenuItem_Click);
            // 
            // createNewWindowToolStripMenuItem
            // 
            this.createNewWindowToolStripMenuItem.Name = "createNewWindowToolStripMenuItem";
            this.createNewWindowToolStripMenuItem.Size = new System.Drawing.Size(388, 26);
            this.createNewWindowToolStripMenuItem.Text = "Create New Window";
            this.createNewWindowToolStripMenuItem.Click += new System.EventHandler(this.createNewWindowToolStripMenuItem_Click);
            // 
            // createNewThreadToolStripMenuItem
            // 
            this.createNewThreadToolStripMenuItem.Name = "createNewThreadToolStripMenuItem";
            this.createNewThreadToolStripMenuItem.Size = new System.Drawing.Size(388, 26);
            this.createNewThreadToolStripMenuItem.Text = "Create New Thread";
            this.createNewThreadToolStripMenuItem.Click += new System.EventHandler(this.createNewThreadToolStripMenuItem_Click);
            // 
            // processToolStripMenuItem
            // 
            this.processToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.browserProcessInfoToolStripMenuItem,
            this.crashBrowserProcessToolStripMenuItem});
            this.processToolStripMenuItem.Name = "processToolStripMenuItem";
            this.processToolStripMenuItem.Size = new System.Drawing.Size(72, 24);
            this.processToolStripMenuItem.Text = "&Process";
            // 
            // browserProcessInfoToolStripMenuItem
            // 
            this.browserProcessInfoToolStripMenuItem.Name = "browserProcessInfoToolStripMenuItem";
            this.browserProcessInfoToolStripMenuItem.Size = new System.Drawing.Size(238, 26);
            this.browserProcessInfoToolStripMenuItem.Text = "Browser Process Info";
            this.browserProcessInfoToolStripMenuItem.Click += new System.EventHandler(this.browserProcessInfoToolStripMenuItem_Click);
            // 
            // crashBrowserProcessToolStripMenuItem
            // 
            this.crashBrowserProcessToolStripMenuItem.Name = "crashBrowserProcessToolStripMenuItem";
            this.crashBrowserProcessToolStripMenuItem.Size = new System.Drawing.Size(238, 26);
            this.crashBrowserProcessToolStripMenuItem.Text = "Crash Browser Process";
            this.crashBrowserProcessToolStripMenuItem.Click += new System.EventHandler(this.crashBrowserProcessToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.blockedDomainsToolStripMenuItem,
            this.setUserAgentToolStripMenuItem,
            this.toolStripSeparator5,
            this.toggleJavaScriptToolStripMenuItem,
            this.toggleWebMessagingToolStripMenuItem,
            this.toggleFullscreenAllowedToolStripMenuItem,
            this.toggleStatusBarEnabledToolStripMenuItem,
            this.toggleDevToolsEnabledToolStripMenuItem,
            this.toggleBlockImagesToolStripMenuItem,
            this.javaScriptDialogsToolStripMenuItem,
            this.toggleContextMenusEnabledToolStripMenuItem,
            this.toggleRemoteObjectsAllowedToolStripMenuItem,
            this.toggleZoomControlEnabledToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(76, 24);
            this.settingsToolStripMenuItem.Text = "S&ettings";
            // 
            // blockedDomainsToolStripMenuItem
            // 
            this.blockedDomainsToolStripMenuItem.Name = "blockedDomainsToolStripMenuItem";
            this.blockedDomainsToolStripMenuItem.Size = new System.Drawing.Size(299, 26);
            this.blockedDomainsToolStripMenuItem.Text = "Blocked Domains";
            this.blockedDomainsToolStripMenuItem.Click += new System.EventHandler(this.blockedDomainsToolStripMenuItem_Click);
            // 
            // setUserAgentToolStripMenuItem
            // 
            this.setUserAgentToolStripMenuItem.Name = "setUserAgentToolStripMenuItem";
            this.setUserAgentToolStripMenuItem.Size = new System.Drawing.Size(299, 26);
            this.setUserAgentToolStripMenuItem.Text = "Set User Agent";
            this.setUserAgentToolStripMenuItem.Click += new System.EventHandler(this.setUserAgentToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(296, 6);
            // 
            // toggleJavaScriptToolStripMenuItem
            // 
            this.toggleJavaScriptToolStripMenuItem.Name = "toggleJavaScriptToolStripMenuItem";
            this.toggleJavaScriptToolStripMenuItem.Size = new System.Drawing.Size(299, 26);
            this.toggleJavaScriptToolStripMenuItem.Text = "Toggle JavaScript";
            this.toggleJavaScriptToolStripMenuItem.Click += new System.EventHandler(this.toggleJavaScriptToolStripMenuItem_Click);
            // 
            // toggleWebMessagingToolStripMenuItem
            // 
            this.toggleWebMessagingToolStripMenuItem.Name = "toggleWebMessagingToolStripMenuItem";
            this.toggleWebMessagingToolStripMenuItem.Size = new System.Drawing.Size(299, 26);
            this.toggleWebMessagingToolStripMenuItem.Text = "Toggle Web Messaging";
            this.toggleWebMessagingToolStripMenuItem.Click += new System.EventHandler(this.toggleWebMessagingToolStripMenuItem_Click);
            // 
            // toggleFullscreenAllowedToolStripMenuItem
            // 
            this.toggleFullscreenAllowedToolStripMenuItem.Name = "toggleFullscreenAllowedToolStripMenuItem";
            this.toggleFullscreenAllowedToolStripMenuItem.Size = new System.Drawing.Size(299, 26);
            this.toggleFullscreenAllowedToolStripMenuItem.Text = "Toggle Fullscreen allowed";
            this.toggleFullscreenAllowedToolStripMenuItem.Click += new System.EventHandler(this.toggleFullscreenAllowedToolStripMenuItem_Click);
            // 
            // toggleStatusBarEnabledToolStripMenuItem
            // 
            this.toggleStatusBarEnabledToolStripMenuItem.Name = "toggleStatusBarEnabledToolStripMenuItem";
            this.toggleStatusBarEnabledToolStripMenuItem.Size = new System.Drawing.Size(299, 26);
            this.toggleStatusBarEnabledToolStripMenuItem.Text = "Toggle Status Bar enabled";
            this.toggleStatusBarEnabledToolStripMenuItem.Click += new System.EventHandler(this.toggleStatusBarEnabledToolStripMenuItem_Click);
            // 
            // toggleDevToolsEnabledToolStripMenuItem
            // 
            this.toggleDevToolsEnabledToolStripMenuItem.Name = "toggleDevToolsEnabledToolStripMenuItem";
            this.toggleDevToolsEnabledToolStripMenuItem.Size = new System.Drawing.Size(299, 26);
            this.toggleDevToolsEnabledToolStripMenuItem.Text = "Toggle DevTools enabled";
            this.toggleDevToolsEnabledToolStripMenuItem.Click += new System.EventHandler(this.toggleDevToolsEnabledToolStripMenuItem_Click);
            // 
            // toggleBlockImagesToolStripMenuItem
            // 
            this.toggleBlockImagesToolStripMenuItem.Name = "toggleBlockImagesToolStripMenuItem";
            this.toggleBlockImagesToolStripMenuItem.Size = new System.Drawing.Size(299, 26);
            this.toggleBlockImagesToolStripMenuItem.Text = "Toggle Block images";
            this.toggleBlockImagesToolStripMenuItem.Click += new System.EventHandler(this.toggleBlockImagesToolStripMenuItem_Click);
            // 
            // javaScriptDialogsToolStripMenuItem
            // 
            this.javaScriptDialogsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.useDefaultScriptDialogsToolStripMenuItem,
            this.useCustomScriptDialogsToolStripMenuItem,
            this.useDeferredScriptDialogsToolStripMenuItem,
            this.completeDeferredScriptDialogToolStripMenuItem});
            this.javaScriptDialogsToolStripMenuItem.Name = "javaScriptDialogsToolStripMenuItem";
            this.javaScriptDialogsToolStripMenuItem.Size = new System.Drawing.Size(299, 26);
            this.javaScriptDialogsToolStripMenuItem.Text = "JavaScript Dialogs";
            // 
            // useDefaultScriptDialogsToolStripMenuItem
            // 
            this.useDefaultScriptDialogsToolStripMenuItem.Name = "useDefaultScriptDialogsToolStripMenuItem";
            this.useDefaultScriptDialogsToolStripMenuItem.Size = new System.Drawing.Size(311, 26);
            this.useDefaultScriptDialogsToolStripMenuItem.Text = "Use Default Script Dialogs";
            this.useDefaultScriptDialogsToolStripMenuItem.Click += new System.EventHandler(this.useDefaultScriptDialogsToolStripMenuItem_Click);
            // 
            // useCustomScriptDialogsToolStripMenuItem
            // 
            this.useCustomScriptDialogsToolStripMenuItem.Name = "useCustomScriptDialogsToolStripMenuItem";
            this.useCustomScriptDialogsToolStripMenuItem.Size = new System.Drawing.Size(311, 26);
            this.useCustomScriptDialogsToolStripMenuItem.Text = "Use Custom Script Dialogs";
            this.useCustomScriptDialogsToolStripMenuItem.Click += new System.EventHandler(this.useCustomScriptDialogsToolStripMenuItem_Click);
            // 
            // useDeferredScriptDialogsToolStripMenuItem
            // 
            this.useDeferredScriptDialogsToolStripMenuItem.Name = "useDeferredScriptDialogsToolStripMenuItem";
            this.useDeferredScriptDialogsToolStripMenuItem.Size = new System.Drawing.Size(311, 26);
            this.useDeferredScriptDialogsToolStripMenuItem.Text = "Use Deferred Script Dialogs";
            this.useDeferredScriptDialogsToolStripMenuItem.Click += new System.EventHandler(this.useDeferredScriptDialogsToolStripMenuItem_Click);
            // 
            // completeDeferredScriptDialogToolStripMenuItem
            // 
            this.completeDeferredScriptDialogToolStripMenuItem.Name = "completeDeferredScriptDialogToolStripMenuItem";
            this.completeDeferredScriptDialogToolStripMenuItem.Size = new System.Drawing.Size(311, 26);
            this.completeDeferredScriptDialogToolStripMenuItem.Text = "Complete Deferred Script Dialog";
            this.completeDeferredScriptDialogToolStripMenuItem.Click += new System.EventHandler(this.completeDeferredScriptDialogToolStripMenuItem_Click);
            // 
            // toggleContextMenusEnabledToolStripMenuItem
            // 
            this.toggleContextMenusEnabledToolStripMenuItem.Name = "toggleContextMenusEnabledToolStripMenuItem";
            this.toggleContextMenusEnabledToolStripMenuItem.Size = new System.Drawing.Size(299, 26);
            this.toggleContextMenusEnabledToolStripMenuItem.Text = "Toggle context menus enabled";
            this.toggleContextMenusEnabledToolStripMenuItem.Click += new System.EventHandler(this.toggleContextMenusEnabledToolStripMenuItem_Click);
            // 
            // toggleRemoteObjectsAllowedToolStripMenuItem
            // 
            this.toggleRemoteObjectsAllowedToolStripMenuItem.Name = "toggleRemoteObjectsAllowedToolStripMenuItem";
            this.toggleRemoteObjectsAllowedToolStripMenuItem.Size = new System.Drawing.Size(299, 26);
            this.toggleRemoteObjectsAllowedToolStripMenuItem.Text = "Toggle remote objects allowed";
            this.toggleRemoteObjectsAllowedToolStripMenuItem.Click += new System.EventHandler(this.toggleRemoteObjectsAllowedToolStripMenuItem_Click);
            // 
            // toggleZoomControlEnabledToolStripMenuItem
            // 
            this.toggleZoomControlEnabledToolStripMenuItem.Name = "toggleZoomControlEnabledToolStripMenuItem";
            this.toggleZoomControlEnabledToolStripMenuItem.Size = new System.Drawing.Size(299, 26);
            this.toggleZoomControlEnabledToolStripMenuItem.Text = "Toggle zoom control enabled";
            this.toggleZoomControlEnabledToolStripMenuItem.Click += new System.EventHandler(this.toggleZoomControlEnabledToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toggleVisibilityToolStripMenuItem,
            this.getWebViewBoundsToolStripMenuItem,
            this.webViewAreaToolStripMenuItem,
            this.webViewZoomToolStripMenuItem,
            this.setFocusToolStripMenuItem,
            this.tabInToolStripMenuItem,
            this.reverseTabInToolStripMenuItem,
            this.toggleTabHandlingToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(55, 24);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // toggleVisibilityToolStripMenuItem
            // 
            this.toggleVisibilityToolStripMenuItem.Name = "toggleVisibilityToolStripMenuItem";
            this.toggleVisibilityToolStripMenuItem.Size = new System.Drawing.Size(234, 26);
            this.toggleVisibilityToolStripMenuItem.Text = "Toggle Visibility";
            this.toggleVisibilityToolStripMenuItem.Click += new System.EventHandler(this.toggleVisibilityToolStripMenuItem_Click);
            // 
            // getWebViewBoundsToolStripMenuItem
            // 
            this.getWebViewBoundsToolStripMenuItem.Name = "getWebViewBoundsToolStripMenuItem";
            this.getWebViewBoundsToolStripMenuItem.Size = new System.Drawing.Size(234, 26);
            this.getWebViewBoundsToolStripMenuItem.Text = "Get WebView Bounds";
            this.getWebViewBoundsToolStripMenuItem.Click += new System.EventHandler(this.getWebViewBoundsToolStripMenuItem_Click);
            // 
            // webViewAreaToolStripMenuItem
            // 
            this.webViewAreaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemViewArea25,
            this.menuItemViewArea50,
            this.menuItemViewArea75,
            this.menuItemViewArea100});
            this.webViewAreaToolStripMenuItem.Name = "webViewAreaToolStripMenuItem";
            this.webViewAreaToolStripMenuItem.Size = new System.Drawing.Size(234, 26);
            this.webViewAreaToolStripMenuItem.Text = "WebView Area";
            // 
            // menuItemViewArea25
            // 
            this.menuItemViewArea25.Name = "menuItemViewArea25";
            this.menuItemViewArea25.Size = new System.Drawing.Size(128, 26);
            this.menuItemViewArea25.Text = "25%";
            this.menuItemViewArea25.Click += new System.EventHandler(this.menuItemViewArea25_Click);
            // 
            // menuItemViewArea50
            // 
            this.menuItemViewArea50.Name = "menuItemViewArea50";
            this.menuItemViewArea50.Size = new System.Drawing.Size(128, 26);
            this.menuItemViewArea50.Text = "50%";
            this.menuItemViewArea50.Click += new System.EventHandler(this.menuItemViewArea50_Click);
            // 
            // menuItemViewArea75
            // 
            this.menuItemViewArea75.Name = "menuItemViewArea75";
            this.menuItemViewArea75.Size = new System.Drawing.Size(128, 26);
            this.menuItemViewArea75.Text = "75%";
            this.menuItemViewArea75.Click += new System.EventHandler(this.menuItemViewArea75_Click);
            // 
            // menuItemViewArea100
            // 
            this.menuItemViewArea100.Name = "menuItemViewArea100";
            this.menuItemViewArea100.Size = new System.Drawing.Size(128, 26);
            this.menuItemViewArea100.Text = "100%";
            this.menuItemViewArea100.Click += new System.EventHandler(this.menuItemViewArea100_Click);
            // 
            // webViewZoomToolStripMenuItem
            // 
            this.webViewZoomToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemZoomhalf,
            this.menuItemZoomOne,
            this.menuItemZoomTwo});
            this.webViewZoomToolStripMenuItem.Name = "webViewZoomToolStripMenuItem";
            this.webViewZoomToolStripMenuItem.Size = new System.Drawing.Size(234, 26);
            this.webViewZoomToolStripMenuItem.Text = "WebView Zoom";
            // 
            // menuItemZoomhalf
            // 
            this.menuItemZoomhalf.Name = "menuItemZoomhalf";
            this.menuItemZoomhalf.Size = new System.Drawing.Size(118, 26);
            this.menuItemZoomhalf.Text = "0.5x";
            this.menuItemZoomhalf.Click += new System.EventHandler(this.menuItemZoomhalf_Click);
            // 
            // menuItemZoomOne
            // 
            this.menuItemZoomOne.Name = "menuItemZoomOne";
            this.menuItemZoomOne.Size = new System.Drawing.Size(118, 26);
            this.menuItemZoomOne.Text = "1.0x";
            this.menuItemZoomOne.Click += new System.EventHandler(this.menuItemZoomOne_Click);
            // 
            // menuItemZoomTwo
            // 
            this.menuItemZoomTwo.Name = "menuItemZoomTwo";
            this.menuItemZoomTwo.Size = new System.Drawing.Size(118, 26);
            this.menuItemZoomTwo.Text = "2.0x";
            this.menuItemZoomTwo.Click += new System.EventHandler(this.menuItemZoomTwo_Click);
            // 
            // setFocusToolStripMenuItem
            // 
            this.setFocusToolStripMenuItem.Name = "setFocusToolStripMenuItem";
            this.setFocusToolStripMenuItem.Size = new System.Drawing.Size(234, 26);
            this.setFocusToolStripMenuItem.Text = "Set Focus";
            this.setFocusToolStripMenuItem.Click += new System.EventHandler(this.setFocusToolStripMenuItem_Click);
            // 
            // tabInToolStripMenuItem
            // 
            this.tabInToolStripMenuItem.Name = "tabInToolStripMenuItem";
            this.tabInToolStripMenuItem.Size = new System.Drawing.Size(234, 26);
            this.tabInToolStripMenuItem.Text = "Tab In";
            this.tabInToolStripMenuItem.Click += new System.EventHandler(this.tabInToolStripMenuItem_Click);
            // 
            // reverseTabInToolStripMenuItem
            // 
            this.reverseTabInToolStripMenuItem.Name = "reverseTabInToolStripMenuItem";
            this.reverseTabInToolStripMenuItem.Size = new System.Drawing.Size(234, 26);
            this.reverseTabInToolStripMenuItem.Text = "Reverse Tab In";
            this.reverseTabInToolStripMenuItem.Click += new System.EventHandler(this.reverseTabInToolStripMenuItem_Click);
            // 
            // toggleTabHandlingToolStripMenuItem
            // 
            this.toggleTabHandlingToolStripMenuItem.Name = "toggleTabHandlingToolStripMenuItem";
            this.toggleTabHandlingToolStripMenuItem.Size = new System.Drawing.Size(234, 26);
            this.toggleTabHandlingToolStripMenuItem.Text = "Toggle Tab Handling";
            this.toggleTabHandlingToolStripMenuItem.Click += new System.EventHandler(this.toggleTabHandlingToolStripMenuItem_Click);
            // 
            // scenerioToolStripMenuItem
            // 
            this.scenerioToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.webMessagingToolStripMenuItem,
            this.remoteObjectsToolStripMenuItem,
            this.eventMonitorToolStripMenuItem});
            this.scenerioToolStripMenuItem.Name = "scenerioToolStripMenuItem";
            this.scenerioToolStripMenuItem.Size = new System.Drawing.Size(80, 24);
            this.scenerioToolStripMenuItem.Text = "S&cenerio";
            // 
            // webMessagingToolStripMenuItem
            // 
            this.webMessagingToolStripMenuItem.Name = "webMessagingToolStripMenuItem";
            this.webMessagingToolStripMenuItem.Size = new System.Drawing.Size(198, 26);
            this.webMessagingToolStripMenuItem.Text = "Web Messaging";
            this.webMessagingToolStripMenuItem.Click += new System.EventHandler(this.webMessagingToolStripMenuItem_Click);
            // 
            // remoteObjectsToolStripMenuItem
            // 
            this.remoteObjectsToolStripMenuItem.Name = "remoteObjectsToolStripMenuItem";
            this.remoteObjectsToolStripMenuItem.Size = new System.Drawing.Size(198, 26);
            this.remoteObjectsToolStripMenuItem.Text = "Remote Objects";
            this.remoteObjectsToolStripMenuItem.Click += new System.EventHandler(this.remoteObjectsToolStripMenuItem_Click);
            // 
            // eventMonitorToolStripMenuItem
            // 
            this.eventMonitorToolStripMenuItem.Name = "eventMonitorToolStripMenuItem";
            this.eventMonitorToolStripMenuItem.Size = new System.Drawing.Size(198, 26);
            this.eventMonitorToolStripMenuItem.Text = "Event Monitor";
            this.eventMonitorToolStripMenuItem.Click += new System.EventHandler(this.eventMonitorToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(55, 24);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(146, 26);
            this.aboutToolStripMenuItem.Text = "&About ...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel1.Controls.Add(this.navigationToolBar, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 28);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1056, 422);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // navigationToolBar
            // 
            this.navigationToolBar.CanCancel = true;
            this.navigationToolBar.CanGoBack = false;
            this.navigationToolBar.CanGoForward = false;
            this.tableLayoutPanel1.SetColumnSpan(this.navigationToolBar, 6);
            this.navigationToolBar.Location = new System.Drawing.Point(5, 5);
            this.navigationToolBar.Margin = new System.Windows.Forms.Padding(5);
            this.navigationToolBar.Name = "navigationToolBar";
            this.navigationToolBar.Size = new System.Drawing.Size(1046, 32);
            this.navigationToolBar.TabIndex = 8;
            this.navigationToolBar.Url = "https://www.bing.com/";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1056, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveScreenshotToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getDocumentTitleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getBrowserVersionAfterCreationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getBrowserVersionBeforeCreationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scriptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem injectScriptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addInitializeScriptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeInitializeScriptToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem postMessageStringToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem postMessageJSONToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem subscribeToCDPEventToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem callCDPMethodToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem addCOMObjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem openDevToolsWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem windowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeWebViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createWebViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createNewWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createNewThreadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem processToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem browserProcessInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem crashBrowserProcessToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem blockedDomainsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setUserAgentToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem toggleJavaScriptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toggleWebMessagingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toggleFullscreenAllowedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toggleStatusBarEnabledToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toggleDevToolsEnabledToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toggleBlockImagesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem javaScriptDialogsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem useDefaultScriptDialogsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem useCustomScriptDialogsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem useDeferredScriptDialogsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem completeDeferredScriptDialogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toggleContextMenusEnabledToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toggleVisibilityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getWebViewBoundsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem webViewAreaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuItemViewArea25;
        private System.Windows.Forms.ToolStripMenuItem menuItemViewArea50;
        private System.Windows.Forms.ToolStripMenuItem menuItemViewArea75;
        private System.Windows.Forms.ToolStripMenuItem menuItemViewArea100;
        private System.Windows.Forms.ToolStripMenuItem webViewZoomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuItemZoomhalf;
        private System.Windows.Forms.ToolStripMenuItem menuItemZoomOne;
        private System.Windows.Forms.ToolStripMenuItem menuItemZoomTwo;
        private System.Windows.Forms.ToolStripMenuItem setFocusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tabInToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reverseTabInToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toggleTabHandlingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scenerioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem webMessagingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem remoteObjectsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eventMonitorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Controls.NavigationToolBar navigationToolBar;
        private System.Windows.Forms.ToolStripMenuItem toggleRemoteObjectsAllowedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toggleZoomControlEnabledToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeWebViewAndCleanupUserDataFolderToolStripMenuItem;
    }
}

