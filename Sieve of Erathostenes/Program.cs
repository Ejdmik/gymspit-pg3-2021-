using System;

namespace Sieve_of_Erathostenes
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a whole number, bigger than 2");
            string input = Console.ReadLine();
            int max;
            bool success = int.TryParse(input, out max) && max>2;
            while (!success)
            {
                Console.WriteLine("Thats not a whole number, bigger than 2!");
                Console.WriteLine("Enter a whole number, bigger than 2");
                input = Console.ReadLine();
                success = int.TryParse(input, out max) && max>2;
            }

            bool [] sieve = new bool [max + 1]; //protože se čísluje od nuly
            for (int i = 0; i < max; i++)
            {
                sieve[i] = true;
            }
            for (int p = 2; p < Math.Sqrt(max); p++)
            {
                if (sieve[p] == true)
                {
                    for (int j = p * p; j < max; j += p)
                    {
                        sieve[j] = false;
                    }
                }
            }
            Console.WriteLine("Prime numbers up to {0} are:", max);
            for (int i = 2; i < max; i++)
                {
                    if (sieve[i] == true)
                    {
                        Console.WriteLine(i);
                    }
                }
            int count = 0;
            for (int c = 2; c < max; c++)
            {
                if (sieve[c] == true)
                {
                    count++;
                }
            }
            Console.WriteLine("There are {0} prime numbers!", count);
        }
    }
}
