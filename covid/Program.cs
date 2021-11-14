using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace covid
{
    class Program
    {
        static void Add(ref int[] a)
        {
            Console.WriteLine("Enter as many positive integers as you want and then press Enter.");
            int count = a.Length;
            string input = Console.ReadLine();
            int result;

            while (input != "")
            {
                bool success = (int.TryParse(input, out result) && result >= 0);
                if (!success)
                {
                    Console.WriteLine("That is not a positive integer. \nEnter a positive integer value!");
                    success = int.TryParse(input, out result) && result >= 0;
                }
                else
                {
                    Array.Resize(ref a, a.Length + 1);
                    a[count-1] = result;
                    count++;
                }
                input = Console.ReadLine();
            }
        }
        static void Print(int [] a)
        {
            Array.Resize(ref a, a.Length - 1);
            Console.WriteLine("Your data:");
            for (int i = 0; i < a.Length; i++)
            {
                Console.WriteLine("Day {0}: {1}", i + 1, a[i]);
            }
        }
        static void Edit(ref int[] a)
        {        
             Console.WriteLine("Which day do you want to edit?");
             int res;
             bool success1 = int.TryParse(Console.ReadLine(), out res) && res > 0 && res <= a.Length;
             while (!success1)
             {
                 Console.WriteLine("That's not a positive integer or the number is out of range. Try again.");
                 success1 = int.TryParse(Console.ReadLine(), out res) && res > 0 && res <= a.Length;
             }
             Console.WriteLine("Enter a new value for day " + res + ".");
             int newres;
             bool success2 = int.TryParse(Console.ReadLine(), out newres) && newres > 0;
             while (!success2)
             {
                 Console.WriteLine("That is not a positive integer. \nEnter a new value for day " + res + ".");
                 success2 = int.TryParse(Console.ReadLine(), out newres) && newres > 0;
             }
             a[res - 1] = newres;
        }
        static void Clear(ref int [] a)
        {
            Console.WriteLine("How many days do you want to clear? Enter a negative number for clearing the newest data. Entering 0 will clear everything.");
            int resu;
            bool success3 = (int.TryParse(Console.ReadLine(), out resu) && Math.Abs(resu) <= a.Length);
            while (!success3)
            {
                Console.WriteLine("That's not an nonzero integer or the number is out of range. Try again.");
                success3 = (int.TryParse(Console.ReadLine(), out resu) && Math.Abs(resu) <= a.Length);
            }
            if (resu == 0)
            {
                Array.Resize(ref a, 1);
                a[0] = 0;
            }
            if (resu > 0)
            {
                Array.Reverse(a);
                Array.Resize(ref a, a.Length - resu);
                Array.Reverse(a);
            }
            if (resu < 0)
            {
                resu = Math.Abs(resu);
                Array.Resize(ref a, a.Length - resu); 
            }
        }
        static void Statistics(int[]a)
        {
            Array.Resize(ref a, a.Length - 1);
            int[] b = new int[a.Length];
            Array.Copy(a, b, a.Length);
            Console.Write("Enter a lower (>= 1) and upper (<= {0}) bound for the statistics.\nLower bound: ",a.Length);
            int l, u;
            bool successl = (int.TryParse(Console.ReadLine(), out l)) && l > 0 && l < b.Length;
            while (!successl)
            {
                Console.Write("Invalid input. Try again.\nLower bound: ");
                successl = (int.TryParse(Console.ReadLine(), out l)) && l > 0 && l < b.Length;
            }
            Console.Write("Upper bound: ");
            bool successu = (int.TryParse(Console.ReadLine(), out u)) && u > l && u <= b.Length;
            while (!successu)
            {
                Console.WriteLine("Invalid input. Try again.\nUpper bound: ");
                successu = (int.TryParse(Console.ReadLine(), out u)) && u > l && u <= b.Length;
            }
            Array.Resize(ref b, u);
            Array.Reverse(b);
            Array.Resize(ref b, b.Length - l + 1);
            Array.Reverse(b);
            for (int i = 0; i < b.Length; i++)
            {
                Console.WriteLine("Day {0}: {1}", l, b[i]);
                l++;
            }
            for (int i = 0; i < b.Length; i++)
            {
                l--;
            }
            Console.WriteLine();
            Console.WriteLine("Statistics:");
            Console.Write("Total amount of infected: ");
            Console.WriteLine(b.Sum());
            int max = b[0];
            int min = b[0];
            int imax = 0;
            int imin = 0;
            for (int k = 0; k < b.Length; k++)
            {
                if (b[k] > max)
                {
                    max = b[k];
                    imax = k;
                }
            }
            for (int k = 0; k < b.Length; k++)
            {
                if (b[k] < min)
                {
                    min = b[k];
                    imin = k;
                }
            }
            Console.Write("Minimum infected in one day: {0}        ", min);
            Console.WriteLine("It was in day number {0}.", imin + l);
            Console.Write("Maximum infected in one day: {0}        ", max);
            Console.WriteLine("It was in day number {0}.", imax + l);
            double mean = (double)b.Sum() / (double)b.Length;
            Console.WriteLine("Arithmetic mean: " + mean);
            const int czpeople = 10701777;
            double s = (double)b.Sum() * 100000 / czpeople;
            Console.WriteLine("Infected/100 000 people: " + s);
            
            for (int i = l; i<=u; i++)
            {
                if (i < 7)
                {
                    Console.WriteLine("Day {0}: {1}; Cannot count neither incidence nor simple R due to lack of data.", i, a[i-1]);
                }
                if (i>= 7 && i < 12)
                {
                    int num = 0;
                    for (int k = i; k > i - 7; k--)
                    {
                        num += a[k-1];

                    }
                    double seven = (double)num*100000 / czpeople;
                    Console.WriteLine("Day {0}: {1}; A7: {2}; Cannot count neither simple R nor A14 due to lack of data.", i, a[i-1], seven);
                }
                if (i>=12 && i < 14)
                {
                    int num = 0;
                    for (int k = i; k > i - 7; k--)
                    {
                        num += a[k-1];

                    }
                    double seven = (double)num*100000 / czpeople;
                    int rnum = 0;
                    int rdenom = 0;
                    for (int k = i; k > i - 7; k--)
                    {
                        rnum += a[k-1];
                        rdenom += a[k - 6];
                    }
                    double r1 = (double)rnum / (double)rdenom;
                    Console.WriteLine("Day {0}: {1}; A7: {2}; simple R: {3}; Cannot count A14 due to lack of data.", i, a[i-1], seven, r1);
                }
                if (i>=14)
                {
                    int num = 0;
                    for (int k = i; k > i - 7; k--)
                    {
                        num += a[k-1];

                    }
                    double seven = (double)num*100000 / czpeople;
                    int rnum = 0;
                    int rdenom = 0;
                    for (int k = i; k > i - 7; k--)
                    {
                        rnum += a[k-1];
                        rdenom += a[k - 6];
                    }
                    double r1 = (double)rnum / (double)rdenom;
                    int nm = 0;
                    for (int k = i; k > i - 14; k--)
                    {
                        nm += a[k-1];

                    }
                    double fourteen = (double)nm*100000 / czpeople;
                    Console.WriteLine("Day {0}: {1}; A7: {2}; simple R: {3}; A14: {4}", i, a[i-1], seven, r1, fourteen);
                }
            }
            Console.WriteLine();
            if (a.Length < 14)
            {
                Console.WriteLine("Predictions cannot be counted due to lack of data. You have to enter at least 14 days.");
            }
            else
            {
                Console.WriteLine("For how many days do you want to see prediction?");
                int days;
                bool success = int.TryParse(Console.ReadLine(), out days) && days > 0;
                while (!success)
                {
                    Console.WriteLine("Invalid input, try again.");
                    success = int.TryParse(Console.ReadLine(), out days) && days > 0;
                }
                int rnumerator = 0;
                int rdenominator = 0;
                for (int k = a.Length; k > a.Length - 7; k--)
                {
                    rnumerator += a[k-1];
                    rdenominator += a[k - 6];
                }
                double r = (double)rnumerator / (double)rdenominator;
                int[] c = new int[a.Length];
                Array.Copy(a, c, a.Length);
                int size = a.Length;
                for (int i = size; i < size + days; i++)
                {
                    Array.Resize(ref a, a.Length + 1);
                    Array.Resize(ref c, c.Length + 1);
                    double d = (r * (a[i - 6] + a[i - 7] + a[i - 8] + a[i - 9] + a[i - 10] + a[i - 11] + a[i - 5])) - (a[i - 2] + a[i - 3] + a[i - 4] + a[i - 5] + a[i - 6] + a[i - 1]);
                    a[i] = (int)Math.Round(d);
                    c[i] = (int)Math.Round((double)c[i-7]*c[i-7]/c[i-14]);
                    Console.WriteLine("Predicted day {0} by R: {1}; by ratio of last weeks: {2}", i + 1, a[i], c[i]);
                }
            }
        }
        static void Main(string[] args)
        {
            int[] ill = new int[1];
            bool options = false;
            int n = 1;
            int m = 0;
            int repeats = 0;
            while (m <=0)
            {
               if (repeats > 0)
                {
                    options = true;
                }
                Console.WriteLine("Choose what do you want to do: \n0 - exit\n1 - add data{0}", options ? "\n2 - print data\n3 - edit data\n4 - clear data\n5 - show statistics" : "");
                int choice;
                bool success = int.TryParse(Console.ReadLine(), out choice) && choice >= 0 && choice <= n; 
                while (!success)
                {
                    Console.WriteLine("Invalid input. Try again");
                    success = int.TryParse(Console.ReadLine(), out choice) && choice >= 0 && choice <= n;
                }
                switch (choice)
                {
                    case 0:
                        m = 1;
                        continue;
                    case 1:
                        Add(ref ill);
                        break;
                    case 2:
                        Console.Clear();
                        Print(ill);
                        break;
                    case 3:
                        Edit(ref ill);
                        break;
                    case 4:
                        Clear(ref ill);
                        break;
                    case 5:
                        Statistics(ill);
                        break;
                }
                repeats++;
                n = 5;
            }
        }
    }
}
