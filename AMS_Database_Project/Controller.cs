using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
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
        public int GetCoachID(string Username)
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
            string query = "SELECT p.Name, pp.Date, pp.Performance_Metric FROM Player_Performance pp JOIN Player p ON pp.Player_ID = p.Player_ID JOIN Team t ON p.Team_ID = t.Team_ID JOIN Coach c ON c.Coach_ID = t.Coach_ID WHERE c.Coach_ID = " + ID;
            return dbMan.ExecuteReader(query);
        }
    }
}
