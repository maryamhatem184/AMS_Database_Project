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
    public partial class Generate_Reports : Form
    {
        public Generate_Reports(int ID)
        {
            InitializeComponent();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        Controller Controller = new Controller();
        private void ShowStatFromSP(string title, string dbColumnName)
        {
            dataGridView1.DataSource = null;

            DataTable dtStats = Controller.GetManagerialStats();

            if (dtStats != null && dtStats.Rows.Count > 0)
            {
                string value = dtStats.Rows[0][dbColumnName].ToString();
                if (string.IsNullOrEmpty(value)) value = "0";

                textBox1.Text = value;
            }
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            textBox1.Text = "";
            dataGridView1.DataSource = Controller.GetTopScorers();
        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            textBox1.Text = "";
            dataGridView1.DataSource = Controller.GetMostBookedMatches();
        }
        private void button3_Click_1(object sender, EventArgs e)
        {
            textBox1.Text = "";
            dataGridView1.DataSource = Controller.GetTopSellingProducts();
        }
        private void button4_Click_1(object sender, EventArgs e)
        {
            ShowStatFromSP("Total Revenue", "TotalRevenue");
        }
        private void button5_Click_1(object sender, EventArgs e)
        {
            ShowStatFromSP("Average Transaction", "AvgTransactionValue");
        }
        private void button6_Click_1(object sender, EventArgs e)
        {
            ShowStatFromSP("Most Expensive", "MostExpensiveItem");
        }
        private void button7_Click_1(object sender, EventArgs e)
        {
            ShowStatFromSP("Cheapest Item", "CheapestItem");
        }
        private void button8_Click_1(object sender, EventArgs e)
        {
            ShowStatFromSP("Total Stock", "TotalItemsInStock");
        }
        private void button9_Click_1(object sender, EventArgs e)
        {
            ShowStatFromSP("Total Fans", "TotalRegisteredFans");
        }
        private void button10_Click_1(object sender, EventArgs e)
        {
            ShowStatFromSP("Total Matches", "TotalMatchesScheduled");
        }
        private void button11_Click_1(object sender, EventArgs e)
        {
            ShowStatFromSP("Total Bookings", "TotalTicketsBooked");
        }
    }
}
