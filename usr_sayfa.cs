using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Controls.WindowsForms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;

namespace webtab
{
    public partial class usr_sayfa : UserControl
    {
        [DllImport("user32.dll")]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll", EntryPoint = "SetWindowLongA", SetLastError = true)]
        private static extern long SetWindowLong(IntPtr hwnd, int nIndex, long dwNewLong);

        const int WM_SYSCOMMAND = 274;
        const int SC_MAXIMIZE = 61488;
        public usr_sayfa()
        {
            InitializeComponent();
        }

        void Klasor(string yol)
        {
            this.Controls.Clear();
            (this.Parent as Control).Text = "Dosya Gezgini";
            ExplorerBrowser ee = new ExplorerBrowser();
            ee.Dock = DockStyle.Fill;
            ee.Navigate(Microsoft.WindowsAPICodePack.Shell.ShellObject.FromParsingName(yol));
            this.Controls.Add(ee);
        }

        private void btn_klasorAc_Click(object sender, EventArgs e)
        {
            Klasor(@"::{20D04FE0-3AEA-1069-A2D8-08002B30309D}");
        }

        private const int GWL_STYLE = (-16);
        private const int WS_VISIBLE = 0x10000000;
        Process p = new Process();
        string[] resimDosyalari = @"JPEG,JPG,PNG,ACT,ART,BMP,BLP,cit,CPT,CUT,DDS,dib,DjVu,EGT,Exif,gif,iCNS,iCO,iFF,ilbm,lbm,JNG,JP2,LBM,MAX,MiFF,MNG,MSP,NiTF,OTA,PBM,PC1,PC2,PC3,PCF,PCX,PDN,PGM,Pi1,Pi2,Pi3,PiCT,PCT,PNM,PPM,PSB,PSD,PSP,PX,PXR,QFX,RAW,RLE,SCT,SGi,TGA,TiFF,XBM,XCF,XPM,AWG,Ai,EPS,CGM,CDR,CMX,DXF,EGT,SVG,STL,VRML,X3D,WMF,EMF,ART,XAR".Split(',');
        string[] videoDosyalari = ".3gp,.act,.aiff,.aac,.amr,.ape,.au,.awb,.dct,.dss,.dvf,.flac,.gsm,.iklax,.ivs,.m4a,.m4p,.mmf,.mp3,.mpc,.msv,.ogg,.oga,.opus,.ra,.rm,.raw,.sln,.tta,.vox,.wav,.wma,.wv,.webm,.webm,.mkv,.flv,.flv,.vob,.ogv,.ogg,.drc,.gif,.gifv,.mng,.avi,.mov,.qt,.wmv,.yuv,.rm,.rmvb,.asf,.mp4,.m4p,.m4v,.mpg,.mp2,.mpeg,.mpe,.mpv,.mpg,.mpeg,.m2v,.m4v,.svi,.3gp,.3g2,.mxf,.roq,.nsv,.flv,.f4v,.f4p.f4a,.f4b".Split(',');
        private void btn_dosyaAc_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd_ac = new OpenFileDialog();
            if (ofd_ac.ShowDialog() == DialogResult.OK)
            {
                bool resimMi = false;
                bool videoMu = false;
                try
                {
                    string dosyaTuru = Path.GetExtension(ofd_ac.FileName);
                    for (int i = 0; i < resimDosyalari.Length; i++)
                    {
                        if (dosyaTuru.ToLower() == "."+resimDosyalari[i].ToLower())
                        {
                            resimMi = true;
                            break;
                        }
                    }

                    if(!resimMi)
                    {
                        for (int i = 0; i < videoDosyalari.Length; i++)
                        {
                            if (dosyaTuru.ToLower() == videoDosyalari[i].ToLower())
                            {
                                videoMu = true;
                                break;
                            }
                        }
                    }

                    if (resimMi)
                    {
                        PictureBox pcr_ac = new PictureBox();

                        pcr_ac.ImageLocation = ofd_ac.FileName;
                        pcr_ac.SizeMode = PictureBoxSizeMode.AutoSize;
                        pcr_ac.InitialImage = null;
                        pcr_ac.Cursor = Cursors.Hand;
                        this.AutoScroll = true;

                        for (int i = 0; i < this.Controls.Count; i++)
                        {
                            this.Controls[i].Hide();
                        }

                        this.Controls.Add(pcr_ac);
                        pcr_ac.BringToFront();
                        (this.Parent as Telerik.WinControls.UI.RadPageViewPage).ToolTipText = ofd_ac.FileName;

                        if (ofd_ac.FileName.Length >= 20)
                            (this.Parent as Control).Text = ofd_ac.FileName.Substring(0, 20) + "...";
                        else
                            (this.Parent as Control).Text = ofd_ac.FileName;
                        pcr_ac.DoubleClick += delegate 
                        {
                            if(pcr_ac.SizeMode == PictureBoxSizeMode.AutoSize)
                            {
                                
                                pcr_ac.SizeMode = PictureBoxSizeMode.StretchImage;
                                pcr_ac.Dock = DockStyle.Fill;
                            }
                            else
                            {
                                pcr_ac.SizeMode = PictureBoxSizeMode.AutoSize;
                                pcr_ac.Dock = DockStyle.None;
                            }
                        };
                    }
                    else if(videoMu)
                    {
                        try
                        {
                            AxWMPLib.AxWindowsMediaPlayer ax_ac = new AxWMPLib.AxWindowsMediaPlayer();
                            this.Controls.Add(ax_ac);
                            ax_ac.BringToFront();
                            ax_ac.URL = ofd_ac.FileName;
                            ax_ac.Ctlcontrols.play();
                            ax_ac.Dock = DockStyle.Fill;
                            (this.Parent as Telerik.WinControls.UI.RadPageViewPage).ToolTipText = ofd_ac.FileName;

                            if (ofd_ac.FileName.Length >= 20)
                                (this.Parent as Control).Text = ofd_ac.FileName.Substring(0, 20) + "...";
                            else
                                (this.Parent as Control).Text = ofd_ac.FileName;
                            (this.Parent.Parent as Telerik.WinControls.UI.RadPageView).ControlRemoved += new ControlEventHandler((sender2, e2) => Usr_sayfa_ControlRemoved(sender2, e2, ax_ac, (this.Parent as Telerik.WinControls.UI.RadPageViewPage)));

                        }
                        catch
                        {
                            MessageBox.Show("Bu Dosya Türü Desteklenmiyor.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        p = Process.Start(ofd_ac.FileName);
                        p.StartInfo.RedirectStandardOutput = true;
                        p.StartInfo.RedirectStandardInput = true;
                        p.StartInfo.UseShellExecute = true;
                        p.StartInfo.CreateNoWindow = true;
                        p.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
                        p.WaitForInputIdle();
                        SetParent(p.MainWindowHandle, this.Handle);
                        if (p.MainWindowHandle == (IntPtr)0)
                            throw null;
                        try
                        {
                            SetWindowLong(p.MainWindowHandle, GWL_STYLE, WS_VISIBLE);
                        }
                        catch { }
                        MoveWindow(p.MainWindowHandle, 0, 0, this.Width, this.Height, true);
                        (this.Parent as Telerik.WinControls.UI.RadPageViewPage).ToolTipText = p.MainWindowTitle;
                        if (p.MainWindowTitle.Length >= 20)
                            (this.Parent as Control).Text = p.MainWindowTitle.Substring(0, 20) + "...";
                        else
                            (this.Parent as Control).Text = p.MainWindowTitle;
                    }
                }
                catch
                {
                    try
                    {
                        p.Kill();
                    }
                    catch { }
                    MessageBox.Show("Bu Dosya Türü Desteklenmiyor.","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }

            }
        }

        private void Usr_sayfa_ControlRemoved(object sender, ControlEventArgs e, AxWMPLib.AxWindowsMediaPlayer media, Telerik.WinControls.UI.RadPageViewPage page)
        {
            if(e.Control.Handle == page.Handle)
            media.Dispose();
        }

        [DllImport("USER32.dll")]
        private static extern bool MoveWindow(IntPtr hwnd, int x, int y, int cx, int cy, bool repaint);
        private void usr_sayfa_Resize(object sender, EventArgs e)
        {
            try
            {
                MoveWindow(p.MainWindowHandle, 0, 0, this.Width, this.Height, true);
            }
            catch { }
        }

        private void btn_klasorAc_MouseLeave(object sender, EventArgs e)
        {
            (sender as Button).BackColor = Color.White;
            (sender as Button).Image = (Image)Properties.Resources.ResourceManager.GetObject((sender as Button).Name);
            (sender as Button).ForeColor = (sender as Button).FlatAppearance.MouseDownBackColor;
        }

        private void btn_klasorAc_MouseEnter(object sender, EventArgs e)
        {
            Color renk = (sender as Button).FlatAppearance.MouseDownBackColor;
            byte blue = renk.B;
            byte red = renk.R;
            byte green = renk.G;
            Color d = Color.FromArgb(renk.A, red >= 27 ? red - 27 : red, green >= 27 ? green - 27 : green - 27,  blue >= 27 ? blue - 27:blue);
            (sender as Button).BackColor = d;
            (sender as Button).Image = (Image)Properties.Resources.ResourceManager.GetObject((sender as Button).Name+"Hover");
            (sender as Button).ForeColor = Color.White;
        }

        private void pcr_dosyaAc_Click(object sender, EventArgs e)
        {
            cms_dosyaAc.Show(this, new Point(pcr_dosyaAc.Location.X - cms_dosyaAc.Width/3, pcr_dosyaAc.Location.Y + pcr_dosyaAc.Height));
        }

        private void pcr_klasorAc_Click(object sender, EventArgs e)
        {
            cms_klasorAc.Show(this,new Point(pcr_klasorAc.Location.X-cms_klasorAc.Width/3, pcr_klasorAc.Location.Y+pcr_klasorAc.Height));
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Klasor(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
        }

        private void belgelerimToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Klasor(@"::{450D8FBA-AD25-11D0-98A8-0800361B1103}");
        }

        private void denetimMasasıToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Klasor(@"::{21EC2020-3AEA-1069-A2DD-08002B30309D}");
        }

        private void çöpKutusuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Klasor(@"::{645FF040-5081-101B-9F08-00AA002F954E}");
        }

        private void komutSatırıDışarıToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("cmd.exe", "/k title Tab Browser - Komut Satırı");
        }

        private void resimBelgesiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd_ac = new OpenFileDialog();
            ofd_ac.Title = "Resim Dosyası Aç";
            if (ofd_ac.ShowDialog() == DialogResult.OK)
            {
                PictureBox pcr_ac = new PictureBox();
                try {
                    pcr_ac.ImageLocation = ofd_ac.FileName;
                    pcr_ac.SizeMode = PictureBoxSizeMode.AutoSize;
                    pcr_ac.InitialImage = null;
                    pcr_ac.Cursor = Cursors.Hand;
                    this.AutoScroll = true;
                    this.Controls.Add(pcr_ac);
                    pcr_ac.BringToFront();
                    (this.Parent as Telerik.WinControls.UI.RadPageViewPage).ToolTipText = ofd_ac.FileName;

                    if (ofd_ac.FileName.Length >= 20)
                        (this.Parent as Control).Text = ofd_ac.FileName.Substring(0, 20) + "...";
                    else
                        (this.Parent as Control).Text = ofd_ac.FileName;
                    pcr_ac.DoubleClick += delegate
                    {
                        if (pcr_ac.SizeMode == PictureBoxSizeMode.AutoSize)
                        {

                            pcr_ac.SizeMode = PictureBoxSizeMode.StretchImage;
                            pcr_ac.Dock = DockStyle.Fill;
                        }
                        else
                        {
                            pcr_ac.SizeMode = PictureBoxSizeMode.AutoSize;
                            pcr_ac.Dock = DockStyle.None;
                        }
                    };
                    if (pcr_ac.Image == null)
                        throw null;
                }
                catch
                {
                    try
                    {
                        pcr_ac.Dispose();
                    }
                    catch { }
                    MessageBox.Show("Bu Dosya Türü Desteklenmiyor.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void müzikVideoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd_ac = new OpenFileDialog();
            ofd_ac.Title = "Müzik/Video Dosyası Aç";
            if (ofd_ac.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    AxWMPLib.AxWindowsMediaPlayer ax_ac = new AxWMPLib.AxWindowsMediaPlayer();
                    this.Controls.Add(ax_ac);
                    ax_ac.BringToFront();
                    ax_ac.URL = ofd_ac.FileName;
                    ax_ac.Ctlcontrols.play();
                    ax_ac.Dock = DockStyle.Fill;
                    (this.Parent as Telerik.WinControls.UI.RadPageViewPage).ToolTipText = ofd_ac.FileName;

                    if (ofd_ac.FileName.Length >= 20)
                        (this.Parent as Control).Text = ofd_ac.FileName.Substring(0, 20) + "...";
                    else
                        (this.Parent as Control).Text = ofd_ac.FileName;
                    (this.Parent.Parent as Telerik.WinControls.UI.RadPageView).ControlRemoved += new ControlEventHandler((sender2, e2) => Usr_sayfa_ControlRemoved(sender2, e2, ax_ac, (this.Parent as Telerik.WinControls.UI.RadPageViewPage)));

                }
                catch
                {
                    MessageBox.Show("Bu Dosya Türü Desteklenmiyor.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
