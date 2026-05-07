using DBapplication;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMS_Database_Project
{
    public partial class Update_Player_Performance : Form
    {
        public Update_Player_Performance(int ID)
        {
            InitializeComponent();
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();
            comboBox5.Items.Clear();
            comboBox6.Items.Clear();
            comboBox7.Items.Clear();
            comboBox8.Items.Clear();
            Controller controller = new Controller();
            DataTable matches = controller.ShowAllMatchesInfo(ID);
            foreach (DataRow dr in matches.Rows)
            {
                comboBox1.Items.Add(dr["MatchInfo"].ToString());
                comboBox7.Items.Add(dr["Match_ID"].ToString());
            }
            for (int i = 0; i <= 10; i++)
            {
                comboBox6.Items.Add(i.ToString("D2"));
            }
            if (comboBox6.Items.Count > 0)
            {
                comboBox6.SelectedIndex = 0;
            }
            for (int i = 0; i <= 9; i++)
            {
                comboBox3.Items.Add(i.ToString("D2"));
            }
            if (comboBox3.Items.Count > 0)
            {
                comboBox3.SelectedIndex = 0;
            }
            for (int i = 0; i <= 20; i++)
            {
                comboBox4.Items.Add(i.ToString("D2"));
                comboBox5.Items.Add(i.ToString("D2"));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox7.SelectedIndex = comboBox1.SelectedIndex;
            comboBox2.Items.Clear();
            comboBox8.Items.Clear();
            comboBox2.Text = "";
            comboBox8.Text = "";
            Controller controller = new Controller();
            DataTable players = controller.ShowAllPlayersForMatchWithPerformance(Convert.ToInt32(comboBox7.Text));
            if (players == null || players.Rows.Count == 0)
            {
                return;
            }
            foreach (DataRow dr in players.Rows)
            {
                comboBox2.Items.Add(dr["Player_Name"].ToString());
                comboBox8.Items.Add(controller.GetPlayerID(dr["Player_Name"].ToString()).ToString());
            }
            comboBox2.SelectedIndex = 0;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex < 0)
                return;
            comboBox8.SelectedIndex = comboBox2.SelectedIndex;
            Controller controller = new Controller();
            DataTable performance = controller.GetPlayerPerformanceForMatch(Convert.ToInt32(comboBox7.Text), Convert.ToInt32(comboBox8.Text));
            if (performance.Rows.Count > 0)
            {
                DataRow dr = performance.Rows[0];
                comboBox4.Text = dr["Goals"].ToString();
                comboBox5.Text = dr["Assists"].ToString();
            }
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox6.SelectedIndex == 10)
            {
                comboBox3.SelectedIndex = 0;
                comboBox3.Enabled = false;
            }
            else
            {
                comboBox3.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label9.Visible = false;
            label10.Visible = false;
            if (comboBox1.Text != "" && comboBox2.Text != "" && comboBox3.Text != "" && comboBox4.Text != "" && comboBox5.Text != "" && comboBox6.Text != "")
            {
                Controller controller = new Controller();
                controller.UpdatePlayerPerformanceForMatch(Convert.ToInt32(comboBox7.Text), Convert.ToInt32(comboBox8.Text), Convert.ToInt32(comboBox4.Text), Convert.ToInt32(comboBox5.Text), Convert.ToInt32(comboBox6.Text), Convert.ToInt32(comboBox3.Text));
                label10.Visible = true;
            }
            else
            {
                label9.Visible = true;
            }
        }
    }
}
