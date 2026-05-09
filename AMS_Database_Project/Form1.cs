using System;
using System.Data;
using System.Windows.Forms;

namespace AMS_Database_Project
{
    public partial class Form1 : Form
    {
        FanController fanController = new FanController();

        public Form1(int fanID)
        {
            InitializeComponent();
        }

        // Load Fans in Grid
        private void LoadFans()
        {
            DataTable dt = fanController.GetFans();

            MessageBox.Show("Rows count = " + dt.Rows.Count);

            dataGridView1.DataSource = dt;

        }

        // ADD
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                fanController.AddFan(
                    int.Parse(textBox1.Text),
                    textBox2.Text,
                    textBox3.Text,
                    textBox4.Text,
                    textBox5.Text
                );

                MessageBox.Show("Fan added successfully");

                LoadFans();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

       
        

        // Fill TextBoxes when clicking row
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["Fan_ID"].Value.ToString();
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells["Name"].Value.ToString();
                textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells["Email"].Value.ToString();
                textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells["Phone"].Value.ToString();
                textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells["National_ID"].Value.ToString();
            }
        }

        // Required Empty Event
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)//////////////
        {
            DataTable dt = fanController.GetFans();

            MessageBox.Show("Rows count = " + dt.Rows.Count);

            dataGridView1.DataSource = dt;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                fanController.UpdateFan(
                    int.Parse(textBox1.Text),
                    textBox2.Text,
                    textBox3.Text,
                    textBox4.Text,
                    textBox5.Text
                );

                MessageBox.Show("Fan updated successfully");

                LoadFans();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            try
            {
                fanController.DeleteFan(
                    int.Parse(textBox1.Text)
                );

                MessageBox.Show("Fan deleted successfully");

                LoadFans();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}