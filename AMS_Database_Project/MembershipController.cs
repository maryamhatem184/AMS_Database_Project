using System;
using System.Data;
using System.Data.SqlClient;

namespace AMS_Database_Project
{
    public class MembershipController
    {
       
        public void AddMembership(int fanId, string type, DateTime startDate, DateTime expiryDate)
        {
            using (SqlConnection con = Database.GetConnection())
            {
                
                string query = @"
                    INSERT INTO Membership
                    (Fan_ID, Type, Start_Date, Expiry_Date)
                    VALUES
                    (@Fan_ID, @Type, @Start_Date, @Expiry_Date)";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Fan_ID", fanId);
                cmd.Parameters.AddWithValue("@Type", type);
                cmd.Parameters.AddWithValue("@Start_Date", startDate);
                cmd.Parameters.AddWithValue("@Expiry_Date", expiryDate);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }


        public DataTable GetMembershipsByFan(int fanId)
        {
            using (SqlConnection con = Database.GetConnection())
            {
                 
                string query = "SELECT * FROM Membership WHERE Fan_ID = @Fan_ID";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Fan_ID", fanId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public void UpdateMembership(int membershipId, int fanId, string type, DateTime startDate, DateTime expiryDate)
        {
            using (SqlConnection con = Database.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("UpdateMembership", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Membership_ID", membershipId);
                cmd.Parameters.AddWithValue("@Fan_ID", fanId);
                cmd.Parameters.AddWithValue("@Type", type);
                cmd.Parameters.AddWithValue("@Start_Date", startDate);
                cmd.Parameters.AddWithValue("@Expiry_Date", expiryDate);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

       
        public void DeleteMembership(int membershipId)
        {
            using (SqlConnection con = Database.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("DeleteMembership", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Membership_ID", membershipId);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}