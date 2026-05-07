using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DBapplication
{
    public class Controller
    {
        DBManager dbMan;
        public Controller()
        {
            dbMan = new DBManager();
        }


        public void TerminateConnection()
        {
            dbMan.CloseConnection();
        }
        public DataTable ShowAllPlayers(int ID)
        {
            string query = "SELECT p.Name, p.Age, p.Position, t.Name AS Team_Name FROM Player p JOIN Team t ON p.Team_ID = t.Team_ID JOIN Coach c ON c.Coach_ID = t.Coach_ID WHERE c.Coach_ID = " + ID;
            return dbMan.ExecuteReader(query);
        }
        public int GetID(string Username)
        {
            string query = "SELECT UserID FROM System_Users WHERE Username = '" + Username + "';";
            object result = dbMan.ExecuteScalar(query);
            if (result != null && result != DBNull.Value)
                return Convert.ToInt32(result);
            else
                return -1;
        }
        public DataTable ShowAllPlayersPerformances(int ID)
        {
            string query = "SELECT p.Name AS Player_Name, t.Name AS Team_Name, m.Date, m.Opponent, pp.Assists, pp.Goals, pp.Rating FROM Player_Performance pp JOIN Player p ON p.Player_ID = pp.Player_ID JOIN Team t ON t.Team_ID = p.Team_ID JOIN \"Match\" m ON m.Match_ID = pp.Match_ID JOIN Coach c ON c.Coach_ID = t.Coach_ID WHERE c.Coach_ID = " + ID;
            return dbMan.ExecuteReader(query);
        }
        public DataTable ShowAllPlayersAvailability(int ID)
        {
            string query = "SELECT p.Name AS Player_Name, t.Name AS Team_Name, p.Availability FROM Player p JOIN Team t ON t.Team_ID = p.Team_ID JOIN Coach c ON c.Coach_ID = t.Coach_ID WHERE c.Coach_ID = " + ID;
            return dbMan.ExecuteReader(query);
        }
        public DataTable GetAllBranchNames(int ID)
        {
            string query = "SELECT Name FROM Branch";
            return dbMan.ExecuteReader(query);
        }
        public DataTable GetAllTeamNames(int ID)
        {
            string query = "SELECT Name FROM Team WHERE Coach_ID = " + ID;
            return dbMan.ExecuteReader(query);
        }
        public int GetNumberOfMatches()
        {
            string query = "SELECT COUNT(*) FROM \"Match\"";
            object result = dbMan.ExecuteScalar(query);
            if (result != null && result != DBNull.Value)
                return Convert.ToInt32(result);
            else
                return 0;
        }
        public int ScheduleTrainingSession(int MatchID, string date, string hour, string minute, int BranchID, int teamID)
        {
            string query = "INSERT INTO dbo.\"Match\"(Match_ID, \"Date\", \"time\", Opponent, Branch_ID, Team_ID) VALUES (" + MatchID + ", '" + date + "', '" + hour + ":" + minute + ":00', NULL, " + BranchID + ", " + teamID + ")";
            return dbMan.ExecuteNonQuery(query);
        }
        public int GetBranchID(string BranchName)
        {
            string query = "SELECT Branch_ID FROM Branch WHERE Name = '" + BranchName + "'";
            object result = dbMan.ExecuteScalar(query);
            if (result != null && result != DBNull.Value)
                return Convert.ToInt32(result);
            else
                return -1;
        }
        public int GetTeamID(string TeamName)
        {
            string query = "SELECT Team_ID FROM Team WHERE Name = '" + TeamName + "'";
            object result = dbMan.ExecuteScalar(query);
            if (result != null && result != DBNull.Value)
                return Convert.ToInt32(result);
            else
                return -1;
        }
        public int ScheduleMatch(int MatchID, string date, string hour, string minute, string Opponent, int BranchID, int teamID)
        {
            string query = "INSERT INTO dbo.\"Match\"(Match_ID, \"Date\", \"time\", Opponent, Branch_ID, Team_ID) VALUES (" + MatchID + ", '" + date + "', '" + hour + ":" + minute + ":00', '" + Opponent + "', " + BranchID + ", " + teamID + ")";
            return dbMan.ExecuteNonQuery(query);
        }
    }
}
