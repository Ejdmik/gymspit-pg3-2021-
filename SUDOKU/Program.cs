using System;
using System.Threading;
using System.IO;

namespace SUDOKU
{
    class Program
    {
        static char ChooseLevel()
        {
            Console.WriteLine("Choose a level: \nE - easy (=checking every tile)\nH - hard (=checking against rules)");
            char level;
            while (true)
            {
                level = Console.ReadKey(true).KeyChar;
                if (level == 'e' || level == 'E') { Console.WriteLine("You chose level easy!"); break; }
                if (level == 'h' || level == 'H') { Console.WriteLine("You chose level hard!"); break; }
            }
            return level;
        }
        static char ChooseMoving()
        {
            Console.WriteLine("Choose how cursor moves in a grid: \n1 - skipping filled squares\n2 - you can move cursor on filled squares");
            char moving;
            while (true)
            {
                moving = Console.ReadKey(true).KeyChar;
                if (moving == '1') { Console.WriteLine("Cursor will skip filled squares!"); break; }
                if (moving == '2') { Console.WriteLine("Cursor won't skip filled squares!"); break; }
            }
            return moving;
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Default;
            Console.WriteLine("Welcome!");
            Console.WriteLine("Loading...");
            Random random = new Random(Guid.NewGuid().GetHashCode());
            int sudokuNumber = random.Next(0, 50);
            //text file - 50 different sudokus
            string baseDir = @"C:\Users\adysu\OneDrive\Plocha\programování\Programy\SUDOKU\sudoku_grids.txt";
            char[,] sudoku = new char[9, 9];
            using (TextReader tr = new StreamReader(baseDir))
            {
                for (int i = 0; i < sudokuNumber*10+1; i++)
                {
                    tr.ReadLine();
                }
                for (int i = 0; i < 9; i++)
                {
                    string line = tr.ReadLine();
                    for (int j = 0; j < 9; j++)
                    {
                        sudoku[j, i] = line[j];
                    }
                }
            }
            
            Thread.Sleep(1000);
            // dodělat loading screen
            char[,] solvedSudoku = (char[,])sudoku.Clone();
            char[,] baseSudoku = (char[,])sudoku.Clone();
            char level = ChooseLevel();
            char moving = ChooseMoving();
            Console.Clear();
            Console.WriteLine("Moving with →, ←, ↑, ↓. H = hint. S = shows the solution and ends the game. Good luck! \n");
            Grid.PrintGrid(sudoku, solvedSudoku);
            Solving.SolveSudoku(solvedSudoku);
            int a, b;
            Grid.FindEmptySpaceRight(sudoku, out a, out b, 0, 0, 1);
            int temp = a;
            a = b;
            b = temp;
            Console.SetCursorPosition(2 + 4 * a, 3 + 2 * b);

            if (moving == '1')
            {
                Grid.MovingInGrid(baseSudoku, sudoku, solvedSudoku, a, b, level);
            }
            else
            {
                Grid.MovingInGridBetter(baseSudoku, sudoku, solvedSudoku, a, b, level);
            }

            
        }

    }
}
