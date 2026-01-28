using System;

namespace Ass1
{
    class Program
    {
        static void Main(string[] args)
        {
            GameEngine engine = new GameEngine();
            GameSaver saver = new GameSaver();
            GameClient ui = new GameClient(engine, saver);
            ui.Run(args);
        }
    }
}
