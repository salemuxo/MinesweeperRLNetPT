using MinesweeperRLNetPT.Core;
using RLNET;

namespace MinesweeperPT1
{
    public static class Program
    {
        // height and width in tiles

        // root console
        private static readonly int _screenWidth = 48;
        private static readonly int _screenHeight = 32;
        private static RLRootConsole _rootConsole;

        // map console
        private static readonly int _mapWidth = 8;
        private static readonly int _mapHeight = 8;
        private static RLConsole _mapConsole;

        public static Map Map { get; private set; }

        public static void Main()
        {
            Map = new Map(_mapHeight, _mapWidth);

            // create root console
            string fontFileName = "terminal8x8.png";
            string consoleTitle = "Minesweeper";
            _rootConsole = new RLRootConsole(fontFileName,
                _screenWidth, _screenHeight, 8, 8, 2f, consoleTitle);

            // create map console
            _mapConsole = new RLConsole(_mapWidth, _mapHeight);

            // set up update and render event handlers
            _rootConsole.Update += OnRootConsoleUpdate;
            _rootConsole.Render += OnRootConsoleRender;

            _rootConsole.Run();
        }

        // event handler for RLNET update event
        private static void OnRootConsoleUpdate(object sender, UpdateEventArgs e)
        {
            _mapConsole.SetBackColor(0, 0, _mapWidth, _mapHeight, Colors.Undiscovered);
            _mapConsole.Print(1, 1, "Map", Colors.Text);
        }

        // event handler for RLNET render event
        private static void OnRootConsoleRender(object sender, UpdateEventArgs e)
        {
            Map.Draw(_mapConsole);

            RLConsole.Blit(_mapConsole, 0, 0, _mapWidth, 
                _mapHeight, _rootConsole, 20, 12);
            _rootConsole.Draw();
        }
    }
}