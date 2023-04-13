using RLNET;

namespace MinesweeperRLNetPT.Core
{
    public class Message
    {
        public string Line { get; set; }
        public int X { get; set; }
        public RLColor Color { get; set; }

        public Message(string line, int x, RLColor color)
        {
            Line = line;
            X = x;
            Color = color;
        }
    }
}
