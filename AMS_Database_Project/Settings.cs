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
    public partial class Settings : Form
    {
        public int CurrentUserID { get; set; }

        public Settings(int userID)
        {
            InitializeComponent();
            CurrentUserID = userID;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Welcome Welcome = new Welcome();
            Welcome.Show();
            foreach (Form form in Application.OpenForms.Cast<Form>().ToArray())
            {
                if (form != Welcome) form.Hide();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                
                if (textBox1.Text == textBox2.Text)
                {
                    label8.Visible = false;
                    label7.Visible = true;
                    label6.Visible = false;
                    label5.Visible = false;
                    label3.Visible = false;
                }
                else { 
                  
                  string connString = @"Data Source=.;Initial Catalog=""Database project - AMS"";Integrated Security=True;";

                    using (SqlConnection conn = new SqlConnection(connString))
                    {

                        try
                        {
                            conn.Open();
                            string query = "UPDATE System_Users SET Password = @pass WHERE UserID = @id";
                            using (SqlCommand cmd = new SqlCommand(query, conn))
                            {
                                cmd.Parameters.AddWithValue("@id", CurrentUserID);
                                cmd.Parameters.AddWithValue("@pass", textBox2.Text.Trim());
                                int rowsAffected = cmd.ExecuteNonQuery();
                                if (rowsAffected > 0)
                                {
                                    label6.Visible = false;
                                    label5.Visible = false;
                                    label3.Visible = true;
                                    label7.Visible = false;
                                }
                                else
                                {
                                    label3.Visible = false;
                                    label5.Visible = true;
                                    label6.Visible = true;
                                    label7.Visible = false;
                                }
                            }

                        }
                        catch (Exception)
                        {
                            label3.Visible = false;
                            label5.Visible = false;
                            label6.Visible = false;
                            label7.Visible = true;
                        }
                    }
                }
            }
            else
            {
                label8.Visible = true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
