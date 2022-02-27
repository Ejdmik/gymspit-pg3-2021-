using System;
using System.IO;

namespace Lecture19Composition
{
    class Healer: IController
    {
		private TextReader input;

		private TextWriter prompt;


		public Healer(TextReader input, TextWriter prompt = null)
		{
			this.input = input;
			this.prompt = prompt;
		}
		public string ChooseAction(Character character, Character enemy)
		{
			while (true)
			{
				if (prompt != null)
				{
					prompt.WriteLine("Choose an action:");
					prompt.WriteLine("(A)ttack (attack {0})", character.Attack);
					prompt.WriteLine("(D)efense");
					prompt.WriteLine("(W)ait");
					prompt.WriteLine("(C)heck opponent");
					prompt.WriteLine("(H)eal (+ 2 lifes)");
				}

				string choice = input.ReadLine();
				if (choice == null)
				{
					return null;
				}
				switch (choice.ToLower())
				{
					case "a":
					case "attack":
						return Character.TURN_CHOICE_ATTACK;
					case "w":
					case "wait":
						return Character.TURN_CHOICE_WAIT;
					case "h":
					case "heal":
						return Character.TURN_CHOICE_HEAL;
					case "c":
					case "check opponent":
						return Character.TURN_CHOICE_CHECK;
					case "d":
					case "defense":
						return Character.TURN_CHOICE_DEFENSE;
				}

				if (prompt != null)
				{
					prompt.WriteLine("Invalid choice!");
				}
			}
		}
	}
}
