using System;
using static System.Console;

namespace Snake
{
    class Food
    {
        private string FoodSymbol = "O";
        public Position FoodPosition;
        Random rnd = new Random();
        public Food(int rangeX, int rangeY)
        {
            Random rnd = new Random();
            int foodX = rnd.Next(1, rangeX - 1);
            int foodY = rnd.Next(1, rangeY - 1);
            FoodPosition = new Position(foodX, foodY);
        }
        public void GeneratePosition(int rangeX, int rangeY)
        {
            FoodPosition.X = rnd.Next(1, rangeX - 1);
            FoodPosition.Y = rnd.Next(1, rangeY - 1);
        }
        public void Draw()
        {
            ForegroundColor = ConsoleColor.Red;
            SetCursorPosition(FoodPosition.X, FoodPosition.Y);
            Write(FoodSymbol);
            ResetColor();
        }
    }
}
