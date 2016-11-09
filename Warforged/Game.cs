using System;
using System.Threading;

namespace Warforged
{
	public class Game
	{
		public Character p1;
		public Character p2;
		public Game ()
		{
			p1 = new Edros();
			p2 = new Edros();
            p1.setOpponent(p2);
            p2.setOpponent(p1);
		}

		private void takeTurn()
		{
            p1.declarePhase();
            p2.declarePhase();
            p1.damagePhase();
            p2.damagePhase();
            p1.dusk();
            p2.dusk();
            p1.dawn();
            p2.dawn();
            // Heal
            // If anyone dies, do it at the end
		}

		public static void Main()
		{
            Thread UIThread = new Thread(UITestThread);
            UIThread.SetApartmentState(ApartmentState.STA);
            UIThread.Start();
            Game game = new Game();
            Console.WriteLine("{0} hand size", game.p1.hand.Count);
            game.p1.playCard(game.p1.hand[0]);
            game.p2.playCard(game.p2.hand[0]);
            Console.WriteLine("{0} vs {1}", game.p1.currCard.name, game.p2.currCard.name);
            game.takeTurn();
            Console.WriteLine("{0} hand size", game.p1.hand.Count);
            game.p1.playCard(game.p1.hand[0]);
            game.p2.playCard(game.p2.hand[0]);
            Console.WriteLine("{0} vs {1}", game.p1.currCard.name, game.p2.currCard.name);
            game.takeTurn();
            Console.WriteLine("{0} hand size", game.p1.hand.Count);
            game.p1.playCard(game.p1.hand[0]);
            game.p2.playCard(game.p2.hand[0]);
            Console.WriteLine("{0} vs {1}", game.p1.currCard.name, game.p2.currCard.name);
            game.takeTurn();
            Console.WriteLine("{0} hand size", game.p1.hand.Count);
            game.p1.playCard(game.p1.hand[0]);
            game.p2.playCard(game.p2.hand[0]);
            Console.WriteLine("{0} vs {1}", game.p1.currCard.name, game.p2.currCard.name);
            game.takeTurn();
            Console.WriteLine("{0} hand size", game.p1.hand.Count);
            Console.WriteLine("{0}", game.p1.hp);
            Console.WriteLine("{0}", game.p2.hp);
		}

        [STAThreadAttribute]
        public static void UITestThread()
        {
            GameWindow gw = new GameWindow();
            gw.ShowDialog();
        }
	}
}

