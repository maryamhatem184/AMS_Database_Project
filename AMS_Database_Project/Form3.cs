using System;
using System.Data;
using System.Windows.Forms;

namespace AMS_Database_Project
{
    public partial class Form3 : Form
    {
        MatchController matchController = new MatchController();

        public Form3(int fanID)
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
                textBox1.Text =
                    dataGridView1.Rows[e.RowIndex].Cells["Match_ID"].Value.ToString();

                textBox2.Text =
                    Convert.ToDateTime(
                        dataGridView1.Rows[e.RowIndex].Cells["Date"].Value
                    ).ToString("yyyy-MM-dd");

                textBox3.Text =
                    dataGridView1.Rows[e.RowIndex].Cells["Time"].Value.ToString();

                textBox4.Text =
                    dataGridView1.Rows[e.RowIndex].Cells["Opponent"].Value.ToString();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                matchController.AddMatch(
                    int.Parse(textBox1.Text),
                    DateTime.Parse(textBox2.Text),
                    TimeSpan.Parse(textBox3.Text),
                    textBox4.Text,
                    int.Parse(textBox5.Text)
                );

                MessageBox.Show("Match added successfully");

                LoadMatches();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                int matchId = int.Parse(textBox1.Text);

                DataTable dt = matchController.GetMatches();

                DataRow[] rows = dt.Select("Match_ID = " + matchId);

                if (rows.Length > 0)
                {
                    DateTime date = textBox2.Text == ""
                        ? Convert.ToDateTime(rows[0]["Date"])
                        : DateTime.Parse(textBox2.Text);

                    TimeSpan time = textBox3.Text == ""
                        ? TimeSpan.Parse(rows[0]["Time"].ToString())
                        : TimeSpan.Parse(textBox3.Text);

                    string opponent = textBox4.Text == ""
                        ? rows[0]["Opponent"].ToString()
                        : textBox4.Text;

                    int branchId = textBox5.Text == ""
                        ? GetBranchIdByMatch(matchId)
                        : int.Parse(textBox5.Text);

                    matchController.UpdateMatch(
                        matchId,
                        date,
                        time,
                        opponent,
                        branchId
                    );

                    MessageBox.Show("Match updated successfully");

                    LoadMatches();
                }
                else
                {
                    MessageBox.Show("Match not found");
                }
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
                matchController.DeleteMatch(
                    int.Parse(textBox1.Text)
                );

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
    }
}