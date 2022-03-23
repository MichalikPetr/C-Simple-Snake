using System;
using static System.Console;

namespace Snake
{
    class Menu
    {
        private string[] Choices;
        private int ChoiceIndex = 0;
        private Position MenuPosition = new Position(0, 0);    

        public Menu(string[] choices)
        {
            Choices = choices;    
        }
        public Menu(string[] choices, Position menuPosition)
        {
            Choices = choices;
            MenuPosition = menuPosition;
        }
    
        public int MenuLoop()
        {
            DrawMenu();
            ConsoleKeyInfo keyPressed;
            do
            {
                keyPressed = ReadKey(true);
                switch (keyPressed.Key)
                {
                    case ConsoleKey.UpArrow or ConsoleKey.W:
                        if (ChoiceIndex == 0) ChoiceIndex = Choices.Length - 1;
                        else ChoiceIndex--;
                        break;
                    case ConsoleKey.DownArrow or ConsoleKey.S:
                        if (ChoiceIndex == Choices.Length - 1) ChoiceIndex = 0;
                        else ChoiceIndex++;
                        break;
                }
                DrawMenu();
                while (KeyAvailable) { ReadKey(true); }
            }
            while(keyPressed.Key != ConsoleKey.Enter && keyPressed.Key != ConsoleKey.Spacebar);
            Clear();
            return ChoiceIndex;
        }

        private void DrawMenu()
        {
            Clear();
            for (int i = 0; i < Choices.Length; i++)
            {
                SetCursorPosition(MenuPosition.X, MenuPosition.Y + i);
                if (i == ChoiceIndex)
                {
                    ForegroundColor = ConsoleColor.Black;
                    BackgroundColor = ConsoleColor.White;
                    Write("> ");
                }
                Write(Choices[i]);
                ResetColor();
            }
        }
    }
}