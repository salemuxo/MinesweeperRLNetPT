using MinesweeperRLNetPT.Core;
using RLNET;
using SalemLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MinesweeperRLNetPT.Core
{
    public class Map
    {
        public int Width { get; set; } // width of map in tiles
        public int Height { get; set; } // height of map in tiles
        public int Mines { get; set; } // number of mines on map
        public int Flags { get; set; } // number of flags
        public List<List<Tile>> Tiles { get; set; }
        public bool AreMinesGenerated { get; set; }

        private Tile highlightedTile;

        public Map(int w, int h, int mines)
        {
            Width = w;
            Height = h;
            Mines = mines;
            Flags = 0;
            CreateTiles();
            AreMinesGenerated = false;
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

        // left clicked Tile -> reveal Tile
        public void LClicked(int x, int y)
        {
            if (AreMinesGenerated)
            {
                var clickedTile = Tiles[x][y];
                if (!clickedTile.IsRevealed)
                {
                    clickedTile.Reveal();
                }
            }
            else // generate mines
            {
                AreMinesGenerated = true;
                CreateMines(new Point(x, y));
                LClicked(x, y);
            }
        }

        // right clicked Tile -> toggle flag
        public void RClicked(int x, int y)
        {
            var clickedTile = Tiles[x][y];
            if (!clickedTile.IsRevealed)
            {
                clickedTile.ToggleFlag();
                Flags += (clickedTile.IsFlagged) ? 1 : -1;
            }
        }

        // middle clicked Tile -> reveal adjacent Tiles
        public void MClicked(int x, int y)
        {
            var clickedTile = Tiles[x][y];
            if (clickedTile.CountAdjacentFlags(this) == clickedTile.AdjacentMines
                && clickedTile.IsRevealed && !clickedTile.IsMine)
            {
                RevealAll(GetAdjacentTiles(clickedTile)
                    .FindAll(n => !n.IsFlagged));
            }
        }

        // highlight tile that mouse is over
        public void HandleMouseHover(int x, int y)
        {
            var hoveredTile = Tiles[x][y];
            if (hoveredTile != highlightedTile)
            {
                highlightedTile?.ToggleHighlight();
                highlightedTile = hoveredTile;
                highlightedTile.ToggleHighlight();
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
                        int newX = tile.Position.X + x;
                        int newY = tile.Position.Y + y;
                        if (newX >= 0 && newX < Width
                            && newY >= 0 && newY < Height)
                        {
                            adjacentTiles.Add(
                            Tiles[newX][newY]);
                        }
                    }
                }
            }
            return adjacentTiles;
        }

        // reveal all blanks in chain as well as numbers on edge
        public void RevealAllConnectedBlanks(Tile Tile)
        {
            List<Tile> connectedBlanks = new List<Tile>()
            {
                Tile
            };
            List<Tile> lastConnectedBlanks = GetAdjacentBlanks(Tile);

            while (true)
            {
                // add all blanks adjacent to last blanks to list
                List<Tile> newConnectedBlanks = new List<Tile>();
                foreach (var tile in lastConnectedBlanks)
                {
                    newConnectedBlanks.AddRange(GetAdjacentBlanks(tile));
                }
                // reveal last blanks
                RevealAll(lastConnectedBlanks);

                // add lastConnectedBlanks to connectedBlanks
                connectedBlanks.AddRange(lastConnectedBlanks);

                // set lastConnectedBlanks to newConnectedBlanks without duplicates
                if (newConnectedBlanks.Count > 0)
                {
                    lastConnectedBlanks = newConnectedBlanks.Distinct().ToList();
                }
                else
                {
                    break;
                }
            }
            // remove duplicates from connectedBlanks
            connectedBlanks = connectedBlanks.Distinct().ToList();
            foreach (var blank in connectedBlanks)
            {
                List<Tile> nonBlanks = GetAdjacentTiles(blank).FindAll(
                    t => t.AdjacentMines > 0 && !t.IsRevealed && !t.IsFlagged);
                RevealAll(nonBlanks);
            }
        }

        // return list of all tiles with mines
        public List<Tile> GetAllMines()
        {
            var mines = new List<Tile>();
            foreach (var list in Tiles)
            {
                mines.AddRange(list.FindAll(t => t.IsMine));
            }
            return mines;
        }

        // reveal all tiles in list
        public void RevealAll(List<Tile> tiles)
        {
            foreach (var tile in tiles)
            {
                tile.Reveal();
            }
        }

        // PRIVATE METHODS

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
        private void CreateMines(Point invalidPosition)
        {
            for (int m = 0; m < Mines; m++)
            {
                var x = Game.Random.Next(Width);
                var y = Game.Random.Next(Height);

                // if tile is not a mine and is not the invalid position, make mine
                if (!Tiles[x][y].IsMine &&
                    !Tiles[x][y].Position.Equals(invalidPosition))
                {
                    Tiles[x][y].IsMine = true;
                }
                // otherwise decrease loop number
                else
                {
                    m--;
                }
            }
        }

        // find all Tiles without mines or adjacent mines adjacent to Tile
        private List<Tile> GetAdjacentBlanks(Tile Tile)
        {
            List<Tile> adjacentBlanks = new List<Tile>();
            List<Tile> adjacentTiles = GetAdjacentTiles(Tile);

            foreach (var tile in adjacentTiles)
            {
                if (!tile.IsMine && !tile.IsRevealed && !tile.IsFlagged
                    && tile.CountAdjacentMines(this) == 0)
                {
                    adjacentBlanks.Add(tile);
                }
            }

            return adjacentBlanks;
        }
    }
}