using System;
using System.IO;

namespace pokladni_denicek
{
    class Program
    {
		static int IntCheck(string b)
        {
			int a;
			bool nrsuccess = int.TryParse(Console.ReadLine(), out a);
			while (!nrsuccess)
			{
				Console.WriteLine("Invalid input. Try again.");
				Console.Write(b);
				nrsuccess = int.TryParse(Console.ReadLine(), out a);
			}
			return a;
		}
		static string[] ReadLines(TextReader tr)
		{
			string[] lines = new string[5];
			int count = 0;
			string line;
			while ((line = tr.ReadLine()) != null)
			{
				if (count >= lines.Length) {Array.Resize(ref lines, lines.Length * 2);}
				lines[count] = line;
				count ++;
			}
			Array.Resize(ref lines, count);
			return lines;
		}
		static void Categories(string[] c)
        {
			int nr = 1;
			for (int i = 1; i < c.Length; i++)
			{
				int j;
				for (j = 0; j < i; j++)
					if (c[i] == c[j])
						break;
				if (i == j)
				{
					Console.WriteLine("{0}) " + c[i], nr);
					nr++;
				}
			}
			Console.WriteLine();
		}
		static void Statement(int[] a, string[] b, int v, string[] c, string d = "")
        {
			int count = v;
			for (int i = 0; i < a.Length - 1; i++)
			{
				if (c[i+1] == d || d == "")
				{
					Console.Write(count + " " + a[i + 1] + " " + b[i + 1] + " ");
					count += a[i + 1];
					Console.Write(count);
					Console.WriteLine();
				}
			}
			Console.WriteLine("Final value: {0}", count);
			int incomes = 0;
			int expenses = 0;
			for (int i = 1; i < a.Length; i++)
			{
				if (c[i] == d || d == "")
				{
					if (a[i] > 0) { incomes += a[i]; }
					if (a[i] < 0) { expenses += a[i]; }
				}
			}
			Console.WriteLine("Sum of incomes: {0}\nSum of expenses: {1}\nTotal: {2}", incomes, expenses, incomes + expenses);
		}
		static void Main(string[] args)
        {
			string baseDir = @"C:\Users\adysu\OneDrive\Plocha\programování\Programy\pokladni_denicek\";
			string[] array;
			Console.WriteLine("Enter a name of your file. (without .txt)");
			string fileName = Console.ReadLine();
			try
			{
				using (TextReader reader = new StreamReader(baseDir + fileName + ".txt"))
					array = ReadLines(reader);
			}catch (FileNotFoundException)
            {
				Console.WriteLine("File not found. Enter an initial value.");
				string value = "Initial value: ";
				int val = IntCheck(value);
				string ival = Convert.ToString(val);
				array = new string [1];
				array[0] = ival + ", initial value, ---";
            }
			string[] bigarray = new string[array.Length * 3];
			string separator = ", ";
			int k = 0;
			int intIndex = 0;
			int stringIndex = 0;
			int categoryIndex = 0;
			for (int i = 0;i<array.Length;i++)
            {
				string[] strlist = array[i].Split(separator);
				bigarray[k] = strlist[0];
				bigarray[k+1] = strlist[1];
				bigarray[k+2] = strlist[2];
				k += 3;
			}
			int[] intarray = new int[array.Length];
			string[] stringarray = new string[array.Length];
			string[] categoryarray = new string [array.Length];
			for (int i = 0;i<bigarray.Length;i++)
            {
				if (i%3==0)
                {
					intarray[intIndex] = Convert.ToInt32(bigarray[i]);
					intIndex++;
                }
                if (i%3 == 1)
                {
					stringarray[stringIndex] = bigarray[i];
					stringIndex++;
                }
				if (i%3 == 2)
                {
					categoryarray[categoryIndex] = bigarray[i];
					categoryIndex++;
                }
            }
			int initialValue = intarray[0];
			if (fileName == "input") { Console.WriteLine("File loaded."); }
			else { Console.WriteLine("File created."); }
			Console.WriteLine("Press one of the following numbers.\n1 - add data \n2 - clear everything\n3 - account statement\n4 - account statement by category\n5 - end (save to file)");
			while (true)
            {
				char choice = Console.ReadKey(true).KeyChar;
				if (choice == '1' || choice == '2' || choice == '3' || choice == '4'|| choice == '5') {Console.WriteLine(choice);}
				if (choice == '1')
				{
					Console.WriteLine("Write a cost (+/-) and than its name. Writing 0 as a cost will end the data addition.");
					while (true)
                    {
						string cost = "Cost: ";
						Console.Write(cost);
						int price = IntCheck(cost);
						if (price == 0) {
							Console.Clear();
							Console.WriteLine("Press one of the following numbers.\n1 - add data \n2 - clear everything\n3 - account statement\n4 - account statement by category\n5 - end (save to file)");
							break; 
						}
						Array.Resize(ref intarray, intarray.Length + 1);
						Array.Resize(ref stringarray, stringarray.Length + 1);
						Array.Resize(ref categoryarray, categoryarray.Length + 1);
						Console.Write("Name: ");
						stringarray[stringarray.Length-1] = Console.ReadLine();
						Console.Write("Category: ");
						categoryarray[categoryarray.Length - 1] = Console.ReadLine();
						intarray[intarray.Length-1] = price;
                    }
				}
				if (choice == '2')
				{
					Console.WriteLine("Enter a new initial value.");
					string iValue = "Initial value: ";
					Console.Write(iValue);
					initialValue = IntCheck(iValue);
					Array.Resize(ref intarray, 1);
					Array.Resize(ref stringarray, 1);
					Array.Resize(ref categoryarray, 1);
					intarray[0] = initialValue;
					Console.Clear();
					Console.WriteLine("Press one of the following numbers.\n1 - add data \n2 - clear everything\n3 - account statement\n4 - account statement by category\n5 - end (save to file)");
				}
				if (choice == '3')
				{
					Console.WriteLine("ACCOUNT STATEMENT");
					Console.WriteLine("Initial value: {0}", initialValue);
					if (intarray.Length == 1) {Console.WriteLine("Final value: {0}\nSum of incomes: 0\nSum of expenses: 0\nTotal: 0", initialValue);}
                    else
					{
						Statement(intarray, stringarray, initialValue, categoryarray);
                    }
					Console.WriteLine("Press any key to continue...");
					Console.ReadKey();
					Console.Clear();
					Console.WriteLine("Press one of the following numbers.\n1 - add data \n2 - clear everything\n3 - account statement\n4 - account statement by category\n5 - end (save to file)");
				}
				if (choice == '4')
                {
					Console.WriteLine("ACCOUNT STATEMENT by category");
					if (intarray.Length == 1) { Console.WriteLine("Final value: {0}\nSum of incomes: 0\nSum of expenses: 0\nTotal: 0", initialValue); }
					else
					{
						Console.WriteLine("Choose one of your categories:");
						Categories(categoryarray);
						Console.Write("Your choice: ");
						string category = Console.ReadLine();
						while (true)
                        {
							if (Array.IndexOf(categoryarray, category)> -1){break;}
                            else 
							{ 
								Console.WriteLine("This category doesn't exist! Try again.");
								Console.Write("Your choice: ");
								category = Console.ReadLine();
							}
                        }
						Console.WriteLine("Initial value: {0}", initialValue);
						Statement(intarray, stringarray, initialValue, categoryarray, category);
					}
					Console.WriteLine("Press any key to continue...");
					Console.ReadKey();
					Console.Clear();
					Console.WriteLine("Press one of the following numbers.\n1 - add data \n2 - clear everything\n3 - account statement\n4 - account statement by category\n5 - end (save to file)");
				}
				if (choice == '5'){break;}
			}
			TextWriter wr = new StreamWriter(baseDir + fileName +".txt", false);
			try
			{
				for (int j = 0; j < intarray.Length; j++){wr.WriteLine(intarray[j] + ", " + stringarray[j] + ", " + categoryarray[j]);}
			}
			finally
			{
				Console.WriteLine("DONE! \nLook into your file.");
				if (wr != null)	{wr.Dispose();}
			}
			
		}
    }
}
