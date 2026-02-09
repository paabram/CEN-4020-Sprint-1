using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace GameUI
{
    public partial class Form_GameMenu : Form
    {
        Form_GameBoard gameBoard;
        Form_GameBoardLvl2 gameBoardLvl2;
        GameEngine gameEngine;
        GameSaver gameSaver;

        public void SetGameBoard(Form_GameBoard GameBoard)
        {
            this.gameBoard = GameBoard;
        }

        public void SetGameBoardLvl2(Form_GameBoardLvl2 GameBoardLvl2)
        {
            this.gameBoardLvl2 = GameBoardLvl2;
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
                if(gameEngine.GetCurrentLevel() == 1)
                {
                    this.Hide();
                    gameBoard.Show();
                    gameBoard.refreshDisplay();
                } else
                {
                    gameEngine.setBoardSize(7);
                    this.Hide();
                    gameBoardLvl2.Show();
                    gameBoardLvl2.refreshDisplay(); 
                }

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
            if(gameEngine.GetCurrentLevel() == 1)
            {
                gameBoard.Show();
            } else
            {
                gameBoardLvl2.Show();
            }
        }

        private void button_LeaderBoard1_Click(object sender, EventArgs e)
        {
            string str = "\r\n";
            int fileNumber = gameSaver.getLatestFileNumber();
            GameState tempState;
            string filePath = "";
            string name;
            string date;
            string points;
            int numValidStates;
            List<GameState> validStates = new List<GameState>();

            str += "";
            if (fileNumber > 0)
            {
                for (int i = 1; i <= fileNumber; i++)
                {
                    //iterating through all currently saved game states
                    filePath = AppDomain.CurrentDomain.BaseDirectory + $"Saves\\Save{i}.txt";
                    tempState = gameSaver.Load(filePath)[0];
                    if (!(tempState.saveDateTime).Equals("") && tempState.currentLevel == 1)
                    {
                        //adding save states with a name into new array
                        validStates.Add(tempState);
                    }
                }

                if ((numValidStates = validStates.Count) > 0)
                {
                    //sort and display states from most to least points
                    validStates.Sort((GameState x, GameState y) => y.Points.CompareTo(x.Points));
                    int i = 1;
                    foreach (GameState state in validStates)
                    {
                        name = state.userName;
                        date = state.saveDateTime;
                        points = state.Points + "";
                        str += $"{i}. Name: {name}\r\nPoints: {points}\r\nDate & Time: {date}\r\n\r\n";
                        for (int r = 0; r < 5; r++)
                        {
                            for (int c = 0; c < 5; c++)
                            {
                                string cell = state.Board[r, c].Value.ToString();
                                str += cell;
                                if (c < 5 - 1) str += "\t";
                            }
                            str += "\r\n\r\n";
                        }
                        i++;
                    }
                    Form lboard = new Form()
                    {
                        StartPosition = FormStartPosition.CenterScreen,
                        Width = 500,
                        Height = 600,
                    };
                    TextBox results = new TextBox()
                    {
                        Multiline = true,
                        ReadOnly = true,
                        Text = str,
                        ScrollBars = ScrollBars.Vertical,
                        BorderStyle = BorderStyle.None,
                        AutoSize = false,
                        Size = new System.Drawing.Size(500, 500),
                        Anchor = (AnchorStyles.Top),
                        Left = 150,
                        TabStop = false,
                    };
                    lboard.Controls.Add(results);
                    lboard.ShowDialog();
                }
                else
                {
                    MessageBox.Show("No valid records to show.");
                    return;
                }
            }
            else
            {
                MessageBox.Show("No records to show.");
            }
        }

        private void button_LeaderBoard2_Click(object sender, EventArgs e)
        {
            string str = "\r\n";
            int fileNumber = gameSaver.getLatestFileNumber();
            GameState tempState;
            string filePath = "";
            string name;
            string date;
            string points;
            int numValidStates;
            List<GameState> validStates = new List<GameState>();

            str += "";
            if (fileNumber > 0)
            {
                for (int i = 1; i <= fileNumber; i++)
                {
                    //iterating through all currently saved game states
                    filePath = AppDomain.CurrentDomain.BaseDirectory + $"Saves\\Save{i}.txt";
                    tempState = gameSaver.Load(filePath)[0];
                    if (!(tempState.saveDateTime).Equals("") && tempState.currentLevel == 2)
                    {
                        //adding save states with a name into new array
                        validStates.Add(tempState);
                    }
                }

                if ((numValidStates = validStates.Count) > 0)
                {
                    //sort and display states from most to least points
                    validStates.Sort((GameState x, GameState y) => y.Points.CompareTo(x.Points));
                    int i = 1;
                    foreach (GameState state in validStates)
                    {
                        name = state.userName;
                        date = state.saveDateTime;
                        points = state.Points + "";
                        str += $"{i}. Name: {name}\r\nPoints: {points}\r\nDate & Time: {date}\r\n\r\n";
                        for (int r = 0; r < 7; r++)
                        {
                            for (int c = 0; c < 7; c++)
                            {
                                string cell = state.Board[r, c].Value.ToString();
                                str += cell;
                                if (c < 7 - 1) str += "\t";
                            }
                            str += "\r\n\r\n\r\n";
                        }
                        i++;
                    }
                    Form lboard = new Form()
                    {
                        StartPosition = FormStartPosition.CenterScreen,
                        Width = 600,
                        Height = 600,
                    };
                    TextBox results = new TextBox()
                    {
                        Multiline = true,
                        ReadOnly = true,
                        Text = str,
                        ScrollBars = ScrollBars.Vertical,
                        BorderStyle = BorderStyle.None,
                        AutoSize = false,
                        Size = new System.Drawing.Size(600, 500),
                        Anchor = (AnchorStyles.Top),
                        Left = 150,
                        TabStop = false,
                    };
                    lboard.Controls.Add(results);
                    lboard.ShowDialog();
                }
                else
                {
                    MessageBox.Show("No valid records to show.");
                    return;
                }
            }
            else
            {
                MessageBox.Show("No records to show.");
            }
        }
    }
}
