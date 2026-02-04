using System;

namespace GameUI
{
    public class GameState
    {
        public int?[,] Board;
        public int Points;
        public int LastRow;
        public int LastCol;
        public int currentNumber;
        public int currentLevel;

        public GameState()
        {
            this.Board = null;
            this.Points = 0;
            this.LastRow = -1;
            this.LastCol = -1;
            this.currentNumber = 1;
            this.currentLevel = 1;
        }
        public GameState(int size)
        {
            this.Board = new int?[size, size];
            this.Points = 0;
            this.LastRow = -1;
            this.LastCol = -1;
            this.currentNumber = 1;
            this.currentLevel = 1;
        }

        public GameState(int?[,] inputBoard, int inputPoints, int inputLastRow, int inputLastCol, int inputCurrentNumber, int inputCurrentLevel)
        {
            this.Board = inputBoard;
            this.Points = inputPoints;
            this.LastRow = inputLastRow;
            this.LastCol = inputLastCol;
            this.currentNumber = inputCurrentNumber;
            this.currentLevel = inputCurrentLevel;
        }

        public GameState(GameState otherState)
        {
            this.Board = otherState.Board;
            this.Points = otherState.Points;
            this.LastRow = otherState.LastRow;
            this.LastCol = otherState.LastCol;
            this.currentNumber = otherState.currentNumber;
            this.currentLevel = otherState.currentLevel;
        }
    }


    public class GameEngine
    {
        private int _size = 5;

        public int getBoardSize() { return _size; }
        public void setBoardSize(int boardSize ) { _size = boardSize; }

        GameState gameState;

        public GameEngine()
        {

        }

        public GameEngine(int boardSize)
        {
            gameState = new GameState(boardSize);
            _size = boardSize;
        }

        public bool Place(int value, int row, int col)
        {

            
            if (row < 0 || row >= _size || col < 0 || col >= _size)
                return false;

            // invalid if occupied
            if (gameState.Board[row, col].HasValue)
                return false;

            gameState.Board[row, col] = value;

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

            return true;
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

        public void SetState(GameState state)
        {
            this.gameState = state;
        }
    }

    
}

