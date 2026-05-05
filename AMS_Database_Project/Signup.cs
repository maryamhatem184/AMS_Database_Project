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
    public partial class Signup : Form
    {
        public Signup()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Welcome Welcome = new Welcome();
            Welcome.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text.Trim(); 
            string password = textBox2.Text.Trim();      
            string nationalID = textBox4.Text.Trim();    
            string email = textBox5.Text.Trim();
            string phone = textBox6.Text.Trim();
            string role = "Fan";

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(nationalID) || string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Please fill in all the required fields.");
                return;
            }

            string connString = @"Data Source=.;Initial Catalog=""Database project - AMS"";Integrated Security=True;";

            using (SqlConnection conn = new SqlConnection(connString))
            {

                try
                {
                    conn.Open();

                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        string userQuery = @"INSERT INTO System_Users (Username, Password, UserRole) 
                                 VALUES (@user, @pass, @role);
                                 SELECT SCOPE_IDENTITY();";

                        int newUserID;

                        using (SqlCommand cmd1 = new SqlCommand(userQuery, conn, transaction))
                        {
                            cmd1.Parameters.AddWithValue("@user", username);
                            cmd1.Parameters.AddWithValue("@pass", password);
                            cmd1.Parameters.AddWithValue("@role", role);

                            newUserID = Convert.ToInt32(cmd1.ExecuteScalar());
                        }

                        string fanQuery = @"INSERT INTO Fan (Fan_ID, Name, National_ID, Email, Phone) 
                                VALUES (@id, @name, @natID, @Email, @Phone)";

                        using (SqlCommand cmd2 = new SqlCommand(fanQuery, conn, transaction))
                        {
                            cmd2.Parameters.AddWithValue("@id", newUserID);
                            cmd2.Parameters.AddWithValue("@name", username);
                            cmd2.Parameters.AddWithValue("@natID", nationalID);
                            cmd2.Parameters.AddWithValue("@Email", email);
                            cmd2.Parameters.AddWithValue("@Phone", phone);

                            cmd2.ExecuteNonQuery();
                        }

                        transaction.Commit();

                        MessageBox.Show("Registration Complete! Your ID is: " + newUserID);

                        new Login().Show();
                        this.Hide();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Registration failed: " + ex.Message);
                }
        }
    }
    }
}
