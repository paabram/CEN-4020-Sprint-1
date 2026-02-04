using System;
using System.Windows.Forms;

namespace GameUI
{
    public partial class Form_GameMenu : Form
    {
        Form_GameBoard gameBoard;
        GameEngine gameEngine;
        GameSaver gameSaver;


        public Form_GameMenu()
        {
            InitializeComponent();
        }

        public Form_GameMenu(GameEngine targetEngine, GameSaver targetSaver)
        {
            InitializeComponent();
            this.gameEngine = targetEngine;
            this.gameSaver = targetSaver;
        }

        private void button_NewGame_Click(object sender, EventArgs e)
        {
            this.Hide();
            gameBoard.Show();
        }

        private void button_LoadGame_Click(object sender, EventArgs e)
        {

        }

        private void button_Exit_Click(object sender, EventArgs e)
        {

        }
    }
}
