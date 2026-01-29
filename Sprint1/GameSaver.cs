using System;
using System.IO;

namespace Ass1
{
    public class GameSaver
    {
        // Save format:
        // line1: points
        // line2: lastRow,lastCol
        // line3: nextNum
        // next 5 lines: board rows with "." for empty
        public void Save(string file, GameState state)
        {
            if (state == null || state.Board == null)
                throw new ArgumentException("Invalid game state.");

            using (StreamWriter w = new StreamWriter(file))
            {
                w.WriteLine(state.Points);
                w.WriteLine(state.LastRow + "," + state.LastCol);
                w.WriteLine(state.NextNum);

                for (int r = 0; r < GameEngine.Size; r++)
                {
                    for (int c = 0; c < GameEngine.Size; c++)
                    {
                        string cell = state.Board[r, c].HasValue ? state.Board[r, c].Value.ToString() : ".";
                        w.Write(cell);
                        if (c < GameEngine.Size - 1) w.Write(" ");
                    }
                    w.WriteLine();
                }
            }
        }

        public GameState Load(string file)
        {
            string[] lines = File.ReadAllLines(file);
            if (lines.Length < 2 + GameEngine.Size)
                throw new Exception("Save file is too short.");

            int points = int.Parse(lines[0].Trim());

            string[] last = lines[1].Trim().Split(',');
            if (last.Length != 2) throw new Exception("Invalid last move line.");
            int lastRow = int.Parse(last[0]);
            int lastCol = int.Parse(last[1]);

            int nextNum = int.Parse(lines[2].Trim());

            int?[,] board = new int?[GameEngine.Size, GameEngine.Size];

            for (int r = 0; r < GameEngine.Size; r++)
            {
                string[] parts = lines[r + 3].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != GameEngine.Size)
                    throw new Exception("Invalid board row at r=" + r);

                for (int c = 0; c < GameEngine.Size; c++)
                {
                    board[r, c] = (parts[c] == ".") ? (int?)null : int.Parse(parts[c]);
                }
            }

            GameState st = new GameState();
            st.Points = points;
            st.LastRow = lastRow;
            st.LastCol = lastCol;
            st.NextNum = nextNum;
            st.Board = board;
            return st;
        }
    }
}
