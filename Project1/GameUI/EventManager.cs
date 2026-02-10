using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace GameUI
{
    public class EventManager
    {
        GameEngine Engine;
        GameSaver Saver;

        private Form_GameMenu _menu;
        private Form_GameBoard _gameboard;
        private Form_GameBoardLvl2 _gameboard_lvl2;


        public EventManager()
        {
            Engine = new GameEngine(5);
            Saver = new GameSaver(5);
            DisplayGameMenu();

            Engine.Level1Completed += OnLevel1Completed;
            Engine.GameStateChanged += OnGameStateChanged;
        }

        private void DisplayGameBoard()
        {
            if(_gameboard != null && !_gameboard.IsDisposed)
            {
                _gameboard.Dispose();
            }

            _gameboard = new Form_GameBoard();
            _gameboard.Dock = DockStyle.Fill;
            _gameboard.TopLevel = false;
            MainForm.MainPanel.Controls.Clear();
            MainForm.MainPanel.Controls.Add(_gameboard);

            _gameboard.ReturnRequested += OnReturnRequested;
            _gameboard.ValuePlaced += OnValuePlaced;
            _gameboard.ValueError += OnValueError;

            _gameboard.Show();
        }

        private void DisplayGameMenu()
        {
            if (_menu != null && !_menu.IsDisposed)                
            {
                _menu.Dispose();
            }

            _menu = new Form_GameMenu();
            _menu.Dock = DockStyle.Fill;
            _menu.TopLevel = false;
            MainForm.MainPanel.Controls.Clear();
            MainForm.MainPanel.Controls.Add(_menu);

            _menu.NewGameRequested += OnNewGameRequested;
            _menu.ExitRequested += OnExitRequested;
            _menu.LoadRequested += OnLoadRequested;
            _menu.ContinueRequested += OnContinueRequested;

            _menu.Show();

        }

        private void DisplayGameBoardLvl2()
        {
            if (_gameboard_lvl2 != null && !_gameboard_lvl2.IsDisposed)
            {
                _gameboard_lvl2.Dispose();
            }

            _gameboard_lvl2 = new Form_GameBoardLvl2();
            _gameboard_lvl2.Dock = DockStyle.Fill;
            _gameboard_lvl2.TopLevel = false;
            MainForm.MainPanel.Controls.Clear();
            MainForm.MainPanel.Controls.Add(_gameboard_lvl2);

            //Subscribe all events that GameBoardLvl2 requires

            _gameboard_lvl2.Show();
        }

        private void OnReturnRequested(object sender, EventArgs e)
        {
            DisplayGameMenu();

            _menu.EnableContinue();

        }

        private void OnNewGameRequested(object sender, EventArgs e)
        {
            DisplayGameBoard();
        }

        private void OnExitRequested(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void OnLoadRequested(object sender, EventArgs e)
        {
            //Load game here
            OpenFileDialog openFileDialog = new OpenFileDialog();

            string filePath = AppDomain.CurrentDomain.BaseDirectory + "Saves\\";
            openFileDialog.InitialDirectory = filePath;
            openFileDialog.Title = "Select Save File";
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                List<GameState> loadStates = Saver.Load(openFileDialog.FileName);
            }


            DisplayGameBoard();
        }

        private void OnContinueRequested(object sender, EventArgs e)
        {
            //Add Logic For Continuing Here


            ///

            DisplayGameBoard();

        }

        private void OnValuePlaced(object sender, VPEventArgs e)
        {
            if (!Engine.Place(e.Value, e.Row, e.Column))
            {
                OnValueError(this, new VEEventArgs(e.Row, e.Column));
            } else
            {
                var btn = _gameboard.Controls.Find($"button_{e.Row + 1}_{e.Column + 1}", true).FirstOrDefault() as Button;
                var txtBox = _gameboard.Controls.Find($"textBox_{e.Row + 1}_{e.Column + 1}", true).FirstOrDefault() as TextBox;
                btn.BackColor = System.Drawing.Color.Green;
                txtBox.Text = e.Value.ToString();
            }
        }

        private async void OnValueError(object sender, VEEventArgs e)
        {
            string btnName = $"button_{e.Row + 1}_{e.Column + 1}";
            string txtBoxName = $"textBox_{e.Row + 1}_{e.Column + 1}";
            var btn = _gameboard.Controls.Find(btnName, true).FirstOrDefault() as Button;
            var txtBox = _gameboard.Controls.Find(txtBoxName, true).FirstOrDefault() as TextBox;
            var defaultColor = btn.BackColor;

            //failureSound.Play();
            btn.BackColor = System.Drawing.Color.Red;

            await Task.Delay(500);

            btn.BackColor = defaultColor;
            txtBox.Text = "";
            txtBox.Enabled = true;

            return;
        }

        private void OnLevel1Completed(object sender, EventArgs e)
        {
            //Play Success Sound
            DialogResult dr = MessageBox.Show("Congratulations! You Won! Would you like to move on to level 2?", "", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
            if (dr == DialogResult.Yes)
            {
                DisplayGameBoardLvl2();
                //Engine.StartLevel2();
            } else
            {
                OnReturnRequested(this, EventArgs.Empty);
            }

        }

        private void OnGameStateChanged(object sender, GameState State)
        {
            if (State.currentLevel == 1)
            {
                _gameboard.RefreshDisplay(State);

            } else
            {

                _gameboard_lvl2.RefreshDisplay(State);

            }
        }


    }

    /*
    public class GameEventHandler
    {
        public GameEventHandler(GameEvent e)
        {
            e.Event += OnEvent;
        }

#nullable enable
        private void OnEvent(object? sender, GameEventArgs e)
        {
            MessageBox.Show("OnEvent Called!");
        }
#nullable disable

    }


    public class GameEvent
    {
        public event EventHandler<GameEventArgs> Event;

        public void FireEvent(string GameName)
        {
            Event?.Invoke(this, new GameEventArgs(GameName));
        }

 
    }

    */
    public class GameEventArgs : EventArgs
    {
        public string GameName { get; }

        public GameEventArgs(string gameName)
        {
            GameName = gameName;
        }
    }

    public class VPEventArgs : EventArgs
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public int Value { get; set; }

        public VPEventArgs(int row, int column, int value)
        {
            Row = row;
            Column = column;
            Value = value;
        }
    }

    public class VEEventArgs : EventArgs
    {
        public int Row { get; set; }
        public int Column { get; set; }
        
        public VEEventArgs (int row, int column)
        {
            Row = row;
            Column = column;
        }
    }
}
