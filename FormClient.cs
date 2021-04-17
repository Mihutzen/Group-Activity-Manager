using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class FormClient : Form
    {
        private readonly TcpClient client = new TcpClient();
        private NetworkStream mainStream;
        private int portNumber;

        IFirebaseConfig ifc = new FirebaseConfig()
        {
            AuthSecret = "dv61oELdN30DK7KcH56f1B3gujL6FFUiJ0Y2l5wh",
            BasePath = "https://groupactivitymanager-default-rtdb.firebaseio.com/"
        };

        IFirebaseClient fbClient;

        private void FormClient_Load(object sender, EventArgs e)
        {
            try
            {
                fbClient = new FireSharp.FirebaseClient(ifc);
            }

            catch
            {
                MessageBox.Show("No Internet or Connection Problem");
            }
        }

        private static Image GrabDesktop()
        {
            Rectangle bounds = Screen.PrimaryScreen.Bounds;
            Bitmap screenshot = new Bitmap(bounds.Width, bounds.Height, PixelFormat.Format32bppArgb);
            Graphics graphic = Graphics.FromImage(screenshot);
            graphic.CopyFromScreen(bounds.X, bounds.Y, 0, 0, bounds.Size, CopyPixelOperation.SourceCopy);
            return screenshot;
        }

        public FormClient()
        {
            InitializeComponent();
        }

        private void SendDesktopImage()
        {
            BinaryFormatter binFormatter = new BinaryFormatter();
            mainStream = client.GetStream();
            binFormatter.Serialize(mainStream, GrabDesktop());

        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            if (txtCodeFornServer.Text == "")
            {
                MessageBox.Show("Pune codul");
                return;
            }

            FirebaseResponse res = fbClient.Get(@"Session/" + txtCodeFornServer.Text);
            Session ResSes = res.ResultAs<Session>();// database result

            int nr_port = 0;

            while(ResSes.Port[nr_port].Contains("=>"))
            {
                nr_port++;
            }
            client.Connect(ResSes.IP, int.Parse(ResSes.Port[nr_port]));
            ResSes.Port[nr_port] += "=>" + Login.connectedName;
            FirebaseResponse res2 = fbClient.Set(@"Session/" + txtCodeFornServer.Text, ResSes);
            timer1.Start();
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            SendDesktopImage();
        }
    }
}
