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
    public partial class Add_New_Inventory : Form
    {
        public Add_New_Inventory(int ID)
        {
            InitializeComponent();
            Controller controller = new Controller();
            dataGridView1.DataSource = controller.GetProducts();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            label5.Visible = false;
            label6.Visible = false;
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "")
            {
                Controller controller = new Controller();
                controller.AddNewInventoryItem(textBox1.Text, Convert.ToDecimal(textBox2.Text), Convert.ToInt32(textBox3.Text));
                dataGridView1.DataSource = controller.GetProducts();
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                label6.Visible = true;
            }
            else
            {
                label5.Visible = true;
            }
        }
    }
}
