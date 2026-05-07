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
        public DataTable ShowAllMatchesInfo(int ID)
        {
            string query = "SELECT m.Match_ID, CONCAT(m.Opponent, ' - ', CAST(m.Date AS VARCHAR), ' @ ', CAST(m.time AS VARCHAR(5))) AS MatchInfo FROM dbo.\"Match\" m JOIN dbo.Team t ON m.Team_ID = t.Team_ID JOIN dbo.Coach c ON t.Coach_ID = c.Coach_ID WHERE m.Opponent IS NOT NULL AND c.Coach_ID = " + ID;
            return dbMan.ExecuteReader(query);
        }
        public DataTable ShowAllPlayersForMatch(int MatchID)
        {
            string query = "SELECT p.Name AS Player_Name, p.Position FROM Player p JOIN Team t ON p.Team_ID = t.Team_ID JOIN \"Match\" m ON m.Team_ID = t.Team_ID WHERE m.Match_ID = " + MatchID + " AND p.Name NOT IN (SELECT p.Name AS Player_Name FROM Player_Performance pp JOIN Player ON pp.Player_ID = p.Player_ID WHERE Match_ID = " + MatchID + ")";
            return dbMan.ExecuteReader(query);
        }
        public int AddPlayerToMatch(int MatchID, string PlayerName)
        {
            string query = "INSERT INTO Player_Performance (Performance_ID, Player_ID, Match_ID) VALUES (((SELECT Count(*) FROM Player_Performance) + 1),(SELECT Player_ID FROM Player WHERE Name = '" + PlayerName + "'), " + MatchID + ")";
            return dbMan.ExecuteNonQuery(query);
        }
        public DataTable ShowAllSelectedPlayersForMatch(int MatchID)
        {
            string query = "SELECT p.Name AS Player_Name, p.Position FROM Player p JOIN Player_Performance pp ON p.Player_ID = pp.Player_ID WHERE pp.Match_ID = " + MatchID;
            return dbMan.ExecuteReader(query);
        }
        public int RemovePlayerFromMatch(int MatchID, string PlayerName)
        {
            string query = "DELETE FROM Player_Performance WHERE Match_ID = " + MatchID + " AND Player_ID = (SELECT Player_ID FROM Player WHERE Name = '" + PlayerName + "')";
            return dbMan.ExecuteNonQuery(query);
        }
        public DataTable GetPlayerPerformanceForMatch(int MatchID, int ID)
        {
            string query = "SELECT p.Name AS Player_Name, pp.Assists, pp.Goals, pp.Rating FROM Player_Performance pp JOIN Player p ON p.Player_ID = pp.Player_ID WHERE pp.Match_ID = " + MatchID + " AND p.Player_ID = " + ID;
            return dbMan.ExecuteReader(query);
        }
        public int UpdatePlayerPerformanceForMatch(int MatchID, int ID, int Assists, int Goals, int RatingNumber, int RatingDecimal)
        {
            string query = "UPDATE Player_Performance SET Assists = " + Assists + ", Goals = " + Goals + ", Rating = " + RatingNumber + "." + RatingDecimal + " WHERE Match_ID = " + MatchID + " AND Player_ID = " + ID;
            return dbMan.ExecuteNonQuery(query);
        }
        public int GetPlayerID(string PlayerName)
        {
            string query = "SELECT Player_ID FROM Player WHERE Name = '" + PlayerName + "'";
            object result = dbMan.ExecuteScalar(query);
            if (result != null && result != DBNull.Value)
                return Convert.ToInt32(result);
            else
                return -1;
        }
        public DataTable ShowAllPlayersForMatchWithPerformance(int MatchID)
        {
            string query = "SELECT p.Name AS Player_Name, pp.Assists, pp.Goals, pp.Rating FROM Player p JOIN Player_Performance pp ON p.Player_ID = pp.Player_ID WHERE pp.Match_ID = " + MatchID;
            return dbMan.ExecuteReader(query);
        }
        public DataTable ShowAllPlayersPerformances()
        {
            string query = "SELECT p.Name AS Player_Name, t.Name AS Team_Name, m.Date, pp.Assists, pp.Goals, pp.Rating FROM Player p JOIN Team t ON p.Team_ID = t.Team_ID JOIN Player_Performance pp ON p.Player_ID = pp.Player_ID JOIN \"Match\" m ON pp.Match_ID = m.Match_ID WHERE pp.Rating IS NOT NULL";
            return dbMan.ExecuteReader(query);
        }
        public DataTable GetAllMerchandiseNames()
        {
            string query = "SELECT Name FROM Merchandise_Item";
            return dbMan.ExecuteReader(query);
        }
        public DataTable GetAllMerchandiseInfoByName(string MerchandiseName)
        {
            string query = "SELECT Name, Stock, Price FROM Merchandise_Item WHERE Name = '" + MerchandiseName + "'";
            return dbMan.ExecuteReader(query);
        }
        public int RestockMerchandiseItem(string MerchandiseName)
        {
            string query = "UPDATE Merchandise_Item SET Stock = Stock + 100 WHERE Name = '" + MerchandiseName + "'";
            return dbMan.ExecuteNonQuery(query);
        }
        public int UpdateMerchandiseItemPrice(string MerchandiseName, decimal NewPrice)
        {
            string query = "UPDATE Merchandise_Item SET Price = " + NewPrice + " WHERE Name = '" + MerchandiseName + "'";
            return dbMan.ExecuteNonQuery(query);
        }
        public DataTable ShowAllMatchSchedules()
        {
            string query = "SELECT m.Match_ID, CONCAT(m.Opponent, ' - ', CAST(m.Date AS VARCHAR), ' @ ', CAST(m.time AS VARCHAR(5))) AS MatchInfo, b.Name AS Branch_Name, t.Name AS Team_Name FROM dbo.\"Match\" m JOIN dbo.Branch b ON m.Branch_ID = b.Branch_ID JOIN dbo.Team t ON m.Team_ID = t.Team_ID";
            return dbMan.ExecuteReader(query);
        }
    }
}
