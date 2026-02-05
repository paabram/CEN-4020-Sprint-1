using System;
using System.Linq;
using System.Windows.Forms;
using System.Media;
using System.Threading.Tasks;

namespace GameUI
{
    public partial class Form_GameBoard : Form
    {
        Form_GameMenu gameMenu;
        GameEngine gameEngine;
        GameSaver gameSaver;

        bool gameInProgress = false;

        public void setGameInProgress(bool gameInProgress)
        { 
            this.gameInProgress = gameInProgress; 
        }

        public bool isGameInProgress() { return gameInProgress; }   

        public Form_GameBoard()
        {
            InitializeComponent();
        }

        public Form_GameBoard(GameEngine targetEngine, GameSaver targetSaver)
        {
            InitializeComponent();
            this.gameEngine = targetEngine;
            this.gameSaver = targetSaver;
        }

        public Form_GameMenu GetGameMenu() { return gameMenu; }
        public void SetGameMenu(Form_GameMenu targetMenu) { gameMenu = targetMenu; }

        public void clearDisplay()
        {
            for (int i = 0; i < gameEngine.getBoardSize(); i++)
            {
                for (int j = 0; j < gameEngine.getBoardSize(); j++)
                {
                    int row = i;
                    int col = j;
                    string btnName = $"button_{row + 1}_{col + 1}";
                    var btn = this.Controls.Find(btnName, true).FirstOrDefault() as Button;
                    btn.Text = "";
                    btn.BackColor = System.Drawing.SystemColors.Control;
                }
            }

            label_CurrentNumber.Text = "1";
            label_currentPoints.Text = "0";
        }
        public void refreshDisplay()
        {
            GameState currentState = new GameState(gameEngine.GetState());

            for (int i = 0; i < gameEngine.getBoardSize(); i++)
            {
                for (int j = 0; j < gameEngine.getBoardSize(); j++)
                {
                    int row = i;
                    int col = j;
                    string btnName = $"button_{row + 1}_{col + 1}";
                    var btn = this.Controls.Find(btnName, true).FirstOrDefault() as Button;
                    btn.Text = currentState.Board[row,col].ToString();
                    if (currentState.Board[row,col].ToString() == "")
                    {
                        btn.BackColor = System.Drawing.SystemColors.Control;
                    } else
                    {
                        btn.BackColor = System.Drawing.Color.Green;
                    }
                }
            }
            label_CurrentNumber.Text = currentState.currentNumber.ToString();
            label_currentPoints.Text = currentState.Points.ToString();
        }

        private async void tryPlaceValue(int row, int col)
        {
            if(gameEngine.Place(gameEngine.GetCurrentNumber(), row, col))
            {
                //play victory noise
                string btnName = $"button_{row+1}_{col+1}";
                int currentNumber = gameEngine.GetCurrentNumber();
                if (currentNumber > gameEngine.getBoardSize() * gameEngine.getBoardSize())
                {
                    var finalBtn = this.Controls.Find(btnName, true).FirstOrDefault() as Button;
                    finalBtn.Text = $"{currentNumber-1}";
                    finalBtn.BackColor = System.Drawing.Color.Green;
                    DialogResult dr = MessageBox.Show("Congratulations! You Won! Would you like to start a new game?", null, MessageBoxButtons.YesNo);
                    if(dr == DialogResult.Yes)
                    {
                        GameState newState = new GameState(gameEngine.getBoardSize());
                        gameEngine.SetState(newState);
                        gameEngine.history.Clear();
                        refreshDisplay();
                        return;
                    } else
                    {
                        Application.Exit();
                    }
                }
                var btn = this.Controls.Find(btnName, true).FirstOrDefault() as Button;
                btn.Text = $"{currentNumber}";
                refreshDisplay();
                return;
            } else
            {
                SystemSounds.Exclamation.Play();
                string btnName = $"button_{row + 1}_{col + 1}";
                var btn = this.Controls.Find(btnName, true).FirstOrDefault() as Button;
                var defaultColor = btn.BackColor;

                btn.BackColor = System.Drawing.Color.Red;

                await Task.Delay(500);

                btn.BackColor = defaultColor;

                return;
            }
        }

        private void Form_GameBoard_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        private void Form_GameBoard_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show(this, "Are you sure you want to exit?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void button_ReturnToMenu_Click(object sender, EventArgs e)
        {
            this.Hide();
            gameMenu.changeContinueVisibility(true);
            gameMenu.Show();
        }




        private void button_1_1_Click(object sender, EventArgs e)
        {
            tryPlaceValue(0, 0);
        }

        private void button_1_2_Click(object sender, EventArgs e)
        {
            tryPlaceValue(0, 1);

        }

        private void button_1_3_Click(object sender, EventArgs e)
        {
            tryPlaceValue(0, 2);

        }

        private void button_1_4_Click(object sender, EventArgs e)
        {
            tryPlaceValue(0, 3);

        }

        private void button_1_5_Click(object sender, EventArgs e)
        {
            tryPlaceValue(0, 4);

        }

        private void button_2_1_Click(object sender, EventArgs e)
        {
            tryPlaceValue(1, 0);
        }

        private void button_2_2_Click(object sender, EventArgs e)
        {
            tryPlaceValue(1, 1);
        }

        private void button_2_3_Click(object sender, EventArgs e)
        {
            tryPlaceValue(1, 2);
        }

        private void button_2_4_Click(object sender, EventArgs e)
        {
            tryPlaceValue(1, 3);
        }

        private void button_2_5_Click(object sender, EventArgs e)
        {
            tryPlaceValue(1, 4);
        }

        private void button_3_1_Click(object sender, EventArgs e)
        {
            tryPlaceValue(2, 0);
        }
        private void button_3_2_Click(object sender, EventArgs e)
        {
            tryPlaceValue(2, 1);
        }


        private void button_3_3_Click(object sender, EventArgs e)
        {
            tryPlaceValue(2, 2);
        }
        private void button_3_4_Click(object sender, EventArgs e)
        {
            tryPlaceValue(2, 3);
        }

        private void button_3_5_Click(object sender, EventArgs e)
        {
            tryPlaceValue(2, 4);
        }


        private void button_4_1_Click(object sender, EventArgs e)
        {
            tryPlaceValue(3, 0);
        }

        private void button_4_2_Click(object sender, EventArgs e)
        {
            tryPlaceValue(3, 1);
        }

        private void button_4_3_Click(object sender, EventArgs e)
        {
            tryPlaceValue(3, 2);
        }

        private void button_4_4_Click(object sender, EventArgs e)
        {
            tryPlaceValue(3, 3);
        }

        private void button_4_5_Click(object sender, EventArgs e)
        {
            tryPlaceValue(3, 4);
        }

        private void button_5_1_Click(object sender, EventArgs e)
        {
            tryPlaceValue(4, 0);
        }

        private void button_5_2_Click(object sender, EventArgs e)
        {
            tryPlaceValue(4, 1);
        }

        private void button_5_3_Click(object sender, EventArgs e)
        {
            tryPlaceValue(4, 2);
        }

        private void button_5_4_Click(object sender, EventArgs e)
        {
            tryPlaceValue(4, 3);
        }

        private void button_5_5_Click(object sender, EventArgs e)
        {
            tryPlaceValue(4, 4);
        }

        private void button_Undo_Click(object sender, EventArgs e)
        {
            if (gameEngine.history.Count <= 0)
            {
                return;
            }
            else if (gameEngine.history.Count == 1)
            {
                GameState targetState = gameEngine.history.Pop();
                gameEngine.SetState(targetState);
                clearDisplay();
                refreshDisplay();
            } else 
            {
                GameState targetState = gameEngine.history.Pop();
                gameEngine.SetState(targetState);
                clearDisplay();
                refreshDisplay();
            }
        }

        private void button_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button_Save_Click(object sender, EventArgs e)
        {
            int fileNumber = gameSaver.getLatestFileNumber();
            string filepath = AppDomain.CurrentDomain.BaseDirectory;

            if (fileNumber == 0)
            {
                string savepath = filepath + "/Saves/Save1.txt";
                gameSaver.Save(savepath, gameEngine.GetState());
            } else if(fileNumber > 25)
            {
                fileNumber = 1;
                string savepath = filepath + $"Saves\\Save{fileNumber + 1}.txt";
            } else 
            {
                string savepath = filepath + $"Saves/Save{fileNumber + 1}.txt";
                gameSaver.Save(savepath, gameEngine.GetState());
            }
        }

    }
}
