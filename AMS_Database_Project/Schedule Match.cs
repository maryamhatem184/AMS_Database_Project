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
    public partial class Schedule_Match : Form
    {
        public Schedule_Match(int ID)
        {
            InitializeComponent();
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();
            for (int i = 0; i <= 23; i++)
            {
                comboBox1.Items.Add(i.ToString("D2"));
            }
            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0;
            }
            for (int i = 0; i <= 60; i += 5)
            {
                comboBox2.Items.Add(i.ToString("D2"));
            }
            if (comboBox2.Items.Count > 0)
            {
                comboBox2.SelectedIndex = 0;
            }
            DataTable dt = new Controller().GetAllBranchNames(ID);
            foreach (DataRow dr in dt.Rows)
            {
                comboBox3.Items.Add(dr["Name"].ToString());
            }
            dt = new Controller().GetAllTeamNames(ID);
            foreach (DataRow dr in dt.Rows)
            {
                comboBox4.Items.Add(dr["Name"].ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label5.Visible = false;
            label8.Visible = false;
            label9.Visible = false;
            if (comboBox1.Text != "" && comboBox2.Text != "" && comboBox3.Text != "" && comboBox4.Text != "" && textBox1.Text != "")
            {
                Controller controller = new Controller();
                if (controller.ScheduleMatch(controller.GetNumberOfMatches() + 501, dateTimePicker1.Value.ToString("yyyy-MM-dd"), comboBox1.Text, comboBox2.Text, textBox1.Text, controller.GetBranchID(comboBox3.Text), controller.GetTeamID(comboBox4.Text)) > 0)
                {
                    label8.Visible = true;
                }
                else
                {
                    label9.Visible = true;
                }
            }
            else
            {
                label5.Visible = true;
            }
        }
    }
}
