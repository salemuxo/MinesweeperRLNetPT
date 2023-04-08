using RLNET;
using System.Diagnostics;

namespace MinesweeperRLNetPT.Core
{
    public class InputHandler
    {
        private readonly RLRootConsole _rootConsole;

        public InputHandler(RLRootConsole rootConsole)
        {
            _rootConsole = rootConsole;
        }

        public void HandleInput()
        {
            HandleKeyInput();
            HandleMouseInput();
        }

        private void HandleKeyInput()
        {
            RLKeyPress keyPress = _rootConsole.Keyboard.GetKeyPress();
            if (keyPress != null)
            {
                Debug.WriteLine($"{keyPress.Key} pressed");
            }
        }

        private void HandleMouseInput()
        {
            RLMouse mouse = _rootConsole.Mouse;

            // mouse on map and game active
            if (mouse.X >= Game.MapPosition.X &&
                mouse.X <= Game.MapPosition.X + Game.MapPosition.W &&
                mouse.Y >= Game.MapPosition.Y &&
                mouse.Y <= Game.MapPosition.Y + Game.MapPosition.H &&
                Game.IsPlaying)
            {
                int clickedTileX = mouse.X - Game.MapPosition.X;
                int clickedTileY = mouse.Y - Game.MapPosition.Y;

                if (mouse.GetLeftClick())
                {
                    Game.Map.LClicked(clickedTileX, clickedTileY);
                }
                if (mouse.GetRightClick())
                {
                    Game.Map.RClicked(clickedTileX, clickedTileY);
                }
            }
        }
    }
}