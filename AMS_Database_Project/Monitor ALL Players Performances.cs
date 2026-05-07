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
    public partial class Monitor_ALL_Players_Performances : Form
    {
        public Monitor_ALL_Players_Performances(int ID)
        {
            InitializeComponent();
            Controller controller = new Controller();
            dataGridView1.DataSource = controller.ShowAllPlayersPerformances();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
