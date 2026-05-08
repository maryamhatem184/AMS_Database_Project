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
    public partial class Track_Rehabilitation_Programs : Form
    {
        private int medicalID;

        public Track_Rehabilitation_Programs(int ID)
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

        private void Track_Rehabilitation_Programs_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue == null || comboBox1.SelectedValue is System.Data.DataRowView)
            {
                return;
            }

            string query = "SELECT Name, Role FROM Medical_Staff WHERE Medical_ID IN (SELECT Medical_ID FROM Player WHERE Player_ID = @PlayerID)";
            using (SqlConnection conn = new SqlConnection(@"Data Source=.;Initial Catalog=""Database project - AMS"";Integrated Security=True;"))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@PlayerID", comboBox1.SelectedValue);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading medicals: " + ex.Message);
                }

            }
        }
    }
}
