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
            if (mouse.GetLeftClick())
            {
                Program.Map.TileLClicked(mouse.X, mouse.Y);
            }
            if (mouse.GetRightClick())
            {
                Program.Map.TileRClicked(mouse.X, mouse.Y);
            }
        }
    }
}
