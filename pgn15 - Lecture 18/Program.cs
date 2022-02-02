// https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/object-oriented/inheritance
// https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/object-oriented/polymorphism
// https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/this
// https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/base
// https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/virtual
// https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/override
// (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/new-modifier)

using System;


namespace Lecture18
{
	class Program
	{
		static void Main(string[] args)
		{
			string carSPZ = "\"4AB 6874\"";
			string truckSPZ = "\"ZGC 2281\"";
			string excavatorSPZ = "\"M315\"";

			Car car = new Car(new Engine(150, 5.0 / 100), new GasTank(40.0), carSPZ);
			Truck truck = new Truck(new Engine(100, 10.0 / 100),new GasTank(100.0),50,truckSPZ) ;
			Excavator excavator = new Excavator(new Engine(120, 7.0/100), new GasTank(84.0), 10.0, excavatorSPZ);
			while (true)
            {
				Console.WriteLine("Choose:\n0: end\n1: car - tank\n2: car - go\n3: car - honk\n4: truck - tank\n5: truck - go\n6: truck - load\n7: truck - unload\n8: truck - unload all\n9: truck - honk\n*: excavator - tank\n!: excavator - go\n?: excavator - dig\n%: excavator - build\n/: excavator - honk");
				char choice = Console.ReadKey(true).KeyChar;
				if (choice == '0') { break; }
				switch (choice)
                {
					case '1':
						car.Tank(40.0);
						break;
					case '2':
						car.Go(500);
						break;
					case '3':
						car.Honk();
						break;
					case '4':
						truck.Tank(40.0);
						break;
					case '5':
						truck.Go(500);
						break;
					case '6':
						truck.Load(7);
						break;
					case '7':
						truck.Unload(2.0);
						break;
					case '8':
						truck.UnloadAll();
						break;
					case '9':
						truck.Honk();
						break;
					case '*':
						excavator.Tank(300);
						break;
					case '!':
						excavator.Go(40.0);
						break;
					case '?':
						excavator.Dig(50);
						break;
					case '%':
						excavator.Build();
						break;
					case '/':
						excavator.Honk();
						break;
					default:
						Console.WriteLine("Wrong char.");
						break;
                }
				Console.WriteLine("Press anything...");
				Console.ReadKey();
				Console.Clear();
			}
			/*
			

			Car[] fleet = { car, truck, excavator };

			foreach (Car fleetCar in fleet)
			{ 
				if (fleetCar is Truck fleetTruck) {	fleetTruck.Load(20);}
				if (fleetCar is Excavator fleetExcavator) { fleetExcavator.Dig(10); }
				fleetCar.Tank(60.0);
				fleetCar.Go(250);
				fleetCar.Honk();
			}*/

			Console.ReadKey();
		}
	}
}
