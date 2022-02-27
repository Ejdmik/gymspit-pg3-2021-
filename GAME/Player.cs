using System;
using System.IO;


namespace Lecture19Composition
{
	class Player : IController
	{
		private TextReader input;

		private TextWriter prompt;


		public Player(TextReader input, TextWriter prompt = null)
		{
			this.input = input;
			this.prompt = prompt;
		}


		public string ChooseAction(Character character, Character enemy)
		{
			while (true) {
				if (prompt != null) {
					prompt.WriteLine("Choose an action:");
					prompt.WriteLine("(A)ttack (attack {0})", character.Attack);
					prompt.WriteLine("(D)efense");
					prompt.WriteLine("(W)ait");
					prompt.WriteLine("(C)heck opponent");
				}

				string choice = input.ReadLine();
				if (choice == null) {
					return null;
				}

				switch (choice.ToLower()) {
					case "a":
					case "attack":
						return Character.TURN_CHOICE_ATTACK;
					case "w":
					case "wait":
						return Character.TURN_CHOICE_WAIT;
					case "c":
					case "check opponent":
						return Character.TURN_CHOICE_CHECK;
					case "d":
					case "defense":
						return Character.TURN_CHOICE_DEFENSE;
				}

				if (prompt != null) {
					prompt.WriteLine("Invalid choice!");
				}
			}
		}
	}
}