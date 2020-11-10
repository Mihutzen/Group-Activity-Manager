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
    public partial class FormServer : Form
    {
        List<Form> formList = new List<Form>();
        public FormServer()
        {
            InitializeComponent();
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            if(PortNumber.Text == "")
            {
                MessageBox.Show("Please insert a port number");
            }
            else
            {
                formList.Add(new FormScreen(int.Parse(PortNumber.Text)));
                //this.formList[formList.Count - 1].Controls.Add(new TextBox());
                formList[formList.Count - 1].Show();
            }
        }
    }
}
