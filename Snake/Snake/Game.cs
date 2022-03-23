using System;
using System.IO;
using System.Threading;
using static System.Console;

namespace Snake
{
    class Game
    {
        private Level CurrentLevel;
        private bool Running;
        private Snake Player;
        private string newDir;
        private Food Food;
        private bool Ate = false;
        private int Score = 0;
        private int HighScore;
        public void Start(string path)
        { 
            CurrentLevel = new Level(Path.Combine(path, "level.txt"));            
            Player = new Snake(5, 9);
            newDir = "r";
            Food = new Food(CurrentLevel.Columns, CurrentLevel.Rows);
            try
            {
                string highScore = File.ReadAllLines(Path.Combine(path, "highscore.txt"))[0];
                HighScore = int.Parse(highScore);
            }
            catch(FileNotFoundException)
            {
                HighScore = 0;
            }       
            UpdateScore();
            while (Player.Body.Contains(Food.FoodPosition))
            {
                Food.GeneratePosition(CurrentLevel.Columns, CurrentLevel.Rows);
            }

            Running = true;
            CurrentLevel.Draw();
            Food.Draw();

            Thread thread = new Thread(UpdatePlayer);
            thread.IsBackground = true;
            thread.Start();

            while (Running)
            {
                if (KeyAvailable) 
                {
                    ConsoleKeyInfo keyPressed = ReadKey(true);
                    switch (keyPressed.Key)
                    {
                        case ConsoleKey.UpArrow or ConsoleKey.W:
                            if (Player.Dir != "d") { newDir = "u"; }
                            break;
                        case ConsoleKey.DownArrow or ConsoleKey.S:
                            if (Player.Dir != "u") { newDir = "d"; }
                            break;
                        case ConsoleKey.LeftArrow or ConsoleKey.A:
                            if (Player.Dir != "r") { newDir = "l"; }
                            break;
                        case ConsoleKey.RightArrow or ConsoleKey.D:
                            if (Player.Dir != "l") { newDir = "r"; }
                            break;
                    }
                }
                Thread.Sleep(1);
            }
            if (Score == HighScore)
            {
                File.WriteAllText(Path.Combine(path, "highscore.txt"), Score.ToString());
            }
            Player.Die();
            SetCursorPosition(0, CurrentLevel.Rows + 1);
            ForegroundColor = ConsoleColor.Red;
            Write("YOU DIED");
            ResetColor();
            Write("\n\n\nPress any key to continue...");
            while (KeyAvailable) { ReadKey(true); }
            ReadKey(true);
        }
        public void UpdateScore()
        {
            Position scorePointer = new Position(CurrentLevel.Columns + 5, 0);
            SetCursorPosition(scorePointer.X, scorePointer.Y);
            WriteLine("Score:");
            SetCursorPosition(scorePointer.X, scorePointer.Y + 1);
            Write(Score);
            if (Score > HighScore)
            {
                HighScore = Score;
                ForegroundColor = ConsoleColor.Green;
            }
            else { ForegroundColor = ConsoleColor.Red; }
            SetCursorPosition(scorePointer.X, scorePointer.Y + 3);
            Write("HighScore:");
            SetCursorPosition(scorePointer.X, scorePointer.Y + 4);
            Write(HighScore);
            ResetColor();
        }
        public void UpdatePlayer()
        {
            while (true)
            {
                Position nextHead = Player.NextHead(newDir);
                if (!CurrentLevel.IsWalkable(nextHead) || Player.Body.Contains(nextHead))
                {
                    Running = false;     
                    break;
                }
                Player.Move(nextHead, Ate);
                Ate = false;
                if (Player.Head == Food.FoodPosition) 
                { 
                    Ate = true;
                    Score += 5;
                    do
                    {
                        Food.GeneratePosition(CurrentLevel.Columns, CurrentLevel.Rows);
                    }
                    while (Player.Body.Contains(Food.FoodPosition) || !CurrentLevel.IsWalkable(Food.FoodPosition));
                    Food.Draw();
                    UpdateScore();
                }
                if (Player.Dir == "u" || Player.Dir == "d")
                {
                    Thread.Sleep(100);
                }
                else { Thread.Sleep(65); }
            }
        }
    }
}
