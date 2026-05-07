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
    public partial class Update_Merchandise_Inventory : Form
    {
        public Update_Merchandise_Inventory(int ID)
        {
            InitializeComponent();
            Controller controller = new Controller();
            DataTable dt = controller.GetAllMerchandiseNames();
            foreach (DataRow row in dt.Rows)
            {
                comboBox1.Items.Add(row["Name"].ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Controller controller = new Controller();
            dataGridView1.ClearSelection();
            dataGridView1.DataSource = controller.GetAllMerchandiseInfoByName(comboBox1.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label3.Visible = false;
            label4.Visible = false;
            if (comboBox1.Text != "")
            {
                Controller controller = new Controller();
                controller.RestockMerchandiseItem(comboBox1.SelectedItem.ToString());
                dataGridView1.ClearSelection();
                dataGridView1.DataSource = controller.GetAllMerchandiseInfoByName(comboBox1.Text);
            }
            else
            {
                label4.Visible = true;
                return;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label3.Visible = false;
            label4.Visible = false;
            Controller controller = new Controller();
            if (textBox1.Text != "" && comboBox1.Text != "")
            {
                controller.UpdateMerchandiseItemPrice(comboBox1.SelectedItem.ToString(), Convert.ToDecimal(textBox1.Text));
                dataGridView1.ClearSelection();
                dataGridView1.DataSource = controller.GetAllMerchandiseInfoByName(comboBox1.Text);
            }
            else
            {
                label3.Visible = true;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
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
    }
}
