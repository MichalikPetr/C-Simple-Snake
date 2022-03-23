using System;

namespace Snake
{
    class Position : IEquatable<Position>
    {
        public int X;
        public int Y;

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }
        public static bool operator ==(Position p1, Position p2)
        {
            return p1.X == p2.X && p1.Y == p2.Y;
        }
        public static bool operator !=(Position p1, Position p2)
        {
            return !(p1 == p2);
        }
        public bool Equals(Position other)
        {
            return this == other;
        }
    }
}
