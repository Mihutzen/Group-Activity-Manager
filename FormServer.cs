using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class FormServer : Form
    {
        public static List<Form> formList = new List<Form>();
        public static string sessionName = "";
        
        public FormServer()
        {
            InitializeComponent();
            //PortNumber.Text = FreeTcpPort().ToString();
            //PortNumber.Text = FindIPv4Adress();
        }

        private string FindIPv4Adress()
        {
            IPHostEntry host;
            string localIP = "?";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                }
            }
            return localIP;
        }

        static int FreeTcpPort()
        {
            TcpListener l = new TcpListener(IPAddress.Loopback, 0);
            l.Start();
            int port = ((IPEndPoint)l.LocalEndpoint).Port;
            l.Stop();
            return port;
        }

        IFirebaseConfig ifc = new FirebaseConfig()
        {
            AuthSecret = "dv61oELdN30DK7KcH56f1B3gujL6FFUiJ0Y2l5wh",
            BasePath = "https://groupactivitymanager-default-rtdb.firebaseio.com/"
        };

        IFirebaseClient client;

        private void FormServer_Load(object sender, EventArgs e)
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

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            if (txtCode.Text == "")
            {
                MessageBox.Show("Va rog introduceti un cod de access");
            }
            else
            {
               sessionName = txtCode.Text;
               Session currentSession = new Session()
                {
                    Code = txtCode.Text,
                    IP = FindIPv4Adress(),
                    Port = CreatePorts()
                };

                SetResponse set = client.Set(@"Session/" + txtCode.Text, currentSession);
            }
        }

        private List<string> CreatePorts()
        {
            List<string> ports = new List<string>();
            for(int i=0; i<10; i++)
            {
                string aux = FreeTcpPort().ToString();
                ports.Add(aux);
                formList.Add(new FormScreen(int.Parse(aux)));
            }
            return ports;
        }

    }
}
