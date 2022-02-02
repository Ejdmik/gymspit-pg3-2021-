using System;


namespace Lecture18
{
	class Car
	{
		// protected je viditelné i v podtřídě
		protected Engine engine;
		protected string SPZ;
		protected GasTank gasTank;


		public Car(Engine engine, GasTank gasTank, string SPZ)
		{
			this.engine = engine;
			this.gasTank = gasTank;
			this.SPZ = SPZ;
		}


		virtual public double LitersPerKm
		{
			get {
				return 2.0 / 100;
			}
		}


		public void Tank(double amount)
		{
			gasTank.Add(amount);
		}

		public virtual void Honk()
        {
			Console.WriteLine("Car honks \"TUTUUU\"");
        }

		public virtual void Go(double distance)
		{
			double realDistance = engine.Run(this, distance, gasTank);
			double time = engine.Time(realDistance);
			Console.WriteLine("Vehicle with SPZ {0} went {1} km in {2} hours. {3} liters of gas left.",this.SPZ, realDistance, time, gasTank.Amount);
		}
	}
}