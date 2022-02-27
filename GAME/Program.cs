using System;
using System.Linq;


namespace Lecture19Composition
{
	class Program
	{
		static Character ReturnCharacter (string[]heroes)
        {
			string type = ChooseHero(heroes);
			switch (type)
			{
				case "healer1":
					return new Character(new Healer(Console.In, Console.Out), GetRandomNumber(3.1, 9.7), 16, 5, 5);
					
				case "wizard1":
					return new Character(new Wizard(Console.In, Console.Out), GetRandomNumber(3.1, 9.7), 10, 7, 5);
					
				case "player1":
					return new Character(new Player(Console.In, Console.Out), GetRandomNumber(3.1, 9.7), 20, 4, 4);
				default:
					throw new Exception();
			}
		}
		static double GetRandomNumber(double minimum, double maximum)
		{
			Random random = new Random(Guid.NewGuid().GetHashCode());
			int i = random.Next(1, 5);
			return Math.Round(random.NextDouble() * (maximum - minimum) + minimum,i);
		}
		static string ChooseHero(string[] c)
        {
			Console.WriteLine("Choose a hero: H - healer, W - wizard, P - player");
			while (true)
			{
				char choice = Console.ReadKey(true).KeyChar;
				switch (choice)
                {
					case 'h':
					case 'H':
						return c[0];
					case 'w':
					case 'W':
						return c[1];
					case 'p':
					case 'P':
						return c[2];
                }
			}
		}
		static void Main(string[] args)
		{
			Random random = new Random(Guid.NewGuid().GetHashCode());

			Character ai1 = new Character(new AI(random), GetRandomNumber(3.1, 9.7), 15, 6, 4);
			Character smartbot1 = new Character(new SmartBot(random), GetRandomNumber(3.1, 9.7), 18, 6, 4);
			string[] heroes = { "healer1", "wizard1", "player1"};

			Console.WriteLine("How many players? (max 9)");
			char[] allowedchars = { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
			while (true)
			{
				char choice = Console.ReadKey(true).KeyChar;
				if (allowedchars.Contains(choice)) { Console.WriteLine(choice);  }
				int answer = Convert.ToInt32(Convert.ToString(choice));
				Character[] multiplayer = new Character[9];
				for (int i = 0; i<answer; i++)
                {
					Console.WriteLine("Player {0}:", i + 1);
					multiplayer[i] = ReturnCharacter(heroes);
					Console.WriteLine("Player {0} name: {1}", i+1, multiplayer[i].Name);
					Console.WriteLine();
                }
				Array.Resize(ref multiplayer, answer);
				if (answer == 1)
				{
					Console.WriteLine("Choose a difficulty.\n1 - easy\n2 - hard");
					choice = Console.ReadKey(true).KeyChar;
					if (allowedchars.Contains(choice)) { Console.WriteLine(choice); }
					switch (choice)
					{
						case '1':
							Array.Resize(ref multiplayer, multiplayer.Length + 1);
							multiplayer[1] = ai1;
							Game gameeasy = new Game(multiplayer, new Die(random, 6));
							gameeasy.Run(Console.Out);
							break;
						case '2':
							Array.Resize(ref multiplayer, multiplayer.Length + 1);
							multiplayer[1] = smartbot1;
							Game gamehard = new Game(multiplayer, new Die(random, 6));
							gamehard.Run(Console.Out);
							break;
						default:
							throw new Exception();
					}
					break;
				}
				Console.WriteLine("Do you want to add some autoplayers?\n1 - artificial itelligence\n2 - smart bot\n0 - nothing more");
				while (multiplayer.Length<9)
                {
					char ans = Console.ReadKey(true).KeyChar;
					if (ans == '0') { break; }
					switch (ans)
                    {
						case '1':
							Array.Resize(ref multiplayer, multiplayer.Length + 1);
							multiplayer[multiplayer.Length-1] = ai1;
							Console.WriteLine("Auto bot added.");
							Console.WriteLine();
							if (multiplayer.Length != 9) { Console.WriteLine("Do you want to add any other autoplayers?\n1 - artificial itelligence\n2 - smart bot\n0 - nothing more"); }
							break;
						case '2':
							Array.Resize(ref multiplayer, multiplayer.Length + 1);
							multiplayer[multiplayer.Length - 1] = smartbot1;
							Console.WriteLine("Smart bot added.");
							Console.WriteLine();
							if (multiplayer.Length != 9) { Console.WriteLine("Do you want to add any other autoplayers?\n1 - artificial itelligence\n2 - smart bot\n0 - nothing more"); }
							break;
						default:
							Console.WriteLine("Invalid input");
							break;
					}
                }
				Game mpgame = new Game(multiplayer, new Die(random, 6));
				mpgame.Run(Console.Out);
				break;
			}
			Console.WriteLine("Press anything...");
			Console.ReadKey();
		}
	}
}
