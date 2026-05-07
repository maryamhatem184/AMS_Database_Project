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
    public partial class Add_Update_Remove_Club_Branch : Form
    {
        public Add_Update_Remove_Club_Branch(int ID)
        {
            InitializeComponent();
            Controller controller = new Controller();
            DataTable dt = controller.GetAllBranchNames(ID);
            foreach (DataRow dr in dt.Rows)
            {
                comboBox1.Items.Add(dr["Name"].ToString());
                comboBox2.Items.Add(dr["Name"].ToString());
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label13.Visible = false;
            label15.Visible = false;
            label8.Visible = false;
            label7.Visible = false;
            label11.Visible = false;
            label9.Visible = false;
            if (comboBox2.Text == "")
            {
                label13.Visible = true;
            }
            else
            {
                Controller controller = new Controller();
                controller.RemoveBranch(comboBox2.Text);
                comboBox2.Items.Clear();
                comboBox1.Items.Clear();
                DataTable dt = controller.GetAllBranchNames(0);
                foreach (DataRow dr in dt.Rows)
                {
                    comboBox1.Items.Add(dr["Name"].ToString());
                    comboBox2.Items.Add(dr["Name"].ToString());
                }
                label15.Visible = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label11.Visible = false;
            label9.Visible = false;
            label8.Visible = false;
            label7.Visible = false;
            label13.Visible = false;
            label15.Visible = false;
            if (comboBox1.Text == "" || textBox4.Text == "")
            {
                label11.Visible = true;
            }
            else
            {
                Controller controller = new Controller();
                controller.UpdateBranch(comboBox1.Text, Convert.ToInt32(textBox4.Text));
                comboBox2.Items.Clear();
                comboBox1.Items.Clear();
                DataTable dt = controller.GetAllBranchNames(0);
                foreach (DataRow dr in dt.Rows)
                {
                    comboBox1.Items.Add(dr["Name"].ToString());
                    comboBox2.Items.Add(dr["Name"].ToString());
                }
                label9.Visible = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label8.Visible = false;
            label7.Visible = false;
            label11.Visible = false;
            label9.Visible = false;
            label13.Visible = false;
            label15.Visible = false;
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
            {
                label7.Visible = true;
            }
            else
            {
                Controller controller = new Controller();
                controller.CreateBranch(textBox1.Text, textBox2.Text, Convert.ToInt32(textBox3.Text));
                comboBox2.Items.Clear();
                comboBox1.Items.Clear();
                DataTable dt = controller.GetAllBranchNames(0);
                foreach (DataRow dr in dt.Rows)
                {
                    comboBox1.Items.Add(dr["Name"].ToString());
                    comboBox2.Items.Add(dr["Name"].ToString());
                }
                label8.Visible = true;
            }
        }
    }
}
