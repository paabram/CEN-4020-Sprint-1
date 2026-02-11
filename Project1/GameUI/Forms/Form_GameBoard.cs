using System;
using System.Linq;
using System.Windows.Forms;
using System.Media;
using System.Threading.Tasks;
using System.Drawing;
using System.Net.Configuration;

namespace GameUI
{
    public partial class Form_GameBoard : Form
    {
        public event EventHandler ReturnRequested;
        public event EventHandler<VPEventArgs> ValuePlaced;
        public event EventHandler<VEEventArgs> ValueError;
        public event EventHandler SaveRequested;
        public event EventHandler UndoRequested;


        public Form_GameBoard()
        {
            InitializeComponent();
        }




        public void RefreshDisplay(GameState State)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    int row = i;
                    int col = j;
                    string txtBoxName = $"textBox_{row + 1}_{col + 1}";
                    string btnName = $"button_{row + 1}_{col + 1}";
                    var txtBox = this.Controls.Find(txtBoxName, true).FirstOrDefault() as TextBox;
                    var btn = this.Controls.Find(btnName, true).FirstOrDefault() as Button;
                    txtBox.Text = State.Board[row,col].ToString();
                    if (State.Board[row,col].ToString() == "")
                    {
                        btn.BackColor = System.Drawing.SystemColors.Control;
                        txtBox.Enabled = true;
                    } else
                    {
                        btn.BackColor = System.Drawing.Color.Green;
                    }
                }
            }
            label_CurrentNumber.Text = State.currentNumber.ToString();
            label_currentPoints.Text = State.Points.ToString();
        }

        private async void tryPlaceValue(int row, int col, string value)
        {
            /*
            string assetsFolder = AppDomain.CurrentDomain.BaseDirectory + "\\assets";
            SoundPlayer failureSound = new SoundPlayer(@assetsFolder + "\\failure.wav");
            SoundPlayer successSound = new SoundPlayer(@assetsFolder + "\\success.wav");
            SoundPlayer completionSound = new SoundPlayer(@assetsFolder + "\\completion.wav");
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
                    completionSound.Play();
                    var finalTxtBox = this.Controls.Find(txtBoxName, true).FirstOrDefault() as TextBox;
                    var finalBtn = this.Controls.Find(btnName, true).FirstOrDefault() as Button;
                    finalTxtBox.Text = $"{currentNumber-1}";
                    finalBtn.BackColor = System.Drawing.Color.Green;

                    //get name and date, and autosave after level completion
                    string username = Prompt.ShowDialog("Enter your name:", "Checkpoint!");
                    string currDateTime = "" + DateTime.Now;
                    gameEngine.SetNameDate(username, currDateTime);
                    int fileNumber = gameSaver.getLatestFileNumber();
                    string filepath = AppDomain.CurrentDomain.BaseDirectory;

                    

                    DialogResult dr = MessageBox.Show("Congratulations! You Won! Would you like to move on to level 2?", "", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                    if(dr == DialogResult.Yes)
                    {
                        gameBoardLvl2.loadLevel2();
                        gameBoardLvl2.Show();
                        this.Hide();
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
            */
            return;
        }

        private void Form_GameBoard_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        private void Form_GameBoard_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*
            if (gameEngine.GetCurrentLevel() != 1)
            {

            } else
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
            */
        }

        private void button_ReturnToMenu_Click(object sender, EventArgs e)
        { 
            ReturnRequested?.Invoke(this, EventArgs.Empty);
        }

        private void button_Undo_Click(object sender, EventArgs e)
        {
            UndoRequested?.Invoke(this, EventArgs.Empty);
        }
        private void button_Clear_Click(object sender, EventArgs e)
        {
            /*
            while (gameEngine.history.Count > 0)
            {
                GameState targetState = gameEngine.history.Pop();
                gameEngine.SetState(targetState);
                clearDisplay();
                refreshDisplay();
            }
            */
        }

        private void MoveLevel(int level)
        {
            if(level == 2)
            {
                Form_GameBoardLvl2 GameBoardLvl2 = new Form_GameBoardLvl2();
                GameBoardLvl2.Dock = DockStyle.Fill;
                GameBoardLvl2.TopLevel = false;
                MainForm.MainPanel.Controls.Clear();
                MainForm.MainPanel.Controls.Add(GameBoardLvl2);
                GameBoardLvl2.Show();
            }
        }

        private void button_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        
        private void button_Save_Click(object sender, EventArgs e)
        {
            SaveRequested?.Invoke(this, EventArgs.Empty);
        }



        private void textBox_1_1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                int value;
                bool isNumber = int.TryParse(textBox_1_1.Text, out value);
                if (!isNumber)
                {
                    ValueError?.Invoke(this, new VEEventArgs(0, 0));
                    return;
                }

                ValuePlaced?.Invoke(this, new VPEventArgs(0, 0, value));
                //tryPlaceValue(0, 0, textBox_1_1.Text);
                e.SuppressKeyPress = true;
                textBox_1_1.Enabled = false;
            } 
            else if (e.KeyCode == Keys.Right)
            {
                var txtBox = this.Controls.Find("textBox_1_2", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                var txtBox = this.Controls.Find("textBox_2_1", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
        }

        private void textBox_1_2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                int value;
                bool isNumber = int.TryParse(textBox_1_2.Text, out value);
                if (!isNumber)
                {
                    ValueError?.Invoke(this, new VEEventArgs(0, 1));
                    return;
                }

                ValuePlaced?.Invoke(this, new VPEventArgs(0, 1, value));
                //tryPlaceValue(0, 0, textBox_1_1.Text);
                e.SuppressKeyPress = true;
                textBox_1_2.Enabled = false;
            }
            else if (e.KeyCode == Keys.Right)
            {
                var txtBox = this.Controls.Find("textBox_1_3", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Left)
            {
                var txtBox = this.Controls.Find("textBox_1_1", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                var txtBox = this.Controls.Find("textBox_2_2", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
        }

        private void textBox_1_3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                int value;
                bool isNumber = int.TryParse(textBox_1_3.Text, out value);
                if (!isNumber)
                {
                    ValueError?.Invoke(this, new VEEventArgs(0, 2));
                    return;
                }

                ValuePlaced?.Invoke(this, new VPEventArgs(0, 2, value));
                //tryPlaceValue(0, 0, textBox_1_1.Text);
                e.SuppressKeyPress = true;
                textBox_1_3.Enabled = false;
            } 
            else if (e.KeyCode == Keys.Right)
            {
                var txtBox = this.Controls.Find("textBox_1_4", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Left)
            {
                var txtBox = this.Controls.Find("textBox_1_2", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                var txtBox = this.Controls.Find("textBox_2_3", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
        }

        private void textBox_1_4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                int value;
                bool isNumber = int.TryParse(textBox_1_4.Text, out value);
                if (!isNumber)
                {
                    ValueError?.Invoke(this, new VEEventArgs(0, 3));
                    return;
                }

                ValuePlaced?.Invoke(this, new VPEventArgs(0, 3, value));
                //tryPlaceValue(0, 0, textBox_1_1.Text);
                e.SuppressKeyPress = true;
                textBox_1_4.Enabled = false;
            }
            else if (e.KeyCode == Keys.Right)
            {
                var txtBox = this.Controls.Find("textBox_1_5", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Left)
            {
                var txtBox = this.Controls.Find("textBox_1_3", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                var txtBox = this.Controls.Find("textBox_2_4", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
        }

        private void textBox_1_5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                int value;
                bool isNumber = int.TryParse(textBox_1_5.Text, out value);
                if (!isNumber)
                {
                    ValueError?.Invoke(this, new VEEventArgs(0, 4));
                    return;
                }

                ValuePlaced?.Invoke(this, new VPEventArgs(0, 4, value));
                //tryPlaceValue(0, 0, textBox_1_1.Text);
                e.SuppressKeyPress = true;
                textBox_1_5.Enabled = false;
            }
            else if (e.KeyCode == Keys.Left)
            {
                var txtBox = this.Controls.Find("textBox_1_4", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                var txtBox = this.Controls.Find("textBox_2_5", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
        }

        private void textBox_2_1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                int value;
                bool isNumber = int.TryParse(textBox_2_1.Text, out value);
                if (!isNumber)
                {
                    ValueError?.Invoke(this, new VEEventArgs(1, 0));
                    return;
                }

                ValuePlaced?.Invoke(this, new VPEventArgs(1, 0, value));
                //tryPlaceValue(0, 0, textBox_1_1.Text);
                e.SuppressKeyPress = true;
                textBox_2_1.Enabled = false;
            }
            else if (e.KeyCode == Keys.Right)
            {
                var txtBox = this.Controls.Find("textBox_2_1", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                var txtBox = this.Controls.Find("textBox_1_1", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                var txtBox = this.Controls.Find("textBox_3_1", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
        }

        private void textBox_2_2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                int value;
                bool isNumber = int.TryParse(textBox_2_2.Text, out value);
                if (!isNumber)
                {
                    ValueError?.Invoke(this, new VEEventArgs(1, 1));
                    return;
                }

                ValuePlaced?.Invoke(this, new VPEventArgs(1, 1, value));
                //tryPlaceValue(0, 0, textBox_1_1.Text);
                e.SuppressKeyPress = true;
                textBox_2_2.Enabled = false;
            }
            else if (e.KeyCode == Keys.Right)
            {
                var txtBox = this.Controls.Find("textBox_2_3", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Left)
            {
                var txtBox = this.Controls.Find("textBox_2_1", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                var txtBox = this.Controls.Find("textBox_1_2", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                var txtBox = this.Controls.Find("textBox_3_2", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
        }

        private void textBox_2_3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                int value;
                bool isNumber = int.TryParse(textBox_2_3.Text, out value);
                if (!isNumber)
                {
                    ValueError?.Invoke(this, new VEEventArgs(1, 2));
                    return;
                }

                ValuePlaced?.Invoke(this, new VPEventArgs(1, 2, value));
                //tryPlaceValue(0, 0, textBox_1_1.Text);
                e.SuppressKeyPress = true;
                textBox_2_3.Enabled = false;
            }
            else if (e.KeyCode == Keys.Right)
            {
                var txtBox = this.Controls.Find("textBox_2_4", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Left)
            {
                var txtBox = this.Controls.Find("textBox_2_2", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                var txtBox = this.Controls.Find("textBox_1_3", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                var txtBox = this.Controls.Find("textBox_3_3", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
        }

        private void textBox_2_4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                int value;
                bool isNumber = int.TryParse(textBox_2_4.Text, out value);
                if (!isNumber)
                {
                    ValueError?.Invoke(this, new VEEventArgs(1, 3));
                    return;
                }

                ValuePlaced?.Invoke(this, new VPEventArgs(1, 3, value));
                //tryPlaceValue(0, 0, textBox_1_1.Text);
                e.SuppressKeyPress = true;
                textBox_2_4.Enabled = false;
            }
            else if (e.KeyCode == Keys.Right)
            {
                var txtBox = this.Controls.Find("textBox_2_5", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Left)
            {
                var txtBox = this.Controls.Find("textBox_2_3", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                var txtBox = this.Controls.Find("textBox_1_4", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                var txtBox = this.Controls.Find("textBox_3_4", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
        }

        private void textBox_2_5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                int value;
                bool isNumber = int.TryParse(textBox_2_5.Text, out value);
                if (!isNumber)
                {
                    ValueError?.Invoke(this, new VEEventArgs(1, 4));
                    return;
                }

                ValuePlaced?.Invoke(this, new VPEventArgs(1, 4, value));
                //tryPlaceValue(0, 0, textBox_1_1.Text);
                e.SuppressKeyPress = true;
                textBox_2_5.Enabled = false;
            }
            else if (e.KeyCode == Keys.Left)
            {
                var txtBox = this.Controls.Find("textBox_2_4", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                var txtBox = this.Controls.Find("textBox_1_5", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                var txtBox = this.Controls.Find("textBox_3_5", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
        }

        private void textBox_3_1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                int value;
                bool isNumber = int.TryParse(textBox_3_1.Text, out value);
                if (!isNumber)
                {
                    ValueError?.Invoke(this, new VEEventArgs(2, 0));
                    return;
                }

                ValuePlaced?.Invoke(this, new VPEventArgs(2, 0, value));
                //tryPlaceValue(0, 0, textBox_1_1.Text);
                e.SuppressKeyPress = true;
                textBox_3_1.Enabled = false;
            }
            else if (e.KeyCode == Keys.Right)
            {
                var txtBox = this.Controls.Find("textBox_3_2", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                var txtBox = this.Controls.Find("textBox_2_1", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                var txtBox = this.Controls.Find("textBox_4_1", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }

        }

        private void textBox_3_2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                int value;
                bool isNumber = int.TryParse(textBox_3_2.Text, out value);
                if (!isNumber)
                {
                    ValueError?.Invoke(this, new VEEventArgs(2, 1));
                    return;
                }

                ValuePlaced?.Invoke(this, new VPEventArgs(2, 1, value));
                //tryPlaceValue(0, 0, textBox_1_1.Text);
                e.SuppressKeyPress = true;
                textBox_3_2.Enabled = false;
            }
            else if (e.KeyCode == Keys.Right)
            {
                var txtBox = this.Controls.Find("textBox_3_3", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Left)
            {
                var txtBox = this.Controls.Find("textBox_3_1", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                var txtBox = this.Controls.Find("textBox_2_2", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                var txtBox = this.Controls.Find("textBox_4_2", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
        }

        private void textBox_3_3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                int value;
                bool isNumber = int.TryParse(textBox_3_3.Text, out value);
                if (!isNumber)
                {
                    ValueError?.Invoke(this, new VEEventArgs(2, 2));
                    return;
                }

                ValuePlaced?.Invoke(this, new VPEventArgs(2, 2, value));
                //tryPlaceValue(0, 0, textBox_1_1.Text);
                e.SuppressKeyPress = true;
                textBox_3_3.Enabled = false;
            }
            else if (e.KeyCode == Keys.Right)
            {
                var txtBox = this.Controls.Find("textBox_3_4", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Left)
            {
                var txtBox = this.Controls.Find("textBox_3_2", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                var txtBox = this.Controls.Find("textBox_2_3", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                var txtBox = this.Controls.Find("textBox_4_3", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
        }

        private void textBox_3_4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                int value;
                bool isNumber = int.TryParse(textBox_3_4.Text, out value);
                if (!isNumber)
                {
                    ValueError?.Invoke(this, new VEEventArgs(2, 3));
                    return;
                }

                ValuePlaced?.Invoke(this, new VPEventArgs(2, 3, value));
                //tryPlaceValue(0, 0, textBox_1_1.Text);
                e.SuppressKeyPress = true;
                textBox_3_4.Enabled = false;
            }
            else if (e.KeyCode == Keys.Right)
            {
                var txtBox = this.Controls.Find("textBox_3_5", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Left)
            {
                var txtBox = this.Controls.Find("textBox_3_3", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                var txtBox = this.Controls.Find("textBox_2_4", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                var txtBox = this.Controls.Find("textBox_4_4", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
        }

        private void textBox_3_5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                int value;
                bool isNumber = int.TryParse(textBox_3_5.Text, out value);
                if (!isNumber)
                {
                    ValueError?.Invoke(this, new VEEventArgs(2, 4));
                    return;
                }

                ValuePlaced?.Invoke(this, new VPEventArgs(2, 4, value));
                //tryPlaceValue(0, 0, textBox_1_1.Text);
                e.SuppressKeyPress = true;
                textBox_3_5.Enabled = false;
            }
            else if (e.KeyCode == Keys.Left)
            {
                var txtBox = this.Controls.Find("textBox_3_4", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                var txtBox = this.Controls.Find("textBox_2_5", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                var txtBox = this.Controls.Find("textBox_4_5", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
        }

        private void textBox_4_1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                int value;
                bool isNumber = int.TryParse(textBox_4_1.Text, out value);
                if (!isNumber)
                {
                    ValueError?.Invoke(this, new VEEventArgs(3, 0));
                    return;
                }

                ValuePlaced?.Invoke(this, new VPEventArgs(3, 0, value));
                //tryPlaceValue(0, 0, textBox_1_1.Text);
                e.SuppressKeyPress = true;
                textBox_4_1.Enabled = false;
            }
            else if (e.KeyCode == Keys.Right)
            {
                var txtBox = this.Controls.Find("textBox_4_2", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                var txtBox = this.Controls.Find("textBox_3_1", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                var txtBox = this.Controls.Find("textBox_5_1", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
        }

        private void textBox_4_2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                int value;
                bool isNumber = int.TryParse(textBox_4_2.Text, out value);
                if (!isNumber)
                {
                    ValueError?.Invoke(this, new VEEventArgs(3, 1));
                    return;
                }

                ValuePlaced?.Invoke(this, new VPEventArgs(3, 1, value));
                //tryPlaceValue(0, 0, textBox_1_1.Text);
                e.SuppressKeyPress = true;
                textBox_4_2.Enabled = false;
            }
            else if (e.KeyCode == Keys.Right)
            {
                var txtBox = this.Controls.Find("textBox_4_3", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Left)
            {
                var txtBox = this.Controls.Find("textBox_4_1", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                var txtBox = this.Controls.Find("textBox_3_2", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                var txtBox = this.Controls.Find("textBox_5_2", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
        }

        private void textBox_4_3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                int value;
                bool isNumber = int.TryParse(textBox_4_3.Text, out value);
                if (!isNumber)
                {
                    ValueError?.Invoke(this, new VEEventArgs(3, 2));
                    return;
                }

                ValuePlaced?.Invoke(this, new VPEventArgs(3, 2, value));
                //tryPlaceValue(0, 0, textBox_1_1.Text);
                e.SuppressKeyPress = true;
                textBox_4_3.Enabled = false;
            }
            else if (e.KeyCode == Keys.Right)
            {
                var txtBox = this.Controls.Find("textBox_4_4", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Left)
            {
                var txtBox = this.Controls.Find("textBox_4_2", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                var txtBox = this.Controls.Find("textBox_3_3", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                var txtBox = this.Controls.Find("textBox_5_3", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
        }

        private void textBox_4_4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                int value;
                bool isNumber = int.TryParse(textBox_4_4.Text, out value);
                if (!isNumber)
                {
                    ValueError?.Invoke(this, new VEEventArgs(3, 3));
                    return;
                }

                ValuePlaced?.Invoke(this, new VPEventArgs(3, 3, value));
                //tryPlaceValue(0, 0, textBox_1_1.Text);
                e.SuppressKeyPress = true;
                textBox_4_4.Enabled = false;
            }
            else if (e.KeyCode == Keys.Right)
            {
                var txtBox = this.Controls.Find("textBox_4_5", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Left)
            {
                var txtBox = this.Controls.Find("textBox_4_3", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                var txtBox = this.Controls.Find("textBox_3_4", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                var txtBox = this.Controls.Find("textBox_5_4", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
        }

        private void textBox_4_5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                int value;
                bool isNumber = int.TryParse(textBox_4_5.Text, out value);
                if (!isNumber)
                {
                    ValueError?.Invoke(this, new VEEventArgs(3, 4));
                    return;
                }

                ValuePlaced?.Invoke(this, new VPEventArgs(3, 4, value));
                //tryPlaceValue(0, 0, textBox_1_1.Text);
                e.SuppressKeyPress = true;
                textBox_4_5.Enabled = false;
            }
            else if (e.KeyCode == Keys.Left)
            {
                var txtBox = this.Controls.Find("textBox_4_4", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                var txtBox = this.Controls.Find("textBox_3_5", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                var txtBox = this.Controls.Find("textBox_5_5", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
        }

        private void textBox_5_1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                int value;
                bool isNumber = int.TryParse(textBox_5_1.Text, out value);
                if (!isNumber)
                {
                    ValueError?.Invoke(this, new VEEventArgs(4, 0));
                    return;
                }

                ValuePlaced?.Invoke(this, new VPEventArgs(4, 0, value));
                //tryPlaceValue(0, 0, textBox_1_1.Text);
                e.SuppressKeyPress = true;
                textBox_5_1.Enabled = false;
            }
            else if (e.KeyCode == Keys.Right)
            {
                var txtBox = this.Controls.Find("textBox_5_2", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                var txtBox = this.Controls.Find("textBox_4_1", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
        }

        private void textBox_5_2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                int value;
                bool isNumber = int.TryParse(textBox_5_2.Text, out value);
                if (!isNumber)
                {
                    ValueError?.Invoke(this, new VEEventArgs(4, 1));
                    return;
                }

                ValuePlaced?.Invoke(this, new VPEventArgs(4, 1, value));
                //tryPlaceValue(0, 0, textBox_1_1.Text);
                e.SuppressKeyPress = true;
                textBox_5_2.Enabled = false;
            }
            else if (e.KeyCode == Keys.Right)
            {
                var txtBox = this.Controls.Find("textBox_5_3", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Left)
            {
                var txtBox = this.Controls.Find("textBox_5_1", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                var txtBox = this.Controls.Find("textBox_4_2", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
        }

        private void textBox_5_3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                int value;
                bool isNumber = int.TryParse(textBox_5_3.Text, out value);
                if (!isNumber)
                {
                    ValueError?.Invoke(this, new VEEventArgs(4, 2));
                    return;
                }

                ValuePlaced?.Invoke(this, new VPEventArgs(4, 2, value));
                //tryPlaceValue(0, 0, textBox_1_1.Text);
                e.SuppressKeyPress = true;
                textBox_5_3.Enabled = false;
            }
            else if (e.KeyCode == Keys.Right)
            {
                var txtBox = this.Controls.Find("textBox_5_4", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Left)
            {
                var txtBox = this.Controls.Find("textBox_5_2", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                var txtBox = this.Controls.Find("textBox_4_3", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
        }

        private void textBox_5_4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                int value;
                bool isNumber = int.TryParse(textBox_5_4.Text, out value);
                if (!isNumber)
                {
                    ValueError?.Invoke(this, new VEEventArgs(4, 3));
                    return;
                }

                ValuePlaced?.Invoke(this, new VPEventArgs(4, 3, value));
                //tryPlaceValue(0, 0, textBox_1_1.Text);
                e.SuppressKeyPress = true;
                textBox_5_4.Enabled = false;
            }
            else if (e.KeyCode == Keys.Right)
            {
                var txtBox = this.Controls.Find("textBox_5_5", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Left)
            {
                var txtBox = this.Controls.Find("textBox_5_3", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                var txtBox = this.Controls.Find("textBox_4_4", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
        }

        private void textBox_5_5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                int value;
                bool isNumber = int.TryParse(textBox_5_5.Text, out value);
                if (!isNumber)
                {
                    ValueError?.Invoke(this, new VEEventArgs(4, 4));
                    return;
                }

                ValuePlaced?.Invoke(this, new VPEventArgs(4, 4, value));
                //tryPlaceValue(0, 0, textBox_1_1.Text);
                e.SuppressKeyPress = true;
                textBox_5_5.Enabled = false;
            }
            else if (e.KeyCode == Keys.Left)
            {
                var txtBox = this.Controls.Find("textBox_5_4", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                var txtBox = this.Controls.Find("textBox_4_5", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
        }

        private void button_1_1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                var txtBox = this.Controls.Find("textBox_1_2", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                var txtBox = this.Controls.Find("textBox_2_1", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
        }

        private void button_1_2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                var txtBox = this.Controls.Find("textBox_1_3", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Left)
            {
                var txtBox = this.Controls.Find("textBox_1_1", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                var txtBox = this.Controls.Find("textBox_2_2", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
        }

        private void button_1_3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                var txtBox = this.Controls.Find("textBox_1_4", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Left)
            {
                var txtBox = this.Controls.Find("textBox_1_2", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                var txtBox = this.Controls.Find("textBox_2_3", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
        }

        private void button_1_4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                var txtBox = this.Controls.Find("textBox_1_5", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Left)
            {
                var txtBox = this.Controls.Find("textBox_1_3", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                var txtBox = this.Controls.Find("textBox_2_4", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
        }

        private void button_1_5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                var txtBox = this.Controls.Find("textBox_1_4", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                var txtBox = this.Controls.Find("textBox_2_5", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
        }

        private void button_2_1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                var txtBox = this.Controls.Find("textBox_2_1", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                var txtBox = this.Controls.Find("textBox_1_1", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                var txtBox = this.Controls.Find("textBox_3_1", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
        }
        private void button_2_2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                var txtBox = this.Controls.Find("textBox_2_3", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Left)
            {
                var txtBox = this.Controls.Find("textBox_2_1", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                var txtBox = this.Controls.Find("textBox_1_2", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                var txtBox = this.Controls.Find("textBox_3_2", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
        }
        private void button_2_3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                var txtBox = this.Controls.Find("textBox_2_4", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Left)
            {
                var txtBox = this.Controls.Find("textBox_2_2", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                var txtBox = this.Controls.Find("textBox_1_3", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                var txtBox = this.Controls.Find("textBox_3_3", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
        }
        private void button_2_4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                var txtBox = this.Controls.Find("textBox_2_5", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Left)
            {
                var txtBox = this.Controls.Find("textBox_2_3", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                var txtBox = this.Controls.Find("textBox_1_4", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                var txtBox = this.Controls.Find("textBox_3_4", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
        }
        private void button_2_5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                var txtBox = this.Controls.Find("textBox_2_4", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                var txtBox = this.Controls.Find("textBox_1_5", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                var txtBox = this.Controls.Find("textBox_3_5", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
        }

        private void button_3_1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                var txtBox = this.Controls.Find("textBox_3_2", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                var txtBox = this.Controls.Find("textBox_2_1", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                var txtBox = this.Controls.Find("textBox_4_1", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
        }
        private void button_3_2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                var txtBox = this.Controls.Find("textBox_3_3", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Left)
            {
                var txtBox = this.Controls.Find("textBox_3_1", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                var txtBox = this.Controls.Find("textBox_2_2", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                var txtBox = this.Controls.Find("textBox_4_2", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
        }
        private void button_3_3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                var txtBox = this.Controls.Find("textBox_3_4", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Left)
            {
                var txtBox = this.Controls.Find("textBox_3_2", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                var txtBox = this.Controls.Find("textBox_2_3", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                var txtBox = this.Controls.Find("textBox_4_3", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
        }
        private void button_3_4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                var txtBox = this.Controls.Find("textBox_3_5", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Left)
            {
                var txtBox = this.Controls.Find("textBox_3_3", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                var txtBox = this.Controls.Find("textBox_2_4", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                var txtBox = this.Controls.Find("textBox_4_4", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
        }
        private void button_3_5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                var txtBox = this.Controls.Find("textBox_3_4", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                var txtBox = this.Controls.Find("textBox_2_5", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                var txtBox = this.Controls.Find("textBox_4_5", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
        }

        private void button_4_1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                var txtBox = this.Controls.Find("textBox_4_2", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                var txtBox = this.Controls.Find("textBox_3_1", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                var txtBox = this.Controls.Find("textBox_5_1", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
        }
        private void button_4_2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                var txtBox = this.Controls.Find("textBox_4_3", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Left)
            {
                var txtBox = this.Controls.Find("textBox_4_1", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                var txtBox = this.Controls.Find("textBox_3_2", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                var txtBox = this.Controls.Find("textBox_5_2", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
        }
        private void button_4_3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                var txtBox = this.Controls.Find("textBox_4_4", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Left)
            {
                var txtBox = this.Controls.Find("textBox_4_2", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                var txtBox = this.Controls.Find("textBox_3_3", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                var txtBox = this.Controls.Find("textBox_5_3", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
        }
        private void button_4_4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                var txtBox = this.Controls.Find("textBox_4_5", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Left)
            {
                var txtBox = this.Controls.Find("textBox_4_3", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                var txtBox = this.Controls.Find("textBox_3_4", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                var txtBox = this.Controls.Find("textBox_5_4", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
        }
        private void button_4_5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                var txtBox = this.Controls.Find("textBox_4_4", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                var txtBox = this.Controls.Find("textBox_3_5", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                var txtBox = this.Controls.Find("textBox_5_5", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
        }

        private void button_5_1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                var txtBox = this.Controls.Find("textBox_5_2", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                var txtBox = this.Controls.Find("textBox_4_1", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
        }
        private void button_5_2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                var txtBox = this.Controls.Find("textBox_5_3", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Left)
            {
                var txtBox = this.Controls.Find("textBox_5_1", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                var txtBox = this.Controls.Find("textBox_4_2", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
        }
        private void button_5_3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                var txtBox = this.Controls.Find("textBox_5_4", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Left)
            {
                var txtBox = this.Controls.Find("textBox_5_2", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                var txtBox = this.Controls.Find("textBox_4_3", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
        }
        private void button_5_4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                var txtBox = this.Controls.Find("textBox_5_5", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Left)
            {
                var txtBox = this.Controls.Find("textBox_5_3", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                var txtBox = this.Controls.Find("textBox_4_4", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
        }
        private void button_5_5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                var txtBox = this.Controls.Find("textBox_5_4", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                var txtBox = this.Controls.Find("textBox_4_5", true).FirstOrDefault() as TextBox;
                txtBox.Focus();
            }
        }
    }

    //Prompt class definition found here: https://stackoverflow.com/questions/5427020/prompt-dialog-in-windows-forms
    //- Josueh R
    public static class Prompt
    {
        public static string ShowDialog(string text, string caption)
        {
            Form prompt = new Form()
            {
                Width = 400,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };
            Label textLabel = new Label() { Left = 150, Top = 20, Text = text };
            TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 300 };
            Button confirmation = new Button() { Text = "Ok", Left = 150, Width = 100, Top = 70, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }
    }

}
