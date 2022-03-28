using System;
using System.Collections.Generic;

namespace Loopover_Plus
{
    public class Puzzle
    {
        public string Name { get; set; }
        private readonly Random rand;
        private readonly int size;
        public int[,] puzzle;
        public readonly Cursor cursor;

        public Puzzle(int size, string name)
        {
            Name = name;
            rand = new Random();
            cursor = new Cursor(0, 0, size);
            this.size = size;
            Generate(true);
        }
        public Puzzle(int size, int[,] puzzle, Cursor cursor, string name)
        {
            Name = name;
            this.size = size;
            this.puzzle = puzzle;
            this.cursor = cursor;
        }

        public void MoveRow(EMove key)
        {
            int len = puzzle.GetLength(0);
            switch (key)
            {
                case EMove.Left:
                    for (int X = len - 1; X >= 0; --X)
                    {
                        int temp = puzzle[cursor.Y, len - 1];
                        puzzle[cursor.Y, len - 1] = puzzle[cursor.Y, X];
                        puzzle[cursor.Y, X] = temp;
                    }
                    cursor.Move(key);
                    break;
                case EMove.Right:
                    for (int X = 0; X < len; ++X)
                    {
                        int temp = puzzle[cursor.Y, len - 1];
                        puzzle[cursor.Y, len - 1] = puzzle[cursor.Y, X];
                        puzzle[cursor.Y, X] = temp;
                    }
                    cursor.Move(key);
                    break;
                default:
                    break;
            }
        }
        public void MoveCol(EMove key)
        {
            int len = puzzle.GetLength(0);
            switch (key)
            {
                case EMove.Up:
                    for (int Y = len - 1; Y >= 0; --Y)
                    {
                        int temp = puzzle[len - 1, cursor.X];
                        puzzle[len - 1, cursor.X] = puzzle[Y, cursor.X];
                        puzzle[Y, cursor.X] = temp;
                    }
                    cursor.Move(key);
                    break;
                case EMove.Down:
                    for (int Y = 0; Y < len; ++Y)
                    {
                        int temp = puzzle[len - 1, cursor.X];
                        puzzle[len - 1, cursor.X] = puzzle[Y, cursor.X];
                        puzzle[Y, cursor.X] = temp;
                    }
                    cursor.Move(key);
                    break;
                default:
                    break;
            }
        }
        public void MoveCursor(EMove dir)
        {
            cursor.Move(dir);
        }
        public void Print()
        {
            Console.ResetColor();
            for (int i = 0; i < size; ++i)
            {
                for (int j = 0; j < size; ++j)
                {
                    if ((cursor.Y, cursor.X) == (i, j)) Console.ForegroundColor = ConsoleColor.Green;
                    else Console.ResetColor();
                    Console.Write(ToString(puzzle[i, j]) + " ");
                }
                Console.WriteLine();
            }
            Console.ResetColor();
        }
        public bool Check()
        {
            bool start = true;
            int min = puzzle[0, 0];
            for (int i = 0; i < size; ++i)
            {
                for (int j = 0; j < size; ++j)
                {
                    if (start)
                    {
                        start = false;
                        continue;
                    }
                    if (puzzle[i, j] != ++min)
                        return false;
                }
            }
            return true;
        }
        public int[,] GetPuzzle()
        {
            return puzzle;
        }

        private string ToString(int i)
        {
            return puzzle.GetLength(0) <= 5 ?
                ((char)i).ToString() :
                i.ToString().PadRight((size * size).ToString().Length, ' ');
        }
        private void Generate(bool isRand)
        {
            if (size <= 1) return; // size 1 is Already Solve Lol
            if (size > 20) return; // size 20 is too much for size of the terminal
            puzzle = new int[size, size];

            int letter = 'A';
            int count = 1;
            Dictionary<int, int> values = new Dictionary<int, int>();
            for (int i = 0; i < size; ++i)
            {
                for (int j = 0; j < size; ++j)
                {
                    if (isRand)
                    {
                        int temp;
                        do
                        {
                            if (size <= 5)
                                temp = rand.Next(letter, letter + (size * size));
                            else
                                temp = rand.Next(1, (size * size) + 1);
                        }
                        while (values.ContainsKey(temp));
                        puzzle[i, j] = temp;
                        values.Add(temp, temp);
                    }
                    else
                    {
                        if (size <= 5)
                            puzzle[i, j] = letter++;
                        else
                            puzzle[i, j] = count++;
                    }
                }
            }
        }
    }
}

