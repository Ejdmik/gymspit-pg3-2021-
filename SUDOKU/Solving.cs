using System;
using System.Text;

namespace SUDOKU
{
    static class Solving
    {
        
        public static bool SolveSudoku(char[,]sudoku)
        {
            for (int i = 0; i < sudoku.GetLength(0); i++)
            {
                for (int j = 0; j < sudoku.GetLength(1); j++)
                {
                    if (sudoku[i, j] == '.')
                    {
                        for (char c = '1'; c <= '9'; c++)
                        {
                            if (IsValid(sudoku, i, j, c))
                            {
                                sudoku[i, j] = c;

                                if (SolveSudoku(sudoku)) { return true; }
                                else {sudoku[i, j] = '.';}
                            }
                        }
                        return false;
                    }
                }
            }
            return true;
        }
        public static bool IsValid(char[,] sudoku, int row, int col, char c)
        {
            for (int i = 0; i < 9; i++)
            {
                //check row  
                if (sudoku[i, col] != '.' && sudoku[i, col] == c)
                    return false;
                //check column  
                if (sudoku[row, i] != '.' && sudoku[row, i] == c)
                    return false;
                //check 3*3 block  
                if (sudoku[3 * (row / 3) + i / 3, 3 * (col / 3) + i % 3] != '.' && sudoku[3 * (row / 3) + i / 3, 3 * (col / 3) + i % 3] == c)
                    return false;
            }
            return true;
        }
        

        
    }
}
