using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace Warforged
{
	public class Game
	{
		public static Character p1;
		public static Character p2;
		public static WindowLibrary library = null;
		public Game ()
		{
		}
        public static void setup(Character character, Character opponent)
        {
            p1 = character;
            if (p1 is Edros)
            {

                p1.hand.Add(new Edros.HandofToren());
                p1.hand.Add(new Edros.CelestialSurge());
                p1.hand.Add(new Edros.PillarofLightning());
                p1.hand.Add(new Edros.TorensFavored());
                foreach (Character.Card c in p1.hand)
                {
                    c.init(p1);
                }
                p1.standby.Add(new Edros.SkyBlessedShield());
                p1.standby.Add(new Edros.RollingThunder());
                p1.standby.Add(new Edros.PurgingLightning());
                p1.standby.Add(new Edros.FaithUnquestioned());
                foreach (Character.Card c in p1.standby)
                {
                    c.init(p1);
                }
                p1.invocation.Add(new Edros.WrathofLightning());
                p1.invocation.Add(new Edros.GraceofHeaven());
                p1.invocation.Add(new Edros.ScornofThunder());
                p1.invocation.Add(new Edros.CrashingSky());
                foreach (Character.Card c in p1.invocation)
                {
                    c.init(p1);
                }
            }
            else if (p1 is Tyras)
            {
                p1.hand.Add(new Tyras.OnraisStrike());
                p1.hand.Add(new Tyras.OnslaughtofTyras());
                p1.hand.Add(new Tyras.WarriorsResolve());
                p1.hand.Add(new Tyras.DecryingRoar());
                foreach (Character.Card c in p1.hand)
                {
                    c.init(p1);
                }
                p1.standby.Add(new Tyras.ArmorofAldras());
                p1.standby.Add(new Tyras.ABrothersVirtue());
                p1.standby.Add(new Tyras.ASoldiersRemorse());
                p1.standby.Add(new Tyras.GrimKnightsDread());
                foreach (Character.Card c in p1.standby)
                {
                    c.init(p1);
                }
                p1.invocation.Add(new Tyras.APromiseUnbroken());
                p1.invocation.Add(new Tyras.AnOathUnforgotten());
                p1.invocation.Add(new Tyras.IntheKingsWake());
                p1.invocation.Add(new Tyras.SunderingStar());
                foreach (Character.Card c in p1.invocation)
                {
                    c.init(p1);
                }
            }
            Thread.Sleep(30);
            p2 = opponent;

            if (p2 is Edros)
            {

                p2.hand.Add(new Edros.HandofToren());
                p2.hand.Add(new Edros.CelestialSurge());
                p2.hand.Add(new Edros.PillarofLightning());
                p2.hand.Add(new Edros.TorensFavored());
                foreach (Character.Card c in p2.hand)
                {
                    c.init(p1);
                }
                p2.standby.Add(new Edros.SkyBlessedShield());
                p2.standby.Add(new Edros.RollingThunder());
                p2.standby.Add(new Edros.PurgingLightning());
                p2.standby.Add(new Edros.FaithUnquestioned());
                foreach (Character.Card c in p2.standby)
                {
                    c.init(p1);
                }
                p2.invocation.Add(new Edros.WrathofLightning());
                p2.invocation.Add(new Edros.GraceofHeaven());
                p2.invocation.Add(new Edros.ScornofThunder());
                p2.invocation.Add(new Edros.CrashingSky());
                foreach (Character.Card c in p2.invocation)
                {
                    c.init(p2);
                }
            }
            else if (p2 is Tyras)
            {
                p2.hand.Add(new Tyras.OnraisStrike());
                p2.hand.Add(new Tyras.OnslaughtofTyras());
                p2.hand.Add(new Tyras.WarriorsResolve());
                p2.hand.Add(new Tyras.DecryingRoar());
                foreach (Character.Card c in p2.hand)
                {
                    c.init(p2);
                }
                p2.standby.Add(new Tyras.ArmorofAldras());
                p2.standby.Add(new Tyras.ABrothersVirtue());
                p2.standby.Add(new Tyras.ASoldiersRemorse());
                p2.standby.Add(new Tyras.GrimKnightsDread());
                foreach (Character.Card c in p2.standby)
                {
                    c.init(p2);
                }
                p2.invocation.Add(new Tyras.APromiseUnbroken());
                p2.invocation.Add(new Tyras.AnOathUnforgotten());
                p2.invocation.Add(new Tyras.IntheKingsWake());
                p2.invocation.Add(new Tyras.SunderingStar());
                foreach (Character.Card c in p2.invocation)
                {
                    c.init(p2);
                }
            }
            p1.setOpponent(p2);
            p2.setOpponent(p1);
        }
		public void takeTurn()
        {

            library.updateUI(p1, true);
            library.updateNetowrk(p1);
            library.updateOpponentUI(p2, true, false);

			p1.playCard();
            //p2.playCard();


			library.updateUI(p1,false);
            library.updateNetowrk(p1);
            library.updateOpponentUI(p2, false,false);

			p1.declarePhase();
			//p2.declarePhase();


            library.updateUI(p1, true);
            library.updateNetowrk(p1);
            library.updateOpponentUI(p2, true, false);
			p1.damagePhase();
            p2.damagePhase();
            Thread.Sleep(2500);


            library.updateUI(p1, true);
            library.updateNetowrk(p1);
            library.updateOpponentUI(p2, true, false);

			p1.dusk();
            

            library.updateUI(p1, true);
            library.updateNetowrk(p1);
            library.updateOpponentUI(p2, true, false);

			p1.dawn();

			// Heal
			// If anyone dies, do it at the end
		}

		//@param: This will return a string which comes from the UI
		//The UI tells the model which character has been selected
		public static void Main()
		{

            //We need to determin our opponent before this point.
            Game game = new Game();
            library = new UnityLibrary();

            if (p1 is Edros)
            {
                library.setupEdros(1);
            }
            else if (p1 is Tyras)
            {
                library.setupTyras(1);
            }

            Thread.Sleep(30);

            if (p2 is Edros)
            {
                library.setupEdros(2);
            }
            else if (p2 is Tyras)
            {
                library.setupTyras(2);
            }

            while (true)
            {
                game.takeTurn();
			}
		}

	}
}

