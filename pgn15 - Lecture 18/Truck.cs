using System;


namespace Lecture18
{
	class Truck : Car
	{
		private double loadAmount = 0;
		private double capacity;


		public Truck(Engine engine, GasTank gasTank, double capacity, string SPZ) :
			base(engine, gasTank, SPZ)
		{
			this.capacity = capacity;
		}


		public double LoadAmount
		{
			get {
				return loadAmount;
			}
		}


		public override double LitersPerKm
		{
			get {
				return 3.0 / 100 + loadAmount / 1000;
			}
		}


		public override void Go(double distance)
		{
			base.Go(distance);
			Console.WriteLine("Transported {0} stuff.", loadAmount);
		}

        public override void Honk()
        {
			Console.WriteLine("Truck honks \"BRUMBRUM\"");
        }

        public void Load(double amount)
		{
			if (amount + loadAmount > capacity) {
				throw new ArgumentException();
			}
			loadAmount += amount;
			Console.WriteLine("{0} stuff loaded. Total: {1} stuff.",amount, loadAmount);
		}


		public void Unload(double amount)
		{
			if (amount > loadAmount) {
				throw new ArgumentException();
			}
			loadAmount -= amount;
			Console.WriteLine("{0} stuff unloaded. Total: {1}.", amount, loadAmount);
		}
		public void UnloadAll()
        {
			if (loadAmount == 0) { throw new ArgumentException(); }
			Console.WriteLine("{0} stuff unloaded, nothing left.", loadAmount);
			loadAmount = 0;
        }
	}
}