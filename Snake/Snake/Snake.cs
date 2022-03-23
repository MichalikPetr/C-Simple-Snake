using System;
using System.Collections.Generic;
using System.Threading;
using static System.Console;

namespace Snake
{
    class Snake
    {
        public string Dir = "r";
        public Position Head;
        public List<Position> Body;
        private string SnakeSymbol = "█";
        public Snake(int x, int y)
        {
            Head = new Position(x, y);
            Body = new List<Position>();
            Body.Add(new Position(x, y));
        }
        public void Draw()
        {
            ForegroundColor = ConsoleColor.Green;
            foreach(Position bodyPart in Body)
            {
                int x = bodyPart.X;
                int y = bodyPart.Y;
                SetCursorPosition(x, y);
                Write(SnakeSymbol);
            }
            ResetColor();
        }
        public void Die()
        {
            ForegroundColor = ConsoleColor.Red;    
            Body.Reverse();        
            foreach(Position bodyPart in Body)
            {
                int x = bodyPart.X;
                int y = bodyPart.Y;
                SetCursorPosition(x, y);
                Write(SnakeSymbol);
                Thread.Sleep(100);
            }
            ResetColor();
                      
        }
        public Position NextHead(string dir)
        {
            Dir = dir;
            Position nextHead = new Position(Head.X, Head.Y);
            switch (Dir)
            {
                case "u":
                    nextHead.Y--;
                    break;
                case "d":
                    nextHead.Y++;
                    break;
                case "l":
                    nextHead.X--;
                    break;
                case "r":
                    nextHead.X++;
                    break;
            }
            return nextHead;
        }
        public void Move(Position nextHead, bool ate)
        {
            Head.X = nextHead.X;
            Head.Y = nextHead.Y;
            if (!ate)
            {
                Position tail = Body[0];
                SetCursorPosition(tail.X, tail.Y);
                Write(" ");
                Body.RemoveAt(0);
            }
            ForegroundColor = ConsoleColor.Green;
            Body.Add(new Position(Head.X, Head.Y));
            SetCursorPosition(Head.X, Head.Y);
            Write(SnakeSymbol);
            ResetColor();
        }
    }
}
