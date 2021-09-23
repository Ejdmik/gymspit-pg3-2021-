using System;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {



                char sign;
                do
                {
                    Console.Clear();
                    Console.WriteLine("Please enter a sign of math operation (+, -, *, /, %) or press 0 for end.");
                    sign = Console.ReadLine()[0];
                    if (sign == '0')
                        return;
                } while (!(sign == '+' || sign == '-' || sign == '*' || sign == '/' || sign == '%'));



                Console.WriteLine("Enter number one.");
                string input = Console.ReadLine();
                double a;
                bool success = double.TryParse(input, out a);

                while (!success)
                {
                    Console.WriteLine("\"{0}\" is not a number.", input);
                    Console.WriteLine("Please enter a number!");
                    input = Console.ReadLine();

                    success = double.TryParse(input, out a);
                }


                Console.WriteLine("Enter number two.");

                string input2 = Console.ReadLine();
                double b;
                bool success2 = double.TryParse(input2, out b);

                while (!success2)
                {
                    Console.WriteLine("\"{0}\" is not a number.", input2);
                    Console.WriteLine("Please enter a number!");
                    input2 = Console.ReadLine();

                    success2 = double.TryParse(input2, out b);
                }


                double result = 0;
                
                if (sign == '/' && b == 0)
                {
                    Console.WriteLine("Nulou nelze dělit");
                    Console.WriteLine("Press any key to repeat the process");
                    Console.ReadKey();
                    continue;
                }
                else if (sign == '%' && b == 0)
                {
                    Console.WriteLine("Nulou nelze dělit");
                    Console.WriteLine("Press any key to repeat the process");
                    Console.ReadKey();
                    continue;
                }

                
                if (sign == '+')
                {
                    result = a + b;
                }
                else if (sign == '-')
                {
                    result = a - b;
                }
                else if (sign == '*')
                {
                    result = a * b;
                }
                else if (sign == '/')
                {
                    result = a / b;
                }
                else if (sign == '%')
                {
                    result = a % b;
                }

                Console.WriteLine("{0} {1} {2} = {3} ", a, sign, b, result);
                Console.WriteLine("Press any key to repeat the process");
                Console.ReadKey();



            } while (true);
        }
    }
}
