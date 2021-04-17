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
using MySql.Data.MySqlClient;
using System.Drawing.Drawing2D;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;

namespace Server
{
    public partial class Login : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public static string connectedName;

        IFirebaseConfig ifc = new FirebaseConfig()
        {
            AuthSecret = "dv61oELdN30DK7KcH56f1B3gujL6FFUiJ0Y2l5wh",
            BasePath = "https://groupactivitymanager-default-rtdb.firebaseio.com/"
        };

        IFirebaseClient client;

        public Login()
        {
            InitializeComponent();
            txtPassword.PasswordChar = '*';
        }

        private void Login_Load(object sender, EventArgs e)
        {
            try
            {
                client = new FireSharp.FirebaseClient(ifc);
            }

            catch
            {
                MessageBox.Show("No Internet or Connection Problem");
            }
        }

        private void Login_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            Register reg = new Register();
            reg.ShowDialog();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            FirebaseResponse res = client.Get(@"Users/" + txtUsername.Text);
            MyUser ResUser = res.ResultAs<MyUser>();// database result

            MyUser CurUser = new MyUser() // USER GIVEN INFO
            {
                Username = txtUsername.Text,
                Password = Register.CreateMD5(txtPassword.Text)
            };

            if (txtUsername.Text == "" || txtPassword.Text == "")
            {
                Introduceti introduceti = new Introduceti();
                introduceti.ShowDialog();
                return;
            }

            if (MyUser.IsEqual(ResUser, CurUser))
            {
                connectedName = ResUser.FirstName + " " + ResUser.LastName;

                if (ResUser.AccountType.Equals("Participant"))
                {
                    this.Hide();
                    var f1 = new Form2();
                    f1.Closed += (s, args) => this.Close();
                    f1.Show();
                }
                else
                {
                    this.Hide();
                    var f1 = new Form1();
                    f1.Closed += (s, args) => this.Close();
                    f1.Show();
                }
            }
            
        }

        private void Login_Paint(object sender, PaintEventArgs e)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle,
                                                                Color.FromArgb(5, 37, 84),
                                                                Color.FromArgb(4, 29, 66),
                                                               90F))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }
    }
}
