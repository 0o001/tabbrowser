using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI; //Component
using TheCodeKing.ActiveButtons.Controls; //Component
using System.Net;
using System.IO;
using System.Globalization;

namespace webtab
{
    public partial class frm_tab : Form
    {
        public frm_tab()
        {
            InitializeComponent();
            this.rpw_sekme.ItemSizeMode = PageViewItemSizeMode.Individual;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            EkleButon("+", YeniSekme);
        }

        private void YeniSekme(object sender, EventArgs e)
        {
            usr_sayfa ac = new usr_sayfa();
            RadPageViewPage sayfa = new RadPageViewPage();
            ac.Dock = DockStyle.Fill;
            sayfa.Text = "Yeni Sekme";
            sayfa.Controls.Add(ac); //pagevievpagede usercontrolü aç
            rpw_sekme.Pages.Add(sayfa);//sekme olarak ekler
            rpw_sekme.SelectedPage = sayfa; //yeni sekme açıldığında aktif sayfa ol
        }

        private void EkleButon(string yazi, EventHandler handler) //Yeni sekme + butonu oluşturmak
        {
            IActiveMenu menu = ActiveMenu.GetInstance(this);
            ActiveButton btn = new ActiveButton();
            btn.Text = null;
            btn.Dock = DockStyle.Fill;
            btn.Visible = false;
            menu.Items.Add(btn);
            ActiveButton button = new ActiveButton();
            button.Font = new Font("Symbol", 12, FontStyle.Regular);
            button.Text = yazi;
            button.FlatStyle = FlatStyle.Flat;
            int argbRenk = (int)Microsoft.Win32.Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColor", null); //Regeditte kayıtlı olan renk değerinin ondalık değeri
            string rif = ConverterToHex(System.Drawing.Color.FromArgb(argbRenk));
            Color renk = HexToColor(rif);
            
            if (renk == Color.White || renk == Color.Black)
            {
                button.ForeColor = Color.Black;
                if (renk == Color.White)
                    button.FlatAppearance.BorderColor = Color.FromKnownColor(KnownColor.Highlight);
                else
                    button.FlatAppearance.BorderColor = Color.Black;
                button.BackColor = renk;
            }
            else
            {
                button.ForeColor = Color.White;
                button.FlatAppearance.BorderColor = button.ForeColor;
                button.BackColor = renk;
            }
            SetStyle(ControlStyles.SupportsTransparentBackColor, true); //Transparan renk kodlarında destekelemi ve hata vermemesi için transparan desteği eklemek
            //button.FlatAppearance.BorderColor = //Color.FromKnownColor(KnownColor.Highlight);
            button.Cursor = Cursors.Hand;
            button.ForeColor = Color.Black;
            menu.ToolTip.SetToolTip(button, "Yeni Sekme");
            button.Click += handler;
            menu.Items.Add(button);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            YeniSekme(sender, e);
        }

        private void radPageView1_SelectedPageChanged(object sender, EventArgs e)
        {
            //if (((RadPageView)sender).Pages.Count == 0) this.Close(); // eğer bir tane sekme varsa sekme çarpısına formu kapatma işlemi
        }
        private static String ConverterToHex(System.Drawing.Color c)
        {
            return String.Format("#{0}{1}{2}", c.R.ToString("X2"), c.G.ToString("X2"), c.B.ToString("X2"));
        }

        public static Color HexToColor(string hexColor)
        {
            if (hexColor.IndexOf('#') != -1)
                hexColor = hexColor.Replace("#", "");

            int kirmizi = 0;
            int yesil = 0;
            int mavi = 0;

            if (hexColor.Length == 6)
            {
                kirmizi = int.Parse(hexColor.Substring(0, 2), NumberStyles.AllowHexSpecifier);
                yesil = int.Parse(hexColor.Substring(2, 2), NumberStyles.AllowHexSpecifier);
                mavi = int.Parse(hexColor.Substring(4, 2), NumberStyles.AllowHexSpecifier);
            }
            else if (hexColor.Length == 3)
            {
                kirmizi = int.Parse(hexColor[0].ToString() + hexColor[0].ToString(), NumberStyles.AllowHexSpecifier);
                yesil = int.Parse(hexColor[1].ToString() + hexColor[1].ToString(), NumberStyles.AllowHexSpecifier);
                mavi = int.Parse(hexColor[2].ToString() + hexColor[2].ToString(), NumberStyles.AllowHexSpecifier);

            }
            return Color.FromArgb(kirmizi, yesil, mavi);
        }
    }
}
