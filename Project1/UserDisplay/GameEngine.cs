using System;

namespace Ass1
{
    public class GameState
    {
        public int?[,] Board;
        public int Points;
        public int LastRow;
        public int LastCol;
    }
    public class GameEngine
    {
        public const int Size = 5;

        private int?[,] _board = new int?[Size, Size];

        public int Points { get; private set; }

        // last move (predecessor)
        public int LastRow { get; set; }
        public int LastCol { get; set; }

        public GameEngine()
        {
            Points = 0;
            LastRow = -1;
            LastCol = -1;
        }

        public bool Place(int value, int row, int col, out int earned)
        {
            earned = 0;

            
            if (row < 0 || row >= Size || col < 0 || col >= Size)
                return false;

            // invalid if occupied
            if (_board[row, col].HasValue)
                return false;

            _board[row, col] = value;

            // reward: diagonal corner cell of predecessor
            if (LastRow != -1 && LastCol != -1)
            {
                if (Math.Abs(row - LastRow) == 1 && Math.Abs(col - LastCol) == 1)
                {
                    Points += 1;
                    earned = 1;
                }
            }

            LastRow = row;
            LastCol = col;

            return true;
        }

        public int? GetCell(int r, int c)
        {
            return _board[r, c];
        }

        public GameState SaveState()
        {
            // deep copy board
            int?[,] copy = new int?[Size, Size];
            for (int r = 0; r < Size; r++)
                for (int c = 0; c < Size; c++)
                    copy[r, c] = _board[r, c];

            GameState st = new GameState();
            st.Board = copy;
            st.Points = Points;
            st.LastRow = LastRow;
            st.LastCol = LastCol;
            return st;
        }

        public void LoadState(GameState state)
        {
            if (state == null || state.Board == null)
                throw new ArgumentException("Invalid game state.");

            _board = state.Board;
            Points = state.Points;
            LastRow = state.LastRow;
            LastCol = state.LastCol;
        }
    }

    
}
