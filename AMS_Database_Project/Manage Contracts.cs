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
    public partial class Manage_Contracts : Form
    {
        public Manage_Contracts(int ID)
        {
            InitializeComponent();
            Controller controller = new Controller();
            DataTable dt = controller.GetAllPlayersNames();
            foreach (DataRow dr in dt.Rows)
            {
                comboBox4.Items.Add(dr["Name"].ToString());
                comboBox2.Items.Add(dr["Name"].ToString());
            }
            dt = controller.GetAllTeamsNames();
            foreach (DataRow dr in dt.Rows)
            {
                comboBox3.Items.Add(dr["Name"].ToString());
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
            label12.Visible = false;
            label7.Visible = false;
            Controller controller = new Controller();
            if (textBox1.Text != "" && textBox3.Text != "" && textBox2.Text != "")
            {
                controller.SignPlayer(textBox1.Text, Convert.ToInt32(textBox2.Text), textBox3.Text);
                comboBox2.Items.Clear();
                comboBox3.Items.Clear();
                comboBox4.Items.Clear();
                textBox3.Text = "";
                comboBox2.Text = "";
                comboBox3.Text = "";
                comboBox4.Text = "";
                textBox1.Text = "";
                textBox2.Text = "";
                DataTable dt = controller.GetAllPlayersNames();
                foreach (DataRow dr in dt.Rows)
                {
                    comboBox4.Items.Add(dr["Name"].ToString());
                    comboBox2.Items.Add(dr["Name"].ToString());
                }
                dt = controller.GetAllTeamsNames();
                foreach (DataRow dr in dt.Rows)
                {
                    comboBox3.Items.Add(dr["Name"].ToString());
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
            label12.Visible = false;
            label7.Visible = false;
            Controller controller = new Controller();
            if (comboBox2.Text != "")
            {
                controller.SackPlayer(comboBox2.Text);
                comboBox2.Items.Clear();
                comboBox3.Items.Clear();
                comboBox4.Items.Clear();
                textBox3.Text = "";
                comboBox2.Text = "";
                comboBox3.Text = "";
                comboBox4.Text = "";
                textBox1.Text = "";
                textBox2.Text = "";
                DataTable dt = controller.GetAllPlayersNames();
                foreach (DataRow dr in dt.Rows)
                {
                    comboBox4.Items.Add(dr["Name"].ToString());
                    comboBox2.Items.Add(dr["Name"].ToString());
                }
                dt = controller.GetAllTeamsNames();
                foreach (DataRow dr in dt.Rows)
                {
                    comboBox3.Items.Add(dr["Name"].ToString());
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
            label8.Visible = false;
            label9.Visible = false;
            label10.Visible = false;
            label11.Visible = false;
            label12.Visible = false;
            label7.Visible = false;
            if (comboBox3.Text == "" || comboBox4.Text == "")
            {
                label7.Visible = true;
            }
            else
            {
                Controller controller = new Controller();
                controller.AssignPlayerToTeam(controller.GetPlayerID(comboBox4.Text), controller.GetTeamID(comboBox3.Text));
                comboBox2.Items.Clear();
                comboBox3.Items.Clear();
                comboBox4.Items.Clear();
                textBox3.Text = "";
                comboBox2.Text = "";
                comboBox3.Text = "";
                comboBox4.Text = "";
                textBox1.Text = "";
                textBox2.Text = "";
                DataTable dt = controller.GetAllPlayersNames();
                foreach (DataRow dr in dt.Rows)
                {
                    comboBox4.Items.Add(dr["Name"].ToString());
                    comboBox2.Items.Add(dr["Name"].ToString());
                }
                dt = controller.GetAllTeamsNames();
                foreach (DataRow dr in dt.Rows)
                {
                    comboBox3.Items.Add(dr["Name"].ToString());
                }
                label12.Visible = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.') && ((sender as System.Windows.Forms.TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
    }
}
