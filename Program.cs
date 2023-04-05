using MinesweeperRLNetPT.Core;
using RLNET;
using System;

namespace MinesweeperRLNetPT
{
    public static class Program
    {
        // height and width in tiles

        // root console
        private static readonly int _screenWidth = 8;
        private static readonly int _screenHeight = 8;
        private static RLRootConsole _rootConsole;

        // map console
        private static readonly int _mapWidth = 8;
        private static readonly int _mapHeight = 8;
        private static readonly int _mines = 9;
        private static RLConsole _mapConsole;

        public static Random Random { get; private set; }
        public static Map Map { get; private set; }
        public static InputHandler InputHandler { get; private set; }

        public static void Main()
        {
            Random = new Random();
            Map = new Map(_mapHeight, _mapWidth, _mines);

            // create root console
            string fontFileName = "terminal8x8.png";
            string consoleTitle = "Minesweeper";
            _rootConsole = new RLRootConsole(fontFileName,
                _screenWidth, _screenHeight, 8, 8, 2f, consoleTitle);

            // create map console
            _mapConsole = new RLConsole(_mapWidth, _mapHeight);

            // set up update and render event handlers and input handler
            _rootConsole.Update += OnRootConsoleUpdate;
            _rootConsole.Render += OnRootConsoleRender;
            InputHandler = new InputHandler(_rootConsole);

            _rootConsole.Run();
        }

        // event handler for RLNET update event
        private static void OnRootConsoleUpdate(object sender, UpdateEventArgs e)
        {
            InputHandler.HandleInput();
        }

        // event handler for RLNET render event
        private static void OnRootConsoleRender(object sender, UpdateEventArgs e)
        {
            Map.Draw(_mapConsole);

            // get centered position in root console for map and blit to root console
            int mapCenteredX = (_screenWidth - _mapWidth) / 2;
            int mapCenteredY = (_screenHeight - _mapHeight) / 2;
            RLConsole.Blit(_mapConsole, 0, 0, _mapWidth, 
                _mapHeight, _rootConsole, mapCenteredX, mapCenteredY);

            _rootConsole.Draw();
        }
    }
}