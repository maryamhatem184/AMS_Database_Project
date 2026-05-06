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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void label4_MouseEnter(object sender, EventArgs e)
        {
            label4.Font = new Font(label4.Font, FontStyle.Underline);
            label4.Cursor = Cursors.Hand;
        }

        private void label4_MouseLeave(object sender, EventArgs e)
        {
            label4.Font = new Font(label4.Font, FontStyle.Regular);
        }


        private void label4_Click(object sender, EventArgs e)
        {
            Signup Signup = new Signup();
            Signup.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Welcome Welcome = new Welcome();
            Welcome.Show();
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string user = textBox1.Text.Trim();
            string pass = textBox2.Text;


            string connString = @"Data Source=.;Initial Catalog=""Database project - AMS"";Integrated Security=True;";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();

                    string query = "SELECT UserRole FROM System_Users WHERE Username = @user AND Password = @pass";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@user", user);
                        cmd.Parameters.AddWithValue("@pass", pass);

                        if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(pass))
                        {
                            label9.Visible = true;
                            label5.Visible = false;
                            return;
                        }

                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            string role = result.ToString();


                            if (role == "Manager")
                            {
                                Manager mForm = new Manager();
                                mForm.Show();
                            }
                            else if (role == "Medical")
                            {
                                Medical medForm = new Medical();
                                medForm.Show();
                            }
                            else if (role == "Coach")
                            {
                                Controller controller = new Controller();
                                Coach coachForm = new Coach(controller.GetCoachID(textBox1.Text));
                                coachForm.Show();
                            }
                            else if (role == "Fan")
                            {
                                Fan fanForm = new Fan();
                                fanForm.Show();
                            }

                            this.Hide();
                        }
                        else
                        {
                            label5.Visible = true;
                            label9.Visible = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Connection Error: " + ex.Message);
                }
            }
        }
    }
}
