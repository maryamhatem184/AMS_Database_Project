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
    public partial class Activate_Deactivate_Manager : Form
    {
        public Activate_Deactivate_Manager(int ID)
        {
            InitializeComponent();
            Controller controller = new Controller();
            DataTable dt = controller.GetAllManagerNames();
            foreach (DataRow dr in dt.Rows)
            {
                comboBox2.Items.Add(dr["Name"].ToString());
                comboBox1.Items.Add(dr["Name"].ToString());
            }
            dt = controller.GetAllBranchNames(ID);
            foreach (DataRow dr in dt.Rows)
            {
                comboBox3.Items.Add(dr["Name"].ToString());
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
            label11.Visible = false;
            label12.Visible = false;
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                label7.Visible = true;
            }
            else
            {
                Controller controller = new Controller();
                controller.RegisterManager(textBox2.Text, textBox1.Text);
                comboBox2.Items.Clear();
                comboBox3.Items.Clear();
                comboBox1.Items.Clear();
                comboBox3.Text = "";
                comboBox1.Text = "";
                comboBox2.Text = "";
                textBox1.Text = "";
                textBox2.Text = "";
                DataTable dt = controller.GetAllManagerNames();
                comboBox2.Items.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    comboBox2.Items.Add(dr["Name"].ToString());
                    comboBox1.Items.Add(dr["Name"].ToString());
                }
                dt = controller.GetAllBranchNames(0);
                foreach (DataRow dr in dt.Rows)
                {
                    comboBox3.Items.Add(dr["Name"].ToString());
                }
                label8.Visible = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label15.Visible = false;
            label13.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            label11.Visible = false;
            label12.Visible = false;
            if (comboBox2.Text == "")
            {
                label13.Visible = true;
            }
            else
            {
                Controller controller = new Controller();
                controller.DeactivateManager(comboBox2.Text);
                comboBox2.Items.Clear();
                comboBox3.Items.Clear();
                comboBox1.Items.Clear();
                comboBox3.Text = "";
                comboBox1.Text = "";
                comboBox2.Text = "";
                textBox1.Text = "";
                textBox2.Text = "";
                DataTable dt = controller.GetAllManagerNames();
                comboBox2.Items.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    comboBox2.Items.Add(dr["Name"].ToString());
                    comboBox1.Items.Add(dr["Name"].ToString());
                }
                dt = controller.GetAllBranchNames(0);
                foreach (DataRow dr in dt.Rows)
                {
                    comboBox3.Items.Add(dr["Name"].ToString());
                }
                label15.Visible = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label7.Visible = false;
            label8.Visible = false;
            label15.Visible = false;
            label13.Visible = false;
            label11.Visible = false;
            label12.Visible = false;
            if (comboBox3.Text == "" || comboBox1.Text == "")
            {
                label11.Visible = true;
            }
            else
            {
                Controller controller = new Controller();
                controller.AssignManagerToBranch(controller.GetManagerIDByName(comboBox1.Text), controller.GetBranchIDByName(comboBox3.Text));
                comboBox2.Items.Clear();
                comboBox3.Items.Clear();
                comboBox1.Items.Clear();
                comboBox3.Text = "";
                comboBox1.Text = "";
                comboBox2.Text = "";
                textBox1.Text = "";
                textBox2.Text = "";
                DataTable dt = controller.GetAllManagerNames();
                comboBox2.Items.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    comboBox2.Items.Add(dr["Name"].ToString());
                    comboBox1.Items.Add(dr["Name"].ToString());
                }
                dt = controller.GetAllBranchNames(0);
                foreach (DataRow dr in dt.Rows)
                {
                    comboBox3.Items.Add(dr["Name"].ToString());
                }
                label12.Visible = true;
            }
        }
    }
}
