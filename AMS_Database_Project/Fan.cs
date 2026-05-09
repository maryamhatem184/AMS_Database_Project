using DBapplication;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMS_Database_Project
{
    public partial class Fan : Form
    {
        private int fanID;

        public Fan(int ID) {
        
            InitializeComponent();
            Controller controller = new Controller();
            fanID = ID;
        }
 
        private void Fan_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings(fanID);
            settings.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4(fanID);
            form4.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3(fanID);
            form3.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MembershipForm form2 = new MembershipForm(fanID);
            form2.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4(fanID);
            form4.Show();
        }
    }
}
