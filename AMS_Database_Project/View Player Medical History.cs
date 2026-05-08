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

namespace AMS_Database_Project
{
    public partial class View_Player_Medical_History : Form
    {
        private int medicalID;

        public View_Player_Medical_History(int ID)
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

        private void View_Player_Medical_History_Load(object sender, EventArgs e)
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

            string query = "SELECT Injury, Recovery_Status, Expected_Return_Date FROM Injury_Record WHERE Player_ID = @PlayerID";
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
                    MessageBox.Show("Error loading medical history: " + ex.Message);
                }

            }
        }
    }
}
