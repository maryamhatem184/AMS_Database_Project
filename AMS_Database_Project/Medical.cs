using DBapplication;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Update_Injury_Recovery_Progress update_Injury_Recovery_Progress = new Update_Injury_Recovery_Progress(medicalID);
            update_Injury_Recovery_Progress.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Track_Rehabilitation_Programs track_Rehabilitation_Programs = new Track_Rehabilitation_Programs(medicalID);
            track_Rehabilitation_Programs.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Update_Player_Availability update_Player_Availability = new Update_Player_Availability(medicalID);  
            update_Player_Availability.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            View_Player_Medical_History view_Player_Medical_History = new View_Player_Medical_History(medicalID);   
            view_Player_Medical_History.Show();
        }
    }
}
