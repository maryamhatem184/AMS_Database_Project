using System.Data;
using System.Data.SqlClient;

namespace AMS_Database_Project
{
    public class BookingController
    {
        public void BookTicket(int bookingId, int fanId, int matchId, string ticketType)
        {
            using (SqlConnection con = Database.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("BookTicket", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Booking_ID", bookingId);
                cmd.Parameters.AddWithValue("@Fan_ID", fanId);
                cmd.Parameters.AddWithValue("@Match_ID", matchId);
                cmd.Parameters.AddWithValue("@Ticket_Type", ticketType);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public DataTable CheckCapacity(int matchId)
        {
            using (SqlConnection con = Database.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("CheckCapacity", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Match_ID", matchId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
        }

        public DataTable GetBookings()
        {
            using (SqlConnection con = Database.GetConnection())
            {
                string query = @"
                    SELECT 
                        b.Booking_ID,
                        b.Fan_ID,
                        f.Name AS Fan_Name,
                        b.Match_ID,
                        m.Opponent,
                        m.Date,
                        m.Time,
                        b.Booking_Date,
                        b.Ticket_Type
                    FROM Booking b
                    JOIN Fan f ON b.Fan_ID = f.Fan_ID
                    JOIN [Match] m ON b.Match_ID = m.Match_ID";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
        }

        public void UpdateBooking(int bookingId, int fanId, int matchId, string ticketType)
        {
            using (SqlConnection con = Database.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("UpdateBooking", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Booking_ID", bookingId);
                cmd.Parameters.AddWithValue("@Fan_ID", fanId);
                cmd.Parameters.AddWithValue("@Match_ID", matchId);
                cmd.Parameters.AddWithValue("@Booking_Date", System.DateTime.Now);
                cmd.Parameters.AddWithValue("@Ticket_Type", ticketType);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteBooking(int bookingId)
        {
            using (SqlConnection con = Database.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("DeleteBooking", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Booking_ID", bookingId);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}