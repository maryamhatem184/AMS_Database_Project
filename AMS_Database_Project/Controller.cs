using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
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
            string query = "SELECT p.Name AS Player_Name, t.Name AS Team_Name, m.Date, m.Opponent, pp.Assists, pp.Goals, pp.Rating FROM Performance pp JOIN Player p ON p.Player_ID = pp.Player_ID JOIN Team t ON t.Team_ID = p.Team_ID JOIN \"Match\" m ON m.Match_ID = pp.Match_ID JOIN Coach c ON c.Coach_ID = t.Coach_ID WHERE c.Coach_ID = " + ID;
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
        public int ScheduleTrainingSession(string date, string hour, string minute, int BranchID, int teamID)
        {
            string query = "INSERT INTO dbo.\"Match\"(\"Date\", \"time\", Opponent, Branch_ID, Team_ID) VALUES ('" + date + "', '" + hour + ":" + minute + ":00', NULL, " + BranchID + ", " + teamID + ")";
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
        public int ScheduleMatch(string date, string hour, string minute, string Opponent, int BranchID, int teamID)
        {
            string query = "INSERT INTO dbo.\"Match\"(\"Date\", \"time\", Opponent, Branch_ID, Team_ID) VALUES ('" + date + "', '" + hour + ":" + minute + ":00', '" + Opponent + "', " + BranchID + ", " + teamID + ")";
            return dbMan.ExecuteNonQuery(query);
        }
        public DataTable ShowAllMatchesInfo(int ID)
        {
            string query = "SELECT m.Match_ID, CONCAT(m.Opponent, ' - ', CAST(m.Date AS VARCHAR), ' @ ', CAST(m.time AS VARCHAR(5))) AS MatchInfo FROM dbo.\"Match\" m JOIN dbo.Team t ON m.Team_ID = t.Team_ID JOIN dbo.Coach c ON t.Coach_ID = c.Coach_ID WHERE m.Opponent IS NOT NULL AND c.Coach_ID = " + ID;
            return dbMan.ExecuteReader(query);
        }
        public DataTable ShowAllPlayersForMatch(int MatchID)
        {
            string query = "SELECT p.Name AS Player_Name, p.Position FROM Player p JOIN Team t ON p.Team_ID = t.Team_ID JOIN \"Match\" m ON m.Team_ID = t.Team_ID WHERE m.Match_ID = " + MatchID + " AND p.Name NOT IN (SELECT p.Name AS Player_Name FROM Performance pp JOIN Player ON pp.Player_ID = p.Player_ID WHERE Match_ID = " + MatchID + ")";
            return dbMan.ExecuteReader(query);
        }
        public int AddPlayerToMatch(int MatchID, string PlayerName)
        {
            string query = "INSERT INTO Performance (Player_ID, Match_ID) VALUES ((SELECT Player_ID FROM Player WHERE Name = '" + PlayerName + "'), " + MatchID + ")";
            return dbMan.ExecuteNonQuery(query);
        }
        public DataTable ShowAllSelectedPlayersForMatch(int MatchID)
        {
            string query = "SELECT p.Name AS Player_Name, p.Position FROM Player p JOIN Performance pp ON p.Player_ID = pp.Player_ID WHERE pp.Match_ID = " + MatchID;
            return dbMan.ExecuteReader(query);
        }
        public int RemovePlayerFromMatch(int MatchID, string PlayerName)
        {
            string query = "DELETE FROM Performance WHERE Match_ID = " + MatchID + " AND Player_ID = (SELECT Player_ID FROM Player WHERE Name = '" + PlayerName + "')";
            return dbMan.ExecuteNonQuery(query);
        }
        public DataTable GetPlayerPerformanceForMatch(int MatchID, int ID)
        {
            string query = "SELECT p.Name AS Player_Name, pp.Assists, pp.Goals, pp.Rating FROM Performance pp JOIN Player p ON p.Player_ID = pp.Player_ID WHERE pp.Match_ID = " + MatchID + " AND p.Player_ID = " + ID;
            return dbMan.ExecuteReader(query);
        }
        public int UpdatePlayerPerformanceForMatch(int MatchID, int ID, int Assists, int Goals, int RatingNumber, int RatingDecimal)
        {
            string query = "UPDATE Performance SET Assists = " + Assists + ", Goals = " + Goals + ", Rating = " + RatingNumber + "." + RatingDecimal + " WHERE Match_ID = " + MatchID + " AND Player_ID = " + ID;
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
            string query = "SELECT p.Name AS Player_Name, pp.Assists, pp.Goals, pp.Rating FROM Player p JOIN Performance pp ON p.Player_ID = pp.Player_ID WHERE pp.Match_ID = " + MatchID;
            return dbMan.ExecuteReader(query);
        }
        public DataTable ShowAllPlayersPerformances()
        {
            string query = "SELECT p.Name AS Player_Name, t.Name AS Team_Name, m.Date, pp.Assists, pp.Goals, pp.Rating FROM Player p JOIN Team t ON p.Team_ID = t.Team_ID JOIN Performance pp ON p.Player_ID = pp.Player_ID JOIN \"Match\" m ON pp.Match_ID = m.Match_ID";
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
        public int RemoveBranch(string BranchName)
        {
            string query = "DELETE FROM Branch WHERE Name = '" + BranchName + "'";
            return dbMan.ExecuteNonQuery(query);
        }
        public int UpdateBranch(string BranchName, int NewCapacity)
        {
            string query = "UPDATE Branch SET Capacity = " + NewCapacity + " WHERE Name = '" + BranchName + "'";
            return dbMan.ExecuteNonQuery(query);
        }
        public int CreateBranch(string BranchName, string BranchLocation, int BranchCapacity)
        {
            string query = "INSERT INTO Branch (Name, Location, Capacity) VALUES ('" + BranchName + "', '" + BranchLocation + "', " + BranchCapacity + ")";
            return dbMan.ExecuteNonQuery(query);
        }
        public DataTable GetAllSportsSectionNames()
        {
            string query = "SELECT Name FROM Sport";
            return dbMan.ExecuteReader(query);
        }
        public int AddSportsSection(string SportName)
        {
            string query = "INSERT INTO Sport (Name) VALUES ('" + SportName + "')";
            return dbMan.ExecuteNonQuery(query);
        }
        public int RemoveSportsSection(string SportName)
        {
            string query = "DELETE FROM Sport WHERE Name = '" + SportName + "'";
            return dbMan.ExecuteNonQuery(query);
        }
        public DataTable GetAllFansNames()
        {
            string query = "SELECT Name FROM Fan";
            return dbMan.ExecuteReader(query);
        }
        public int DeactivateMembership(int FanID)
        {
            string query = "DELETE FROM Membership WHERE Fan_ID = " + FanID;
            return dbMan.ExecuteNonQuery(query);
        }
        public int RegisterMembership(int FanID, string MembershipType, string StartDate, string EndDate)
        {
            string query = "INSERT INTO Membership (Fan_ID, Type, Start_Date, Expiry_Date) VALUES (" + FanID + ", '" + MembershipType + "', '" + StartDate + "', '" + EndDate + "')";
            return dbMan.ExecuteNonQuery(query);
        }
        public int GetFanIDByName(string FanName)
        {
            string query = "SELECT Fan_ID FROM Fan WHERE Name = '" + FanName + "'";
            object result = dbMan.ExecuteScalar(query);
            if (result != null && result != DBNull.Value)
                return Convert.ToInt32(result);
            else
                return -1;
        }
        public DataTable GetAllMembersID()
        {
            string query = "SELECT Fan_ID FROM Membership";
            return dbMan.ExecuteReader(query);
        }
        public string GetFanNameByID(string FanID)
        {
            string query = "SELECT Name FROM Fan WHERE Fan_ID = " + FanID;
            object result = dbMan.ExecuteScalar(query);
            if (result != null && result != DBNull.Value)
                return result.ToString();
            else
                return null;
        }
        public DataTable GetAllMedicalStaffNames()
        {
            string query = "SELECT Name FROM Medical_Staff";
            return dbMan.ExecuteReader(query);
        }
        public int RegisterMedicalStaff(string Name, string Role)
        {
            string query = "INSERT INTO System_Users (Username, Password, UserRole) VALUES ('dr_" + Name + "', 'ahly2026', 'Medical'); INSERT INTO Medical_Staff (Medical_ID, Name, Role) VALUES (SCOPE_IDENTITY(), 'Dr. " + Name + "', '" + Role + "');";
            return dbMan.ExecuteNonQuery(query);
        }
        public int DeactivateMedicalStaff(string Name)
        {
            string query = "DELETE FROM System_Users WHERE UserID = (SELECT Medical_ID FROM Medical_Staff WHERE Name = '" + Name + "'); DELETE FROM Medical_Staff WHERE Name = '" + Name + "';";
            return dbMan.ExecuteNonQuery(query);
        }
        public DataTable GetAllCoachNames()
        {
            string query = "SELECT Name FROM Coach";
            return dbMan.ExecuteReader(query);
        }
        public int RegisterCoach(string Name, string Specialization)
        {
            string query = "INSERT INTO System_Users (Username, Password, UserRole) VALUES ('coach_" + Name + "', 'ahly2026', 'Coach'); INSERT INTO Coach (Coach_ID, Name, Specialization) VALUES (SCOPE_IDENTITY(), '" + Name + "', '" + Specialization + "');";
            return dbMan.ExecuteNonQuery(query);
        }
        public int DeactivateCoach(string Name)
        {
            string query = "DELETE FROM System_Users WHERE UserID = (SELECT Coach_ID FROM Coach WHERE Name = '" + Name + "'); DELETE FROM Coach WHERE Name = '" + Name + "';";
            return dbMan.ExecuteNonQuery(query);
        }
        public DataTable GetAllTeamsNames()
        {
            string query = "SELECT Name FROM Team";
            return dbMan.ExecuteReader(query);
        }
        public int RegisterTeam(string Name, string Level, string SportName)
        {
            string query = "INSERT INTO Team (Name, Level, Sport_ID) VALUES ('Al Ahly " + Name + "', '" + Level + "', (SELECT Sport_ID FROM Sport WHERE Name = '" + SportName + "'))";
            return dbMan.ExecuteNonQuery(query);
        }
        public int DeactivateTeam(string Name)
        {
            string query = "DELETE FROM Team WHERE Name = '" + Name + "'";
            return dbMan.ExecuteNonQuery(query);
        }
        public DataTable GetAllManagerNames()
        {
            string query = "SELECT Name FROM Manager";
            return dbMan.ExecuteReader(query);
        }
        public int RegisterManager(string Name, string Role)
        {
            string query = "INSERT INTO System_Users (Username, Password, UserRole) VALUES ('manager_" + Name + "', 'ahly2026', 'Manager'); INSERT INTO Manager (Manager_ID, Name, Role) VALUES (SCOPE_IDENTITY(), '" + Name + "', '" + Role + "');";
            return dbMan.ExecuteNonQuery(query);
        }
        public int DeactivateManager(string Name)
        {
            string query = "DELETE FROM System_Users WHERE UserID = (SELECT Manager_ID FROM Manager WHERE Name = '" + Name + "'); DELETE FROM Manager WHERE Name = '" + Name + "';";
            return dbMan.ExecuteNonQuery(query);
        }
        public int GetBranchIDByName(string BranchName)
        {
            string query = "SELECT Branch_ID FROM Branch WHERE Name = '" + BranchName + "'";
            object result = dbMan.ExecuteScalar(query);
            if (result != null && result != DBNull.Value)
                return Convert.ToInt32(result);
            else
                return -1;
        }
        public int GetManagerIDByName(string ManagerName)
        {
            string query = "SELECT Manager_ID FROM Manager WHERE Name = '" + ManagerName + "'";
            object result = dbMan.ExecuteScalar(query);
            if (result != null && result != DBNull.Value)
                return Convert.ToInt32(result);
            else
                return -1;
        }
        public int AssignManagerToBranch(int ManagerID, int BranchID)
        {
            string query = "UPDATE Manager SET Branch_ID = " + BranchID + " WHERE Manager_ID = " + ManagerID;
            return dbMan.ExecuteNonQuery(query);
        }
        public DataTable GetTopScorers()
        {
            return dbMan.ExecuteReader("EXEC GetTopScorers;");
        }
        public DataTable GetMostBookedMatches()
        {
            return dbMan.ExecuteReader("EXEC GetMostBookedMatches;");
        }
        public DataTable GetTopSellingProducts()
        {
            string query = @"SELECT M.Name, SUM(C.Quantity) AS Total_Items_Sold 
                             FROM Merchandise_Item M 
                             JOIN contain C ON M.Merchandise_ID = C.Merchandise_ID 
                             GROUP BY M.Name 
                             ORDER BY Total_Items_Sold DESC;";
            return dbMan.ExecuteReader(query);
        }
        public DataTable GetManagerialStats()
        {
            return dbMan.ExecuteReader("EXEC GetManagerialStats;");
        }

        public DataTable GetProducts()
        {
            string query = "SELECT Name, Price, Stock FROM Merchandise_Item;";
            return dbMan.ExecuteReader(query);
        }



        public int AddProduct(string name, decimal price, int stock)
        {

            string idQuery = "SELECT ISNULL(MAX(Merchandise_ID), 0) + 1 FROM Merchandise_Item;";
            int newId = Convert.ToInt32(dbMan.ExecuteScalar(idQuery));

            string query = $"INSERT INTO Merchandise_Item (Merchandise_ID, Name, Price, Stock) VALUES ({newId}, '{name}', {price}, {stock});";
            return dbMan.ExecuteNonQuery(query);
        }


        public int UpdateStock(int merchandiseId, int newStock)
        {
            string query = $"UPDATE Merchandise_Item SET Stock = {newStock} WHERE Merchandise_ID = {merchandiseId};";
            return dbMan.ExecuteNonQuery(query);
        }

        public int DeleteProduct(int merchandiseId)
        {
            string query = $"DELETE FROM Merchandise_Item WHERE Merchandise_ID = {merchandiseId};";
            return dbMan.ExecuteNonQuery(query);
        }



        public DataTable GetFanNames()
        {
            string query = "SELECT Fan_ID, Name FROM Fan;";
            return dbMan.ExecuteReader(query);
        }

        public DataTable GetProductNames()
        {
            string query = "SELECT Merchandise_ID, Name FROM Merchandise_Item WHERE Stock > 0;";
            return dbMan.ExecuteReader(query);
        }

        public object GetProductPrice(string productName)
        {
            string query =
                $"SELECT Price FROM Merchandise_Item WHERE Name = '{productName}';";

            return dbMan.ExecuteScalar(query);
        }

        public int CreateTransaction(int fanID, decimal total)
        {
            string idQuery = "SELECT ISNULL(MAX(Transaction_ID), 0) + 1 FROM Transaction_Header;";
            int newTxnId = Convert.ToInt32(dbMan.ExecuteScalar(idQuery));

            string query = $"INSERT INTO Transaction_Header (Transaction_ID, Fan_ID, Date, Total_Amount) VALUES ({newTxnId}, {fanID}, GETDATE(), {total});";
            dbMan.ExecuteNonQuery(query);

            return newTxnId;
        }

        public void AddToTransaction(int transactionID, int merchandiseID, int quantity)
        {
            string query = $"INSERT INTO contain (Transaction_ID, Merchandise_ID, Quantity) VALUES ({transactionID}, {merchandiseID}, {quantity});";
            dbMan.ExecuteNonQuery(query);
        }

        public void ReduceStock(int merchandiseID, int quantity)
        {
            string query = $"UPDATE Merchandise_Item SET Stock = Stock - {quantity} WHERE Merchandise_ID = {merchandiseID};";
            dbMan.ExecuteNonQuery(query);
        }
        public int GetCoachIDByName(string CoachName)
        {
            string query = "SELECT Coach_ID FROM Coach WHERE Name = '" + CoachName + "'";
            object result = dbMan.ExecuteScalar(query);
            if (result != null && result != DBNull.Value)
                return Convert.ToInt32(result);
            else
                return -1;
        }
        public int GetTeamIDByName(string TeamName)
        {
            string query = "SELECT Team_ID FROM Team WHERE Name = '" + TeamName + "'";
            object result = dbMan.ExecuteScalar(query);
            if (result != null && result != DBNull.Value)
                return Convert.ToInt32(result);
            else
                return -1;
        }
        public int AssignCoachToTeam(int CoachID, int TeamID)
        {
            string query = "UPDATE Team SET Coach_ID = " + CoachID + " WHERE Team_ID = " + TeamID;
            return dbMan.ExecuteNonQuery(query);
        }
        public int AddNewInventoryItem(string itemName, decimal price, int stock)
        {
            string query = "INSERT INTO Merchandise_Item (Name, Stock, Price) VALUES ('" + itemName + "', " + stock + ", " + price + ");";
            return dbMan.ExecuteNonQuery(query);
        }
        public DataTable GetAllPlayersNames()
        {
            string query = "SELECT Name FROM Player";
            return dbMan.ExecuteReader(query);
        }
        public int SignPlayer(string PlayerName, int age, string Position)
        {
            string query = "INSERT INTO Player (Name, Age, Position) VALUES ('" + PlayerName + "', " + age + ", '" + Position + "')";
            return dbMan.ExecuteNonQuery(query);
        }
        public int SackPlayer(string PlayerName)
        {
            string query = "DELETE FROM Player WHERE Name = '" + PlayerName + "'";
            return dbMan.ExecuteNonQuery(query);
        }
        public int AssignPlayerToTeam(int PlayerID, int TeamID)
        {
            string query = "UPDATE Player SET Team_ID = " + TeamID + " WHERE Player_ID = " + PlayerID;
            return dbMan.ExecuteNonQuery(query);
        }
    }
}