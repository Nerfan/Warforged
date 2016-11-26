using System;

namespace Warforged
{
    public class Tyras : Character
    {
        public Tyras() : base()
        {
            name = "Tyras";
            title = "Hero of a Lost Age";
        }

        public override void dawn()
        {
            base.dawn();
            // A Promise Unbroken
            foreach (Card card in invocation)
            {
                if (card is APromiseUnbroken && card.active)
                {
                    if (bloodlust)
                    {
                        empower += 1;
                    }
                    if (stalwart)
                    {
                        reinforce += 1;
                    }
                }
            }
        }

        // playCard()

        public override void declarePhase()
        {
            base.declarePhase(); // Activate effects of cards first
            if (currCard.color - opponent.currCard.color == -1
                || currCard.color - opponent.currCard.color == 2)
            {
                // An Oath Unforgotten
                // TODO I think there's a more efficient way to do this
                foreach (Card card in invocation)
                {
                    if (card is AnOathUnforgotten && card.active
                        && standby.Count > 0)
                    {
                        Game.library.setPromptText("Choose a card to take from your standby.");
                        while (true)
                        {
                            card1 = Game.library.waitForClick();
                            if (user.standby.Contains(card1))
                            {
                                break;
                            }
                        }
                    }
                }
                boslter(); // This may cause problems; idk how things should be ordered
            }
            // Damage applies after this; I don't think that should be an issue
        }

        // damagePhase()

        // dusk()

        private class OnraisStrike : Card
        {
            public OnraisStrike(Character user) : base(user)
            {
                name = "Onrai’s Strike";
                effect = "Deal 2 damage.\nCounter (G): Deal 2 additional damage.";
                color = Color.red;
            }

            public override void activate()
            {
                user.damage += 2 + user.empower;
                user.empower = 0;
                if (user.opponent.currCard.color == Color.green)
                {
                    user.damage += 2;
                }
            }
        }

        private class OnslaughtofTyras : Card
        {
            public OnslaughtofTyras(Character user) : base(user)
            {
                name = "Onslaught of Tyras";
                effect = "Deal 2 damage.\nBloodlust: Negate 2 damage this turn.";
                color = Color.red;
            }

            public override void activate()
            {
                user.damage += 2 + user.empower;
                user.empower = 0;
                if (user.bloodlust)
                {
                    user.negate += 2 + user.reinforce;
                    user.reinforce = 0;
                }
            }
        }

        private class ASoldiersRemorse : Card
        {
            Card standbyCard = null;
            public ASoldiersRemorse(Character user) : base(user)
            {
                name = "A Soldier’s Remorse";
                effect = "Deal 2 damage.\nChain (R): Send a Standby Defense card to your hand.";
                color = Color.red;
            }

            public override void activate()
            {
                user.damage += 2 + user.empower;
                user.takeStandby(standbyCard);
                standbyCard = null;
            }

            public override void declare()
            {
                if (user.prevCard.color == Color.red)
                {
                    while (true)
                    {
                        // Ideally this should only happen once
                        // But if the user chooses an invalid card, try again
                        Game.library.setPromptText("Choose a blue standby card to send to your hand.");
                        standbyCard = Game.library.waitForClick();
                        if (standbyCard != null) // Safety check
                        {
                            if (standbyCard.color == Color.blue && user.standby.Contains(standbyCard))
                            {
                                break;
                            }
                        }
                    }
                }
            }
        }

        private class WarriorsResolve : Card
        {
            Card card1 = null;
            Card card2 = null;
            public WarriorsResolve(Character user) : base(user)
            {
                name = "Warrior’s Resolve";
                effect = "Send a Standby card to your hand.\nCounter (B): Send an additional Standby card to your hand.";
                color = Color.green;
            }

            public override void activate()
            {
                user.takeStandby(card1);
                card1 = null;
                user.takeStandby(card2);
                card2 = null;
            }

            public override void declare()
            {
                while (true)
                {
                    Game.library.setPromptText("Choose a standby card to send to your hand.");
                    card1 = Game.library.waitForClick();
                    if (user.standby.Contains(card1))
                    {
                        break;
                    }
                }
                if (opponent.currCard.color == Color.blue)
                {
                    while (true)
                    {
                        Game.library.setPromptText("Choose a standby card to send to your hand.");
                        card2 = Game.library.waitForClick();
                        if (user.standby.Contains(card2))
                        {
                            break;
                        }
                    }
                }
            }
        }

        private class GrimKnightsDread : Card
        {
            Card stdbyCard1 = null;
            Card stdbyCard2 = null;
            Card handCard1 = null;
            Card handCard2 = null;
            public GrimKnightsDread(Character user) : base(user)
            {
                name = "Grim Knight’s Dread";
                effect = "Swap 2 cards in your hand with 2 Standby cards.\nEndure (5): Put all Standby Offense cards in your hand.";
                color = Color.green;
            }

            public override void activate()
            {
                user.swap(handCard1, stdbyCard1);
                handCard1 = null;
                stdbyCard1 = null;
                user.swap(handCard2, stdbyCard2);
                handCard2 = null;
                stdbyCard2 = null;
                if (user.hp <= 5) // TODO does this come before or after
                {
                    foreach (Card card in user.standby)
                    {
                        if (card.color == Color.red)
                        {
                            user.takeStandby(card);
                        }
                    }
                }
            }

            public override void declare()
            {
                // First swap
                if (user.standby.Count > 0 && user.hand.Count > 0)
                {
                    Game.library.setPromptText("Pick the first pair of cards to swap.");
                    while(true)
                    {
                        Character.Card card1 = Game.library.waitForClick();
                        Game.library.highlight(card1, 255, 255, 0);
                        Character.Card card2 = Game.library.waitForClick();
                        Game.library.clearAllHighlighting();
                        if(user.hand.Contains(card1) && user.standby.Contains(card2))
                        {
                            handCard1 = card1;
                            stdbyCard1 = card2;
                            break;
                        }
                        else if(user.hand.Contains(card2) && user.standby.Contains(card1))
                        {
                            handCard1 = card2;
                            stdbyCard1 = card1;
                            break;
                        }
                    }
                }
                // Second swap
                if (user.standby.Count > 1 && user.hand.Count > 1)
                {
                    Game.library.setPromptText("Pick the first pair of cards to swap.");
                    while(true)
                    {
                        Character.Card card1 = Game.library.waitForClick();
                        Game.library.highlight(card1, 255, 255, 0);
                        Character.Card card2 = Game.library.waitForClick();
                        Game.library.clearAllHighlighting();
                        if(user.hand.Contains(card1) && user.standby.Contains(card2))
                        {
                            handCard1 = card1;
                            stdbyCard1 = card2;
                            break;
                        }
                        else if(user.hand.Contains(card2) && user.standby.Contains(card1))
                        {
                            handCard1 = card2;
                            stdbyCard1 = card1;
                            break;
                        }
                    }
                }
            }
        }

        private class DecryingRoar : Card
        {
            public DecryingRoar(Character user) : base(user)
            {
                name = "Decrying Roar";
                effect = "Negate 2 damage.\nCounter (R): Seal (B).";
                color = Color.blue;
            }

            public override void activate()
            {
                user.negate += 2 + user.reinforce;
                user.reinforce == 0;
                if (user.opponent.currCard.color == Color.red)
                {
                    user.sealColor(Color.blue);
                }
            }
        }

        private class ArmorofAldras : Card
        {
            public ArmorofAldras(Character user) : base(user)
            {
                name = "Armor of Aldras";
                effect = "Gain 2 health.\nAlign (G, R): Safeguard.";
                color = Color.blue;
            }

            public override void activate()
            {
                user.heal += 2;
                if (user.hasAlign("GR"))
                {
                    user.negate = 255;
                }
            }
        }

        private class ABrothersVirtue : Card
        {
            public ABrothersVirtue(Character user) : base(user)
            {
                name = "A Brother’s Virtue";
                effect = "Negate 2 damage.\nStalwart: Reflect.";
                color = Color.blue;
            }

            public override void activate()
            {
                user.negate += 2 + user.reinforce;
                user.reinforce = 0;
                if (user.stalwart)
                {
                    user.reflect = true;
                }
            }
        }

        private class AnOathUnforgotten : Card
        {
            public AnOathUnforgotten(Character user) : base(user)
            {
                name = "An Oath Unforgotten";
                effect = "Whenever you play a counter type of your opponent’s card, send a Standby card to your hand.";
                color = Color.black;
            }

            // Effects are covered in declarePhase()
        }

        private class APromiseUnbroken : Card
        {
            public APromiseUnbroken(Character user) : base(user)
            {
                name = "A Promise Unbroken";
                effect = "Bloodlust: Empower (1).\nStalwart: Reinforce (1).";
                color = Color.black;
            }
            // Effects are covered in dawn()
        }

        private class SunderingStar : Card
        {
            public SunderingStar(Character user) : base(user)
            {
                name = "Sundering Star";
                effect = "Strive (2): Deal 2 damage for each of your standby Offense cards.\nCounter (G): Deal 3 additional damage.";
                color = Color.red;
            }

            public override void activate()
            {
                //TODO
            }
        }

        private class IntheKingsWake : Card
        {
            public IntheKingsWake(Character user) : base(user)
            {
                name = "In the King’s Wake";
                effect = "Strive (X): Gain 3 health for every Inherent Card you deactivated.\nCounter (R): Safeguard.";
                color = Color.blue;
            }

            public override void activate()
            {
                //TODO
            }
        }
    }
}
