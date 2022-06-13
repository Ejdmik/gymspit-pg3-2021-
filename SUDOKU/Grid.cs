using System;
using System.Threading;
using System.Text;
using System.Linq;

namespace SUDOKU
{
    static class Grid
    {
        public static void PlayGame(char[,] sudoku, char[,] solvedSudoku, ref int x, ref int y, char keyInfo, char level)
        {
            if (level == 'e' || level == 'E')
            {
                if (keyInfo == solvedSudoku[x, y])
                {
                    sudoku[x, y] = keyInfo;
                    Console.SetCursorPosition(0, 22);
                    Console.WriteLine("Correct!");
                    int a, b;
                    FindEmptySpaceRight(sudoku, out a, out b, x, y, 1);
                    x = b;
                    y = a;
                }
                else
                {
                    Console.SetCursorPosition(2 + 4 * x, 3 + 2 * y);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(keyInfo);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(0, 22);
                    Console.WriteLine("Incorrect, try again!");
                }
                Console.SetCursorPosition(2 + 4 * x, 3 + 2 * y);
                Thread.Sleep(1000);
                Console.SetCursorPosition(0, 22);
                Console.WriteLine("                        ");
                Console.SetCursorPosition(2 + 4 * x, 3 + 2 * y);
            }
            else
            {
                if (Solving.IsValid(sudoku, x, y, keyInfo))
                {
                    sudoku[x, y] = keyInfo;
                    Console.SetCursorPosition(0, 22);
                    Console.WriteLine("So far so good.");
                    int a, b;
                    FindEmptySpaceRight(sudoku, out a, out b, x, y, 1);
                    x = b;
                    y = a;
                    Console.SetCursorPosition(2 + 4 * x, 3 + 2 * y); 
                    Thread.Sleep(1000);
                    Console.SetCursorPosition(0, 22);
                    Console.WriteLine("                        ");
                    Console.SetCursorPosition(2 + 4 * x, 3 + 2 * y);
                }
                else
                {
                    Console.SetCursorPosition(0, 22);
                    Console.WriteLine("Oops. There is a mistake somewhere.");
                    Console.WriteLine("The number {0} will disappear.", keyInfo);
                    Console.SetCursorPosition(2 + 4 * x, 3 + 2 * y); 
                    Thread.Sleep(2300);
                    Console.Write(" ");
                    Console.SetCursorPosition(0, 22);
                    Console.WriteLine("                                   \n                               ");
                    Console.SetCursorPosition(2 + 4 * x, 3 + 2 * y);
                }
                if (sudoku == solvedSudoku)
                {
                    Console.SetCursorPosition(0, 22);
                    Console.WriteLine("Congratulations! You've done it!");
                    Environment.Exit(0);
                }
            }
        }
        public static bool IsSolved(char[,] sudoku)
        {
            for (int i = 0; i < sudoku.GetLength(0); i++)
            {
                for (int j = 0; j < sudoku.GetLength(1); j++)
                {
                    if (sudoku[i, j] == '.') { return false; }
                }
            }
            return true;
        }
        public static void PrintGrid(char[,] sudoku, char[,] solvedSudoku)
        {
            Console.WriteLine("╔═══╤═══╤═══╦═══╤═══╤═══╦═══╤═══╤═══╗");
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    Console.WriteLine("║   │   │   ║   │   │   ║   │   │   ║");
                    Console.WriteLine("╟───┼───┼───╫───┼───┼───╫───┼───┼───╢");
                }
                Console.WriteLine("║   │   │   ║   │   │   ║   │   │   ║");
                Console.WriteLine("╠═══╪═══╪═══╬═══╪═══╪═══╬═══╪═══╪═══╣");
            }
            for (int j = 0; j < 2; j++)
            {
                Console.WriteLine("║   │   │   ║   │   │   ║   │   │   ║");
                Console.WriteLine("╟───┼───┼───╫───┼───┼───╫───┼───┼───╢");
            }
            Console.WriteLine("║   │   │   ║   │   │   ║   │   │   ║");
            Console.WriteLine("╚═══╧═══╧═══╩═══╧═══╧═══╩═══╧═══╧═══╝");


            for (int i = 0; i < 9; i += 1)
            {
                for (int j = 0; j < 9; j += 1)
                {
                    Console.SetCursorPosition(i * 4 + 2, j * 2 + 3);
                    if (sudoku[i, j] != '.')
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(sudoku[i, j].ToString());
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        if (solvedSudoku[i, j] == '.')
                        {
                            Console.Write(" ");
                        }
                        else
                        {
                            Console.Write(solvedSudoku[i, j].ToString());
                        }
                    }
                }
            }
        }

        public static void FindEmptySpaceUp(char[,] grid, out int a, out int b, int x, int y, int c)
        {
            a = 0; b = 0;
            if (IsSolved(grid) == false)
            {
                bool dot = false;
                for (int i = x; i >= 0; i--)
                {
                    for (int j = y; j >= 0; j--)
                    {
                        if (grid[i, j] == '.')
                        {
                            a = i;
                            b = j;
                            c++;
                            if (c > 1)
                            {
                                dot = true;
                                break;
                            }
                        }
                        y = 8;
                    }
                    if (dot) { break; }
                }
                if (!dot) { FindEmptySpaceUp(grid, out a, out b, 8, 8, 1); }
            }
            else
            {
                Console.WriteLine("Congratulations! You've done it! ");
                Thread.Sleep(2000);
                Environment.Exit(0);
            }
        }
        public static void FindEmptySpaceDown(char[,] grid, out int a, out int b, int x, int y, int c)
        {
            a = 0; b = 0;
            bool dot = false;
            for (int i = x; i < 9; i++)
            {
                for (int j = y; j < 9; j++)
                {
                    if (grid[i, j] == '.')
                    {
                        a = i;
                        b = j;
                        c++;
                        if (c > 1)
                        {
                            dot = true;
                            break;
                        }
                    }
                    y = 0;
                }
                if (dot) { break; }
            }
            if (!dot) { FindEmptySpaceDown(grid, out a, out b, 0, 0, 1); }
        }
        public static void FindEmptySpaceLeft(char[,] grid, out int a, out int b, int x, int y, int c)
        {
            a = 0; b = 0;
            bool dot = false;
            for (int i = y; i >= 0; i--)
            {
                for (int j = x; j >= 0; j--)
                {
                    if (grid[j, i] == '.')
                    {
                        a = i;
                        b = j;
                        c++;
                        if (c > 1)
                        {
                            dot = true;
                            break;
                        }
                    }
                    x = 8;
                }
                if (dot) { break; }
            }
            if (!dot) { FindEmptySpaceLeft(grid, out a, out b, 8, 8, 1); }
        }
        public static void FindEmptySpaceRight(char[,] grid, out int a, out int b, int x, int y, int c)
        {
            a = 0; b = 0;
            if (IsSolved(grid) == false)
            {
                bool dot = false;
                for (int i = y; i < 9; i++)
                {
                    for (int j = x; j < 9; j++)
                    {
                        if (grid[j, i] == '.')
                        {
                            a = i;
                            b = j;
                            c++;
                            if (c > 1)
                            {
                                dot = true;
                                break;
                            }
                        }
                        x = 0;
                    }
                    if (dot) { break; }
                }
                if (!dot) { FindEmptySpaceRight(grid, out a, out b, 0, 0, 1); }
            }
            else
            {
                Console.WriteLine("Congratulations! You've done it! ");
                Thread.Sleep(2000);
                Environment.Exit(0);
            }
        }

        public static void MovingInGrid(char[,] baseSudoku, char[,] sudoku, char[,] solvedSudoku, int x, int y, char level)
        {
            int a, b;
            int c = 0;
            char[] allowedchars = { '1', '2', '3', '4', '5', '6', '7', '8', '9', 'S', 'H' };
            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                if (keyInfo.KeyChar == 'S')
                {
                    Console.Clear();
                    Console.WriteLine("Here is the solution.\n");
                    PrintGrid(baseSudoku, solvedSudoku);
                    Console.WriteLine("\n");
                    Environment.Exit(0);
                }
                else if (keyInfo.KeyChar == 'H')
                {
                    Random r = new Random(Guid.NewGuid().GetHashCode());
                    FindEmptySpaceDown(sudoku, out a, out b, r.Next(0, 9), r.Next(0, 9), 1);
                    sudoku[a, b] = solvedSudoku[a, b];
                    baseSudoku[a, b] = sudoku[a, b];
                    Console.SetCursorPosition(2 + 4 * a, 3 + 2 * b);
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.Write(sudoku[a, b]);
                    Console.SetCursorPosition(0, 22);
                    Console.WriteLine("Hint applied!");
                    Thread.Sleep(1000);
                    Console.SetCursorPosition(2 + 4 * a, 3 + 2 * b);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(sudoku[a, b]);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(0, 22);
                    Console.WriteLine("             ");
                    FindEmptySpaceRight(sudoku, out a, out b, x, y, 1);
                    x = b;
                    y = a;
                    Console.SetCursorPosition(2 + 4 * x, 3 + 2 * y);
                }
                else if (allowedchars.Contains(keyInfo.KeyChar))
                {
                    if (c % 2 == 0) { Console.Write(keyInfo.KeyChar); c++; }
                    Console.SetCursorPosition(2 + 4 * x, 3 + 2 * y);
                    
                    PlayGame(sudoku, solvedSudoku, ref x, ref y, keyInfo.KeyChar, level);
                    
                    MovingInGrid(baseSudoku, sudoku, solvedSudoku, x, y, level);
                }
                else if (keyInfo.Key == ConsoleKey.Backspace || keyInfo.Key == ConsoleKey.Delete)
                {
                    Console.SetCursorPosition(2 + 4 * x, 3 + 2 * y);
                    if (level == 'e' || level == 'E')
                    {
                        if (sudoku[x, y] == '.') { Console.Write(" "); c++; }
                    }
                    else
                    {
                        if (baseSudoku[x, y] == '.') { Console.Write(" "); c++; sudoku[x, y] = '.'; }
                    }
                    Console.SetCursorPosition(2 + 4 * x, 3 + 2 * y);
                    MovingInGrid(baseSudoku, sudoku, solvedSudoku, x, y, level);
                }

                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    if (c % 2 == 1) { c++; }
                    FindEmptySpaceUp((level == 'e' || level == 'E') ? sudoku : baseSudoku, out a, out b, x, y, 0);
                    Console.SetCursorPosition(2 + 4 * a, 3 + 2 * b);
                    MovingInGrid(baseSudoku, sudoku, solvedSudoku, a, b, level);
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    if (c % 2 == 1) { c++; }
                    FindEmptySpaceDown((level == 'e' || level == 'E') ? sudoku : baseSudoku, out a, out b, x, y, 0);
                    Console.SetCursorPosition(2 + 4 * a, 3 + 2 * b);
                    MovingInGrid(baseSudoku, sudoku, solvedSudoku, a, b, level);
                }
                else if (keyInfo.Key == ConsoleKey.LeftArrow)
                {
                    if (c % 2 == 1) { c++; }
                    FindEmptySpaceLeft((level == 'e' || level == 'E') ? sudoku : baseSudoku, out a, out b, x, y, 0);
                    int temp = a;
                    a = b;
                    b = temp;
                    Console.SetCursorPosition(2 + 4 * a, 3 + 2 * b);
                    MovingInGrid(baseSudoku, sudoku, solvedSudoku, a, b, level);
                }
                else if (keyInfo.Key == ConsoleKey.RightArrow)
                {
                    if (c % 2 == 1) { c++; }
                    FindEmptySpaceRight((level =='e' || level =='E')?sudoku:baseSudoku, out a, out b, x, y, 0);
                    int temp = a;
                    a = b;
                    b = temp;
                    Console.SetCursorPosition(2 + 4 * a, 3 + 2 * b);
                    MovingInGrid(baseSudoku, sudoku, solvedSudoku, a, b, level);
                }
            }
        }
        public static void MovingInGridBetter (char[,] baseSudoku, char[,] sudoku, char[,] solvedSudoku, int x, int y, char level)
        {
            int c = 0;
            char[] allowedchars = { '1', '2', '3', '4', '5', '6', '7', '8', '9', 'S', 'H'};
            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                if (keyInfo.KeyChar == 'S')
                {
                    Console.Clear();
                    Console.WriteLine("Here is the solution.\n");
                    PrintGrid(baseSudoku, solvedSudoku);
                    Console.WriteLine("\n");
                    Environment.Exit(0);
                }
                else if (keyInfo.KeyChar == 'H')
                {
                    int a, b;
                    Random r = new Random(Guid.NewGuid().GetHashCode());
                    FindEmptySpaceDown(sudoku, out a, out b, r.Next(0, 9), r.Next(0, 9), 1);
                    sudoku[a, b] = solvedSudoku[a, b];
                    baseSudoku[a, b] = sudoku[a, b];
                    Console.SetCursorPosition(2 + 4 * a, 3 + 2 * b);
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.Write(sudoku[a, b]);
                    Console.SetCursorPosition(2 + 4 * x, 3 + 2 * y);
                    Thread.Sleep(1000);
                    Console.SetCursorPosition(2 + 4 * a, 3 + 2 * b);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(sudoku[a, b]);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(2 + 4 * x, 3 + 2 * y);
                }
                else if (allowedchars.Contains(keyInfo.KeyChar))
                {
                    if (baseSudoku[x, y] == '.')
                    {
                        if (c % 2 == 0) { Console.Write(keyInfo.KeyChar); c++; }
                        Console.SetCursorPosition(2 + 4 * x, 3 + 2 * y);

                        PlayGame(sudoku, solvedSudoku, ref x, ref y, keyInfo.KeyChar, level);

                        MovingInGridBetter(baseSudoku,sudoku, solvedSudoku, x, y, level);
                    }
                }
                else if (keyInfo.Key == ConsoleKey.Backspace || keyInfo.Key == ConsoleKey.Delete)
                {
                    Console.SetCursorPosition(2 + 4 * x, 3 + 2 * y);
                    if (level == 'e' || level == 'E')
                    {
                        if (sudoku[x, y] == '.') { Console.Write(" "); c++; }
                    }
                    else
                    {
                        if (baseSudoku[x, y] == '.') { Console.Write(" "); c++; sudoku[x, y] = '.'; }
                    }
                    Console.SetCursorPosition(2 + 4 * x, 3 + 2 * y);
                    MovingInGridBetter(baseSudoku,sudoku, solvedSudoku, x, y, level);
                }

                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    if (c % 2 == 1) { c++; }
                    if (x == 0 && y == 0) { x = 8; y = 8; }
                    else if (y == 0) { y = 8; x--; }
                    else { y--; }
                    Console.SetCursorPosition(2 + 4 * x, 3 + 2 * y);
                    MovingInGridBetter(baseSudoku,sudoku, solvedSudoku, x, y, level);
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    if (c % 2 == 1) { c++; }
                    if (x == 8 && y == 8) { x = 0; y = 0; }
                    else if (y == 8) { y = 0; x++; }
                    else { y++; }
                    Console.SetCursorPosition(2 + 4 * x, 3 + 2 * y);
                    MovingInGridBetter(baseSudoku,sudoku, solvedSudoku, x, y, level);
                }
                else if (keyInfo.Key == ConsoleKey.LeftArrow)
                {
                    if (c % 2 == 1) { c++; }
                    if (x == 0 && y == 0) { x = 8; y = 8; }
                    else if (x == 0) { x = 8; y--; }
                    else { x--; }
                    Console.SetCursorPosition(2 + 4 * x, 3 + 2 * y);
                    MovingInGridBetter(baseSudoku,sudoku, solvedSudoku, x, y, level);
                }
                else if (keyInfo.Key == ConsoleKey.RightArrow)
                {
                    if (c % 2 == 1) { c++; }
                    if (x == 8 && y == 8) { x = 0; y = 0; }
                    else if (x == 8) { x = 0;y++; }
                    else { x++; }
                    Console.SetCursorPosition(2 + 4 * x, 3 + 2 * y);
                    MovingInGridBetter(baseSudoku,sudoku, solvedSudoku, x, y, level);
                }
            }
        }
    }
}
