using RLNET;
using SalemLib;
using System.Collections.Generic;
using System.Diagnostics;

namespace MinesweeperRLNetPT.Core
{
    public class Map
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public List<List<Tile>> Tiles { get; set; }

        public Map(int w, int h)
        {
            Width = w; 
            Height = h;
            InitTiles();
        }
        public void Draw(RLConsole mapConsole)
        {
            foreach (var list in Tiles)
            {
                foreach (var tile in list)
                {
                    SetConsoleSymbol(mapConsole, tile.Position);
                }
            }
        }

        private void InitTiles()
        {
            Tiles = new List<List<Tile>>();
            for (int x = 0; x < Width; x++)
            {
                Tiles.Add(new List<Tile>());
                for (int y = 0; y < Height; y++)
                {
                    Tiles[x].Add(new Tile(new Point(x, y)));
                }
            }
        }

        private void SetConsoleSymbol(RLConsole console, Point position)
        {
            console.Set(position.X, position.Y, Colors.Flag, Colors.Undiscovered, 'X');
        }
    }
}
