using System;
using System.Data;
using System.Windows.Forms;

namespace AMS_Database_Project
{
    public partial class Form4 : Form
    {
        BookingController bookingController = new BookingController();
        private int fanID;
        public Form4(int ID)
        {
            InitializeComponent();
            fanID = ID;
        }

        private void LoadBookings()
        {
           
                 
                dataGridView1.DataSource = bookingController.GetBookingsByFan(fanID);
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                 
                textBox1.Text = row.Cells[0].Value.ToString();

          
                textBox3.Text = row.Cells["Match_ID"].Value.ToString();
                textBox4.Text = row.Cells["Ticket_Type"].Value.ToString();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        { 
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            textBox2.Text = fanID.ToString();
            textBox2.ReadOnly = true;

            LoadBookings();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                bookingController.BookTicket(
                       fanID,                      
                       int.Parse(textBox3.Text),   
                      textBox4.Text              
                                            );

                MessageBox.Show("Ticket booked successfully");
                LoadBookings();
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
                DataTable dt = bookingController.CheckCapacity(
                    int.Parse(textBox3.Text)
                );

                dataGridView1.DataSource = dt;

                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show(
                        "Booked Seats = " + dt.Rows[0]["Booked_Seats"].ToString()
                        + "\nCapacity = " + dt.Rows[0]["Capacity"].ToString()
                        + "\nAvailable Seats = " + dt.Rows[0]["Available_Seats"].ToString()
                    );
                }
                else
                {
                    MessageBox.Show("Match not found");
                }
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
                    MessageBox.Show("يا ريت تختاري الحجز من الجدول الأول (دوس كليك على السطر)");
                    return;
                }

                int bookingId = int.Parse(textBox1.Text);
                bookingController.DeleteBooking(bookingId);

                MessageBox.Show("Booking deleted successfully");
                LoadBookings();  
                textBox1.Clear();  
            }
            catch (Exception ex)
            {
                MessageBox.Show("فيه غلطة حصلت: " + ex.Message);
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            try
            {
                LoadBookings(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}