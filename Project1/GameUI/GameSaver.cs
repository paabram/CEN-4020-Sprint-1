using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Windows.Forms;

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
        // line1: currentNumber
        // line2: currentLevel
        // line3: points
        // line4: lastRow,lastCol
        // next 5/7 lines: board rows with "." for empty
        // line10/12: userName(if any)
        // line11/13: saveDateTime(if any)
        public void Save(string file, GameState state)
        {
            if (state == null || state.Board == null)
                throw new ArgumentException("Invalid game state.");

            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            if (!Directory.Exists(baseDirectory + "\\Saves"))
            {
                Directory.CreateDirectory(baseDirectory + "\\Saves");
            }

            using (StreamWriter w = new StreamWriter(file, append: true))
            {
                w.WriteLine(state.currentNumber);
                w.WriteLine(state.currentLevel);
                w.WriteLine(state.Points);
                w.WriteLine(state.LastRow + "," + state.LastCol);
                if (state.currentLevel == 1)
                {
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
                    w.WriteLine(state.userName);
                    w.WriteLine(state.saveDateTime);
                }
                else
                {
                    int Level2Size = boardSize + 2;

                    for (int r = 0; r < Level2Size; r++)
                    {
                        for (int c = 0; c < Level2Size; c++)
                        {
                            string cell = state.Board[r, c].HasValue ? state.Board[r, c].Value.ToString() : ".";
                            w.Write(cell);
                            if (c < Level2Size - 1) w.Write(" ");
                        }
                        w.WriteLine();
                    }
                    w.WriteLine(state.userName);
                    w.WriteLine(state.saveDateTime);
                }
            }
        }

        public List<GameState> Load(string file)
        {
            
            List<GameState> results = new List<GameState>();
            string[] lines = File.ReadAllLines(file);
            if (lines.Length < 2 + boardSize) throw new Exception("Save file is too short.");

            for(int i = 0; i < lines.Length; i++)
            {
                int currNum = int.Parse(lines[i].Trim()); //i=10
                i++;//0
                int currLevel = int.Parse(lines[i].Trim()); //i=11
                i++;//1
                int points = int.Parse(lines[i].Trim()); //i=12
                i++;//2

                string[] last = lines[i].Trim().Split(',');
                if (last.Length != 2) throw new Exception("Invalid last move line.");
                int lastRow = int.Parse(last[0]);
                int lastCol = int.Parse(last[1]);
                if (currLevel == 1)
                {
                    int?[,] board = new int?[boardSize, boardSize];

                    for (int r = 0; r < boardSize; r++)
                    {
                        i++;
                        string[] parts = lines[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        if (parts.Length != boardSize)
                            throw new Exception("Invalid board row at r=" + r);

                        for (int c = 0; c < boardSize; c++)
                        {
                            board[r, c] = (parts[c] == ".") ? (int?)null : int.Parse(parts[c]);
                        }
                    }

                    i++;
                    string uName = lines[i].Trim();
                    i++;
                    string saveDate = lines[i].Trim();

                    results.Add(new GameState(board, points, lastRow, lastCol, currNum, currLevel, uName, saveDate));

                } else
                {
                    int Level2Size = boardSize + 2;

                    int?[,] board = new int?[Level2Size, Level2Size];
                    for (int r = 0; r < Level2Size; r++)
                    {
                        i++;
                        string[] parts = lines[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        if (parts.Length != Level2Size)
                            throw new Exception("Invalid board row at r=" + r);

                        for (int c = 0; c < Level2Size; c++)
                        {
                            board[r, c] = (parts[c] == ".") ? (int?)null : int.Parse(parts[c]);
                        }
                    }

                    i++;
                    string uName = lines[i].Trim();
                    i++;
                    string saveDate = lines[i].Trim();

                    results.Add(new GameState(board, points, lastRow, lastCol, currNum, currLevel));
                }
            }
            return results;
        }
    }
}

