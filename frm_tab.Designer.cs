namespace webtab
{
    partial class frm_tab
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_tab));
            this.rpw_sekme = new Telerik.WinControls.UI.RadPageView();
            this.office2013LightTheme1 = new Telerik.WinControls.Themes.Office2013LightTheme();
            ((System.ComponentModel.ISupportInitialize)(this.rpw_sekme)).BeginInit();
            this.SuspendLayout();
            // 
            // rpw_sekme
            // 
            this.rpw_sekme.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rpw_sekme.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.rpw_sekme.Cursor = System.Windows.Forms.Cursors.Default;
            this.rpw_sekme.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.rpw_sekme.ItemSizeMode = Telerik.WinControls.UI.PageViewItemSizeMode.Individual;
            this.rpw_sekme.Location = new System.Drawing.Point(-5, 0);
            this.rpw_sekme.Name = "rpw_sekme";
            // 
            // 
            // 
            this.rpw_sekme.RootElement.ControlBounds = new System.Drawing.Rectangle(-5, 0, 400, 300);
            this.rpw_sekme.Size = new System.Drawing.Size(783, 441);
            this.rpw_sekme.TabIndex = 0;
            this.rpw_sekme.Text = "rpv";
            this.rpw_sekme.ThemeName = "Office2013Light";
            this.rpw_sekme.SelectedPageChanged += new System.EventHandler(this.radPageView1_SelectedPageChanged);
            ((Telerik.WinControls.UI.RadPageViewStripElement)(this.rpw_sekme.GetChildAt(0))).StripButtons = Telerik.WinControls.UI.StripViewButtons.Scroll;
            ((Telerik.WinControls.UI.RadPageViewStripElement)(this.rpw_sekme.GetChildAt(0))).ShowItemCloseButton = true;
            ((Telerik.WinControls.UI.RadPageViewStripElement)(this.rpw_sekme.GetChildAt(0))).ItemSizeMode = Telerik.WinControls.UI.PageViewItemSizeMode.Individual;
            // 
            // frm_tabber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(773, 436);
            this.Controls.Add(this.rpw_sekme);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(300, 300);
            this.Name = "frm_tabber";
            this.Text = "Tab Browser";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.rpw_sekme)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadPageView rpw_sekme;
        private Telerik.WinControls.Themes.Office2013LightTheme office2013LightTheme1;
    }
}

