using System;
using System.IO;
using System.Runtime.InteropServices;
using static System.Console;

namespace Snake
{
    class Program
    {
        const int MF_BYCOMMAND = 0x00000000;
        const int SC_MINIMIZE = 0xF020;
        const int SC_MAXIMIZE = 0xF030;
        const int SC_SIZE = 0xF000;

        [DllImport("user32.dll")]
        public static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();

        static void Main(string[] args)
        {
            DeleteMenu(GetSystemMenu(GetConsoleWindow(), false), SC_MINIMIZE, MF_BYCOMMAND);
            DeleteMenu(GetSystemMenu(GetConsoleWindow(), false), SC_MAXIMIZE, MF_BYCOMMAND);
            DeleteMenu(GetSystemMenu(GetConsoleWindow(), false), SC_SIZE, MF_BYCOMMAND);

            CursorVisible = false;
            Title = "Python";
            string[] mainMenuChoices = {"Play", "About", "Exit"};
            Menu mainMenu = new Menu(mainMenuChoices);

            while(true)
            {
              int mainMenuChoice = mainMenu.MenuLoop();
              switch(mainMenuChoice)
              {
                case 0:
                  LevelSelect();
                  break;
                case 1:
                  Write("By FrAg");
                  Write("\n\nPress any key to continue...");
                  ReadKey(true);
                  break;
                case 2:
                  Environment.Exit(0);
                  break;
              }
            }            
        }
        static void LevelSelect()
        {
            string[] levelsMenuChoices = { "Level 1", "Level 2", "Back"};
            Menu levelMenu = new Menu(levelsMenuChoices);
            Game currentGame;
            bool levelMenuOpen = true;

            do
            {
                int levelChosen = levelMenu.MenuLoop();
                switch (levelChosen)
                {
                    case 0:
                        currentGame = new Game();
                        currentGame.Start(Path.Combine("Levels", "Level1"));
                        levelMenuOpen = false;
                        break;
                    case 1:
                        currentGame = new Game();
                        currentGame.Start(Path.Combine("Levels", "Level2"));
                        levelMenuOpen = false;
                        break;
                    case 2:
                        levelMenuOpen = false;
                        break;
                }
            } while (levelMenuOpen);
        }
    }
}
