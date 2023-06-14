using RLNET;
using System.Collections;
using System.Collections.Generic;

namespace MinesweeperRLNetPT.Core
{
    public class MessageLog
    {
        private readonly int _width;
        private readonly int _height;
        private readonly Queue<Message> _lines;

        public MessageLog(int width, int height)
        {
            _width = width;
            _height = height;
            _lines = new Queue<Message>();
        }

        // add message with default colour
        public void Add(string line)
        {
            Message newMessage = new Message(line, GetCenteredPos(line), RLColor.White);
            _lines.Enqueue(newMessage);

            if (_lines.Count > _height)
            {
                _lines.Dequeue();
            }
        }

        // add message with specified colour
        public void Add(string line, RLColor color)
        {
            Message newMessage = new Message(line, GetCenteredPos(line), color);
            _lines.Enqueue(newMessage);

            if (_lines.Count > _height)
            {
                _lines.Dequeue();
            }
        }

        // draw log to logConsole
        public void Draw(RLConsole logConsole)
        {
            logConsole.Clear();
            Message[] lines = _lines.ToArray();
            for (int i = 0; i < lines.Length; i++)
            {
                Message message = lines[i];
                logConsole.Print(0, i, message.Line, message.Color);
            }
        }

        // get centered position in log
        private int GetCenteredPos(string str)
        {
            return (_width - str.Length) / 2;
        }
    }
}