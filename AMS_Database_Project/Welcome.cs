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
    public partial class Welcome : Form
    {
        public Welcome()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Signup Signup = new Signup();
            Signup.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Login Login = new Login();
            Login.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Fan Fan = new Fan();
            Fan.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Medical Medical = new Medical();
            Medical.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Coach Coach = new Coach();
            Coach.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Manager Manager = new Manager();
            Manager.Show();
            this.Hide();
        }
    }
}

