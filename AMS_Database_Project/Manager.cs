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
    public partial class Manager : Form
    {
        private int managerID;
        public Manager(int ID)
        {
            InitializeComponent();
            Controller controller = new Controller();
            managerID = ID;
        }

        private void Manager_Load(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings(managerID);
            settings.Show();
        }
    }
}
