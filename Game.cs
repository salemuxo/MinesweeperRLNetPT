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
        private const int _mapX = _mapCenteredX;
        private const int _mapY = 1;
        private const int _mines = 9;
        private static RLConsole _mapConsole;

        // stat console
        private const int _statWidth = _mapWidth;
        private const int _statHeight = 1;
        private const int _statX = _mapX;
        private const int _statY = 0;
        private static RLConsole _statConsole;

        // log console
        private const int _logWidth = 10;
        private const int _logHeight = 5;
        private const int _logX = 0;
        private const int _logY = 9;
        private static RLConsole _logConsole;

        public static bool IsPlaying = true;

        public static Random Random { get; private set; }
        public static Map Map { get; private set; }
        public static StatDisplay StatDisplay { get; private set; }
        public static MessageLog MessageLog { get; private set; }
        public static InputHandler InputHandler { get; private set; }
        public static Rectangle MapPosition { get; private set; }

        public static void Main()
        {
            Random = new Random();
            StatDisplay = new StatDisplay();
            MessageLog = new MessageLog(_logWidth, _logHeight);

            Map = new Map(_mapHeight, _mapWidth, _mines);
            MapPosition = new Rectangle(_mapX, _mapY, _mapWidth, _mapHeight);

            // create root console
            const string fontFileName = "terminal8x8.png";
            const string consoleTitle = "Minesweeper";
            _rootConsole = new RLRootConsole(fontFileName,
                _screenWidth, _screenHeight, 8, 8, 4f, consoleTitle);

            // create subconsoles
            _mapConsole = new RLConsole(_mapWidth, _mapHeight);
            _statConsole = new RLConsole(_statWidth, _statHeight);
            _logConsole = new RLConsole(_logWidth, _logHeight);

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
            StatDisplay.Update();
        }

        // event handler for RLNET render event
        private static void OnRootConsoleRender(object sender, UpdateEventArgs e)
        {
            Map.Draw(_mapConsole);
            StatDisplay.Draw(_statConsole);
            MessageLog.Draw(_logConsole);

            RLConsole.Blit(_mapConsole, 0, 0, _mapWidth,
                _mapHeight, _rootConsole, _mapX, _mapY);
            RLConsole.Blit(_statConsole, 0, 0, _statWidth,
                _statHeight, _rootConsole, _statX, _statY);
            RLConsole.Blit(_logConsole, 0, 0, _logWidth, 
                _logHeight, _rootConsole, _logX, _logY);

            _rootConsole.Draw();
        }

        public static void EndGame()
        {
            //IsPlaying = false;
            MessageLog.Add("Game Over!");
        }
    }
}