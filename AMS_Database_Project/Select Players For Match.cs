using DBapplication;
using System;
using System.Data;
using System.Windows.Forms;

namespace AMS_Database_Project
{
    public partial class Select_Players_For_Match : Form
    {
        private DataTable availablePlayers;
        private DataTable selectedPlayers;
        public Select_Players_For_Match(int ID)
        {
            InitializeComponent();
            Controller controller = new Controller();
            DataTable matches = controller.ShowAllMatchesInfo(ID);
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            foreach (DataRow dr in matches.Rows)
            {
                comboBox1.Items.Add(dr["MatchInfo"].ToString());
                comboBox2.Items.Add(dr["Match_ID"].ToString());
            }
            selectedPlayers = new DataTable();
            if (comboBox2.Items.Count > 0)
            {
                selectedPlayers = controller.ShowAllSelectedPlayersForMatch(Convert.ToInt32(comboBox2.Items[0]));
            }
            else
            {
                selectedPlayers = new DataTable();
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.SelectedIndex = comboBox1.SelectedIndex;
            Controller controller = new Controller();
            availablePlayers = controller.ShowAllPlayersForMatch(Convert.ToInt32(comboBox2.SelectedItem));
            selectedPlayers = controller.ShowAllSelectedPlayersForMatch(Convert.ToInt32(comboBox2.SelectedItem));
            if (selectedPlayers == null || selectedPlayers.Columns.Count == 0)
            {
                if (availablePlayers != null)
                    selectedPlayers = availablePlayers.Clone();
                else
                    selectedPlayers = new DataTable();
            }
            dataGridView1.DataSource = availablePlayers;
            dataGridView2.DataSource = selectedPlayers;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                DataRowView rowView =(DataRowView)dataGridView1.CurrentRow.DataBoundItem;
                DataRow row = rowView.Row;
                Controller controller = new Controller();
                string name = row["Player_Name"].ToString();
                if (controller.AddPlayerToMatch(Convert.ToInt32(comboBox2.SelectedItem), name) == 0)
                {
                    MessageBox.Show("Failed to add " + name + " to the match.");
                    return;
                }
                selectedPlayers.ImportRow(row);
                availablePlayers.Rows.Remove(row);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentRow != null)
            {
                DataRowView rowView =(DataRowView)dataGridView2.CurrentRow.DataBoundItem;
                DataRow row = rowView.Row;
                Controller controller = new Controller();
                string name = row["Player_Name"].ToString();
                if (controller.RemovePlayerFromMatch(Convert.ToInt32(comboBox2.SelectedItem), name) == 0)
                {
                    MessageBox.Show("Failed to remove " + name + " from the match.");
                    return;
                }
                availablePlayers.ImportRow(row);
                selectedPlayers.Rows.Remove(row);
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}