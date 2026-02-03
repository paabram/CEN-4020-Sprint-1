using System;

namespace Ass1
{
    public class GameClient
    {
        private readonly GameEngine _engine;
        private readonly GameSaver _saver;

        public GameClient(GameEngine engine, GameSaver saver)
        {
            _engine = engine;
            _saver = saver;
        }

        public void Run(string[] args)
        {
            // Optional: load from command line argument
            if (args != null && args.Length > 0)
            {
                try
                {
                    GameState st = _saver.Load(args[0]);
                    _engine.LoadState(st);
                    Console.WriteLine("Loaded game from: " + args[0]);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Could not load file. Starting new game. Error: " + ex.Message);
                }
            }

            while (true)
            {
                DrawBoard();
                Console.WriteLine("Points: " + _engine.Points);

                // check win condition
                if (_engine.NextNum > GameEngine.Size * GameEngine.Size)
                {
                    Console.WriteLine("All squares filled! You win!");
                    return;
                }

                Console.WriteLine("Menu: (P)lace (U)ndo  (S)ave  (L)oad  (Q)uit");
                Console.Write("Choice: ");
                string choice = Console.ReadLine();

                if (choice == null) continue;
                choice = choice.Trim().ToUpperInvariant();

                if (choice == "Q") return;

                if (choice == "S")
                {
                    Save();
                }
                else if (choice == "L")
                {
                    Load();
                }
                else if (choice == "P")
                {
                    Place(); 
                }
                else if (choice == "U")
                {
                    Undo();
                }
            }
        }

        private bool Place()
        {
           int r, c;
            int val = _engine.NextNum;

            Console.Write("Row (0-4): ");
            if (!int.TryParse(Console.ReadLine(), out r))
            {
                Console.WriteLine("Invalid row input. Try again.");
                return false;
            }

            Console.Write("Col (0-4): ");
            if (!int.TryParse(Console.ReadLine(), out c))
            {
                Console.WriteLine("Invalid col input. Try again.");
                return false;
            }

            int earned;
            // If you added TryPlace + PlaceResult in the engine, prefer that:
            // var res = _engine.TryPlace(val, r, c, out earned);
            // if (res != PlaceResult.Success) { Console.WriteLine("Invalid move: " + res); return false; }

            bool placed = _engine.Place(val, r, c, out earned);

            if (!placed)
            {
                Console.WriteLine("Invalid move. Use Undo if needed, or try a different cell.");
                return false;
            }

            if (earned == 1)
                Console.WriteLine("Diagonal corner cell of predecessor! +1 point.");

            Console.WriteLine("Points now: " + _engine.Points);
            return true;
        }
        private void Undo()
        {
            if (!_engine.CanUndo)
            {
                Console.WriteLine("Nothing to undo.");
                return;
            }

            MoveInformation undone;
            if (_engine.UndoOne(out undone))
            {
                Console.WriteLine("Undid move: " + undone.Value + " at (" + undone.Row + "," + undone.Col + ")");
                Console.WriteLine("Points now: " + _engine.Points);
                Console.WriteLine("Next number now: " + _engine.NextNum);
            }
        }
        private void Save()
        {
            Console.Write("Filename to save: ");
            string file = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(file))
            {
                Console.WriteLine("No filename given.");
                return;
            }

            try
            {
                _saver.Save(file, _engine.SaveState());
                Console.WriteLine("Saved to: " + file);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Save failed: " + ex.Message);
            }
        }

        private void Load()
        {
            Console.Write("Filename to load: ");
            string file = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(file))
            {
                Console.WriteLine("No filename given.");
                return;
            }

            try
            {
                GameState st = _saver.Load(file);
                _engine.LoadState(st);
                Console.WriteLine("Loaded: " + file);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Load failed: " + ex.Message);
            }
        }

        private void DrawBoard()
        {
            Console.WriteLine();
            Console.WriteLine("Board (5x5): '.' means empty");
            Console.WriteLine("    0  1  2  3  4");
            Console.WriteLine("  +----------------");

            for (int r = 0; r < GameEngine.Size; r++)
            {
                Console.Write(r + " | ");
                for (int c = 0; c < GameEngine.Size; c++)
                {
                    int? cell = _engine.GetCell(r, c);
                    string s = cell.HasValue ? cell.Value.ToString() : ".";
                    // small spacing
                    if (s.Length == 1) s = " " + s;
                    Console.Write(s + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
