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
    public partial class Form2 : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public Form2()
        {
            InitializeComponent();
            openChildFormInPanel(new FormHome());
            this.StartPosition = FormStartPosition.CenterScreen;
            buttonHome.BackColor = Color.FromArgb(6, 45, 103);
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonMinimize_Click(object sender, EventArgs e)
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
            buttonClient.BackColor = Color.FromArgb(4, 29, 66);
            buttonHelp.BackColor = Color.FromArgb(4, 29, 66);
        }

        private void buttonClient_Click(object sender, EventArgs e)
        {
            openChildFormInPanel(new FormClient());
            buttonHome.BackColor = Color.FromArgb(4, 29, 66);
            buttonClient.BackColor = Color.FromArgb(6, 45, 103);
            buttonHelp.BackColor = Color.FromArgb(4, 29, 66);
        }

        private void buttonHelp_Click(object sender, EventArgs e)
        {
            openChildFormInPanel(new FormAC());//TODO: Form Help
            buttonHome.BackColor = Color.FromArgb(4, 29, 66);
            buttonClient.BackColor = Color.FromArgb(4, 29, 66);
            buttonHelp.BackColor = Color.FromArgb(6, 45, 103);
        }
        private void panelMenu_Paint(object sender, PaintEventArgs e)
        {

        }

        private void buttonInvizibil_Click(object sender, EventArgs e)
        {

        }

        private void panelLogo_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelTop_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelMenu_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void buttonInvizibil_Click_1(object sender, EventArgs e)
        {

        }

        private void panelLogo_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void panelTop_Paint_1(object sender, PaintEventArgs e)
        {

        }
    }
}
