using System;
using System.IO;

namespace jazykovy_slovnik
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseDir = @"C:\Users\adysu\OneDrive\Plocha\programování\Programy\jazykovy_slovnik\";
            string[] array = File.ReadAllLines(baseDir + "slovicka.txt");
            string separator = ", ";
            string[] czech = new string[array.Length];
            string[] foreign = new string[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                string[] strlist = array[i].Split(separator);
                czech[i] = strlist[0];
                foreign[i] = strlist[1];
            }
            Console.Clear();
            char choice;
            int validWords = array.Length;
            while (true)
            {
                Random rnd = new Random(Guid.NewGuid().GetHashCode());
                int i = rnd.Next(array.Length);
                if (validWords== 0) { Console.WriteLine("Šikulka, už umíš všechno.");break; }
                while (czech[i] == "") {
                    if (i == 0) { i = array.Length; }
                    i--;
                }
                Console.WriteLine("Napiš německy: {0}. Napiš \"idk\" jestli nevíš.", czech[i]);
                string answer = Console.ReadLine();
                while (answer != foreign[i])
                {
                    if (answer == "idk")
                    {
                        Console.WriteLine("{0} se německy řekne {1}", czech[i], foreign[i]);
                        Console.WriteLine("Zmáčkni cokoliv pro pokračování...");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    }
                    Console.WriteLine("Ajeeje, chybička. Zkus to znovu.");
                    Console.WriteLine("Napiš německy: {0}", czech[i]);
                    answer = Console.ReadLine();
                }
                if (answer != "idk")
                {
                    Console.WriteLine("Správně! :)");
                    Console.WriteLine("Zmáčki \"n\" pro nechání slovíčka, \"v\" pro vyřazení nebo \"k\" pro konec zkoušení.");
                    choice = Console.ReadKey(true).KeyChar;
                    while (true)
                    {
                        if (choice == 'n' || choice == 'v' || choice == 'k') { Console.WriteLine(choice); break; }
                        choice = Console.ReadKey(true).KeyChar;
                    }
                    if (choice == 'v')
                    {
                        czech[i] = "";
                        validWords--;
                    }
                    if (choice == 'k') { break; }
                    Console.Clear();
                }
            } 
        }
    }
}
