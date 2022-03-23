using System.IO;
using static System.Console;

namespace Snake
{
    class Level
    {
        public string[,] Grid;
        public int Rows;
        public int Columns;
        public string LevelPath;
        public static string[,] LevelParser(string path)
        {
            string[] lines = File.ReadAllLines(path);
            string firstLine = lines[0];
            int rows = lines.Length;
            int columns = firstLine.Length;
            string[,] grid = new string[rows, columns];
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    grid[y, x] = lines[y][x].ToString();
                }
            }
            return grid;
        }
        public Level(string path)
        {
            LevelPath = path;
            Grid = LevelParser(path);
            Rows = Grid.GetLength(0);
            Columns = Grid.GetLength(1);
        }
        public void Draw()
        {
            for (int y = 0; y < Rows; y++)
            {
                for (int x = 0; x < Columns; x++)
                {
                    SetCursorPosition(x, y);
                    Write(Grid[y, x]);
                }
            }
        }
        public bool IsWalkable(Position gridPosition)
        {
            string item = Grid[gridPosition.Y, gridPosition.X];
            bool isWalkable = false;
            switch (item)
            {
                case " ":
                    isWalkable = true;
                    break;
                case "x":
                    isWalkable = false;
                    break;
            }

            return isWalkable;
        }


    }
}
