using System;
using System.Data;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AMS_Database_Project
{
    public partial class MembershipForm : Form
    {
        MembershipController membershipController = new MembershipController();

        public MembershipForm(int fanID)
        {
            InitializeComponent();
        }

        private void LoadMemberships()
        {
            DataTable dt = membershipController.GetMemberships();
            dataGridView1.DataSource = dt;
        }

        // ADD
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                membershipController.AddMembership(
                    int.Parse(textBox1.Text),
                int.Parse(textBox2.Text),
                    textBox3.Text,
                    DateTime.Parse(textBox4.Text),
                    DateTime.Parse(textBox5.Text)
                );

                MessageBox.Show("Membership added successfully");
                LoadMemberships();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

      
       
        

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["Membership_ID"].Value.ToString();
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells["Fan_ID"].Value.ToString();
                textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells["Type"].Value.ToString();
                textBox4.Text = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells["Start_Date"].Value).ToString("yyyy-MM-dd");
                textBox5.Text = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells["Expiry_Date"].Value).ToString("yyyy-MM-dd");
            }
        }
        // VIEW
        private void button4_Click_1(object sender, EventArgs e)
        {
            try
            {
                LoadMemberships();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            try
            {
                membershipController.DeleteMembership(
                    int.Parse(textBox1.Text)
                );

                MessageBox.Show("Membership deleted successfully");
                LoadMemberships();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                int membershipId = int.Parse(textBox1.Text);

                DataTable dt = membershipController.GetMemberships();

                DataRow[] rows = dt.Select("Membership_ID = " + membershipId);

                if (rows.Length > 0)
                {
                    int fanId = textBox2.Text == ""
                        ? Convert.ToInt32(rows[0]["Fan_ID"])
                        : int.Parse(textBox2.Text);

                    string type = textBox3.Text == ""
                        ? rows[0]["Type"].ToString()
                        : textBox3.Text;

                    DateTime startDate = textBox4.Text == ""
                        ? Convert.ToDateTime(rows[0]["Start_Date"])
                        : DateTime.Parse(textBox4.Text);

                    DateTime expiryDate = textBox5.Text == ""
                        ? Convert.ToDateTime(rows[0]["Expiry_Date"])
                        : DateTime.Parse(textBox5.Text);

                    membershipController.UpdateMembership(
                        membershipId,
                        fanId,
                        type,
                        startDate,
                        expiryDate
                    );

                    MessageBox.Show("Membership updated successfully");

                    LoadMemberships();
                }
                else
                {
                    MessageBox.Show("Membership not found");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}