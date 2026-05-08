using DBapplication;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AMS_Database_Project
{
    public partial class Update_Player_Availability : Form
    {
        private int medicalID;

        public Update_Player_Availability(int ID)
        {
            InitializeComponent();
            Controller controller = new Controller();
            medicalID = ID;
            FillPlayerComboBox();
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

                    comboBox1.SelectedIndex = -1;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading players: " + ex.Message);
                }
            }
        }

        private void Update_Player_Availability_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = "UPDATE Player SET Availability = @Availability WHERE Player_ID = @PlayerID";
            using (SqlConnection conn = new SqlConnection(@"Data Source=.;Initial Catalog=""Database project - AMS"";Integrated Security=True;"))
            {
                try
                { 

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Availability", textBox2.Text);
                    cmd.Parameters.AddWithValue("@PlayerID", comboBox1.SelectedValue);

                    if (comboBox1.SelectedValue == null || comboBox1.SelectedIndex == -1 || string.IsNullOrWhiteSpace(textBox2.Text))
                    {
                        label6.Visible = true;
                        label5.Visible = false;
                        label3.Visible = false;
                        label7.Visible = false;
                        return;
                    }
                    if (textBox2.Text == textBox1.Text)
                    {
                        label7.Visible = true;
                        label6.Visible = false;
                        label5.Visible = false;
                        label3.Visible = false;
                        return;
                    }

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        label6.Visible = false;
                        label3.Visible = true;
                        label5.Visible = false;
                        label7.Visible = false;
                        textBox1.Text = textBox2.Text;
                    }
                    else
                    {
                        label6.Visible = false;
                        label5.Visible = true;
                        label3.Visible = false;
                        label7.Visible = false;
                    }
                }
                catch (Exception)
                {
                        label5.Visible = true;
                        label3.Visible = false;
                        label6.Visible = false;
                        label7.Visible = false;
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue == null || comboBox1.SelectedIndex == -1) return;

            string connString = @"Data Source=.;Initial Catalog=""Database project - AMS"";Integrated Security=True;";
            string query = "SELECT Name, Availability FROM Player WHERE Player_ID = @PlayerID";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@PlayerID", comboBox1.SelectedValue);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        textBox1.Text = dt.Rows[0]["Availability"].ToString();

                    }
                }
                catch (Exception)
                {

                }
            }
        }
    }
}
