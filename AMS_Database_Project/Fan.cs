using DBapplication;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMS_Database_Project
{
    public partial class Fan : Form
    {
        private int fanID;

        public Fan(int ID) {
        
            InitializeComponent();
            Controller controller = new Controller();
            fanID = ID;
        }
 
        private void Fan_Load(object sender, EventArgs e)
        {

        }
    }
}
