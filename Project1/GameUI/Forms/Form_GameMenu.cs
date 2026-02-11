using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace GameUI
{
    public partial class Form_GameMenu : Form
    {
        public event EventHandler LoadRequested;
        public event EventHandler ExitRequested;
        public event EventHandler NewGameRequested;
        public event EventHandler ContinueRequested;
        public event EventHandler LeaderBoard1Requested;



        public Form_GameMenu()
        {
            InitializeComponent();
        }


        private void button_NewGame_Click(object sender, EventArgs e)
        {
            NewGameRequested?.Invoke(this, EventArgs.Empty);    
        }

        private void button_LoadGame_Click(object sender, EventArgs e)
        {
            LoadRequested?.Invoke(this, EventArgs.Empty);
        }

        private void button_Exit_Click(object sender, EventArgs e)
        {
            ExitRequested?.Invoke(this, EventArgs.Empty);
        }

        public void changeContinueVisibility(bool visible)
        {
            button_Continue.Visible = visible;
        }

        private void button_Continue_Click(object sender, EventArgs e)
        {
            ContinueRequested?.Invoke(this, EventArgs.Empty);
        }

        public void EnableContinue()
        {
            button_Continue.Visible = true;
        }

        public void DisableContinue()
        {
            button_Continue.Visible = false;
        }

        private void button_LeaderBoard1_Click(object sender, EventArgs e)
        {
            LeaderBoard1Requested?.Invoke(this, EventArgs.Empty);
        }

        private void button_LeaderBoard2_Click(object sender, EventArgs e)
        {
            /*
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
          */
        }

    }
}
