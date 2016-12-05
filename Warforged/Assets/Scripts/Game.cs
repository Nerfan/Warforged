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
		public Game (Character character, Character opponent)
		{
			p1 = character;
			p2 = opponent;
			p1.setOpponent(p2);
			p2.setOpponent(p1);
		}

		public void takeTurn()
		{
			library.updateUI(p1, true);
			library.updateOpponentUI(p2, true, false);

			p1.playCard();
			//p2.playCard();

			library.updateUI(p1,false);
			library.updateOpponentUI(p2, false,false);

			p1.declarePhase();
			//p2.declarePhase();

			library.updateUI(p1, true);
			library.updateOpponentUI(p2, true, false);
			Thread.Sleep(2500);
			p1.damagePhase();
			//p2.damagePhase();

			library.updateUI(p1, true);
			library.updateOpponentUI(p2, true, false);

			p1.dusk();

			library.updateUI(p1, true);
			library.updateOpponentUI(p2, true, false);

			p1.dawn();

			// Heal
			// If anyone dies, do it at the end
		}

		//@param: This will return a string which comes from the UI
		//The UI tells the model which character has been selected
		public static void Main(Character character)
		{

			//We need to determin our opponent before this point.
			Game game = new Game(character,new Edros());
			library = new UnityLibrary();
			if (game.p1 is Edros)
				library.setupEdros(1);
			else if (game.p1 is Tyras)
				library.setupTyras(1);
			Thread.Sleep(30);
			library.setupEdros(2);
			while(true)
			{
				game.takeTurn();
			}
		}

	}
}

