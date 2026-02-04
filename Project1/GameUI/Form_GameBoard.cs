using System;
using System.Linq;
using System.Windows.Forms;
using System.Media;

namespace GameUI
{
    public partial class Form_GameBoard : Form
    {
        Form_GameMenu gameMenu;
        GameEngine gameEngine;
        GameSaver gameSaver;

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

        private void refreshDisplay()
        {
            for (int i = 0; i < gameEngine.getBoardSize();i++)
            {
                for (int j = 0; j < gameEngine.getBoardSize(); j++)
                {
                    int row = i;
                    int col = j;
                    string buttonName = $"button_{row}_{col}";
                    var btn = this.Controls.Find(buttonName, true).FirstOrDefault() as Button;

                    if(btn != null)
                    {
                        btn.Text = gameEngine.GetCell(row, col).ToString();
                    }
                }
            }

            label_CurrentNumber.Text = gameEngine.GetCurrentNumber().ToString();
            label_currentPoints.Text = gameEngine.GetPoints().ToString();


        }

        private void tryPlaceValue(int row, int col)
        {
            if(gameEngine.Place(gameEngine.GetCurrentNumber(), row, col))
            {
                //play victory noise
                SystemSounds.Beep.Play();
                return;
            } else
            {
                SystemSounds.Exclamation.Play();
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
            gameMenu.Show();
        }




        private void button_1_1_Click(object sender, EventArgs e)
        {
            tryPlaceValue(1, 1);
        }

        private void button_1_2_Click(object sender, EventArgs e)
        {
            tryPlaceValue(1, 2);

        }

        private void button_1_3_Click(object sender, EventArgs e)
        {
            tryPlaceValue(1, 3);

        }

        private void button_1_4_Click(object sender, EventArgs e)
        {
            tryPlaceValue(1, 4);

        }

        private void button_1_5_Click(object sender, EventArgs e)
        {
            tryPlaceValue(1, 5);

        }

        private void button_2_1_Click(object sender, EventArgs e)
        {
            tryPlaceValue(2, 1);
        }

        private void button_2_2_Click(object sender, EventArgs e)
        {
            tryPlaceValue(2, 2);
        }

        private void button_2_3_Click(object sender, EventArgs e)
        {
            tryPlaceValue(2, 3);
        }

        private void button_2_4_Click(object sender, EventArgs e)
        {
            tryPlaceValue(2, 4);
        }

        private void button_2_5_Click(object sender, EventArgs e)
        {
            tryPlaceValue(2, 5);
        }

        private void button_3_1_Click(object sender, EventArgs e)
        {
            tryPlaceValue(3, 1);
        }
        private void button_3_2_Click(object sender, EventArgs e)
        {
            tryPlaceValue(3, 2);
        }


        private void button_3_3_Click(object sender, EventArgs e)
        {
            tryPlaceValue(3, 3);
        }
        private void button_3_4_Click(object sender, EventArgs e)
        {
            tryPlaceValue(3, 4);
        }

        private void button_3_5_Click(object sender, EventArgs e)
        {
            tryPlaceValue(3, 5);
        }


        private void button_4_1_Click(object sender, EventArgs e)
        {
            tryPlaceValue(4, 1);
        }

        private void button_4_2_Click(object sender, EventArgs e)
        {
            tryPlaceValue(4, 2);
        }

        private void button_4_3_Click(object sender, EventArgs e)
        {
            tryPlaceValue(4, 3);
        }

        private void button_4_4_Click(object sender, EventArgs e)
        {
            tryPlaceValue(4, 4);
        }

        private void button_4_5_Click(object sender, EventArgs e)
        {
            tryPlaceValue(4, 5);
        }

        private void button_5_1_Click(object sender, EventArgs e)
        {
            tryPlaceValue(5, 1);
        }

        private void button_5_2_Click(object sender, EventArgs e)
        {
            tryPlaceValue(5, 2);
        }

        private void button_5_3_Click(object sender, EventArgs e)
        {
            tryPlaceValue(5, 3);
        }

        private void button_5_4_Click(object sender, EventArgs e)
        {
            tryPlaceValue(5, 4);
        }

        private void button_5_5_Click(object sender, EventArgs e)
        {
            tryPlaceValue(5, 5);
        }

        private void button_Undo_Click(object sender, EventArgs e)
        {

        }
    }
}
