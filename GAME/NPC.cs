using System;


namespace Lecture19Composition
{
	class AI : IController
	{
		private Random random;


		public AI(Random random)
		{
			this.random = random;
		}


		public string ChooseAction(Character character, Character enemy)
		{
			string[] choices = new string[] {
				Character.TURN_CHOICE_ATTACK,
				Character.TURN_CHOICE_ATTACK,
				Character.TURN_CHOICE_WAIT,
			};

			return choices[this.random.Next(choices.Length)];
		}
	}
}
