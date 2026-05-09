using System;
using System.Data;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AMS_Database_Project
{
    public partial class MembershipForm : Form
    {
        MembershipController membershipController = new MembershipController();
        private int fanID;
        public MembershipForm(int ID)
        {
            InitializeComponent();
            fanID = ID;
        }

        private void LoadMemberships()
        {
            try
            {
                DataTable dt = membershipController.GetMembershipsByFan(this.fanID);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
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
                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    MessageBox.Show("Please select a membership to delete.");
                    return;
                }

                DialogResult result = MessageBox.Show("Are you sure you want to delete this membership?", "Confirm", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    membershipController.DeleteMembership(int.Parse(textBox1.Text));
                    MessageBox.Show("Membership deleted successfully!");
                    LoadMemberships();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Delete Error: " + ex.Message);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                
                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    MessageBox.Show("Please select a membership from the table first!");
                    return;
                }

                int membershipId = int.Parse(textBox1.Text);

                
                DateTime start = string.IsNullOrEmpty(textBox4.Text)
                                 ? Convert.ToDateTime(dataGridView1.CurrentRow.Cells["Start_Date"].Value)
                                 : DateTime.Parse(textBox4.Text);

                DateTime expiry = string.IsNullOrEmpty(textBox5.Text)
                                  ? Convert.ToDateTime(dataGridView1.CurrentRow.Cells["Expiry_Date"].Value)
                                  : DateTime.Parse(textBox5.Text);

                 
                string membershipType = string.IsNullOrEmpty(textBox3.Text)
                                        ? dataGridView1.CurrentRow.Cells["Type"].Value.ToString()
                                        : textBox3.Text;

                
                membershipController.UpdateMembership(
                    membershipId,
                    this.fanID,
                    membershipType,
                    start,
                    expiry
                );

                MessageBox.Show("Membership Updated Successfully!");

                LoadMemberships();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update Error: " + ex.Message);
            }
        }

        private void MembershipForm_Load(object sender, EventArgs e)
        {
            textBox2.Text = fanID.ToString();
            textBox2.ReadOnly = true;

            LoadMemberships();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}