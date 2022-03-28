using System;
using System.Collections.Generic;
using System.IO;

namespace Loopover_Plus
{
    class Loopover
    {
        private Puzzle puzzle;
        private Data data;

        public Loopover(int size, Data data)
        {
            puzzle = new Puzzle(size, null);
            this.data = data;
        }

        public Loopover(int size, int[,] puzzle, Cursor cursor, string name, Data data)
        {
            this.puzzle = new Puzzle(size, puzzle, cursor, name);
            this.data = data;
        }

        public ConsoleKey Move(ConsoleKey key, bool win)
        {
            if (!win)
            {
                if ((ConsoleKey)data.Controls.Move.MUp == key)
                {
                    puzzle.MoveCol(EMove.Up);
                    return ConsoleKey.Spacebar;
                }
                else if ((ConsoleKey)data.Controls.Move.MDown == key)
                {
                    puzzle.MoveCol(EMove.Down);
                    return ConsoleKey.Spacebar;
                }
                else if ((ConsoleKey)data.Controls.Move.MLeft == key)
                {
                    puzzle.MoveRow(EMove.Left);
                    return ConsoleKey.Spacebar;
                }
                else if ((ConsoleKey)data.Controls.Move.MRight == key)
                {
                    puzzle.MoveRow(EMove.Right);
                    return ConsoleKey.Spacebar;
                }
                else if ((ConsoleKey)data.Controls.CursorData.CUp == key)
                {
                    puzzle.MoveCursor(EMove.Up);
                    return ConsoleKey.Spacebar;
                }
                else if ((ConsoleKey)data.Controls.CursorData.CDown == key)
                {
                    puzzle.MoveCursor(EMove.Down);
                    return ConsoleKey.Spacebar;
                }
                else if ((ConsoleKey)data.Controls.CursorData.CLeft == key)
                {
                    puzzle.MoveCursor(EMove.Left);
                    return ConsoleKey.Spacebar;
                }
                else if ((ConsoleKey)data.Controls.CursorData.CRight == key)
                {
                    puzzle.MoveCursor(EMove.Right);
                    return ConsoleKey.Spacebar;
                }

            }
            return key;
        }

        // Info
        public static void PrintCredits()
        {
            Console.WriteLine("Made by Nexus#3205");
            Console.WriteLine("I saw this coding problem in codewars.com :v");
            Console.WriteLine("Play The original game in https://openprocessing.org/sketch/580366");
            Console.WriteLine("The video https://www.youtube.com/watch?v=95rtiz-V2zM");
        }
        public static void PrintHelp(Data data)
        {
            Console.WriteLine("How to play");
            Console.WriteLine("Your task is to solve the puzzle in ascending order like this");
            Console.WriteLine("A B C D");
            Console.WriteLine("E F G H");
            Console.WriteLine("I J K L");
            Console.WriteLine("M N O P");
            Console.WriteLine("The board size is depends on what you want, size 2 to 5\n");
            Console.WriteLine("Use controls to solve the puzzle");
            Console.WriteLine("Notice the green highlighted letter, that is where your cursor is");
            Console.WriteLine("If you press {0}, letters in that column will move up", data.Controls.Move.MUp.ToString());
            Console.WriteLine("If you press {0}, letters in that column will move down", data.Controls.Move.MDown.ToString());
            Console.WriteLine("If you press {0}, letters in that row will move left", data.Controls.Move.MLeft.ToString());
            Console.WriteLine("If you press {0}, letters in that row will move rigth", data.Controls.Move.MRight.ToString());
            Console.WriteLine("\nControls");
            Console.WriteLine("Press {0}{1}{2}{3}(Up,Left,Down,Rigth) to move letters",
                data.Controls.Move.MUp.ToString(),
                data.Controls.Move.MLeft.ToString(),
                data.Controls.Move.MDown.ToString(),
                data.Controls.Move.MRight.ToString());
            Console.WriteLine("Press {0}/{1}/{2}/{3} Arrow Key to move the cursor",
                data.Controls.CursorData.CUp.ToString().Replace("Arrow", ""),
                data.Controls.CursorData.CLeft.ToString().Replace("Arrow", ""),
                data.Controls.CursorData.CDown.ToString().Replace("Arrow", ""),
                data.Controls.CursorData.CRight.ToString().Replace("Arrow", ""));
            Console.WriteLine("Press {0} to restart", data.Controls.Restart.ToString());
            Console.WriteLine("Press {0} to create new game", data.Controls.NewGame.ToString());
            Console.WriteLine("Press {0} to show help menu", data.Controls.Help.ToString());
            Console.WriteLine("Ones you save your current game it will automatically save");
            Console.WriteLine("\n\nAdditional:\nCustomizable ui and controls in next update i guess");
            Console.WriteLine("You get stuck? Well you are not smart enough :v to solve this or just try to ask me");
            Console.WriteLine("Size 2 is super easy but in Size 3 above");
            Console.WriteLine("Dm me if something wen't wrong i guess");
            Console.WriteLine("Well have fun! Lol!");
        }
        public static void ChangeLogs(Data data)
        {
            Console.WriteLine("-=V1.0=-");
            Console.WriteLine("The First release");
            Console.WriteLine("-=V2.0=-");
            Console.WriteLine("Addeds.");
            Console.WriteLine("  -Main Menu");
            Console.WriteLine("  -Game\\Data Saves");
            Console.WriteLine("  -Better Controls");
        }
        private static void Statistics(Data data)
        {
            Console.WriteLine($"Solves: {data.Solves}");
        }

        // Data Reload/Save
        public static Data LoadData()
        {
            const string path = "Data.json";
            if (File.Exists(path))
                return Data.FromJson(File.ReadAllText(path));

            Move move = new Move()
            {
                MUp = ConsoleKey.W,
                MDown = ConsoleKey.S,
                MLeft = ConsoleKey.A,
                MRight = ConsoleKey.D
            };

            CursorControl cursor = new CursorControl()
            {
                CUp = ConsoleKey.UpArrow,
                CDown = ConsoleKey.DownArrow,
                CLeft = ConsoleKey.LeftArrow,
                CRight = ConsoleKey.RightArrow
            };

            Controls control = new Controls()
            {
                CursorData = cursor,
                Move = move,
                Credits = ConsoleKey.C,
                ExitMenu = ConsoleKey.Spacebar,
                Help = ConsoleKey.H,
                NewGame = ConsoleKey.N,
                Restart = ConsoleKey.R,
                GameMenu = ConsoleKey.Oem3
            };

            return new Data()
            {
                Controls = control,
                Saves = new Dictionary<string, Save>(),
                Solves = 0
            };
        }
        public static void SaveData(Data data)
        {
            File.WriteAllText("Data.json", Serialize.ToJson(data));
        }

        // Utils
        public static void Clear() => Console.Clear();

        public static void Start()
        {
            const float ver = 2.0f;
            Console.Title = "Loopover - Made by Nexus v" + ver;
            // Game
            Loopover game = null;
            Data data = LoadData();
            int size = -1;
            bool win = false;

            EMenus menu = EMenus.Menu;
            EMenus prevMenu = EMenus.None;
            while (true)
            {
                Clear();
                switch (menu)
                {
                    case EMenus.Start:
                        {
                            try
                            {
                                Console.WriteLine("Enter -1 to cancel");
                                Console.Write("Enter a board size(2-20): ");
                                size = Convert.ToInt32(Console.ReadLine());
                                if (size > 1 && size <= 20)
                                {
                                    game = new Loopover(size, data);
                                    menu = EMenus.Game;
                                    win = false;
                                }
                                else if (size == -1)
                                    menu = prevMenu;
                            }
                            catch
                            {
                                continue;
                            }
                        }
                        break;
                    case EMenus.Game:
                        {
                            if (game.puzzle.Check() && !win)
                            {
                                win = true;
                                data.Solves += 1;
                                if (game.puzzle.Name != null)
                                    if (data.Saves.ContainsKey(game.puzzle.Name))
                                        data.Saves.Remove(game.puzzle.Name);
                            }
                            if (win)
                                Console.WriteLine("Puzzle Solve!!");
                            game.puzzle.Print();
                            ConsoleKey key = Console.ReadKey().Key;
                            ConsoleKey returnKey = game.Move(key, win);
                            if (returnKey != ConsoleKey.Spacebar)
                            {
                                if (data.Controls.NewGame == key)
                                {
                                    menu = EMenus.Start;
                                }
                                else if (data.Controls.GameMenu == key)
                                {
                                    menu = EMenus.GameMenu;
                                }
                                else if (data.Controls.Help == key)
                                {
                                    menu = EMenus.Help;
                                }
                            }
                            prevMenu = EMenus.Game;
                        }
                        break;
                    case EMenus.Help:
                        PrintHelp(data);
                        Console.WriteLine($"Press {data.Controls.ExitMenu} to exit");
                        if (Console.ReadKey().Key == data.Controls.ExitMenu)
                            menu = prevMenu;
                        break;
                    case EMenus.Credits:
                        PrintCredits();
                        Console.WriteLine($"Press {data.Controls.ExitMenu} to exit");
                        if (Console.ReadKey().Key == data.Controls.ExitMenu)
                            menu = prevMenu;
                        break;
                    case EMenus.Menu:
                        {
                            Console.WriteLine("Press a number key in the following");
                            Console.WriteLine("1) Start Game");
                            Console.WriteLine("2) Customize Controls");
                            Console.WriteLine("3) Help");
                            Console.WriteLine("4) Credits");
                            Console.WriteLine("5) Statictics");
                            Console.WriteLine("6) Change Logs");
                            ConsoleKey key = Console.ReadKey().Key;
                            switch (key)
                            {
                                case ConsoleKey.D1:
                                    menu = EMenus.GameSelection;
                                    break;
                                case ConsoleKey.D2:
                                    menu = EMenus.ChangeControls;
                                    break;
                                case ConsoleKey.D3:
                                    menu = EMenus.Help;
                                    break;
                                case ConsoleKey.D4:
                                    menu = EMenus.Credits;
                                    break;
                                case ConsoleKey.D5:
                                    menu = EMenus.Statistics;
                                    break;
                                case ConsoleKey.D6:
                                    menu = EMenus.ChangeLogs;
                                    break;
                            }
                            prevMenu = EMenus.Menu;
                            break;
                        }
                    case EMenus.ChangeControls:
                        Console.WriteLine("Coming Soon! ^^");
                        Console.WriteLine("Lol this part is hard to code lol");
                        Console.WriteLine($"Press {data.Controls.ExitMenu} to exit");
                        if (Console.ReadKey().Key == data.Controls.ExitMenu)
                            menu = prevMenu;
                        break;
                    case EMenus.GameSelection:
                        {
                            Console.WriteLine("Press a number key in the following");
                            Console.WriteLine("1) New Game");
                            Console.WriteLine("2) Load from saves");
                            Console.WriteLine($"Press {data.Controls.ExitMenu} to exit");
                            ConsoleKey key = Console.ReadKey().Key;
                            switch (key)
                            {
                                case ConsoleKey.D1:
                                    menu = EMenus.Start;
                                    break;
                                case ConsoleKey.D2:
                                    menu = EMenus.LoadASave;
                                    prevMenu = EMenus.GameSelection;
                                    break;
                            }
                            if (key == data.Controls.ExitMenu)
                                menu = prevMenu;
                            break;
                        }
                    case EMenus.GameMenu:
                        {
                            Console.WriteLine("Press a number key in the following");
                            Console.WriteLine("1) Continue");
                            Console.WriteLine("2) Save the current game");
                            Console.WriteLine("3) Restart");
                            Console.WriteLine("4) New Game");
                            Console.WriteLine("5) Load a game save");
                            Console.WriteLine("6) Exit");
                            ConsoleKey key = Console.ReadKey().Key;
                            switch (key)
                            {
                                case ConsoleKey.D1:
                                    menu = prevMenu;
                                    break;
                                case ConsoleKey.D2:
                                    menu = EMenus.Saving;
                                    prevMenu = EMenus.Game;
                                    break;
                                case ConsoleKey.D3:
                                    game = new Loopover(size, data);
                                    menu = EMenus.Game;
                                    win = false;
                                    break;
                                case ConsoleKey.D4:
                                    menu = EMenus.Start;
                                    prevMenu = EMenus.Game;
                                    break;
                                case ConsoleKey.D5:
                                    menu = EMenus.LoadASave;
                                    prevMenu = EMenus.Game;
                                    break;
                                case ConsoleKey.D6:
                                    menu = EMenus.Menu;
                                    game = null;
                                    size = -1;
                                    win = false;
                                    break;
                            }
                        }
                        break;
                    case EMenus.ChangeLogs:
                        ChangeLogs(data);
                        Console.WriteLine($"Press {data.Controls.ExitMenu} to exit");
                        if (Console.ReadKey().Key == data.Controls.ExitMenu)
                            menu = prevMenu;
                        break;
                    case EMenus.Statistics:
                        Statistics(data);
                        Console.WriteLine($"Press {data.Controls.ExitMenu} to exit");
                        if (Console.ReadKey().Key == data.Controls.ExitMenu)
                            menu = prevMenu;
                        break;
                    case EMenus.LoadASave:
                        {
                            if (data.Saves.Count == 0)
                            {
                                Console.WriteLine("Theres no save games");
                                Console.WriteLine($"Press {data.Controls.ExitMenu} to exit");
                                if (Console.ReadKey().Key == data.Controls.ExitMenu)
                                    menu = prevMenu;
                            }
                            else
                            {
                            s:
                                try
                                {
                                    Console.WriteLine("Enter a -1 number to exit");
                                    int num = -1;
                                    string[] keys = new string[data.Saves.Keys.Count];
                                    data.Saves.Keys.CopyTo(keys, 0);
                                    for (int i = 0; i < keys.Length; ++i)
                                    {
                                        Save temp = data.Saves[keys[i]];
                                        Console.WriteLine($"{i}) {temp.Name} {temp.Date}");
                                    }
                                    Console.Write("Enter a number: ");
                                    num = Convert.ToInt32(Console.ReadLine());
                                    if (num < -1 || num >= keys.Length)
                                        goto s;
                                    if (num == -1)
                                    {
                                        menu = prevMenu;
                                        continue;
                                    }
                                    Save save = data.Saves[keys[num]];
                                    game = new Loopover(save.Size, save.Puzzle, new Cursor(save.X, save.Y, save.Size), save.Name, data);
                                }
                                catch
                                {
                                    goto s;
                                }
                                menu = EMenus.Game;
                            }
                            break;
                        }
                    case EMenus.Saving:
                        {
                            if (!win)
                            {
                                if (data.Saves.ContainsKey(game.puzzle.Name))
                                {
                                    //string rep = game.puzzle.Name;
                                    //data.Saves[rep] = new Save(game.puzzle.puzzle, rep, game.puzzle.cursor.Size, game.puzzle.cursor.X, game.puzzle.cursor.Y);
                                }
                                else
                                {
                                start:
                                    Console.WriteLine("Enter a Name: ");
                                    string name = Console.ReadLine();
                                    if (name.Length <= 0 || data.Saves.ContainsKey(name))
                                        goto start;

                                    data.Saves.Add(name, new Save(game.puzzle.puzzle, name, game.puzzle.cursor.Size, game.puzzle.cursor.X, game.puzzle.cursor.Y));
                                }
                            }

                            menu = EMenus.Game;
                        }
                        break;
                }
                SaveData(data);
            }
        }

    }
}

