using System;
using System.Data;
using System.Windows.Forms;

namespace AMS_Database_Project
{
    public partial class Form3 : Form
    {
        MatchController matchController = new MatchController();

        public Form3(int ID)
        {
            InitializeComponent();
        }

        private void LoadMatches()
        {
            dataGridView1.DataSource = matchController.GetMatches();
        }

    
 
        private int GetBranchIdByMatch(int matchId)
        {
            using (System.Data.SqlClient.SqlConnection con = Database.GetConnection())
            {
                string query = "SELECT Branch_ID FROM [Match] WHERE Match_ID = @Match_ID";

                System.Data.SqlClient.SqlCommand cmd =
                    new System.Data.SqlClient.SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@Match_ID", matchId);

                con.Open();

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                textBox1.Text = row.Cells["Match_ID"].Value.ToString();
                textBox2.Text = Convert.ToDateTime(row.Cells["Date"].Value).ToString("yyyy-MM-dd");
                textBox3.Text = row.Cells["Time"].Value.ToString();
                textBox4.Text = row.Cells["Opponent"].Value.ToString();
                textBox5.Text = row.Cells["Branch_ID"].Value.ToString();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

         

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBox1.Text)) return;

                int matchId = int.Parse(textBox1.Text);
                 
                DateTime date = string.IsNullOrEmpty(textBox2.Text)
                    ? Convert.ToDateTime(dataGridView1.CurrentRow.Cells["Date"].Value)
                    : DateTime.Parse(textBox2.Text);

                TimeSpan time = string.IsNullOrEmpty(textBox3.Text)
                    ? TimeSpan.Parse(dataGridView1.CurrentRow.Cells["Time"].Value.ToString())
                    : TimeSpan.Parse(textBox3.Text);

                string opponent = string.IsNullOrEmpty(textBox4.Text)
                    ? dataGridView1.CurrentRow.Cells["Opponent"].Value.ToString()
                    : textBox4.Text;

                int branchId = string.IsNullOrEmpty(textBox5.Text)
                    ? int.Parse(dataGridView1.CurrentRow.Cells["Branch_ID"].Value.ToString())
                    : int.Parse(textBox5.Text);

                matchController.UpdateMatch(matchId, date, time, opponent, branchId);
                MessageBox.Show("Match updated successfully");
                LoadMatches();
            }
            catch (Exception ex) {
                MessageBox.Show("Update Error: " + ex.Message);
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBox1.Text)) return;
                matchController.DeleteMatch(int.Parse(textBox1.Text));
                MessageBox.Show("Match deleted successfully");
                LoadMatches();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); 
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            try
            {
                LoadMatches();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            LoadMatches();
        }
    }
}