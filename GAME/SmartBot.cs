using System;

namespace Lecture19Composition
{
    class SmartBot: IController
    {
		private Random random;
		private int round = 0;
		public SmartBot(Random random)
        {
			this.random = random;
        }
		public string ChooseAction(Character character, Character enemy)
		{
			string[] choices = new string[] {Character.TURN_CHOICE_ATTACK,Character.TURN_CHOICE_ATTACK,Character.TURN_CHOICE_DEFENSE,};
			if (round == 0)
            {
				round++;
				return Character.TURN_CHOICE_DEFENSE;
			}
			if (character.Hp >= 14) { return Character.TURN_CHOICE_ATTACK; }
			if (character.Hp >= 3) 
			{
				if (character.DefenseBonus <= 2.7)
					return choices[this.random.Next(choices.Length)];
				else
					return Character.TURN_CHOICE_ATTACK;
			}
            else
            {
				if (character.Hp < enemy.Hp)
                {
					Console.WriteLine("Prank! Lifes swapped.");
					int tmp = character.hp;
					character.hp = enemy.hp;
					enemy.hp = tmp;
					return Character.TURN_CHOICE_WAIT;
                }
				else { return Character.TURN_CHOICE_ATTACK; }
            }
		}
	}
}
