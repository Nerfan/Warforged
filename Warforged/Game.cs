using System;

namespace Warforged
{
	public class Game
	{
		Character p1;
		Character p2;
		public Game ()
		{
			p1 = new AdriusAspirer();
			p2 = new AdriusAspirer();
            p1.playCard(p1.hand[0]);
            p2.playCard(p2.hand[0]);
		}

		private void takeTurn()
		{
			// Store current info
            // Declaration phase
            // Strive now and those effects happen
			// Calclate damages
            int p1dmg = p1.damage - p2.negate;
            int p2dmg = p2.damage - p1.negate;
            // Deal damages
            p1.takeDamage(p2dmg);
            p2.takeDamage(p1dmg);
            // Heal
            // If anyone dies, do it at the end
		}

		public static void Main()
		{
            Game game = new Game();
            game.takeTurn();
            Console.WriteLine("{0}", game.p1.hp);
            Console.WriteLine("{0}", game.p2.hp);
		}
	}
}

