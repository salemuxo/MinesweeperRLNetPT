using RLNET;
using SalemLib;
using System;
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
            if (IsFlagged)
            {
                SetConsoleSymbol(console, Colors.Flag, 'F');
            }
            else if (!IsRevealed)
            {
                SetConsoleSymbol(console, Colors.Undiscovered, 'X');
            }
            else if (IsMine)
            {
                SetConsoleSymbol(console, Colors.Mine, 'M');
            }
            else if (AdjacentMines != 0)
            {
                SetConsoleSymbolFromAdjMines(console);
            }
            else
            {
                SetConsoleSymbol(console, Colors.Background, 0);
            }
        }

        public void SetConsoleSymbol(RLConsole console, RLColor color, int symbol)
        {
            console.Set(Position.X, Position.Y, color, Colors.Background, symbol);
        }

        public void SetConsoleSymbolFromAdjMines(RLConsole console)
        {
            int charIndex = AdjacentMines + 48;
            SetConsoleSymbol(console, Colors.Number, charIndex);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Tile t = (Tile)obj;
                return t.Position.Equals(Position)
                    && t.IsMine == IsMine
                    && t.IsRevealed == IsRevealed
                    && t.IsFlagged == IsFlagged
                    && t.AdjacentMines == AdjacentMines;
            }
        }

        // generated GetHashCode override
        public override int GetHashCode()
        {
            int hashCode = -1020351265;
            hashCode = hashCode * -1521134295 + Position.GetHashCode();
            hashCode = hashCode * -1521134295 + IsMine.GetHashCode();
            hashCode = hashCode * -1521134295 + IsRevealed.GetHashCode();
            hashCode = hashCode * -1521134295 + IsFlagged.GetHashCode();
            hashCode = hashCode * -1521134295 + AdjacentMines.GetHashCode();
            return hashCode;
        }
    }
}
