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
    public partial class Coach : Form
    {
        public Coach()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            View_Team_Roster View_Team_Roster = new View_Team_Roster();
            View_Team_Roster.Show();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            Monitor_Player_Performance Monitor_Player_Performance = new Monitor_Player_Performance();
            Monitor_Player_Performance.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Check_Player_Availability Check_Player_Availability = new Check_Player_Availability();
            Check_Player_Availability.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Schedule_Training_Session Schedule_Training_Session = new Schedule_Training_Session();
            Schedule_Training_Session.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Schedule_Match Schedule_Match = new Schedule_Match();
            Schedule_Match.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Select_Players_For_Match Select_Players_For_Match = new Select_Players_For_Match();
            Select_Players_For_Match.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Update_Player_Performance Update_Player_Performance = new Update_Player_Performance();
            Update_Player_Performance.Show();
        }
        private void button8_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.Show();
        }
    }
}
