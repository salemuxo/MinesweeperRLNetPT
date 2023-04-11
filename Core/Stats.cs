using RLNET;

namespace MinesweeperRLNetPT.Core
{
    public class Stats
    {
        public int RemainingMines { get; set; }

        public void Draw(RLConsole statConsole)
        {
            statConsole.Print(0, 0, $"M: {RemainingMines}", Colors.Flag);
        }

        public void Update()
        {
            RemainingMines = Game.Map.Mines - Game.Map.Flags;
        }
    }
}
