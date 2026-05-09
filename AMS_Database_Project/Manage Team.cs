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
    public partial class Manage_Team : Form
    {
        public Manage_Team(int ID)
        {
            InitializeComponent();
            Controller controller = new Controller();
            DataTable dt = controller.GetAllCoachNames();
            foreach (DataRow dr in dt.Rows)
            {
                comboBox1.Items.Add(dr["Name"].ToString());
            }
            dt = controller.GetAllTeamsNames();
            foreach (DataRow dr in dt.Rows)
            {
                comboBox2.Items.Add(dr["Name"].ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label8.Visible = false;
            label9.Visible = false;
            label10.Visible = false;
            label11.Visible = false;
            Controller controller = new Controller();
            if (textBox1.Text != "" && comboBox1.Text != "" && textBox2.Text != "")
            {
                controller.RegisterTeam(textBox1.Text, textBox2.Text, comboBox1.Text);
                comboBox1.Items.Clear();
                comboBox2.Items.Clear();
                textBox1.Text = "";
                textBox2.Text = "";
                DataTable dt = controller.GetAllSportsSectionNames();
                foreach (DataRow dr in dt.Rows)
                {
                    comboBox1.Items.Add(dr["Name"].ToString());
                }
                dt = controller.GetAllTeamsNames();
                foreach (DataRow dr in dt.Rows)
                {
                    comboBox2.Items.Add(dr["Name"].ToString());
                }
                label9.Visible = true;
            }
            else
            {
                label8.Visible = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label8.Visible = false;
            label9.Visible = false;
            label10.Visible = false;
            label11.Visible = false;
            Controller controller = new Controller();
            if (comboBox2.Text != "")
            {
                controller.DeactivateTeam(comboBox2.Text);
                comboBox1.Items.Clear();
                comboBox2.Items.Clear();
                textBox1.Text = "";
                textBox2.Text = "";
                DataTable dt = controller.GetAllSportsSectionNames();
                foreach (DataRow dr in dt.Rows)
                {
                    comboBox1.Items.Add(dr["Name"].ToString());
                }
                dt = controller.GetAllTeamsNames();
                foreach (DataRow dr in dt.Rows)
                {
                    comboBox2.Items.Add(dr["Name"].ToString());
                }
                label11.Visible = true;
            }
            else
            {
                label10.Visible = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
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
                controller.AssignCoachToTeam(controller.GetCoachIDByName(comboBox1.Text), controller.GetTeamIDByName(comboBox3.Text));
                comboBox2.Items.Clear();
                comboBox3.Items.Clear();
                comboBox1.Items.Clear();
                comboBox3.Text = "";
                comboBox1.Text = "";
                comboBox2.Text = "";
                textBox1.Text = "";
                textBox2.Text = "";
                DataTable dt = controller.GetAllCoachNames();
                comboBox2.Items.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    comboBox2.Items.Add(dr["Name"].ToString());
                    comboBox1.Items.Add(dr["Name"].ToString());
                }
                dt = controller.GetAllTeamNames(0);
                foreach (DataRow dr in dt.Rows)
                {
                    comboBox3.Items.Add(dr["Name"].ToString());
                }
                label12.Visible = true;
            }
        }
    }
}
