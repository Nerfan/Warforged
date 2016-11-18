using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace Warforged
{
	public class Game
	{
		public Character p1;
		public Character p2;
        public static WindowLibrary library;
		public Game ()
		{
			p1 = new Edros();
			p2 = new Edros();
            p1.setOpponent(p2);
            p2.setOpponent(p1);
        }

		public void takeTurn()
        {
            library.updateUI(p1, true);
            library.updateOpponentUI(p2, true, false);

            p1.playCard();

            library.updateUI(p1,false);
            library.updateOpponentUI(p2, false,false);

            p1.declarePhase();

            library.updateUI(p1, true);
            library.updateOpponentUI(p2, true, false);

            p1.damagePhase();

            library.updateUI(p1, true);
            library.updateOpponentUI(p2, true, false);

            p1.dusk();

            library.updateUI(p1, true);
            library.updateOpponentUI(p2, true, false);

            p1.dawn();

            // Heal
            // If anyone dies, do it at the end
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
            library = new UnityLibrary();
            library.setupEdros(1);
            library.updateUI(game.p1,true);
            library.setPromptText(library.waitForClickOrCancel("Click or Cancel")+"");
            //library.multiPrompt("Prompt Text",new List<string>() { "b1", "b2", "b3", "b4", "b5", "b6" }, new List<object>() { "b1", "b2", "b3", "b4", "b5", "b6" });
        }

    }
}

