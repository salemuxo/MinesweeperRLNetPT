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
                mouse.X < Game.MapPosition.X + Game.MapPosition.W &&
                mouse.Y >= Game.MapPosition.Y &&
                mouse.Y < Game.MapPosition.Y + Game.MapPosition.H &&
                Game.IsPlaying)
            {
                int mouseTileX = mouse.X - Game.MapPosition.X;
                int mouseTileY = mouse.Y - Game.MapPosition.Y;

                Game.Map.HandleMouseHover(mouseTileX, mouseTileY);
                if (mouse.GetLeftClick())
                {
                    Game.Map.LClicked(mouseTileX, mouseTileY);
                }
                if (mouse.GetRightClick())
                {
                    Game.Map.RClicked(mouseTileX, mouseTileY);
                }
                if (mouse.GetMiddleClick())
                {
                    Game.Map.MClicked(mouseTileX, mouseTileY);
                }
            }
        }
    }
}