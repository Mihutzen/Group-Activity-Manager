using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class FormAC : Form
    {
        private int nr = 0;

        IFirebaseConfig ifc = new FirebaseConfig()
        {
            AuthSecret = "dv61oELdN30DK7KcH56f1B3gujL6FFUiJ0Y2l5wh",
            BasePath = "https://groupactivitymanager-default-rtdb.firebaseio.com/"
        };

        IFirebaseClient client;

        public FormAC()
        {
            InitializeComponent();
            
        }

        private void FormAC_Load(object sender, EventArgs e)
        {
            try
            {
                client = new FireSharp.FirebaseClient(ifc);
            }

            catch
            {
                MessageBox.Show("No Internet or Connection Problem");
            }
            if (FormServer.sessionName != "")
            {
                FirebaseResponse res = client.Get(@"Session/" + FormServer.sessionName);
                Session session = res.ResultAs<Session>();
                for (int i = 0; i < 10; i++)
                {
                    if (session.Port[i].Contains(">"))
                    {
                        nr++;
                        ListViewItem item = new ListViewItem(nr.ToString());
                        item.SubItems.Add(session.Port[i].Split('>')[1]);
                        listView.Items.Add(item);
                    }
                }
            }

        }

        private void listView_Click(object sender, EventArgs e)
        {
            string firstSelectedItem = listView.SelectedItems[0].Text;
            FormServer.formList[Convert.ToInt32(firstSelectedItem)-1].Show();
            //MessageBox.Show(firstSelectedItem.Text);
        }

    }
}
