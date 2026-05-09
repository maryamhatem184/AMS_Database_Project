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
    public partial class Register_Deactivate_Coach : Form
    {
        public Register_Deactivate_Coach(int ID)
        {
            InitializeComponent();
            Controller controller = new Controller();
            DataTable dt = controller.GetAllCoachNames();
            foreach (DataRow dr in dt.Rows)
            {
                comboBox2.Items.Add(dr["Name"].ToString());
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label15.Visible = false;
            label13.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            if (comboBox2.Text == "")
            {
                label13.Visible = true;
            }
            else
            {
                Controller controller = new Controller();
                controller.DeactivateCoach(comboBox2.Text);
                DataTable dt = controller.GetAllCoachNames();
                comboBox2.Items.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    comboBox2.Items.Add(dr["Name"].ToString());
                }
                label15.Visible = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label7.Visible = false;
            label8.Visible = false;
            label15.Visible = false;
            label13.Visible = false;
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                label7.Visible = true;
            }
            else
            {
                Controller controller = new Controller();
                controller.RegisterCoach(textBox2.Text, textBox1.Text);
                DataTable dt = controller.GetAllCoachNames();
                comboBox2.Items.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    comboBox2.Items.Add(dr["Name"].ToString());
                }
                label8.Visible = true;
            }
        }
    }
}
