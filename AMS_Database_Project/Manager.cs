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

        private void button11_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings(managerID);
            settings.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Add_Update_Remove_Club_Branch add_Update_Remove_Club_Branch = new Add_Update_Remove_Club_Branch(managerID);
            add_Update_Remove_Club_Branch.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Monitor_ALL_Players_Performances monitor_ALL_Players_Performances = new Monitor_ALL_Players_Performances(managerID);
            monitor_ALL_Players_Performances.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Manage_Team manage_Team = new Manage_Team(managerID);
            manage_Team.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Register_Deactivate_Membership register_Deactivate_Membership = new Register_Deactivate_Membership(managerID);
            register_Deactivate_Membership.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Add_Update_Remove_Sports_Section add_Update_Remove_Sports_Section = new Add_Update_Remove_Sports_Section(managerID);
            add_Update_Remove_Sports_Section.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Update_Merchandise_Inventory update_Merchandise_Inventory = new Update_Merchandise_Inventory(managerID);
            update_Merchandise_Inventory.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Register_Deactivate_Coach register_Deactivate_Coach = new Register_Deactivate_Coach(managerID);
            register_Deactivate_Coach.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            View_Match_Schedules view_Match_Schedules = new View_Match_Schedules(managerID);
            view_Match_Schedules.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Register_Deactivate_Medical_Staff register_Deactivate_Medical_Staff = new Register_Deactivate_Medical_Staff(managerID);
            register_Deactivate_Medical_Staff.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Activate_Deactivate_Manager Activate_Deactivate_Manager = new Activate_Deactivate_Manager(managerID);
            Activate_Deactivate_Manager.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Generate_Reports generate_Reports = new Generate_Reports(managerID);
            generate_Reports.Show();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Add_New_Inventory add_New_Inventory = new Add_New_Inventory(managerID);
            add_New_Inventory.Show();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Manage_Contracts manage_Contracts = new Manage_Contracts(managerID);
            manage_Contracts.Show();
        }
    }
}
