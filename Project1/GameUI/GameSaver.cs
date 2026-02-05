using System;
using System.IO;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace GameUI
{
    public class GameSaver
    {
        private int boardSize;


        public GameSaver(int boardSize)
        {
            this.boardSize = boardSize;
        }

        public int getLatestFileNumber()
        {
            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string targetDirectory = currentDirectory + "Saves";
            int maxNumber = 0;
            if (Directory.Exists(targetDirectory))
            {
                var files = Directory.GetFiles(targetDirectory);
                foreach (string file in files)
                {
                    string filename = Path.GetFileNameWithoutExtension(file);

                    string digits = Regex.Match(filename, @"\d+").Value;

                    if (int.TryParse(digits, out int testNumber))
                    {
                        if(testNumber > maxNumber)
                        {
                            maxNumber = testNumber;
                        }
                    }
                }
                return maxNumber;
            } else
            {
                return maxNumber;
            }
        }

        // Save format:
        // line1: points
        // line2: lastRow,lastCol
        // next 5 lines: board rows with "." for empty
        public void Save(string file, GameState state)
        {
            if (state == null || state.Board == null)
                throw new ArgumentException("Invalid game state.");

            using (StreamWriter w = new StreamWriter(file))
            {
                w.WriteLine(state.currentNumber);
                w.WriteLine(state.currentLevel);
                w.WriteLine(state.Points);
                w.WriteLine(state.LastRow + "," + state.LastCol);

                for (int r = 0; r < boardSize; r++)
                {
                    for (int c = 0; c < boardSize; c++)
                    {
                        string cell = state.Board[r, c].HasValue ? state.Board[r, c].Value.ToString() : ".";
                        w.Write(cell);
                        if (c < boardSize - 1) w.Write(" ");
                    }
                    w.WriteLine();
                }
            }
        }

        public GameState Load(string file)
        {
            int currNum;
            int currLevel;
            int points;

            string[] lines = File.ReadAllLines(file);
            if (lines.Length < 2 + boardSize)
                throw new Exception("Save file is too short.");

            currNum = int.Parse(lines[0].Trim());
            currLevel = int.Parse(lines[1].Trim());
            points = int.Parse(lines[2].Trim());

            string[] last = lines[3].Trim().Split(',');
            if (last.Length != 2) throw new Exception("Invalid last move line.");
            int lastRow = int.Parse(last[0]);
            int lastCol = int.Parse(last[1]);

            int?[,] board = new int?[boardSize, boardSize];

            for (int r = 0; r < boardSize; r++)
            {
                string[] parts = lines[r + 4].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != boardSize)
                    throw new Exception("Invalid board row at r=" + r);

                for (int c = 0; c < boardSize; c++)
                {
                    board[r, c] = (parts[c] == ".") ? (int?)null : int.Parse(parts[c]);
                }
            }

            GameState st = new GameState(board, points, lastRow, lastCol, currNum, currLevel);
            return st;
        }
    }
}

