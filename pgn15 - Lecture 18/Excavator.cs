using System;


namespace Lecture18
{
	class Excavator : Car
	{
		private double digCapacity;
		private double digAmount = 0;

		public Excavator(Engine engine, GasTank gasTank, double digCapacity, string SPZ) :
			base(engine, gasTank, SPZ)
		{
			this.digCapacity = digCapacity;
		}

		
		public override void Honk()
		{
			Console.WriteLine("Excavator honks \"VRMVRMVRM\"");
		}

		public void Dig(int howmanytimes)
		{
			digAmount += howmanytimes * digCapacity;
			if (howmanytimes >= 0) { Console.WriteLine("Excavator digged {0} soil. Total soil: {1}.", howmanytimes * digCapacity, digAmount); }
            else { Console.WriteLine("Excavator returned {0} soil. Total soil: {1}.", howmanytimes * digCapacity, digAmount); }
		}

		public void Build()
		{
			Console.WriteLine("Choose what do you want to build. You have currently {0} soil.\n1 - mansion (1000 soil required)\n2 - bridge (500 soil required)\n3 - cottage (200 soil required)", digAmount);
			char choice = Console.ReadKey(true).KeyChar;
			if (choice == '1' || choice == '2' || choice == '3') { Console.WriteLine(choice); }
			switch (choice)
            {
				case '1':
					if (digAmount>= 1000) { Console.WriteLine("Congratulations, mansion built."); digAmount -= 1000; }
					else { Console.WriteLine("Not enough soil. Try again next time."); }
					break;
				case '2':
					if (digAmount >= 500) { Console.WriteLine("Congratulations, bridge built."); digAmount -= 500; }
					else { Console.WriteLine("Not enough soil. Try again next time."); }
					break;
				case '3':
					if (digAmount >= 200) { Console.WriteLine("Congratulations, cottage built."); digAmount -= 200; }
					else { Console.WriteLine("Not enough soil. Try again next time."); }
					break;
            }
		}
	}
}