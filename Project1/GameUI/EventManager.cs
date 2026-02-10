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
        private readonly Form_GameBoard _gameboard_lvl2;


        public EventManager(Form_GameMenu menu)
        {
            Engine = new GameEngine();
            Saver = new GameSaver();
            _menu = menu;


            _menu.NewGameRequested += OnNewGameRequested;
            _menu.ExitRequested += OnExitRequested;
            _menu.LoadRequested += OnLoadRequested;
            _menu.ContinueRequested += OnContinueRequested;

        }  

        private void OnReturnRequested(object sender, EventArgs e)
        {
            if (_menu != null && !_menu.IsDisposed)
            {
                _menu.Close();
                _menu.Dispose();
            }

            _menu = new Form_GameMenu();
            _menu.Dock = DockStyle.Fill;
            _menu.TopLevel = false;
            MainForm.MainPanel.Controls.Clear();
            MainForm.MainPanel.Controls.Add(_menu);

            _menu.EnableContinue();
            _menu.NewGameRequested += OnNewGameRequested;
            _menu.ExitRequested += OnExitRequested;
            _menu.LoadRequested += OnLoadRequested;
            _menu.ContinueRequested += OnContinueRequested;

            _menu.Show();
        }


        private void OnNewGameRequested(object sender, EventArgs e)
        {
            if (_gameboard != null && !_gameboard.IsDisposed)
            {
                _gameboard.ReturnRequested -= OnReturnRequested;

                MainForm.MainPanel.Controls.Remove(_gameboard);

                _gameboard.Dispose();
            }

            _gameboard = new Form_GameBoard(this);
            _gameboard.Dock = DockStyle.Fill;
            _gameboard.TopLevel = false;
            MainForm.MainPanel.Controls.Clear();
            MainForm.MainPanel.Controls.Add(_gameboard);

            _gameboard.ReturnRequested += OnReturnRequested;


            _gameboard.Show();
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


            Form_GameBoard GameBoard = new Form_GameBoard();
            GameBoard.Dock = DockStyle.Fill;
            GameBoard.TopLevel = false;
            MainForm.MainPanel.Controls.Clear();
            MainForm.MainPanel.Controls.Add(GameBoard);
            GameBoard.Show();
        }

        private void OnContinueRequested(object sender, EventArgs e)
        {
            //Add Logic For Continuing Here


            ///

            if (_gameboard != null && !_gameboard.IsDisposed)
            {
                _gameboard.ReturnRequested -= OnReturnRequested;

                MainForm.MainPanel.Controls.Remove(_gameboard);

                _gameboard.Dispose();
            }

            _gameboard = new Form_GameBoard(this);
            _gameboard.Dock = DockStyle.Fill;
            _gameboard.TopLevel = false;
            MainForm.MainPanel.Controls.Clear();
            MainForm.MainPanel.Controls.Add(_gameboard);

            _gameboard.ReturnRequested += OnReturnRequested;


            _gameboard.Show();
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

    public class GameEventArgs : EventArgs
    {
        public string GameName { get; }

        public GameEventArgs(string gameName)
        {
            GameName = gameName;
        }
    }
    */
}
