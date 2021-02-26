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

        public Login()
        {
            InitializeComponent();
            txtPassword.PasswordChar = '*';
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

        private bool wardenCheck()
        {
            String passHash = Register.CreateMD5(txtPassword.Text);
            MySqlConnection connection;
            string server = "127.0.0.1";
            string database = "gamdb";
            string uid = "admin";
            string password = "admin";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);

            connection.Open();

            MySqlCommand cmd_admin = new MySqlCommand("SELECT Password FROM warden WHERE Username=@Username", connection);
            cmd_admin.Parameters.AddWithValue("@Username", txtUsername.Text);
            MySqlDataReader dr = cmd_admin.ExecuteReader();
            while (dr.Read())
            {
                String pass = dr.GetString(0);
                if (pass == passHash)
                {
                    return true;
                }
            }
            return false;
        }

        private bool participantsCheck()
        {
            String passHash = Register.CreateMD5(txtPassword.Text);
            MySqlConnection connection;
            string server = "127.0.0.1";
            string database = "gamdb";
            string uid = "admin";
            string password = "admin";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);

            connection.Open();

            MySqlCommand cmd_admin = new MySqlCommand("SELECT Password FROM participants WHERE Username=@Username", connection);
            cmd_admin.Parameters.AddWithValue("@Username", txtUsername.Text);
            MySqlDataReader dr = cmd_admin.ExecuteReader();
            while (dr.Read())
            {
                String pass = dr.GetString(0);
                if (pass == passHash)
                {
                    return true;
                }
            }
            return false;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if(wardenCheck().Equals(true))
            {
                /*Form1 f1 = new Form1();
                f1.ShowDialog();*/

                this.Hide();
                var f1 = new Form1();
                f1.Closed += (s, args) => this.Close();
                f1.Show();
            }
            else if(participantsCheck().Equals(true))
            {
                /*FormClient formClient = new FormClient();
                formClient.ShowDialog();*/

                this.Hide();
                var formClient = new FormClient();
                formClient.Closed += (s, args) => this.Close();
                formClient.Show();
            }
            else
            {
                MessageBox.Show("NO :<");
            }
        }
    }
}
