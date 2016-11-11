using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;

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
            Thread thread = new Thread(UIThread);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            barrier.SignalAndWait();
            GameWindowLibrary library = new GameWindowLibrary(gameWindow);
            library.setupEdros(false);
            library.setupEdros(true);
            Console.WriteLine(library.yesnoPrompt("Could you hit yes please?"));
            Console.WriteLine(library.yesnoPrompt("Could you hit no please?"));
            Console.WriteLine(library.multiPrompt("Hit any button",new List<string>() {"b1", "b2", "b3", "b4", "b5", "b6", "b7", }, new List<object>() { "b1", "b2", "b3", "b4", "b5", "b6", "b7", }));
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
            while (true)
            {
                library.updateUI(game.p1, true);
                library.updateOpponentUI(game.p2, true, false);
                Console.WriteLine(library.waitForClick().name);
            }
        }

        public static GameWindow gameWindow = null;
        public static Barrier barrier = new Barrier(2);
        [STAThreadAttribute]
        public static void UIThread()
        {
            gameWindow = new GameWindow();
            barrier.SignalAndWait();
            gameWindow.ShowDialog();
        }
	}
}

