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

        public Register()
        {
            InitializeComponent();
            txtConfirm.PasswordChar = '*';
            txtPassword.PasswordChar = '*';
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
            Application.Exit();
        }

        private void buttonMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            //TODO: verifica adresa email
            //TODO: duplicate username & stuff
            if(comboBox.Text == "" || txtConfirm.Text=="" || txtEmail.Text=="" || txtFirstName.Text=="" || txtLastName.Text=="" || txtPassword.Text=="" || txtUsername.Text=="")
            {
                CaseteGoale caseteGoale = new CaseteGoale();
                caseteGoale.ShowDialog();
                return;
            }
            else if(txtConfirm.Text!=txtPassword.Text)
            {
                ConfirmareParola confirmareParola = new ConfirmareParola();
                confirmareParola.ShowDialog();
                return;
            }

            MySqlConnection connection;
            string server = "127.0.0.1";
            string database = "gamdb";
            string uid = "admin";
            string password = "admin";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);
            if(comboBox.Text=="Participant")
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("insert into participants (FirstName,LastName,Email,Username,Password) values(@FirstName,@LastName,@Email,@Username,@Password)", connection);
                cmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
                cmd.Parameters.AddWithValue("@LastName", txtLastName.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                cmd.Parameters.AddWithValue("@Password", CreateMD5(txtPassword.Text));
                cmd.ExecuteNonQuery();

                ContCreatCuSucces contCreatCuScucces = new ContCreatCuSucces();
                contCreatCuScucces.ShowDialog();

                this.Close();
            }
            else if(comboBox.Text=="Warden")
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("insert into warden (FirstName,LastName,Email,Username,Password) values(@FirstName,@LastName,@Email,@Username,@Password)", connection);
                cmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
                cmd.Parameters.AddWithValue("@LastName", txtLastName.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                cmd.Parameters.AddWithValue("@Password", CreateMD5(txtPassword.Text));
                cmd.ExecuteNonQuery();

                ContCreatCuSucces contCreatCuScucces = new ContCreatCuSucces();
                contCreatCuScucces.ShowDialog();

                this.Close();
            }
            connection.Close();

        }
    }
}

