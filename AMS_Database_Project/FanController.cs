using System;
using System.Data;
using System.Data.SqlClient;

namespace AMS_Database_Project
{
    public class FanController
    {
        
        public DataTable GetFanByID(int fanId)
        {
            using (SqlConnection con = Database.GetConnection())
            {
                string query = "SELECT * FROM Fan WHERE Fan_ID = @Fan_ID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Fan_ID", fanId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public void UpdateFan(int fanId, string name, string email, string phone, string nationalId)
        {
            using (SqlConnection con = Database.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("UpdateFan", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Fan_ID", fanId);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Phone", phone);
                cmd.Parameters.AddWithValue("@National_ID", nationalId);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteFan(int fanId)
        {
            using (SqlConnection con = Database.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("DeleteFan", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Fan_ID", fanId);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}