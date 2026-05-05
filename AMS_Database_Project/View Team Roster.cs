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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AMS_Database_Project
{
    public partial class View_Team_Roster : Form
    {
        public View_Team_Roster(int ID)
        {
            InitializeComponent();
            Controller controller = new Controller();
            dataGridView1.DataSource = controller.ShowAllPlayers(ID);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
