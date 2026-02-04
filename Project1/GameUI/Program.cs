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
            Form_GameBoard GameBoard = new Form_GameBoard(engine, saver);
            Form_GameMenu GameMenu = new Form_GameMenu(engine, saver);

            GameBoard.Hide();
            Application.Run(GameMenu);
            
        }
    }
}
