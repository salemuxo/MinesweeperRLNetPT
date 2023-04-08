using MinesweeperRLNetPT.Core;
using RLNET;
using SalemLib;
using System;
using System.Diagnostics;

namespace MinesweeperRLNetPT
{
    public static class Game
    {
        // height and width in tiles
        // root console
        private const int _screenWidth = 10;
        private const int _screenHeight = 14;
        private static RLRootConsole _rootConsole;

        // map console
        private const int _mapWidth = 8;
        private const int _mapHeight = 8;
        private const int _mapCenteredX = (_screenWidth - _mapWidth) / 2;
        private const int _mapCenteredY = (_screenHeight - _mapHeight) / 2;
        private const int _mapX = _mapCenteredX;
        private const int _mapY = 1;
        private const int _mines = 9;
        private static RLConsole _mapConsole;

        public static bool IsPlaying = true;

        public static Random Random { get; private set; }
        public static Map Map { get; private set; }
        public static InputHandler InputHandler { get; private set; }
        public static Rectangle MapPosition { get; private set; }

        public static void Main()
        {
            Random = new Random();
            Map = new Map(_mapHeight, _mapWidth, _mines);
            MapPosition = new Rectangle(_mapX, _mapY, _mapWidth, _mapHeight);

            // create root console
            const string fontFileName = "terminal8x8.png";
            const string consoleTitle = "Minesweeper";
            _rootConsole = new RLRootConsole(fontFileName,
                _screenWidth, _screenHeight, 8, 8, 4f, consoleTitle);

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
            RLConsole.Blit(_mapConsole, 0, 0, _mapWidth,
                _mapHeight, _rootConsole, _mapX, _mapY);

            _rootConsole.Draw();
        }

        public static void EndGame()
        {
            IsPlaying = false;
            Debug.WriteLine("Game Over!");
        }
    }
}