using RLNET;
using System.Collections;
using System.Collections.Generic;

namespace MinesweeperRLNetPT.Core
{
    public class MessageLog
    {
        private readonly int _width;
        private readonly int _height;
        private readonly Queue<string> _lines;

        public MessageLog(int width, int height)
        {
            _width = width;
            _height = height;
            _lines = new Queue<string>();
        }

        public void Add(string line)
        {
            _lines.Enqueue(line);

            if (_lines.Count > _height)
            {
                _lines.Dequeue();
            }
        }

        public void Draw(RLConsole logConsole)
        {
            logConsole.Clear();
            string[] lines = _lines.ToArray();
            for (int i = 0; i < lines.Length; i++)
            {
                logConsole.Print(0, i, lines[i], RLColor.White);
            }
        }
    }
}
