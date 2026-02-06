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
                    string txtBoxName = $"textBox_{row + 1}_{col + 1}";
                    var btn = this.Controls.Find(btnName, true).FirstOrDefault() as Button;
                    var txtBox = this.Controls.Find(txtBoxName, true).FirstOrDefault() as TextBox;
                    txtBox.Text = "";
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
                    string txtBoxName = $"textBox_{row + 1}_{col + 1}";
                    string btnName = $"button_{row + 1}_{col + 1}";
                    var txtBox = this.Controls.Find(txtBoxName, true).FirstOrDefault() as TextBox;
                    var btn = this.Controls.Find(btnName, true).FirstOrDefault() as Button;
                    txtBox.Text = currentState.Board[row,col].ToString();
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

        private async void tryPlaceValue(int row, int col, string value)
        {
            int intValue;
            bool isNumber = int.TryParse(value, out intValue);
            bool isCurrentNumber = intValue == gameEngine.GetCurrentNumber();
            if (isNumber && isCurrentNumber && gameEngine.Place(intValue, row, col))
            {
                //play victory noise
                string txtBoxName = $"textBox_{row+1}_{col+1}";
                string btnName = $"button_{row + 1}_{col + 1}";
                int currentNumber = gameEngine.GetCurrentNumber();
                if (currentNumber > gameEngine.getBoardSize() * gameEngine.getBoardSize())
                {
                    var finalTxtBox = this.Controls.Find(txtBoxName, true).FirstOrDefault() as TextBox;
                    var finalBtn = this.Controls.Find(btnName, true).FirstOrDefault() as Button;
                    finalTxtBox.Text = $"{currentNumber-1}";
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
                var txtBox = this.Controls.Find(txtBoxName, true).FirstOrDefault() as TextBox;
                txtBox.Text = $"{currentNumber}";
                refreshDisplay();
                return;
            } else
            {
                SystemSounds.Exclamation.Play();
                string btnName = $"button_{row + 1}_{col + 1}";
                string txtBoxName = $"textBox_{row + 1}_{col + 1}";
                var btn = this.Controls.Find(btnName, true).FirstOrDefault() as Button;
                var txtBox = this.Controls.Find(txtBoxName, true).FirstOrDefault() as TextBox;
                var defaultColor = btn.BackColor;

                btn.BackColor = System.Drawing.Color.Red;

                await Task.Delay(500);

                btn.BackColor = defaultColor;
                txtBox.Text = "";
                txtBox.Enabled = true;

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

        }

        private void button_1_2_Click(object sender, EventArgs e)
        {

        }

        private void button_1_3_Click(object sender, EventArgs e)
        {

        }

        private void button_1_4_Click(object sender, EventArgs e)
        {

        }

        private void button_1_5_Click(object sender, EventArgs e)
        {

        }

        private void button_2_1_Click(object sender, EventArgs e)
        {

        }

        private void button_2_2_Click(object sender, EventArgs e)
        {

        }

        private void button_2_3_Click(object sender, EventArgs e)
        {

        }

        private void button_2_4_Click(object sender, EventArgs e)
        {

        }

        private void button_2_5_Click(object sender, EventArgs e)
        {

        }

        private void button_3_1_Click(object sender, EventArgs e)
        {

        }
        private void button_3_2_Click(object sender, EventArgs e)
        {

        }


        private void button_3_3_Click(object sender, EventArgs e)
        {

        }
        private void button_3_4_Click(object sender, EventArgs e)
        {

        }

        private void button_3_5_Click(object sender, EventArgs e)
        {

        }


        private void button_4_1_Click(object sender, EventArgs e)
        {
 
        }

        private void button_4_2_Click(object sender, EventArgs e)
        {

        }

        private void button_4_3_Click(object sender, EventArgs e)
        {
 
        }

        private void button_4_4_Click(object sender, EventArgs e)
        {

        }

        private void button_4_5_Click(object sender, EventArgs e)
        {

        }

        private void button_5_1_Click(object sender, EventArgs e)
        {

        }

        private void button_5_2_Click(object sender, EventArgs e)
        {

        }

        private void button_5_3_Click(object sender, EventArgs e)
        {

        }

        private void button_5_4_Click(object sender, EventArgs e)
        {

        }

        private void button_5_5_Click(object sender, EventArgs e)
        {

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
                foreach (GameState state in gameEngine.history)
                {
                    gameSaver.Save(savepath, state);
                }
            } else if(fileNumber > 25)
            {
                fileNumber = 1;
                string savepath = filepath + $"Saves\\Save{fileNumber + 1}.txt";
                gameSaver.Save(savepath, gameEngine.GetState());
                foreach (GameState state in gameEngine.history)
                {
                    gameSaver.Save(savepath, state);
                }
            } else 
            {
                string savepath = filepath + $"Saves/Save{fileNumber + 1}.txt";
                gameSaver.Save(savepath, gameEngine.GetState());
                foreach (GameState state in gameEngine.history)
                {
                    gameSaver.Save(savepath, state);
                }
            }
        }

        private void textBox_1_1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                tryPlaceValue(0, 0, textBox_1_1.Text);
                e.SuppressKeyPress = true;
                textBox_1_1.Enabled = false;
            }
        }

        private void textBox_1_2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                tryPlaceValue(0, 1, textBox_1_2.Text);
                e.SuppressKeyPress = true;
                textBox_1_2.Enabled = false;
            }
        }

        private void textBox_1_3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                tryPlaceValue(0, 2, textBox_1_3.Text);
                e.SuppressKeyPress = true;
                textBox_1_3.Enabled = false;
            }
        }

        private void textBox_1_4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                tryPlaceValue(0, 3, textBox_1_4.Text);
                e.SuppressKeyPress = true;
                textBox_1_4.Enabled = false;
            }
        }

        private void textBox_1_5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                tryPlaceValue(0, 4, textBox_1_5.Text);
                e.SuppressKeyPress = true;
                textBox_1_5.Enabled = false;
            }
        }

        private void textBox_2_1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                tryPlaceValue(1, 0, textBox_2_1.Text);
                e.SuppressKeyPress = true;
                textBox_2_1.Enabled = false;
            }
        }

        private void textBox_2_2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                tryPlaceValue(1, 1, textBox_2_2.Text);
                e.SuppressKeyPress = true;
                textBox_2_2.Enabled = false;
            }
        }

        private void textBox_2_3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                tryPlaceValue(1, 2, textBox_2_3.Text);
                e.SuppressKeyPress = true;
                textBox_2_3.Enabled = false;
            }
        }

        private void textBox_2_4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                tryPlaceValue(1, 3, textBox_2_4.Text);
                e.SuppressKeyPress = true;
                textBox_2_4.Enabled = false;
            }
        }

        private void textBox_2_5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                tryPlaceValue(1, 4, textBox_2_5.Text);
                e.SuppressKeyPress = true;
                textBox_2_5.Enabled = false;
            }
        }

        private void textBox_3_1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                tryPlaceValue(2, 0, textBox_3_1.Text);
                e.SuppressKeyPress = true;
                textBox_3_1.Enabled = false;
            }
        }

        private void textBox_3_2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                tryPlaceValue(2, 1, textBox_3_2.Text);
                e.SuppressKeyPress = true;
                textBox_3_2.Enabled = false;
            }
        }

        private void textBox_3_3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                tryPlaceValue(2, 2, textBox_3_3.Text);
                e.SuppressKeyPress = true;
                textBox_3_3.Enabled = false;
            }
        }

        private void textBox_3_4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                tryPlaceValue(2, 3, textBox_3_4.Text);
                e.SuppressKeyPress = true;
                textBox_3_4.Enabled = false;
            }
        }

        private void textBox_3_5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                tryPlaceValue(2, 4, textBox_3_5.Text);
                e.SuppressKeyPress = true;
                textBox_3_5.Enabled = false;
            }
        }

        private void textBox_4_1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                tryPlaceValue(3, 0, textBox_4_1.Text);
                e.SuppressKeyPress = true;
                textBox_1_1.Enabled = false;
            }
        }

        private void textBox_4_2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                tryPlaceValue(3, 1, textBox_4_2.Text);
                e.SuppressKeyPress = true;
                textBox_4_2.Enabled = false;
            }
        }

        private void textBox_4_3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                tryPlaceValue(3, 2, textBox_4_3.Text);
                e.SuppressKeyPress = true;
                textBox_4_3.Enabled = false;
            }
        }

        private void textBox_4_4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                tryPlaceValue(3, 3, textBox_4_4.Text);
                e.SuppressKeyPress = true;
                textBox_4_4.Enabled = false;
            }
        }

        private void textBox_4_5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                tryPlaceValue(3, 4, textBox_4_5.Text);
                e.SuppressKeyPress = true;
                textBox_4_5.Enabled = false;
            }
        }

        private void textBox_5_1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                tryPlaceValue(4, 0, textBox_5_1.Text);
                e.SuppressKeyPress = true;
                textBox_5_1.Enabled = false;
            }
        }

        private void textBox_5_2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                tryPlaceValue(4, 1, textBox_5_2.Text);
                e.SuppressKeyPress = true;
                textBox_5_2.Enabled = false;
            }
        }

        private void textBox_5_3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                tryPlaceValue(4, 2, textBox_5_3.Text);
                e.SuppressKeyPress = true;
                textBox_5_3.Enabled = false;
            }
        }

        private void textBox_5_4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                tryPlaceValue(4, 3, textBox_5_4.Text);
                e.SuppressKeyPress = true;
                textBox_5_4.Enabled = false;
            }
        }

        private void textBox_5_5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                tryPlaceValue(4, 4, textBox_5_5.Text);
                e.SuppressKeyPress = true;
                textBox_5_5.Enabled = false;
            }
        }
    }
}
