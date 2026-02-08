using System;
using System.Windows.Forms;

namespace GameUI
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            GameEngine engine = new GameEngine(5);
            GameSaver saver = new GameSaver(5);
            Form_GameBoardLvl2 GameBoardLvl2 = new Form_GameBoardLvl2(engine, saver);
            Form_GameBoard GameBoard = new Form_GameBoard(engine, saver, GameBoardLvl2);
            Form_GameMenu GameMenu = new Form_GameMenu(engine, saver);


            GameMenu.SetGameBoard(GameBoard);
            GameBoard.SetGameMenu(GameMenu);
            GameBoard.Height = GameMenu.Height;
            GameBoard.Width = GameMenu.Width;
            GameBoard.SetDesktopLocation(GameMenu.DesktopLocation.X, GameBoard.DesktopLocation.Y); ;
            GameBoard.Hide();
            Application.Run(GameMenu);
            
        }
    }
}
