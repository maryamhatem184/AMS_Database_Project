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
    public partial class Update_Injury_Recovery_Progress : Form
    {

        private int medicalID;

        public Update_Injury_Recovery_Progress(int ID)
        {
            InitializeComponent();
            medicalID = ID;
            FillPlayerComboBox();
            FillInjuryComboBox();
        }


        private void Update_Injury_Recovery_Progress_Load(object sender, EventArgs e)
        {

        }

        private void FillPlayerComboBox()
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

        private void FillInjuryComboBox()
        {
            
            string connString = @"Data Source=.;Initial Catalog=""Database project - AMS"";Integrated Security=True;";
            string query = "SELECT Injury_ID,Injury FROM Injury_Record WHERE Player_ID =" + comboBox1.SelectedValue;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    comboBox2.DataSource = dt;
                    comboBox2.DisplayMember = "Injury";
                    comboBox2.ValueMember = "Injury_ID";


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading injuries: " + ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = "UPDATE Injury_Record SET Recovery_Status = @RecoveryStatus, Expected_Return_Date = @ExpectedReturnDate WHERE Player_ID = @PlayerID AND Injury_ID = @InjuryID";

            using (SqlConnection conn = new SqlConnection(@"Data Source=.;Initial Catalog=""Database project - AMS"";Integrated Security=True;"))
            {
                textBox1.Enabled = false;

                try
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@RecoveryStatus", textBox2.Text);
                    cmd.Parameters.AddWithValue("@ExpectedReturnDate", dateTimePicker1.Value);
                    cmd.Parameters.AddWithValue("@InjuryID", comboBox2.SelectedValue);
                    cmd.Parameters.AddWithValue("@PlayerID", comboBox1.SelectedValue);

                    if (comboBox1.SelectedValue == null || comboBox2.SelectedValue == null || string.IsNullOrWhiteSpace(textBox2.Text) || dateTimePicker1.Value == null)
                    {
                        label7.Visible = true;
                        label5.Visible = false;
                        label6.Visible = false;
                        return;
                    }

                    if (textBox1.Text == textBox2.Text && dateTimePicker1.Value == dateTimePicker1.Value)
                    {
                        label6.Visible = true;
                        label5.Visible = false;
                        label7.Visible = false;
                        return;
                    }

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        label5.Visible = true;
                        label6.Visible = false;
                        label7.Visible = false;
                    }
                    else
                    {
                        label6.Visible = true;
                        label5.Visible = false; 
                        label7.Visible = false;
                    }
                }
                catch (Exception)
                {
                    label7.Visible = true;
                }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedValue == null || comboBox2.SelectedIndex == -1) return;

            string connString = @"Data Source=.;Initial Catalog=""Database project - AMS"";Integrated Security=True;";
            string query = "SELECT Recovery_Status, Expected_Return_Date FROM Injury_Record WHERE Injury_ID = @InjuryID";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@InjuryID", comboBox2.SelectedValue);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        dateTimePicker1.Value = Convert.ToDateTime(dt.Rows[0]["Expected_Return_Date"]);
                        textBox1.Text = dt.Rows[0]["Recovery_Status"].ToString();
                    }
                }
                catch (Exception)
                {
                    
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
