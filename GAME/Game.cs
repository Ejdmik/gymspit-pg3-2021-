using System;
using System.IO;


namespace Lecture19Composition
{
	static class RandomExtensions
	{
		public static void Shuffle<T>(this Random rng, T[] array)
		{
			int n = array.Length;
			while (n > 1)
			{
				int k = rng.Next(n--);
				T temp = array[n];
				array[n] = array[k];
				array[k] = temp;
			}
		}
	}
	class Game
	{
		private Character[] multiplayer;
		private Random random = new Random(Guid.NewGuid().GetHashCode());
		private Die die;
		private int round = 0;


		public Game(Character [] multiplayer, Die die)
		{
			this.multiplayer = multiplayer;
			this.die = die;
		}

		public static Character ChooseOpponent(Character[] array, Character active)
		{
			Console.WriteLine("Choose an opponent for this round. Write a number of a player.");
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == active) { continue; }
				if (array[i].Alive) { Console.WriteLine("Player {0}: {1}", i + 1, array[i].Name); }
			}
			bool success = int.TryParse(Console.ReadLine(), out int result) && result >= 0 && result <= array.Length && (array[result - 1] != active) && array[result-1].Alive;
			while (!success)
            {
				Console.WriteLine("Invalid input. Try again.");
				success = int.TryParse(Console.ReadLine(), out result) && result >= 0 && result <= array.Length && (array[result - 1] != active) && array[result - 1].Alive;
			}
			return array[result - 1];
			
		}
		public static Character FindWinner(Character[] array)
        {
			Character winner = null;
			foreach (Character c in array)
            {
				if (c.Alive) { winner = c; }
            }
			return winner;
        }
		public static int SomeoneAlive(Character[] array)
        {
			int count = 0;
			foreach (Character c in array)
            {
				if (c.Alive) { count++; }
            }
			return count;
        }
		
		public void Run(TextWriter output)
		{
			Console.WriteLine("\nLet the games begin!");
			Console.WriteLine();
			random.Shuffle(multiplayer);
			foreach (Character c in multiplayer) { c.Reset(); PrintStatus(output, c); }
			Console.WriteLine();
			Character opponent;
			Character active;

			while (SomeoneAlive(multiplayer)>1) 
			{
				active = multiplayer[round % multiplayer.Length];
				if (!active.Alive) { round++;continue; }
				Console.WriteLine("{0}'s turn:", active.Name);
				if (multiplayer.Length != 2) {
					if (active.controller is AI || active.controller is SmartBot)
                    {
						opponent = multiplayer[random.Next(multiplayer.Length)];
						while(opponent == active) { opponent = multiplayer[random.Next(multiplayer.Length )]; }
                    }
					else { opponent = ChooseOpponent(multiplayer, active); }
				}
                else
                {
					if (multiplayer[0] == active) { opponent = multiplayer[1]; }
                    else { opponent = multiplayer[0]; }
                }
				active.TakeTurn(output, opponent, active, die);
				Console.WriteLine();

				foreach (Character c in multiplayer) { PrintStatus(output, c);}
				Console.WriteLine();

				round++;
			}

			Console.WriteLine("GAME OVER!");
			Character winner = FindWinner(multiplayer);
			Console.WriteLine("The winner is {0}!", winner.Name);
			
		}
		

		private void PrintStatus(TextWriter output, Character character)
		{
			output.WriteLine("{0}: {1}, {2} / {3} HP",character.Name,character.Alive ? "alive" : "dead",character.Hp,character.MaxHp);
		}
	}
}
