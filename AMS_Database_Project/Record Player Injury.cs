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
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {


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
                        }
                        else
                        {
                            label7.Visible = false;
                            cmd.Parameters.AddWithValue("@PlayerID", comboBox1.SelectedValue);
                            cmd.Parameters.AddWithValue("@Injury", textBox1.Text.Trim());
                            cmd.Parameters.AddWithValue("@RecoveryStatus", textBox2.Text.Trim());
                            cmd.Parameters.AddWithValue("@ExpectedReturnDate", dateTimePicker1.Value);
                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                label5.Visible = true;
                                label6.Visible = false;
                            }
                            else
                            {
                                label5.Visible = false;
                                label6.Visible = true;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }
    }
}
