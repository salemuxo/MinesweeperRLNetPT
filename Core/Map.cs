using OpenTK.Graphics.OpenGL;
using RLNET;
using SalemLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MinesweeperRLNetPT.Core
{
    public class Map
    {
        public int Width { get; set; } // width of map in tiles
        public int Height { get; set; } // height of map in tiles
        public int Mines { get; set; } // number of mines on map
        public List<List<Tile>> Tiles { get; set; }

        public Map(int w, int h, int mines)
        {
            Width = w; 
            Height = h;
            Mines = mines;
            InitTiles();
        }

        // PUBLIC METHODS

        // draw map to mapConsole
        public void Draw(RLConsole mapConsole)
        {
            foreach (var list in Tiles)
            {
                foreach (var tile in list)
                {
                    tile.Draw(mapConsole);
                }
            }
        }

        // 
        public void TileLClicked(int x, int y)
        {
            var clickedTile = Tiles[x][y];
            if (!clickedTile.IsRevealed)
            {
                clickedTile.IsRevealed = true;
                if (clickedTile.IsMine)
                {

                }
                else
                {
                    //int adjacentMines = clickedTile.CountAdjacentMines(this);
                    //if (adjacentMines == 0)
                    //{
                    //    var adjacentTiles = GetAdjacentTiles(clickedTile);
                    //    foreach (var tile in adjacentTiles)
                    //    {

                    //    }
                    //}
                    //else
                    //{

                    //}
                }

            }
        }

        // right clicked Tile -> toggle flag
        public void TileRClicked(int x, int y)
        {
            var clickedTile = Tiles[x][y];
            if (clickedTile.IsFlagged)
            {
                clickedTile.IsFlagged = false;
            }
            else
            {
                clickedTile.IsFlagged = true;
            }
        }

        // return list of Tiles adjacent to tile
        public List<Tile> GetAdjacentTiles(Tile tile)
        {
            var adjacentTiles = new List<Tile>();
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
                        adjacentTiles.Add(
                            Tiles[tile.Position.X + x][tile.Position.Y + y]);
                    }
                }
            }
            return adjacentTiles;
        }

        // PRIVATE METHODS

        // create Tiles and Mines
        private void InitTiles()
        {
            CreateTiles();
            CreateMines();
        }

        // create Tiles based on Width and Height
        private void CreateTiles()
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

        // create # of mines based on Mines at random positions
        private void CreateMines()
        {
            for (int m = 0; m < Mines; m++)
            {
                var x = Program.Random.Next(Width);
                var y = Program.Random.Next(Height);
                
                // if tile is not a mine, make it one, otherwise don't increase loop number
                if (!Tiles[x][y].IsMine)
                {
                    Tiles[x][y].IsMine = true;
                }
                else
                {
                    m--;
                }
            }
        }
    }
}
