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
    public partial class Fan_Username : Form
    {
        public Fan_Username(string firstname, string username)
        {
            
            InitializeComponent();
            label2.Text = "You have successfully signed up, "+ firstname + "!";
            label3.Text = "Your username is: " + username + ".";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Welcome Welcome = new Welcome();
            Welcome.Show();
            this.Hide();
        }
    }
}
