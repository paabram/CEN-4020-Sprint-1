using System;

namespace Ass1
{
    public enum CheckResult
    {
        Success,
        OutOfBounds,
        Occupied,
        IncorrectPlacement
    }

    public class MoveInformation
    {
        public int Row;
        public int Col;
        public int Value;
        public int EarnedPoints;
        public int PrevRow;
        public int PrevCol;
        public int Num;
    }
    public class GameState
    {
        public int?[,] Board;
        public int Points;
        public int LastRow;
        public int LastCol;
        public int NextNum;
        public MoveInformation[] recordedMoves;
    }
    public class GameEngine
    {
        public const int Size = 5;

        private int?[,] _board = new int?[Size, Size];
        private Stack<MoveInformation> _history = new Stack<MoveInformation>();
        public int Points { get; private set; }

        // last move (predecessor)
        public int LastRow { get; set; }
        public int LastCol { get; set; }

        public int NextNum {get; private set;}

        public GameEngine()
        {
            Points = 0;
            LastRow = -1;
            LastCol = -1;
            NextNum = 2;

            // Place 1 at a random square on game start
            Random rand = new Random();
            int randomRow = rand.Next(Size);
            int randomCol = rand.Next(Size);
            _board[randomRow, randomCol] = 1;
            LastRow = randomRow;
            LastCol = randomCol;
        }

        public CheckResult CheckPlay (int value, int row, int col, out int earned)
        {
            earned = 0;
            
            if (row < 0 || row >= Size || col < 0 || col >= Size)
                return CheckResult.OutOfBounds;

            // invalid if occupied
            if (_board[row, col].HasValue)
                return CheckResult.Occupied;
            
            // invalid if not touching previous square
            if (LastRow != -1 && LastCol != -1)
            {
                if (Math.Abs(row - LastRow) > 1 || Math.Abs(col - LastCol) > 1)
                    return CheckResult.IncorrectPlacement;
            }
            _history.Push(new MoveInformation
            {
                Row = row,
                Col = col,
                Value = value,
                EarnedPoints = earned,
                PrevRow = LastRow,
                PrevCol = LastCol,
                Num = NextNum
            });

           _board[row, col] = value;
            Points += earned;
            LastRow = row;
            LastCol = col;
            NextNum++;

            return CheckResult.Success;
        }

        public bool Place(int value, int row, int col, out int earned)
        {
            CheckResult res = CheckPlay(value, row, col, out earned);
            return res == CheckResult.Success;
        }

        public bool CanUndo
        {
            get { return _history.Count > 0; }
        }

        public bool UndoOne(out MoveInformation undone)
        {
            undone = null;
            if (_history.Count == 0) return false;

            MoveInformation m = _history.Pop();

            _board[m.Row, m.Col] = null;
            Points -= m.EarnedPoints;

            LastRow = m.PrevRow;
            LastCol = m.PrevCol;

            NextNum = m.Num;

            undone = m;
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
            st.NextNum = NextNum;
            st.recordedMoves = _history.ToArray();
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
            NextNum = state.NextNum;
            _history = new Stack<MoveInformation>();
            if (state.recordedMoves != null)
            {
                //gives top-first; rebuild stack by pushing reverse
                for (int i = state.recordedMoves.Length - 1; i >= 0; i--)
                    _history.Push(state.recordedMoves[i]);
            }
        }
    }
}
