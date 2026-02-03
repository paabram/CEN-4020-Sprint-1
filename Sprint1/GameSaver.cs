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
                // history
                int historyCount = (state.recordedMoves == null) ? 0 : state.recordedMoves.Length;
                w.WriteLine(historyCount);

                if (historyCount > 0)
                {
                    // state.History is top-first
                    for (int i = 0; i < historyCount; i++)
                    {
                        MoveInformation m = state.recordedMoves[i];
                        w.WriteLine(
                            m.Row + " " +
                            m.Col + " " +
                            m.Value + " " +
                            m.EarnedPoints + " " +
                            m.PrevRow + " " +
                            m.PrevCol + " " +
                            m.Num
                        );
                    }
                }
            }
        }

        public GameState Load(string file)
        {
            string[] lines = File.ReadAllLines(file);
            if (lines.Length < 3 + GameEngine.Size)
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
            int historyLineIndex = 3 + GameEngine.Size; // after board
            MoveInformation[] recordedMoves = new MoveInformation[0];

            if (lines.Length > historyLineIndex)
            {
                // If there is an extra line, treat it as historyCount (v2)
                int historyCount;
                if (int.TryParse(lines[historyLineIndex].Trim(), out historyCount) && historyCount >= 0)
                {
                    recordedMoves = new MoveInformation[historyCount];

                    for (int i = 0; i < historyCount; i++)
                    {
                        int index = historyLineIndex + 1 + i;
                        if (index >= lines.Length)
                            throw new Exception("Save file ended early while reading history.");

                        string[] p = lines[index].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        if (p.Length != 7)
                            throw new Exception("Invalid history line: " + lines[index]);

                        recordedMoves[i] = new MoveInformation
                        {
                            Row = int.Parse(p[0]),
                            Col = int.Parse(p[1]),
                            Value = int.Parse(p[2]),
                            EarnedPoints = int.Parse(p[3]),
                            PrevRow = int.Parse(p[4]),
                            PrevCol = int.Parse(p[5]),
                            Num = int.Parse(p[6])
                        };
                    }
                }
            }
            GameState st = new GameState();
            st.Points = points;
            st.LastRow = lastRow;
            st.LastCol = lastCol;
            st.NextNum = nextNum;
            st.Board = board;
            st.recordedMoves = recordedMoves;
            return st;
        }
    }
}
