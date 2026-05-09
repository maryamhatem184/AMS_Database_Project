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
            textBox1.Text = "admin_taher";
            textBox2.Text = "pass123";
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

        private void button1_Click(object sender, EventArgs e)
        {

            string user = textBox1.Text.Trim();
            string pass = textBox2.Text.Trim();


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
                                Controller controller = new Controller();
                                Manager mForm = new Manager(controller.GetID(textBox1.Text));
                                mForm.Show();
                            }
                            else if (role == "Medical")
                            {
                                Controller controller = new Controller();
                                Medical medForm = new Medical(controller.GetID(textBox1.Text));
                                medForm.Show();
                            }
                            else if (role == "Coach")
                            {
                                Controller controller = new Controller();
                                Coach coachForm = new Coach(controller.GetID(textBox1.Text));
                                coachForm.Show();
                            }
                            else if (role == "Fan")
                            {
                                Controller controller = new Controller();
                                Fan fanForm = new Fan(controller.GetID(textBox1.Text));
                                fanForm.Show();
                            }

                            else
                            {
                                label6.Visible = true;
                                label5.Visible = false;
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

        private void button3_Click(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = !textBox2.UseSystemPasswordChar;

            if (textBox2.UseSystemPasswordChar)
            {
                button3.Text = "Show Password";
            }
            else
            {
                button3.Text = "Hide Password";
            }
        }
    }
}
