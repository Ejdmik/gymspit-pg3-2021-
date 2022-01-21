using System;
using System.IO;

namespace Exceptions
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseDir = @"C:\Users\adysu\OneDrive\Plocha\programování\Exceptions\";
            string[] array;
            string fileName;
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter a file name.");
                    fileName = Console.ReadLine();
                    array = File.ReadAllLines(baseDir + fileName + ".txt");
                    break;
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Try again.");
                }
            }
            int k = 9;
            do
            {
                try
                {
                    Console.WriteLine("Which row do you want to write? (your number must be in this interval <1; {0}>)\nInputing zero will close the program.",array.Length);
                    k = int.Parse(Console.ReadLine());
                    if (k == 0) { break; }
                }
                catch (FormatException e)
                {
                    Console.WriteLine(e.Message + "\nTry again.");
                    continue;
                }

                try
                {
                    string a = array[k - 1];
                    Console.WriteLine("Line {0}: " + a,k);
                }catch (IndexOutOfRangeException e)
                {
                    Console.WriteLine(e.Message + "\nTry again.");
                }
            } while (k != 0);
        }
    }
}
