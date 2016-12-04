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
                //TODO
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
                //TODO
            }
        }

        private class ASoldiersRemorse : Card
        {
            public ASoldiersRemorse(Character user) : base(user)
            {
                name = "A Soldier’s Remorse";
                effect = "Deal 2 damage.\nChain (R): Send a Standby Defense card to your hand.";
                color = Color.red;
            }

            public override void activate()
            {
                //TODO
            }
        }

        private class WarriorsResolve : Card
        {
            public WarriorsResolve(Character user) : base(user)
            {
                name = "Warrior’s Resolve";
                effect = "Send a Standby card to your hand.\nCounter (B): Send an additional Standby card to your hand.";
                color = Color.green;
            }

            public override void activate()
            {
                //TODO
            }
        }

        private class GrimKnightsDread : Card
        {
            public GrimKnightsDread(Character user) : base(user)
            {
                name = "Grim Knight’s Dread";
                effect = "Swap 2 cards in your hand with 2 Standby cards.\nEndure (5): Put all Standby Offense cards in your hand.";
                color = Color.green;
            }

            public override void activate()
            {
                //TODO
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
                //TODO
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
                //TODO
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
                //TODO
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

            public override void activate()
            {
                //TODO
            }
        }

        private class APromiseUnbroken : Card
        {
            public APromiseUnbroken(Character user) : base(user)
            {
                name = "A Promise Unbroken";
                effect = "Bloodlust: Empower (1).\nStalwart: Reinforce (1).";
                color = Color.black;
            }

            public override void activate()
            {
                //TODO
            }
        }

        private class SunderingStar : Card
        {
            public SunderingStar(Character user) : base(user)
            {
                name = "Sundering Star";
                effect = "fense\nEffect: Strive (2): Deal 2 damage for each of your standby Offense cards.\nCounter (G): Deal 3 additional damage.";
                color = Color.black; //TODO;
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
                effect = "fense\nEffect: Strive (X): Gain 3 health for every Inherent Card you deactivated.\nCounter (R): Safeguard.";
                color = Color.black; //TODO;
            }

            public override void activate()
            {
                //TODO
            }
        }
    }
}