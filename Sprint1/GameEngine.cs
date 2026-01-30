using System;

namespace Ass1
{
    public class GameState
    {
        public int Size;
        public int?[,] Board;
        public int Points;
        public int LastRow;
        public int LastCol;
        public int NextNum;
        public bool Level2;
    }
    public class GameEngine
    {
        public int Size;

        private int?[,] _board;

        public int Points { get; private set; }

        // last move (predecessor)
        public int LastRow { get; set; }
        public int LastCol { get; set; }

        public int NextNum {get; private set;}

        public bool Level2 { get; set; }

        public GameEngine()
        {
            // set board size before using it
            Size = 5;
            _board = new int?[Size, Size];
            Points = 0;
            LastRow = -1;
            LastCol = -1;
            NextNum = 2;
            Level2 = false;

            // Place 1 at a random square on game start
            Random rand = new Random();
            int randomRow = rand.Next(Size);
            int randomCol = rand.Next(Size);
            _board[randomRow, randomCol] = 1;
            LastRow = randomRow;
            LastCol = randomCol;
        }

        public bool Place(int value, int row, int col, out int earned)
        {
            earned = 0;
            
            if (row < 0 || row >= Size || col < 0 || col >= Size)
                return false;

            // invalid if occupied
            if (_board[row, col].HasValue)
                return false;
            
            // invalid if not touching previous square
            if (LastRow != -1 && LastCol != -1)
            {
                if (Math.Abs(row - LastRow) > 1 || Math.Abs(col - LastCol) > 1)
                    return false;
            }

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
            NextNum += 1;

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
            st.Size = Size;
            st.Board = copy;
            st.Points = Points;
            st.LastRow = LastRow;
            st.LastCol = LastCol;
            st.NextNum = NextNum;
            return st;
        }

        public void LoadState(GameState state)
        {
            if (state == null || state.Board == null)
                throw new ArgumentException("Invalid game state.");

            Size = state.Size;
            _board = state.Board;
            Points = state.Points;
            LastRow = state.LastRow;
            LastCol = state.LastCol;
            NextNum = state.NextNum;
        }

        public static GameState InitLevel2FromState(GameState state)
        {
            GameState L2State = state;
            L2State.Size = 7;
            int?[,] newBoard = new int?[L2State.Size, L2State.Size];
            for (int r = 1; r < L2State.Size - 1; r++)
                for (int c = 1; c < L2State.Size - 1; c++)
                    newBoard[r, c] = L2State.Board[r - 1, c - 1];
            L2State.Board = newBoard;
            L2State.NextNum = 2;
            L2State.Level2 = true;
            return L2State;
        }
    }

    
}
