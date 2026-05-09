using System;
using System.Data;
using System.Windows.Forms;

namespace AMS_Database_Project
{
    public partial class Form1 : Form
    {
        FanController fanController = new FanController();
        private int fanID;

        public Form1(int ID)
        {
            InitializeComponent();
            fanID = ID;    
        }
         
        private void LoadFans()
        {
             
            DataTable dt = fanController.GetFanByID(fanID);

           
            if (dt.Rows.Count > 0)
            {
                textBox1.Text = dt.Rows[0]["Fan_ID"].ToString();
                textBox2.Text = dt.Rows[0]["Name"].ToString();
                textBox3.Text = dt.Rows[0]["Email"].ToString();
                textBox4.Text = dt.Rows[0]["Phone"].ToString();
                textBox5.Text = dt.Rows[0]["National_ID"].ToString();
            }

            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                textBox1.Text = row.Cells["Fan_ID"].Value.ToString();
                textBox2.Text = row.Cells["Name"].Value.ToString();
                textBox3.Text = row.Cells["Email"].Value.ToString();
                textBox4.Text = row.Cells["Phone"].Value.ToString();
                textBox5.Text = row.Cells["National_ID"].Value.ToString();
            }
        }

     
        private void button4_Click_1(object sender, EventArgs e)
        {
            LoadFans();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                 
                fanController.UpdateFan(
                    fanID,
                    textBox2.Text,
                    textBox3.Text,
                    textBox4.Text,
                    textBox5.Text
                );

                MessageBox.Show("Your data has been updated successfully!");
                LoadFans();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update Error: " + ex.Message);
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete your account?", "Confirm Delete", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    fanController.DeleteFan(fanID);
                    MessageBox.Show("Account deleted successfully.");

                   
                    Login loginForm = new Login();
                    loginForm.Show();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Delete Error: " + ex.Message);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = fanID.ToString();
            textBox1.ReadOnly = true;
            LoadFans();  
        }
 
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}