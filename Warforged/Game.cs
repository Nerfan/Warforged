using System;

namespace Warforged
{
	public class Game
	{
		Character p1;
		Character p2;
		public Game ()
		{
			p1 = new Edros();
			p2 = new Edros();
            p1.setOpponent(p2);
            p2.setOpponent(p1);
            p1.playCard(p1.hand[0]);
            p2.playCard(p2.hand[0]);
		}

		private void takeTurn()
		{
			// Store current info
            // Declaration phase
            //      idk exactly how I want to handle green cards
            // Strive now and those effects happen
			// Calclate damages
            // Deal damages
            p1.mainPhase();
            p2.mainPhase();

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

