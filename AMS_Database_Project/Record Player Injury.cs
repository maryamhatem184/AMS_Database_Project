using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace AMS_Database_Project
{
    public partial class Record_Player_Injury : Form
    {
        private int medicalID;
        public Record_Player_Injury(int ID)
        {
            InitializeComponent();
            medicalID = ID;
            FillComboBox();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        private void FillComboBox()
        {
            string connString = @"Data Source=.;Initial Catalog=""Database project - AMS"";Integrated Security=True;";
            string query = "SELECT Player_ID,Name FROM Player WHERE Medical_ID =" + medicalID;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    comboBox1.DataSource = dt;
                    comboBox1.DisplayMember = "Name";
                    comboBox1.ValueMember = "Player_ID";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading players: " + ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = "INSERT INTO Injury_Record (Player_ID, Injury, Recovery_Status, Expected_Return_Date) VALUES (@PlayerID, @Injury, @RecoveryStatus, @ExpectedReturnDate)";

            using (SqlConnection conn = new SqlConnection(@"Data Source=.;Initial Catalog=""Database project - AMS"";Integrated Security=True;"))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (comboBox1.SelectedValue == null || string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text) || dateTimePicker1.Value == null)
                        {
                            label7.Visible = true;
                            label8.Visible = false;
                            label9.Visible = false;
                        }
                        else
                        {
                            string checkQuery = "SELECT COUNT(*) FROM Injury_Record WHERE Player_ID = @PlayerID AND Injury = @Injury AND Recovery_Status = @RecoveryStatus AND Expected_Return_Date = @ExpectedReturnDate";
                            using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                            {
                                checkCmd.Parameters.AddWithValue("@PlayerID", comboBox1.SelectedValue);
                                checkCmd.Parameters.AddWithValue("@Injury", textBox1.Text.Trim());
                                checkCmd.Parameters.AddWithValue("@RecoveryStatus", textBox2.Text.Trim());
                                checkCmd.Parameters.AddWithValue("@ExpectedReturnDate", dateTimePicker1.Value);
                                int exists = (int)checkCmd.ExecuteScalar();
                                if (exists > 0)
                                {
                                    label8.Visible = true;
                                    label9.Visible = false;
                                    return;
                                }
                            }


                            label7.Visible = false;
                            cmd.Parameters.AddWithValue("@PlayerID", comboBox1.SelectedValue);
                            cmd.Parameters.AddWithValue("@Injury", textBox1.Text.Trim());
                            cmd.Parameters.AddWithValue("@RecoveryStatus", textBox2.Text.Trim());
                            cmd.Parameters.AddWithValue("@ExpectedReturnDate", dateTimePicker1.Value);
                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                label8.Visible = false;
                                label5.Visible = true;
                                label6.Visible = false;
                                label9.Visible = false;
                            }
                            else
                            {
                                label8.Visible = false;
                                label5.Visible = false;
                                label9.Visible = false;
                                label6.Visible = true;
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    label9.Visible = true;
                    
                }
            }
        }
    }
}
