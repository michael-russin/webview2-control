using MtrDev.WinForms;

namespace WinForms.WebView2.Demo
{
    partial class WebView2Browser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WebView2Browser));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.controlsWebView2 = new MtrDev.WinForms.WebView2Control();
            this.webView2Control2 = new MtrDev.WinForms.WebView2Control();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.controlsWebView2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.webView2Control2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 87F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1067, 554);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // controlsWebView2
            // 
            this.controlsWebView2.AreDevToolsEnabled = true;
            this.controlsWebView2.BackColor = System.Drawing.SystemColors.Control;
            this.controlsWebView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlsWebView2.IsFullscreenAllowed = true;
            this.controlsWebView2.Location = new System.Drawing.Point(0, 0);
            this.controlsWebView2.Margin = new System.Windows.Forms.Padding(0);
            this.controlsWebView2.Name = "controlsWebView2";
            this.controlsWebView2.Size = new System.Drawing.Size(1067, 87);
            this.controlsWebView2.TabIndex = 0;
            this.controlsWebView2.BeforeEnvironmentCreated += new System.EventHandler<MtrDev.WinForms.BeforeEnvironmentCreatedEventArgs>(this.controlsWebView2_BeforeEnvironmentCreated);
            this.controlsWebView2.EnvironmentCreated += new System.EventHandler<MtrDev.WinForms.EnvironmentCreatedEventArgs>(this.controlsWebView2_EnvironmentCreated);
            this.controlsWebView2.BrowserCreated += new System.EventHandler<System.EventArgs>(this.controlsWebView2_BrowserCreated);
            this.controlsWebView2.ZoomFactorChanged += new System.EventHandler<MtrDev.WinForms.ZoomFactorCompletedEventArgs>(this.controlsWebView2_ZoomFactorChanged);
            this.controlsWebView2.WebMessageRecieved += new System.EventHandler<MtrDev.WinForms.WebMessageReceivedEventArgs>(this.controlsWebView2_WebMessageRecieved);
            // 
            // webView2Control2
            // 
            this.webView2Control2.AreDevToolsEnabled = true;
            this.webView2Control2.BackColor = System.Drawing.SystemColors.Control;
            this.webView2Control2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webView2Control2.IsFullscreenAllowed = true;
            this.webView2Control2.Location = new System.Drawing.Point(4, 91);
            this.webView2Control2.Margin = new System.Windows.Forms.Padding(4);
            this.webView2Control2.Name = "webView2Control2";
            this.webView2Control2.Size = new System.Drawing.Size(1059, 459);
            this.webView2Control2.TabIndex = 1;
            this.webView2Control2.Visible = false;
            this.webView2Control2.BeforeEnvironmentCreated += new System.EventHandler<MtrDev.WinForms.BeforeEnvironmentCreatedEventArgs>(this.webView2Control2_BeforeEnvironmentCreated);
            this.webView2Control2.EnvironmentCreated += new System.EventHandler<MtrDev.WinForms.EnvironmentCreatedEventArgs>(this.webView2Control2_EnvironmentCreated);
            // 
            // WebView2Browser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "WebView2Browser";
            this.Text = "WebView2 Windows Forms Browser";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private WebView2Control controlsWebView2;
        private WebView2Control webView2Control2;
    }
}