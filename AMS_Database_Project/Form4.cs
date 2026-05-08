using System;
using System.Data;
using System.Windows.Forms;

namespace AMS_Database_Project
{
    public partial class Form4 : Form
    {
        BookingController bookingController = new BookingController();

        public Form4()
        {
            InitializeComponent();
        }

        private void LoadBookings()
        {
            dataGridView1.DataSource = bookingController.GetBookings();
        }
 
      
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                textBox1.Text =
                    dataGridView1.Rows[e.RowIndex].Cells["Booking_ID"].Value.ToString();

                textBox2.Text =
                    dataGridView1.Rows[e.RowIndex].Cells["Fan_ID"].Value.ToString();

                textBox3.Text =
                    dataGridView1.Rows[e.RowIndex].Cells["Match_ID"].Value.ToString();

                textBox4.Text =
                    dataGridView1.Rows[e.RowIndex].Cells["Ticket_Type"].Value.ToString();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                bookingController.BookTicket(
                    int.Parse(textBox1.Text),
                    int.Parse(textBox2.Text),
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
                bookingController.DeleteBooking(
                    int.Parse(textBox1.Text)
                );

                MessageBox.Show("Booking deleted successfully");

                LoadBookings();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
    }
}