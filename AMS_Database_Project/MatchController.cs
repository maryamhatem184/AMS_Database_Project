using System;
using System.Data;
using System.Data.SqlClient;

namespace AMS_Database_Project
{
    public class MatchController
    {
        
        public void AddMatch(int matchId, DateTime date,
                             TimeSpan time, string opponent, int branchId)
        {
            using (SqlConnection con = Database.GetConnection())
            {
                string query = @"
                    INSERT INTO [Match]
                    (Match_ID, Date, Time, Opponent, Branch_ID)
                    VALUES
                    (@Match_ID, @Date, @Time, @Opponent, @Branch_ID)";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@Match_ID", matchId);
                cmd.Parameters.AddWithValue("@Date", date);
                cmd.Parameters.AddWithValue("@Time", time);
                cmd.Parameters.AddWithValue("@Opponent", opponent);
                cmd.Parameters.AddWithValue("@Branch_ID", branchId);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        
        public DataTable GetMatches()
        {
            using (SqlConnection con = Database.GetConnection())
            {
                string query = @"
                    SELECT 
                        m.Match_ID,
                        m.Date,
                        m.Time,
                        m.Opponent,
                        b.Name AS Branch_Name,
                        b.Capacity
                    FROM [Match] m
                    JOIN Branch b
                    ON m.Branch_ID = b.Branch_ID";

                SqlDataAdapter da = new SqlDataAdapter(query, con);

                DataTable dt = new DataTable();

                da.Fill(dt);

                return dt;
            }
        }

        
        public void UpdateMatch(int matchId, DateTime date,
                                TimeSpan time, string opponent, int branchId)
        {
            using (SqlConnection con = Database.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("UpdateMatch", con);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Match_ID", matchId);
                cmd.Parameters.AddWithValue("@Date", date);
                cmd.Parameters.AddWithValue("@Time", time);
                cmd.Parameters.AddWithValue("@Opponent", opponent);
                cmd.Parameters.AddWithValue("@Branch_ID", branchId);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        
        public void DeleteMatch(int matchId)
        {
            using (SqlConnection con = Database.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("DeleteMatch", con);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Match_ID", matchId);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}