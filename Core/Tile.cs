using RLNET;
using SalemLib;
using System.Collections.Generic;
using System.Diagnostics;

namespace MinesweeperRLNetPT.Core
{
    public class Tile
    {
        public Point Position { get; set; }
        public bool IsMine { get; set; }
        public bool IsRevealed { get; set; }
        public bool IsFlagged { get; set; }
        public int AdjacentMines { get; set; }
        
        public Tile(Point position)
        {
            Position = position;
            IsRevealed = false;
            IsMine = false;
        }

        public int CountAdjacentMines(Map map)
        {
            var adjacentTiles = map.GetAdjacentTiles(this);
            int mineCount = 0;

            foreach (var tile in adjacentTiles)
            {
                if (tile.IsMine)
                {
                    mineCount++;
                }
            }

            AdjacentMines = mineCount;
            return mineCount;
        }

        public void Draw(RLConsole console)
        {
            if (!IsRevealed)
            {
                SetConsoleSymbol(console, Colors.Undiscovered, 'X');
            }
            else if (IsMine)
            {
                SetConsoleSymbol(console, Colors.Mine, 'M');
            }
            else if (IsFlagged)
            {
                SetConsoleSymbol(console, Colors.Flag, 'F');
            }
            else
            {
                SetConsoleSymbol(console, RLColor.Red, (char)AdjacentMines);
            }
        }

        public void SetConsoleSymbol(RLConsole console, RLColor color, int symbol)
        {
            console.Set(Position.X, Position.Y, color, Colors.Background, symbol);
        }
    }
}
