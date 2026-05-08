using System;
using System.Data;
using System.Data.SqlClient;

namespace AMS_Database_Project
{
    public class MembershipController
    {
        public void AddMembership(int membershipId, int fanId, string type, DateTime startDate, DateTime expiryDate)
        {
            using (SqlConnection con = Database.GetConnection())
            {
                string query = @"
                    INSERT INTO Membership
                    (Membership_ID, Fan_ID, Type, Start_Date, Expiry_Date)
                    VALUES
                    (@Membership_ID, @Fan_ID, @Type, @Start_Date, @Expiry_Date)";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Membership_ID", membershipId);
                cmd.Parameters.AddWithValue("@Fan_ID", fanId);
                cmd.Parameters.AddWithValue("@Type", type);
                cmd.Parameters.AddWithValue("@Start_Date", startDate);
                cmd.Parameters.AddWithValue("@Expiry_Date", expiryDate);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public DataTable GetMemberships()
        {
            using (SqlConnection con = Database.GetConnection())
            {
                string query = @"
                    SELECT 
                        m.Membership_ID,
                        m.Fan_ID,
                        f.Name AS Fan_Name,
                        m.Type,
                        m.Start_Date,
                        m.Expiry_Date
                    FROM Membership m
                    JOIN Fan f ON m.Fan_ID = f.Fan_ID";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
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