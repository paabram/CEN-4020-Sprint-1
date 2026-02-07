using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GameUI
{
    public partial class Form_GameMenu : Form
    {
        Form_GameBoard gameBoard;
        GameEngine gameEngine;
        GameSaver gameSaver;

        public void SetGameBoard(Form_GameBoard GameBoard)
        {
            this.gameBoard = GameBoard;
        }

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
            gameBoard.refreshDisplay();
        }

        private void button_LoadGame_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            string filePath = AppDomain.CurrentDomain.BaseDirectory + "Saves\\";
            openFileDialog.InitialDirectory = filePath;
            openFileDialog.Title = "Select Save File";
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                List<GameState> loadStates = gameSaver.Load(openFileDialog.FileName);
                for (int i = loadStates.Count - 1; i >= 0; i--)
                {
                    if (i == 0)
                    {
                        gameEngine.SetState(loadStates[i]);
                    }
                    else
                    {
                        gameEngine.history.Push(loadStates[i]);
                    }
                }

                this.Hide();
                gameBoard.Show();
                gameBoard.refreshDisplay();
            } 
        }

        private void button_Exit_Click(object sender, EventArgs e)
        {

        }

        public void changeContinueVisibility(bool visible)
        {
            button_Continue.Visible = visible;
        }

        private void button_Continue_Click(object sender, EventArgs e)
        {
            this.Hide();
            gameBoard.Show();
        }
    }
}
