using DBapplication;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AMS_Database_Project
{
    public partial class Register_Deactivate_Medical_Staff : Form
    {
        public Register_Deactivate_Medical_Staff(int ID)
        {
            InitializeComponent();
            Controller controller = new Controller();
            DataTable dt = controller.GetAllMedicalStaffNames();
            foreach (DataRow dr in dt.Rows)
            {
                comboBox2.Items.Add(dr["Name"].ToString());
                comboBox3.Items.Add(dr["Name"].ToString());
            }
            dt = controller.GetAllPlayersNames();
            foreach (DataRow dr in dt.Rows)
            {
                comboBox4.Items.Add(dr["Name"].ToString());
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label15.Visible = false;
            label13.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            label5.Visible = false;
            label12.Visible = false;
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                label7.Visible = true;
            }
            else
            {
                Controller controller = new Controller();
                controller.RegisterMedicalStaff(textBox2.Text, textBox1.Text);
                comboBox2.Items.Clear();
                comboBox3.Items.Clear();
                comboBox4.Items.Clear();
                comboBox2.Text = "";
                comboBox3.Text = "";
                comboBox4.Text = "";
                textBox1.Text = "";
                textBox2.Text = "";
                DataTable dt = controller.GetAllMedicalStaffNames();
                foreach (DataRow dr in dt.Rows)
                {
                    comboBox2.Items.Add(dr["Name"].ToString());
                    comboBox3.Items.Add(dr["Name"].ToString());
                }
                dt = controller.GetAllPlayersNames();
                foreach (DataRow dr in dt.Rows)
                {
                    comboBox4.Items.Add(dr["Name"].ToString());
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
            label5.Visible = false;
            label12.Visible = false;
            if (comboBox2.Text == "")
            {
                label13.Visible = true;
            }
            else
            {
                Controller controller = new Controller();
                controller.DeactivateMedicalStaff(comboBox2.Text);
                comboBox2.Items.Clear();
                comboBox3.Items.Clear();
                comboBox4.Items.Clear();
                comboBox2.Text = "";
                comboBox3.Text = "";
                comboBox4.Text = "";
                textBox1.Text = "";
                textBox2.Text = "";
                DataTable dt = controller.GetAllMedicalStaffNames();
                foreach (DataRow dr in dt.Rows)
                {
                    comboBox2.Items.Add(dr["Name"].ToString());
                    comboBox3.Items.Add(dr["Name"].ToString());
                }
                dt = controller.GetAllPlayersNames();
                foreach (DataRow dr in dt.Rows)
                {
                    comboBox4.Items.Add(dr["Name"].ToString());
                }
                label15.Visible = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label15.Visible = false;
            label13.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            label5.Visible = false;
            label12.Visible = false;
            if (comboBox3.Text == "" || comboBox4.Text == "")
            {
                label7.Visible = true;
            }
            else
            {
                Controller controller = new Controller();
                controller.AssignDoctorToPlayer(controller.GetDoctorIDByName(comboBox3.Text), controller.GetPlayerID(comboBox4.Text));
                comboBox2.Items.Clear();
                comboBox3.Items.Clear();
                comboBox4.Items.Clear();
                comboBox2.Text = "";
                comboBox3.Text = "";
                comboBox4.Text = "";
                textBox1.Text = "";
                textBox2.Text = "";
                DataTable dt = controller.GetAllMedicalStaffNames();
                foreach (DataRow dr in dt.Rows)
                {
                    comboBox2.Items.Add(dr["Name"].ToString());
                    comboBox3.Items.Add(dr["Name"].ToString());
                }
                dt = controller.GetAllPlayersNames();
                foreach (DataRow dr in dt.Rows)
                {
                    comboBox4.Items.Add(dr["Name"].ToString());
                }
                label12.Visible = true;
            }
        }
    }   
}
