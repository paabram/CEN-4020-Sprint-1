using System;
using System.Linq;
using System.Windows.Forms;
using System.Media;
using System.Threading.Tasks;

namespace GameUI
{
    public partial class Form_GameBoardLvl2 : Form
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

        public Form_GameBoardLvl2()
        {
            InitializeComponent();
        }

        public Form_GameBoardLvl2(GameEngine targetEngine, GameSaver targetSaver)
        {
            InitializeComponent();
            this.gameEngine = targetEngine;
            this.gameSaver = targetSaver;
        }

        public Form_GameMenu GetGameMenu() { return gameMenu; }
        public void SetGameMenu(Form_GameMenu targetMenu) { gameMenu = targetMenu; }

        public void loadLevel2()
        {
            GameState currentState = new GameState(gameEngine.GetState());
            GameState newState = new GameState(7);

            for (int i = 1; i < 6; i++)
            {
                for (int j = 1; j < 6; j++)
                {
                    int row = i;
                    int col = j;
                    string btnName = $"button_{row+1}_{col+1}";
                    var btn = this.Controls.Find(btnName, true).FirstOrDefault() as Button;
                    btn.Text = currentState.Board[row - 1, col - 1].ToString();
                    btn.BackColor = System.Drawing.Color.Green;
                    newState.Board[row, col] = currentState.Board[row - 1, col - 1];
                }
            }
            label_CurrentNumber.Text = currentState.currentNumber.ToString();
            label_currentPoints.Text = currentState.Points.ToString();
            newState.Points = currentState.Points;
            gameEngine.setBoardSize(7);
            gameEngine.SetState(newState);
        }

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
            GameState currentState = gameEngine.GetState();

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    int row = i;
                    int col = j;

                    if (row == 0 || row == 6 || col == 0 || col ==6)
                    {
                        string txtBoxName = $"textBox_{row + 1}_{col + 1}";
                        var textBox = this.Controls.Find(txtBoxName, true).FirstOrDefault() as TextBox;
                        string btnName = $"button_{row + 1}_{col + 1}";
                        var btn = this.Controls.Find(btnName, true).FirstOrDefault() as Button;
                        textBox.Text = currentState.Board[row, col].ToString();
                    }
                }
            }
            label_CurrentNumber.Text = currentState.currentNumber.ToString();
            label_currentPoints.Text = currentState.Points.ToString();
        }

        private async void tryPlaceValue(int row, int col, string value)
        {
            string assetsFolder = AppDomain.CurrentDomain.BaseDirectory + "\\assets";
            SoundPlayer failureSound = new SoundPlayer(@assetsFolder + "\\failure.wav");
            SoundPlayer successSound = new SoundPlayer(@assetsFolder + "\\success.wav");
            SoundPlayer completionSound = new SoundPlayer(@assetsFolder + "\\completion.wav");
            int intValue;
            bool isNumber = int.TryParse(value, out intValue);
            bool isCurrentNumber = (intValue == gameEngine.GetCurrentNumber());
            if (isNumber && gameEngine.Place(intValue, row, col))
            {
                //play victory noise
                string txtBoxName = $"textBox_{row+1}_{col+1}";
                string btnName = $"button_{row + 1}_{col + 1}";
                int currentNumber = gameEngine.GetCurrentNumber();
                if (currentNumber > 25)
                {
                    completionSound.Play();
                    var finalTxtBox = this.Controls.Find(txtBoxName, true).FirstOrDefault() as TextBox;
                    var finalBtn = this.Controls.Find(btnName, true).FirstOrDefault() as Button;
                    finalTxtBox.Text = $"{currentNumber-1}";
                    finalBtn.BackColor = System.Drawing.Color.Green;
                    DialogResult dr = MessageBox.Show("Congratulations! You Won! Would you like to start a new game?", "", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
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
                successSound.Play();
                var txtBox = this.Controls.Find(txtBoxName, true).FirstOrDefault() as TextBox;
                txtBox.Text = $"{currentNumber}";
                refreshDisplay();
                return;
            } else
            {
                string btnName = $"button_{row + 1}_{col + 1}";
                string txtBoxName = $"textBox_{row + 1}_{col + 1}";
                var btn = this.Controls.Find(btnName, true).FirstOrDefault() as Button;
                var txtBox = this.Controls.Find(txtBoxName, true).FirstOrDefault() as TextBox;
                var defaultColor = btn.BackColor;

                failureSound.Play();
                btn.BackColor = System.Drawing.Color.Red;

                await Task.Delay(500);

                btn.BackColor = defaultColor;
                txtBox.Text = "";
                txtBox.Enabled = true;

                return;
            }
        }

        private void Form_GameBoardLvl2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        private void Form_GameBoardLvl2_FormClosing(object sender, FormClosingEventArgs e)
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

        private void button_Save_Click(object sender, EventArgs e)
        {

        }

        private void button_Undo_Click(object sender, EventArgs e)
        {

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

        private void textBox_1_6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                tryPlaceValue(0, 5, textBox_1_6.Text);
                e.SuppressKeyPress = true;
                textBox_1_6.Enabled = false;
            }
        }

        private void textBox_1_7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                tryPlaceValue(0, 6, textBox_1_7.Text);
                e.SuppressKeyPress = true;
                textBox_1_7.Enabled = false;
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

        private void textBox_2_7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                tryPlaceValue(1, 6, textBox_2_7.Text);
                e.SuppressKeyPress = true;
                textBox_2_7.Enabled = false;
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

        private void textBox_3_7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                tryPlaceValue(2, 6, textBox_3_7.Text);
                e.SuppressKeyPress = true;
                textBox_3_7.Enabled = false;
            }
        }

        private void textBox_4_1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                tryPlaceValue(3, 0, textBox_4_1.Text);
                e.SuppressKeyPress = true;
                textBox_4_1.Enabled = false;
            }
        }

        private void textBox_4_7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                tryPlaceValue(3, 6, textBox_4_7.Text);
                e.SuppressKeyPress = true;
                textBox_4_7.Enabled = false;
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

        private void textBox_5_7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                tryPlaceValue(4, 6, textBox_5_7.Text);
                e.SuppressKeyPress = true;
                textBox_5_7.Enabled = false;
            }
        }

        private void textBox_6_1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                tryPlaceValue(5, 0, textBox_6_1.Text);
                e.SuppressKeyPress = true;
                textBox_6_1.Enabled = false;
            }
        }

        private void textBox_6_7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                tryPlaceValue(5, 6, textBox_6_7.Text);
                e.SuppressKeyPress = true;
                textBox_6_7.Enabled = false;
            }
        }

        private void textBox_7_1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                tryPlaceValue(6, 0, textBox_7_1.Text);
                e.SuppressKeyPress = true;
                textBox_7_1.Enabled = false;
            }
        }

        private void textBox_7_2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                tryPlaceValue(6, 1, textBox_7_2.Text);
                e.SuppressKeyPress = true;
                textBox_7_2.Enabled = false;
            }
        }

        private void textBox_7_3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                tryPlaceValue(6, 2, textBox_7_3.Text);
                e.SuppressKeyPress = true;
                textBox_7_3.Enabled = false;
            }
        }

        private void textBox_7_4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                tryPlaceValue(6, 3, textBox_7_4.Text);
                e.SuppressKeyPress = true;
                textBox_7_4.Enabled = false;
            }
        }

        private void textBox_7_5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                tryPlaceValue(6, 4, textBox_7_5.Text);
                e.SuppressKeyPress = true;
                textBox_7_5.Enabled = false;
            }
        }

        private void textBox_7_6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                tryPlaceValue(6, 5, textBox_7_6.Text);
                e.SuppressKeyPress = true;
                textBox_7_6.Enabled = false;
            }
        }

        private void textBox_7_7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                tryPlaceValue(6, 6, textBox_7_7.Text);
                e.SuppressKeyPress = true;
                textBox_7_7.Enabled = false;
            }
        }


    }
}
