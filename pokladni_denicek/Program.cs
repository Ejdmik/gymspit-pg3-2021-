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
		static void Main(string[] args)
        {
			string baseDir = @"C:\Users\adysu\OneDrive\Plocha\programování\pokladni_denicek\";
			string[] array;
			Console.WriteLine("Enter a name of your file. (without .txt)");
			string fileName = Console.ReadLine();
			try
			{
				using (TextReader reader = new StreamReader(baseDir + fileName + ".txt")){array = ReadLines(reader);}
			}catch (FileNotFoundException)
            {
				Console.WriteLine("File not found. Enter an initial value.");
				string value = "Initial value: ";
				int val = IntCheck(value);
				string ival = Convert.ToString(val);
				array = new string [1];
				array[0] = ival + ", initial value";
            }
			string[] bigarray = new string[array.Length * 2];
			string separator = ", ";
			int k = 0;
			int l = 0;
			int m = 0;
			for (int i = 0;i<array.Length;i++)
            {
				string[] strlist = array[i].Split(separator);
				bigarray[k] = strlist[0];
				bigarray[k+1] = strlist[1];
				k += 2;
			}
			int[] intarray = new int[array.Length];
			string[] stringarray = new string[array.Length];
			int result;
			for (int i = 0;i<bigarray.Length;i++)
            {
				bool success = int.TryParse(bigarray[i], out result);
				if (success)
                {
					intarray[l] = result;
					l++;
                }
                else
                {
					stringarray[m] = bigarray[i];
					m++;
                }
            }
			int initialValue = intarray[0];
			if (fileName == "input") { Console.WriteLine("File loaded."); }
			else { Console.WriteLine("File created."); }
			Console.WriteLine("Press one of the following numbers.\n1 - add data \n2 - clear everything\n3 - account statement\n4 - end (save to file)");
			while (true)
            {
				char choice = Console.ReadKey(true).KeyChar;
				if (choice == '1' || choice == '2' || choice == '3' || choice == '4') {Console.WriteLine(choice);}
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
							Console.WriteLine("Press one of the following numbers.\n1 - add data \n2 - clear everything\n3 - account statement\n4 - end (save to file)");
							break; 
						}
						Array.Resize(ref intarray, intarray.Length + 1);
						Array.Resize(ref stringarray, stringarray.Length + 1);
						Console.Write("Name: ");
						stringarray[stringarray.Length-1] = Console.ReadLine();
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
					intarray[0] = initialValue;
					Console.Clear();
					Console.WriteLine("Press one of the following numbers.\n1 - add data \n2 - clear everything\n3 - account statement\n4 - end (save to file)");
				}
				if (choice == '3')
				{
					Console.WriteLine("ACCOUNT STATEMENT");
					Console.WriteLine("Initial value: {0}", initialValue);
					if (intarray.Length == 1) {Console.WriteLine("Final value: {0}\nSum of incomes: 0\nSum of expenses: 0\nTotal: 0", initialValue);}
                    else
					{
						int count = initialValue;
						for (int i = 0; i < intarray.Length-1; i++)
                        {
							Console.Write(count + " " + intarray[i + 1] + " " + stringarray[i + 1] + " ");
							count += intarray[i + 1];
							Console.Write(count);
							Console.WriteLine();
						}
						Console.WriteLine("Final value: {0}", count);
						int incomes = 0;
						int expenses = 0;
						for (int i = 1; i< intarray.Length;i++)
                        {
							if (intarray[i] > 0) { incomes += intarray[i]; }
							if (intarray[i] < 0) { expenses += intarray[i]; }
                        }
						Console.WriteLine("Sum of incomes: {0}\nSum of expenses: {1}\nTotal: {2}", incomes, expenses, incomes + expenses);
                    }
					Console.WriteLine("Press any key to continue...");
					Console.ReadKey();
					Console.Clear();
					Console.WriteLine("Press one of the following numbers.");
					Console.WriteLine("1 - add data \n2 - clear everything\n3 - account statement\n4 - end (save to file)");
				}
				if (choice == '4'){break;}
			}
			TextWriter wr = new StreamWriter(baseDir + fileName +".txt", false);
			try
			{
				for (int j = 0; j < intarray.Length; j++){wr.WriteLine(intarray[j] + ", " + stringarray[j]);}
			}
			finally
			{
				Console.WriteLine("DONE! \nLook into your file.");
				if (wr != null)	{wr.Dispose();}
			}
			
		}
    }
}
