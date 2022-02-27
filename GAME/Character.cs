using System;
using System.IO;


namespace Lecture19Composition
{
	class Character
	{
		public const string TURN_CHOICE_ATTACK = "attack";
		public const string TURN_CHOICE_WAIT = "wait";
		public const string TURN_CHOICE_HEAL = "heal";
		public const string TURN_CHOICE_MAGIC = "magic";
		public const string TURN_CHOICE_CHECK = "check opponent";
		public const string TURN_CHOICE_DEFENSE = "defense";

		public IController controller;

		private string name;

		public static string GenerateName(double len)
		{
			Random r = new Random(Guid.NewGuid().GetHashCode());
			string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "n", "p", "q", "r", "s", "t", "v", "w", "x", "z", "sh", "th", "br", "st", "rr", "ch", "tt"};
			string[] vowels = { "a", "e", "i", "o", "u", "y", "ou", "ea", "ai", "ie", "ao", "oo", "ee" };
			string RandomName = "";
			RandomName += consonants[r.Next(consonants.Length-7)].ToUpper();
			RandomName += vowels[r.Next(vowels.Length)];
			int b = 2;
			while (b < Math.Floor(len))
			{
				RandomName += consonants[r.Next(consonants.Length)];
				b++;
				RandomName += vowels[r.Next(vowels.Length)];
				b++;
			}
			double remainder = len - Math.Floor(len);
			string digits = remainder.ToString();
			if (digits.Length > 6) { digits = digits.Remove(6); }
			if (remainder == 0) { return RandomName; }
			for(int i = 2; i<digits.Length; i++)
            {
				RandomName += digits[i];
            }
			return RandomName;
		}

		public int hp;
		private int maxHp;

		private int attack;
		private int defense;

		public int magic = 1;
		private double defenseBonus;

		public double DefenseBonus
        {
            get { return defenseBonus; }
        }
		public string Name
		{
			get {
				return name;
			}
		}

		public int Hp
		{
			get {
				return hp;
			}
		}


		public int MaxHp
		{
			get {
				return maxHp;
			}
		}


		public bool Alive
		{
			get {
				return hp > 0;
			}
		}


		public int Attack
		{
			get {
				return attack;
			}
		}

		public Character(IController controller, double nameLength, int maxHp, int attack, int defense)
		{
			this.controller = controller;
			name = GenerateName(nameLength);
			this.maxHp = maxHp;
			this.attack = attack;
			this.defense = defense;

			Reset();
		}


		public void TakeTurn(TextWriter output, Character enemy, Character character, Die die)
		{
			string action = controller.ChooseAction(this, enemy);

			switch (action) {
				case TURN_CHOICE_ATTACK:
					AttackEnemy(output, enemy, character, die);
					break;

				case TURN_CHOICE_WAIT:
					Wait(output, die);
					break;

				case TURN_CHOICE_HEAL:
					Heal(output, character);
					break;

				case TURN_CHOICE_MAGIC:
					Magic(output, enemy, character, die);
					break;

				case TURN_CHOICE_CHECK:
					Check(output, enemy);
					break;

				case TURN_CHOICE_DEFENSE:
					Defense(output, character);
					break;

				default:
					output.WriteLine("{0} does nothing...", name);
					break;
			}
		}


		public void Reset()
		{
			hp = maxHp;
			defenseBonus = 0;
		}

		private void Defense(TextWriter output, Character character)
		{

			if (character.defenseBonus == 3.7)
			{
				output.WriteLine("You're not allowed to raise defense bonus anymore.");
			}

			if (character.defenseBonus >= 2.7)
			{
				character.defenseBonus = 3.7;
				output.WriteLine("{0}'s current defense bonus: {1} (MAX)", character.Name, character.defenseBonus);
			}
			if (character.defenseBonus <= 2.7)
			{
				character.defenseBonus += 1;
				output.WriteLine("{0}'s current defense bonus: {1}", character.Name, character.defenseBonus);
			}
		}
		private void Check(TextWriter output, Character enemy)
        {
			if (enemy.controller is AI) { output.WriteLine("Enemy type: artificial intelligence"); }
			if (enemy.controller is SmartBot) { output.WriteLine("Enemy type: smart bot"); }
			if (enemy.controller is Player) { output.WriteLine("Enemy type: basic player"); }
			if (enemy.controller is Healer) { output.WriteLine("Enemy type: healer"); }
			if (enemy.controller is Wizard) { output.WriteLine("Enemy type: wizard"); }
			output.WriteLine("Enemy name: {0}\nEnemy attack: {1}\nEnemy defense: {2}\nEnemy defense bonus: {3}", enemy.Name, enemy.Attack, enemy.defense, enemy.defenseBonus);
        }
		private void AttackEnemy(TextWriter output, Character enemy, Character character, Die die)
		{
			output.WriteLine("{0} attacks {1}!", name, enemy.Name);
			int attackRoll = attack + die.Roll();
			enemy.ReceiveAttack(output, attackRoll, die);
			if (character.defenseBonus >= 1) { character.defenseBonus -= 0.6; }
		}


		private void ReceiveAttack(TextWriter output, int attackRoll, Die die)
		{
			int defenseRoll = defense + die.Roll();
			int damage = attackRoll - defenseRoll - (int)Math.Round(defenseBonus);

			if (damage > 0) {
				hp -= damage;
				output.WriteLine("{0} takes {1} damage!", name, damage);
			} else {
				output.WriteLine("{0} takes no damage!", name);
			}
		}
		private void Magic(TextWriter output, Character enemy, Character character, Die die)
		{
			if ((character.magic - 1) % 3 == 0)
			{
				output.WriteLine("{0} used magic against {1}!", name, enemy.Name);
				int attackRoll = attack + 2 * die.Roll();
				enemy.ReceiveAttack(output, attackRoll, die);
			}
            else
            {
				output.WriteLine("Don't cheat! Punishment: -1 life");
				character.hp--;
            }
		}

		private void Wait(TextWriter output, Die die)
		{
			output.WriteLine("{0} waits and rolls a die...", name);
			output.WriteLine("They rolled a {0}!", die.Roll());
		}
		
		private void Heal(TextWriter output, Character character)
        {
			if (character.hp + 2 <= character.maxHp)
            {
				character.hp += 2;
				output.WriteLine("{0} healed itself. (+2 lifes)", name);
            }
			else
            {
				output.WriteLine("Unable to heal. {0} has too much lifes.", name);
            }
        }
	}
}
