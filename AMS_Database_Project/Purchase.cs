using DBapplication;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AMS_Database_Project
{
    public partial class purchase : Form
    {
        Controller Controller = new Controller();
        decimal currentTotal = 0;
        DataTable cartTable;
        private int fanID;
        public purchase(int fanID)
        {
            InitializeComponent();
            this.fanID = fanID;
            DataTable fans = Controller.GetFanNames();
            comboBox1.DataSource = fans;
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "Fan_ID";

            DataTable products = Controller.GetProductNames();
            comboBox2.DataSource = products;
            comboBox2.DisplayMember = "Name";
            comboBox2.ValueMember = "Merchandise_ID";

            cartTable = new DataTable();
            cartTable.Columns.Add("MerchandiseID", typeof(int));
            cartTable.Columns.Add("Product", typeof(string));
            cartTable.Columns.Add("Price", typeof(decimal));
            cartTable.Columns.Add("Quantity", typeof(int));
            cartTable.Columns.Add("Subtotal", typeof(decimal));

            dataGridView1.DataSource = cartTable;
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedValue == null) return;

            int merchId = (int)comboBox2.SelectedValue;
            string productName = comboBox2.Text;

            if (int.TryParse(numericUpDown1.Text, out int quantity) && quantity > 0)
            {
                decimal price = (decimal)Controller.GetProductPrice(productName);
                decimal subtotal = price * quantity;

                cartTable.Rows.Add(merchId, productName, price, quantity, subtotal);

                currentTotal += subtotal;
                textBox1.Text = currentTotal.ToString("0.00");


            }
            else
            {
                MessageBox.Show("Please enter a valid numeric quantity greater than 0.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (cartTable.Rows.Count == 0)
            {
                MessageBox.Show("The cart is empty. Please add items first.");
                return;
            }

            if (comboBox1.SelectedValue == null)
            {
                MessageBox.Show("Please select a Fan.");
                return;
            }

            int fanId = (int)comboBox1.SelectedValue;

            int newTransactionId = Controller.CreateTransaction(fanId, currentTotal);

            foreach (DataRow row in cartTable.Rows)
            {
                int merchId = Convert.ToInt32(row["MerchandiseID"]);
                int qty = Convert.ToInt32(row["Quantity"]);

                Controller.AddToTransaction(newTransactionId, merchId, qty);

                Controller.ReduceStock(merchId, qty);
            }

            MessageBox.Show($"Transaction created successfully! (Transaction ID: {newTransactionId})");

            cartTable.Rows.Clear();
            currentTotal = 0;
            textBox1.Text = "0.00";


            comboBox2.DataSource = Controller.GetProductNames();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
