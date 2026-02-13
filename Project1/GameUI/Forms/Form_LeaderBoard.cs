using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameUI.Forms
{
    public partial class Form_LeaderBoard : Form
    {
        public event EventHandler ReturnToMenuRequested;
        public Form_LeaderBoard()
        {
            InitializeComponent();
        }

        private void button_ReturnToMenu_Click(object sender, EventArgs e)
        {
            ReturnToMenuRequested?.Invoke(this, EventArgs.Empty);
        }

        public void LoadLeaderBoard(List<GameState> leaderboardEntries)
        {
            // Clear existing entries
            dataGridView_LeaderBoardDisplay.DataSource = null;

            dataGridView_LeaderBoardDisplay.DataSource = leaderboardEntries.Where(e => e.currentLevel == 1)
                                                                           .Select(e => new 
                                                                           {
                                                                                Name = e.userName,
                                                                                Date = e.saveDateTime,
                                                                                e.Points
                                                                           }).ToList();
        }
    }
}
