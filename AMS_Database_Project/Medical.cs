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
    public partial class Medical : Form
    {
        private int medicalID;
        public Medical(int ID)
        {
            InitializeComponent();
            Controller controller = new Controller();
            medicalID = ID;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings(medicalID);
            settings.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Record_Player_Injury record_Player_Injury = new Record_Player_Injury(medicalID);
            record_Player_Injury.Show();
            this.Hide();
        }
    }
}
