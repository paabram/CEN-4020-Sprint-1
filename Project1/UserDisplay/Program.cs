using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserDisplay
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
            Form_GameBoard GameBoard = new Form_GameBoard();
            GameBoard.Hide();
            Form_GameMenu GameMenu = new Form_GameMenu(GameBoard);
            GameBoard.SetGameMenu(GameMenu);
            Application.Run(GameMenu);
        }
    }
}
