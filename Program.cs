using System;

namespace pgn2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Speak, friend and Enter:");
            string password = Console.ReadLine();

            if (password != "mellon")
            {
                Console.WriteLine("Wrong password!");

                return;
            }

            Console.WriteLine("You may now enter!");

            Console.WriteLine("Please input a number");
            string input = Console.ReadLine();
            int result;
            
            bool success = int.TryParse(input, out result);

            while (!success)
            {
                Console.WriteLine("\"{0}\" is not a number!", input);
                // uvozovka se píše takhle   \"
                Console.WriteLine("Please input a number!");
                input = Console.ReadLine();
                //musím změnit proměnné v podmínce, jinak by byl success pořád false a zacyklil bych se
                success = int.TryParse(input, out result);
            }

            Console.WriteLine("You entered: {0}", result);

            //každá proměná platí jen mezi nejbližšími {}
            bool success2;
            int result2;
            do
            {
                Console.WriteLine("Please input a positive number.");
                string input2 = Console.ReadLine();
                success2 = int.TryParse(input2, out result2) && result2 > 0;

                if (!success2)
                {
                    Console.WriteLine("You failed.");
                }
            } while (!success2);

            Console.WriteLine("You entered: {0}", result2);

            // a += b je to samé jako a = a + b
            // a += 1 je to samé jako a++, resp. ++a

            int sum = 0;
            //for (inicializace; podmíka; příkaz)
            for (int i = 1; i <= 10; i += 1)
            {
                sum += i;

                Console.WriteLine("i={0} sum is: {1}",i, sum);

            }
            Console.WriteLine("Sum is: {0}", sum);

            for (int k = 1; k <= 100; k += 1)
            {
                if (k % 15 == 0)
                {
                    Console.WriteLine("FizzBuzz");
                }
                else if (k % 3 == 0)
                {
                    Console.WriteLine("Fizz");
                }
                else if (k % 5 == 0)
                {
                    Console.WriteLine("Buzz");
                }
                
                else
                {
                    Console.WriteLine(k);
                }
            }
        }
    }
}
