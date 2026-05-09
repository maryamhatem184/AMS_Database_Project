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
    public partial class Register_Deactivate_Membership : Form
    {
        public Register_Deactivate_Membership(int ID)
        {
            InitializeComponent();
            Controller controller = new Controller();
            DataTable dt = controller.GetAllFansNames();
            foreach (DataRow dr in dt.Rows)
            {
                comboBox1.Items.Add(dr["Name"].ToString());
            }
            dt = controller.GetAllMembersID();
            foreach (DataRow dr in dt.Rows)
            {
                comboBox2.Items.Add(controller.GetFanNameByID(dr["Fan_ID"].ToString()));
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label7.Visible = false;
            label8.Visible = false;
            label15.Visible = false;
            label13.Visible = false;
            if (comboBox1.Text == "" || textBox2.Text == "")
            {
                label7.Visible = true;
            }
            else
            {
                comboBox1.Items.Clear();
                comboBox2.Items.Clear();
                Controller controller = new Controller();
                controller.RegisterMembership(controller.GetFanIDByName(comboBox1.Text), textBox2.Text, dateTimePicker1.Value.ToString("yyyy-MM-dd"), dateTimePicker2.Value.ToString("yyyy-MM-dd"));
                DataTable dt = controller.GetAllFansNames();
                foreach (DataRow dr in dt.Rows)
                {
                    comboBox1.Items.Add(dr["Name"].ToString());
                }
                dt = controller.GetAllMembersID();
                foreach (DataRow dr in dt.Rows)
                {
                    comboBox2.Items.Add(controller.GetFanNameByID(dr["Fan_ID"].ToString()));
                }
                comboBox1.Text = "";
                comboBox2.Text = "";
                label8.Visible = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label15.Visible = false;
            label13.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            if (comboBox2.SelectedItem != null)
            {
                comboBox1.Items.Clear();
                comboBox2.Items.Clear();
                Controller controller = new Controller();
                controller.DeactivateMembership(controller.GetFanIDByName(comboBox2.Text));
                DataTable dt = controller.GetAllFansNames();
                foreach (DataRow dr in dt.Rows)
                {
                    comboBox1.Items.Add(dr["Name"].ToString());
                }
                dt = controller.GetAllMembersID();
                foreach (DataRow dr in dt.Rows)
                {
                    comboBox2.Items.Add(controller.GetFanNameByID(dr["Fan_ID"].ToString()));
                }
                comboBox1.Text = "";
                comboBox2.Text = "";
                label15.Visible = true;
            }
            else
            {
                label13.Visible = true;
            }
        }
    }
}
