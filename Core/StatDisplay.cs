using RLNET;

namespace MinesweeperRLNetPT.Core
{
    public class StatDisplay
    {
        public int RemainingMines { get; set; }

        // draw to statConsole
        public void Draw(RLConsole statConsole)
        {
            statConsole.Print(0, 0, $"M: {RemainingMines}", Colors.Flag);
        }

        // update remaining mines
        public void Update()
        {
            RemainingMines = Game.Map.Mines - Game.Map.Flags;
        }
    }
}