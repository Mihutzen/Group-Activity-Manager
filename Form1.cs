using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Server
{
    public partial class Form1 : Form
    {

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd,int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public Form1()
        {
            InitializeComponent();
            openChildFormInPanel(new FormHome());
            this.StartPosition = FormStartPosition.CenterScreen;
            buttonHome.BackColor = Color.FromArgb(6, 45, 103);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Form1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private Form activeForm = null;
        private void openChildFormInPanel(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelMain.Controls.Add(childForm);
            panelMain.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            //HOW TO: access the function fur dumb ass people: openChildFormInPanel(new FormHome());
        }

        private void buttonHome_Click(object sender, EventArgs e)
        {
            openChildFormInPanel(new FormHome());
            buttonHome.BackColor = Color.FromArgb(6, 45, 103);
            buttonServer.BackColor = Color.FromArgb(4, 29, 66);
            buttonClient.BackColor = Color.FromArgb(4, 29, 66);
            buttonAbout.BackColor = Color.FromArgb(4, 29, 66);
        }

        private void buttonServer_Click(object sender, EventArgs e)
        {
            openChildFormInPanel(new FormServer());
            buttonHome.BackColor = Color.FromArgb(4, 29, 66);
            buttonServer.BackColor = Color.FromArgb(6, 45, 103);
            buttonClient.BackColor = Color.FromArgb(4, 29, 66);
            buttonAbout.BackColor = Color.FromArgb(4, 29, 66);
        }

        private void buttonClient_Click(object sender, EventArgs e)
        {
            openChildFormInPanel(new FormClient());
            buttonHome.BackColor = Color.FromArgb(4, 29, 66);
            buttonServer.BackColor = Color.FromArgb(4, 29, 66);
            buttonClient.BackColor = Color.FromArgb(6, 45, 103);
            buttonAbout.BackColor = Color.FromArgb(4, 29, 66);
        }

        private void buttonAbout_Click(object sender, EventArgs e)
        {
            openChildFormInPanel(new FormAbout());
            buttonHome.BackColor = Color.FromArgb(4, 29, 66);
            buttonServer.BackColor = Color.FromArgb(4, 29, 66);
            buttonClient.BackColor = Color.FromArgb(4, 29, 66);
            buttonAbout.BackColor = Color.FromArgb(6, 45, 103);
        }
    }
}
