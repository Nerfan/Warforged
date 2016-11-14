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
            p1.library.updateUI(p1, true);
            p1.library.updateOpponentUI(p2, true, false);
            p2.library.updateUI(p2, true);
            p2.library.updateOpponentUI(p1, true, false);

            p1.playCard();
            p2.playCard();

            p1.library.updateUI(p1,false);
            p1.library.updateOpponentUI(p2, false,false);
            p2.library.updateUI(p2, false);
            p2.library.updateOpponentUI(p1, false, false);

            p1.declarePhase();
            p2.declarePhase();

            p1.library.updateUI(p1, true);
            p1.library.updateOpponentUI(p2, true, false);
            p2.library.updateUI(p2, true);
            p2.library.updateOpponentUI(p1, true, false);

            p1.damagePhase();
            p2.damagePhase();

            p1.library.updateUI(p1, true);
            p1.library.updateOpponentUI(p2, true, false);
            p2.library.updateUI(p2, true);
            p2.library.updateOpponentUI(p1, true, false);

            p1.dusk();
            p2.dusk();

            p1.library.updateUI(p1, true);
            p1.library.updateOpponentUI(p2, true, false);
            p2.library.updateUI(p2, true);
            p2.library.updateOpponentUI(p1, true, false);

            p1.dawn();
            p2.dawn();

            // Heal
            // If anyone dies, do it at the end
        }
        Barrier sync = new Barrier(2);
        private void takeTurnThread1()
        {
            while (true)
            {
                p1.library.updateUI(p1, false);
                p1.library.updateOpponentUI(p2, true, false);

                p1.playCard();
                p1.library.updateUI(p1, false);
                sync.SignalAndWait();
                //sync = new Barrier(2);

                p1.library.updateUI(p1, false);
                p1.library.updateOpponentUI(p2, false, false);

                p1.declarePhase();
                sync.SignalAndWait();
                //sync = new Barrier(2);

                p1.library.updateUI(p1, true);
                p1.library.updateOpponentUI(p2, true, false);
                Thread.Sleep(2000);
                p1.damagePhase();
                sync.SignalAndWait();
                //sync = new Barrier(2);

                p1.library.updateUI(p1, true);
                p1.library.updateOpponentUI(p2, true, false);

                p1.dusk();
                sync.SignalAndWait();
                //sync = new Barrier(2);

                p1.library.updateUI(p1, true);
                p1.library.updateOpponentUI(p2, true, false);

                p1.dawn();
                sync.SignalAndWait();
                //sync = new Barrier(2);
            }
        }

        private void takeTurnThread2()
        {
            while (true)
            {


                p2.library.updateUI(p2, true);
                p2.library.updateOpponentUI(p1, true, false);

                p2.playCard();
                p2.library.updateUI(p2, false);
                sync.SignalAndWait();
                //sync = new Barrier(2);

                p2.library.updateUI(p2, false);
                p2.library.updateOpponentUI(p1, false, false);

                p2.declarePhase();
                sync.SignalAndWait();
                //sync = new Barrier(2);

                p2.library.updateUI(p2, true);
                p2.library.updateOpponentUI(p1, true, false);
                Thread.Sleep(2000);
                p2.damagePhase();
                sync.SignalAndWait();
                //sync = new Barrier(2);

                p2.library.updateUI(p2, true);
                p2.library.updateOpponentUI(p1, true, false);

                p2.dusk();
                sync.SignalAndWait();
                //sync = new Barrier(2);

                p2.library.updateUI(p2, true);
                p2.library.updateOpponentUI(p1, true, false);

                p2.dawn();
                sync.SignalAndWait();
                //sync = new Barrier(2);
            }
        }

        public static void Main()
		{
            
            /*GameWindowLibrary library = new GameWindowLibrary();
            library.setupEdros(1);
            library.setupEdros(2);
            Console.WriteLine(library.yesnoPrompt("Could you hit yes please?"));
            Console.WriteLine(library.yesnoPrompt("Could you hit no please?"));
            Console.WriteLine(library.multiPrompt("Hit any button",new List<string>() {"b1", "b2", "b3", "b4", "b5", "b6", "b7", }, new List<object>() { "b1", "b2", "b3", "b4", "b5", "b6", "b7", }));*/
            Game game = new Game();
            /*Console.WriteLine("{0} hand size", game.p1.hand.Count);
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
            Console.WriteLine("{0}", game.p2.hp);*/
            /*while (true)
            {
                library.updateUI(game.p1, true);
                library.updateOpponentUI(game.p2, true, false);
                Character.Card clickedCard = library.waitForClick();
                while (!game.p1.playCard(clickedCard))
                {
                    library.highlight(clickedCard, 255, 255, 0);
                    Character.Card highlight = clickedCard;
                    clickedCard = library.waitForClick();
                    library.clearAllHighlighting();
                }
                game.p2.playCard(game.p2.hand[0]);
                game.takeTurn();
                //Console.WriteLine(library.waitForClick().name);
            }*/
            /*while(true)
            {
                game.takeTurn();
            }*/
            Thread thread1 = new Thread(game.takeTurnThread1);
            Thread thread2 = new Thread(game.takeTurnThread2);
            thread1.Start();
            thread2.Start();
        }

        
	}
}

