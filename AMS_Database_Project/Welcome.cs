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
    public partial class Welcome : Form
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }


        public Welcome()
        {
            Background background = new Background();
            background.Show();

            InitializeComponent();
            this.DoubleBuffered = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Background background = new Background();
            background.Show();
     
            Signup Signup = new Signup();
            Signup.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Background background = new Background();
            background.Show();

            Login Login = new Login();
            Login.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

