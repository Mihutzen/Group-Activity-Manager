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
using MySql;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;

using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System.Drawing.Drawing2D;

namespace Server
{
    public partial class Register : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        IFirebaseConfig ifc = new FirebaseConfig()
        {
            AuthSecret = "dv61oELdN30DK7KcH56f1B3gujL6FFUiJ0Y2l5wh",
            BasePath = "https://groupactivitymanager-default-rtdb.firebaseio.com/"
        };

        IFirebaseClient client;

        public Register()
        {
            InitializeComponent();
            txtConfirm.PasswordChar = '*';
            txtPassword.PasswordChar = '*';
        }

        private void Register_Load(object sender, EventArgs e)
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

        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        private void Register_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            //TODO: verifica adresa email
            //TODO: duplicate username & stuff
            if (cmbType.Text == "" || txtConfirm.Text == "" || txtEmail.Text == "" || txtFirstName.Text == "" || txtLastName.Text == "" || txtPassword.Text == "" || txtUsername.Text == "")
            {
                CaseteGoale caseteGoale = new CaseteGoale();
                caseteGoale.ShowDialog();
                return;
            }
            else if (txtConfirm.Text != txtPassword.Text)
            {
                ConfirmareParola confirmareParola = new ConfirmareParola();
                confirmareParola.ShowDialog();
                return;
            }

            MyUser user = new MyUser()
            {
                Username = txtUsername.Text,
                Password = CreateMD5(txtPassword.Text),
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                Email = txtEmail.Text,
                AccountType = cmbType.Text
            };

            SetResponse set = client.Set(@"Users/" + txtUsername.Text, user);

            ContCreatCuSucces contCreatCuScucces = new ContCreatCuSucces();
            contCreatCuScucces.ShowDialog();

            this.Close();
        }

        private void Register_Paint(object sender, PaintEventArgs e)
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

