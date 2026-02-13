using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.Json.Serialization;

namespace GameUI
{
    [Serializable]
    public class GameState
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int?[,] Board { get; set; }
        public int Points { get; set; }
        public int LastRow { get; set; }
        public int LastCol { get; set; }
        public int currentNumber { get; set; }
        public int currentLevel { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string userName { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string saveDateTime { get; set; }

        public GameState()
        {
            this.Board = null;
            this.Points = 0;
            this.LastRow = -1;
            this.LastCol = -1;
            this.currentNumber = 1;
            this.currentLevel = 1;
            this.userName = "";
            this.saveDateTime = "";
        }
        public GameState(int size)
        {
            this.Board = new int?[size, size];
            this.Points = 0;
            this.LastRow = -1;
            this.LastCol = -1;
            this.currentNumber = 1;
            this.currentLevel = 1;
            this.userName = "";
            this.saveDateTime = "";
        }

        public GameState(int?[,] inputBoard, int inputPoints, int inputLastRow, int inputLastCol, int inputCurrentNumber, int inputCurrentLevel)
        {
            this.Board = inputBoard;
            this.Points = inputPoints;
            this.LastRow = inputLastRow;
            this.LastCol = inputLastCol;
            this.currentNumber = inputCurrentNumber;
            this.currentLevel = inputCurrentLevel;
            this.userName = "";
            this.saveDateTime = "";
        }

        public GameState(int?[,] inputBoard, int inputPoints, int inputLastRow, int inputLastCol, int inputCurrentNumber, int inputCurrentLevel, string inputName, string inputDate)
        {
            this.Board = inputBoard;
            this.Points = inputPoints;
            this.LastRow = inputLastRow;
            this.LastCol = inputLastCol;
            this.currentNumber = inputCurrentNumber;
            this.currentLevel = inputCurrentLevel;
            this.userName = inputName;
            this.saveDateTime = inputDate;
        }

        public GameState(GameState otherState)
        {
            this.Board = (int?[,])otherState.Board.Clone();
            this.Points = otherState.Points;
            this.LastRow = otherState.LastRow;
            this.LastCol = otherState.LastCol;
            this.currentNumber = otherState.currentNumber;
            this.currentLevel = otherState.currentLevel;
            this.userName = otherState.userName;
            this.saveDateTime = otherState.saveDateTime;
        }

        public GameState(GameState otherState, string userName, string saveDt)
        {
            this.Board = (int?[,])otherState.Board.Clone();
            this.Points = otherState.Points;
            this.LastRow = otherState.LastRow;
            this.LastCol = otherState.LastCol;
            this.currentNumber = otherState.currentNumber;
            this.currentLevel = otherState.currentLevel;
            this.userName = userName;
            this.saveDateTime = saveDt;

        }
    }

    public class GameEngine
    {
        private int _size;

        public int getBoardSize() { return _size; }
        public void setBoardSize(int boardSize ) { _size = boardSize; }

        GameState gameState;

        public Stack<GameState> history;

        public EventHandler<GameState> GameStateChanged;
        public EventHandler Level1Completed;
        
        public List<GameState> GetHistory() { return new List<GameState>(history); }

        public void SetHistory(List<GameState> History) { history = new Stack<GameState>(History); }

        public GameEngine()
        {

        }

        public GameEngine(int boardSize)
        {
            gameState = new GameState(boardSize);
            _size = boardSize;
            history = new Stack<GameState>();
        }

        public bool isInRow(int value, int row)
        {
            for (int i = 1; i < getBoardSize()-1; i++)
            {
                if(value == gameState.Board[row, i])
                {
                    return true;
                }
            }
            return false;
        }

        public bool isInCol(int value, int col)
        {
            for (int i = 1; i < getBoardSize() - 1; i++)
            {
                if (value == gameState.Board[i, col])
                {
                    return true;
                }
            }
            return false;
        }

        public bool isOnDiagonal(int value, int row, int col)
        {
            bool topleft = (row == 0) && (col == 0);
            bool topright = (row == 0) && (col == 7);
            bool bottomleft = (row == getBoardSize()) && (col == 0);
            bool bottomright = (row == getBoardSize()) && (col == getBoardSize());

            if (topleft || bottomright)
            {
                int j = 1;
                for (int i = 1; i < getBoardSize(); i++)
                {
                    if (gameState.Board[i, j] == value)
                    {
                        return true; 
                    }
                    j++;
                }

                return false;
            } else if (bottomleft || bottomright)
            {
                int j = 6;
                for (int i = 1; i < getBoardSize(); i++)
                {
                    if (gameState.Board[i,j] == value)
                    {
                        return true;
                    }
                    j--;
                }
                return false;
            } else
            {
                return false;
            }
        }

        public void UndoLastMove()
        {
            if (history.Count <= 0)
            {
                return;
            }
            else if (history.Count == 1)
            {
                GameState targetState = new GameState(history.Pop());
                SetState(targetState);
            }
            else
            {
                GameState targetState = history.Pop();
                SetState(targetState);
            }
            GameStateChanged?.Invoke(this, gameState);
            return;
        }

        public bool Place(int value, int row, int col)
        {
            bool isCurrentNumber = value == gameState.currentNumber;
            bool occupied = gameState.Board[row, col].HasValue;
            bool outOfBounds = (row < 0 || row >= _size || col < 0 || col >= _size);
            bool isNotAdjacentRow = ((gameState.LastRow != -1 && gameState.LastRow > row + 1) || (gameState.LastRow != -1 && gameState.LastRow < row - 1));
            bool isNotAdjacentCol = ((gameState.LastCol != -1 && gameState.LastCol > col + 1) || (gameState.LastCol != -1 && gameState.LastCol < col - 1));

            if(GetCurrentLevel() == 1)
            {
                if (!isCurrentNumber || occupied || outOfBounds || isNotAdjacentRow || isNotAdjacentCol)
                {
                    return false;
                }
                GameState previousMove = new GameState(GetState());
                history.Push(previousMove);

                gameState.Board[row, col] = value;

                gameState.currentNumber++;

                // reward: diagonal corner cell of predecessor
                if (gameState.LastRow != -1 && gameState.LastCol != -1)
                {
                    if (Math.Abs(row - gameState.LastRow) == 1 && Math.Abs(col - gameState.LastCol) == 1)
                    {
                        gameState.Points += 1;
                    }
                }

                gameState.LastRow = row;
                gameState.LastCol = col;

                GameStateChanged?.Invoke(this, gameState);

                if (gameState.currentNumber == 26)
                {
                    Level1Completed?.Invoke(this, EventArgs.Empty);
                }
                return true;
            } else  //current level = 2
            {
                var (targetRow, targetCol) = FindIndexInL1(value);

                if (targetRow == -1  && targetCol == -1 || occupied || value != GetCurrentNumber())
                {
                    return false;
                }

                if (!((row == targetRow) ||
                      (col == targetCol) ||
                      (row == col && targetRow == targetCol) ||
                      (row + col == getBoardSize() - 1 && targetRow + targetCol == getBoardSize() - 1))
                   )
                {
                    return false;
                }


                history.Push(new GameState(GetState()));

                gameState.Board[row,col] = value;
                gameState.currentNumber++;
                GameStateChanged?.Invoke(this, gameState);
                return true;
            }
         
        }

        private void LevelCompleted()
        {
            if(gameState.currentLevel == 1)
            {
                Level1Completed.Invoke(this, EventArgs.Empty);
            } else
            {
                //Level 2 Complete Game Over
            }
        }

        public (int, int) FindIndexInL1(int value)
        {
            for (int r = 1; r < getBoardSize() - 1; r++)
            {
               for (int c = 1; c < getBoardSize() - 1; c++)
                {
                    if (gameState.Board[r,c] == value)
                    {
                        return (r, c);
                    }
                }
            }

            return (-1, -1);
        }

        public int? GetCell(int r, int c)
        {
            return gameState.Board[r, c];
        }

        public int GetPoints() { return gameState.Points; }

        public int GetCurrentNumber() { return gameState.currentNumber; }

        public int GetCurrentLevel() { return gameState.currentLevel; }

        public GameState GetState()
        {
            // deep copy board
            GameState st = new GameState(this.gameState);
            return st;
        }

        public void ClearHistory()
        {
            history.Clear();
        }

        public void SetState(GameState state)
        {
            this.gameState = new GameState(state);
        }

        public void SetNameDate(string name, string date)
        {
            gameState.userName = name;
            gameState.saveDateTime = date;
        }

        public void placeRandomOne()
        {
            Random rand = new Random();
            int randomRow = rand.Next(_size);
            int randomCol = rand.Next(_size);
            gameState.Board[randomRow, randomCol] = 1;
            gameState.LastRow = randomRow;
            gameState.LastCol = randomCol;
            gameState.currentNumber = 2;

        }
    }

    
}

