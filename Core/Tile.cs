using SalemLib;
using System.Collections.Generic;

namespace MinesweeperRLNetPT.Core
{
    public class Tile
    {
        public Point Position { get; set; }
        public bool IsMine { get; set; }
        public bool IsRevealed { get; set; }
        
        public Tile(Point position)
        {
            Position = position;
        }

        public int CountAdjacentMines(List<List<Tile>> list)
        {
            int mineCount = 0;
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (y == 0 && x == 0)
                    {
                        continue;
                    }
                    else
                    {
                        if (list[x][y].IsMine)
                        {
                            mineCount++;
                        }
                    }
                }
            }
            return mineCount;
        }
    }
}
