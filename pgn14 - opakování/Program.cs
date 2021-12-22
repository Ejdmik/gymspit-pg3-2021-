using System;
using System.IO;

namespace pgn14___opakování
{
	static class RandomExtensions
	{
		public static void Shuffle<T>(this Random rnd, T[] array)
		{
			int n = array.Length;
			while (n > 1)
			{
				int k = rnd.Next(n--);
				T temp = array[n];
				array[n] = array[k];
				array[k] = temp;
			}
		}
	}
	class Program
    {
		static string ChooseString(string[] lines,ref int p)
        {
			Random ran = new Random(p++);
			int r = ran.Next(1, lines.Length + 1);
			string s = lines[r - 1];
			return s;
        }
		static string[] ReadLines(TextReader tr)
		{
			string[] lines = new string[10];
			int count = 0;
			string line;

			
			while ((line = tr.ReadLine()) != null)
			{
				if (count >= lines.Length)
				{
					Array.Resize(ref lines, lines.Length*2);
				}
				lines[count] = line;
				count += 1;
			}

			Array.Resize(ref lines, count);
			return lines;
		}
		static string[] ReadFile(string fileName)
		{
			using (TextReader reader = new StreamReader(fileName))
			{
				return ReadLines(reader);
			}
		}
		static void WriteLines(TextWriter tw, string[] lines)
		{
			foreach (string line in lines)
			{
				tw.WriteLine(line);
			}
		}


		static void WriteFile(string fileName, string[] lines, bool append = false)
		{
			
			using (TextWriter writer = new StreamWriter(fileName, append))
			{
				WriteLines(writer, lines);
			}
		}

		
		static void Main(string[] args)
        {
			string baseDir = @"C:\Users\adysu\OneDrive\Plocha\programování\pgn14 - opakování\";

			string[] array;
			using (TextReader reader = new StreamReader(baseDir + "rgb.txt"))
			{
				array = ReadLines(reader);
				
			}
			string[] arraynew = array;
			Console.WriteLine("Data from your file:");
			bool semicolon = true;
			for (int i = 0; i<array.Length; i++)
			{
				if (i == array.Length -1)
                {
					semicolon = false;
                }
				Console.Write(array[i] + "{0}", semicolon?", ":" ");
			}
			Console.WriteLine();
			int end;
			int countr = 0;
			int countg = 0;
			int countb = 0;
			int input;
			int k = 0;
			int n = 0;
			int p = 2;
			do
			{ 
				Console.WriteLine("Enter 1 for randomizing with repetition or 2 for randomizing without repetition.");
				bool success1 = int.TryParse(Console.ReadLine(), out input);
				while (!success1)
				{
					Console.WriteLine("Invalid input, try again.");
					success1 = int.TryParse(Console.ReadLine(), out input);
				}
				
			
				switch (input)
				{
					case 1:
						end = 0;
						Console.WriteLine("Enter a positive integer.");
						bool success2 = int.TryParse(Console.ReadLine(), out k) && k > 0;
						while (!success2)
						{
							Console.WriteLine("Invalid input, try again!");
							success2 = int.TryParse(Console.ReadLine(), out k) && k > 0;
						}
						string[] array1 = new string[arraynew.Length];
						Array.Copy(arraynew, array1, arraynew.Length);
						Array.Resize(ref array1, k);
						for (int i = array.Length; i<k; i++)
                        {
							string str = ChooseString(array,ref p);
							array1[i] = str;
                        }
						TextWriter wr = new StreamWriter(baseDir + "vystup.txt", false);
						try
						{
							foreach (string item in array1)
							{
								wr.WriteLine(item);
							}
						}
						finally
						{
							Console.WriteLine("Level 1 DONE! \nLook into your file.");
							if (wr != null)
							{
								wr.Dispose();
							}
						}
						Array.Resize(ref arraynew, array1.Length);
						Array.Copy(array1, arraynew, array1.Length);
						break;
					case 2:
						end = 0;
						var rnd = new Random();
						rnd.Shuffle(arraynew);

						Console.WriteLine("Enter a positive integer, smaller than or equal to {0}.",array.Length);
						bool success = int.TryParse(Console.ReadLine(), out n) && n <= array.Length && n > 0;
						while (!success)
						{
							Console.WriteLine("Invalid input, try again!");
							success = int.TryParse(Console.ReadLine(), out n) && n <= array.Length && n > 0;
						}

						Array.Resize(ref arraynew, n);


						TextWriter writer = new StreamWriter(baseDir + "vystup.txt", false);
						try
						{
							foreach (string item in arraynew)
							{
								writer.WriteLine(item);
							}
						}
						finally
						{
							Console.WriteLine("Level 1 DONE! \nLook into your file.");
							if (writer != null)
							{
								writer.Dispose();
							}
						}
						break;
					default:
						end = 1;
						Console.WriteLine("Wrong number, try again.");
						break;
				}
			} while (end != 0);

			foreach (string item in arraynew)
            {
				if (item == "r")
                {
					countr++;
                }
				if (item == "g")
				{
					countg++;
				}
				if (item == "b")
				{
					countb++;
				}
			}
			Console.WriteLine("Level 2:\nr - {0}x \ng - {1}x \nb - {2}x",countr,countg,countb);
			//level über
			Console.WriteLine("Level ÜBER:\nEnter a positive integer, greater than 1.");
			countr = 0;
			countg = 0;
		    countb = 0;
			int m;
			bool success3 = int.TryParse(Console.ReadLine(), out m) && m > 1;
			while (!success3)
			{
				Console.WriteLine("Invalid input, try again!");
				success3 = int.TryParse(Console.ReadLine(), out m) && m > 1;
			}
			int count = 1;
			while (count <= m)
            {
				string[] arraylast = array;
				var rnd = new Random();
				if (input == 1)
                {
					Array.Resize(ref arraylast, k);
					for (int i = array.Length; i < k; i++)
					{
						string str = ChooseString(array,ref p);
						arraylast[i] = str;
					}
					rnd.Shuffle(arraylast); 
				}
				if (input == 2)
                {
					rnd.Shuffle(arraylast); 
					Array.Resize(ref arraylast, n);
				}

				foreach (string item in arraylast)
				{
					if (item == "r")
					{
						countr++;
					}
					if (item == "g")
					{
						countg++;
					}
					if (item == "b")
					{
						countb++;
					}
				}
				count++;
            }
			Console.WriteLine("Statistics after {0} repetitons:\nr - {1}\ng - {2}\nb - {3}",m,(double)countr/m,(double)countg/m,(double)countb/m);
            
		}
    }
}
