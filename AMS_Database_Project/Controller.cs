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
        public DataTable ShowAllPlayers()
        {
            string query = "SELECT player_name, player_position, player_team FROM Players";
            return dbMan.ExecuteReader(query);
        }

    }
}
