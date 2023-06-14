using RLNET;
using SalemLib;
using System.Diagnostics;

namespace MinesweeperRLNetPT.Core
{
    public class Tile
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsMine { get; set; }
        public bool IsRevealed { get; set; }
        public bool IsFlagged { get; set; }
        public bool IsHighlighted { get; set; }
        public int AdjacentMines { get; set; }

        private RLColor Color;
        private RLColor BgColor;

        public Tile(int x, int y)
        {
            X = x;
            Y = y;
            IsRevealed = false;
            IsMine = false;
            UpdateColor();
            UpdateBgColor();
        }

        // PUBLIC METHODS

        // count number of mines adjacent to tile
        public int CountAdjacentMines()
        {
            var adjacentTiles = Game.Map.GetAdjacentTiles(this);
            int mineCount = adjacentTiles.FindAll(x => x.IsMine).Count;

            AdjacentMines = mineCount;
            return mineCount;
        }

        // count number of flags adjacent to tile
        public int CountAdjacentFlags()
        {
            var adjacentTiles = Game.Map.GetAdjacentTiles(this);
            int flagCount = adjacentTiles.FindAll(x => x.IsFlagged).Count;

            return flagCount;
        }

        // draw tile at position to console
        public void Draw(RLConsole console)
        {
            if (Game.IsPlaying)
            {
                if (IsFlagged)
                {
                    SetConsoleSymbol(console, 'F');
                }
                else if (!IsRevealed)
                {
                    SetConsoleSymbol(console, 'X');
                }
                else if (IsMine)
                {
                    SetConsoleSymbol(console, 'M');
                }
                else if (AdjacentMines != 0)
                {
                    SetConsoleSymbolFromAdjMines(console);
                }
                else
                {
                    SetConsoleSymbol(console, 0);
                }
            }
            else
            {
                if (IsMine)
                {
                    SetConsoleSymbol(console, 'M');
                }
                else if (IsFlagged)
                {
                    SetConsoleSymbol(console, 'F');
                }
            }
        }

        // reveal tile, check if mine
        public void Reveal()
        {
            if (!IsRevealed)
            {
                IsRevealed = true;
                UpdateColor();
                Game.Map.RevealedTiles++;
                //Game.MessageLog.Add($"{Game.Map.RevealedTiles} Revealed");

                if (IsMine)
                {
                    if (Game.IsPlaying)
                    {
                        Game.EndGame();
                    }
                }
                else
                {
                    int adjacentMines = CountAdjacentMines();
                    if (adjacentMines == 0)
                    {
                        Game.Map.RevealAllConnectedBlanks(this);
                    }
                }
            }
        }

        // toggle flag and update color
        public void ToggleFlag()
        {
            IsFlagged = !IsFlagged;
            UpdateColor();
        }

        // toggle highlighted (hovered tile)
        public void ToggleHighlight()
        {
            IsHighlighted = !IsHighlighted;
            UpdateBgColor();
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
                return t.X == X && t.Y == Y
                    && t.IsMine == IsMine
                    && t.IsRevealed == IsRevealed
                    && t.IsFlagged == IsFlagged
                    && t.AdjacentMines == AdjacentMines;
            }
        }

        // generated GetHashCode override
        public override int GetHashCode()
        {
            int hashCode = -1005644505;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            hashCode = hashCode * -1521134295 + IsMine.GetHashCode();
            hashCode = hashCode * -1521134295 + IsRevealed.GetHashCode();
            hashCode = hashCode * -1521134295 + IsFlagged.GetHashCode();
            hashCode = hashCode * -1521134295 + IsHighlighted.GetHashCode();
            hashCode = hashCode * -1521134295 + AdjacentMines.GetHashCode();
            hashCode = hashCode * -1521134295 + Color.GetHashCode();
            hashCode = hashCode * -1521134295 + BgColor.GetHashCode();
            return hashCode;
        }

        // PRIVATE METHODS

        // set symbol at tiles position
        private void SetConsoleSymbol(RLConsole console, int symbol)
        {
            console.Set(X, Y, Color, BgColor, symbol);
        }

        // set symbol based on number of mines
        private void SetConsoleSymbolFromAdjMines(RLConsole console)
        {
            int charIndex = AdjacentMines + 48;
            SetConsoleSymbol(console, charIndex);
        }

        // update color based on flag, mine, undiscovered, number
        private void UpdateColor()
        {
            if (IsFlagged)
            {
                Color = Colors.Flag;
            }
            else if (!IsRevealed)
            {
                Color = Colors.Undiscovered;
            }
            else if (IsMine)
            {
                Color = Colors.Mine;
            }
            else
            {
                Color = Colors.Number;
            }
        }

        // update background color based on highlighted
        private void UpdateBgColor()
        {
            if (IsHighlighted)
            {
                BgColor = Colors.HBackground;
            }
            else
            {
                BgColor = Colors.Background;
            }
        }
    }
}