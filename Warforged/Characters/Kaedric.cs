using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Warforged
{
    public class Kaedric : Character
    {
        public Kaedric() : base()
        {
            name = "Kaedric";
            title = "Heir of Chaos";
        }

        private class ShiftingChaos : Card
        {
            public ShiftingChaos(Character user) : base(user)
            {
                name = "Shifting Chaos";
                effect = "Deal 1 damage. You may Suspend up to 2 Standby cards to deal 1 additional damage for each card suspended.";
                color = Color.red;
            }

            public override void activate()
            {
                //TODO
            }
        }

        private class StrikingChaos : Card
        {
            public StrikingChaos(Character user) : base(user)
            {
                name = "Striking Chaos";
                effect = "Deal 2 damage.\nAlign (G, B, G): Deal 3 additional damage.";
                color = Color.red;
            }

            public override void activate()
            {
                //TODO
            }
        }

        private class GraspofChaos : Card
        {
            public GraspofChaos(Character user) : base(user)
            {
                name = "Grasp of Chaos";
                effect = "Deal 1 damage for each of your Suspended cards.\nAlign (R, B): Suspend 1 of your cards and then return a Suspended card to your hand.";
                color = Color.red;
            }

            public override void activate()
            {
                //TODO
            }
        }

        private class EncroachingDarkness : Card
        {
            public EncroachingDarkness(Character user) : base(user)
            {
                name = "Encroaching Darkness";
                effect = "Return the leftmost Standby card to your hand.\nAlign (R, B): Return all Suspended cards to your hand.";
                color = Color.green;
            }

            public override void activate()
            {
                //TODO
            }
        }

        private class ScheminginShadow : Card
        {
            public ScheminginShadow(Character user) : base(user)
            {
                name = "Scheming in Shadow";
                effect = "Swap 2 cards in your hand with two Standby cards.\nResidual: Rearrange your Standby cards in any order.";
                color = Color.green;
            }

            public override void activate()
            {
                //TODO
            }
        }

        private class UmbralOrder : Card
        {
            public UmbralOrder(Character user) : base(user)
            {
                name = "Umbral Order";
                effect = "Swap up to 2 Suspended cards with 2 cards. (Does not trigger Residual)\nResidual: Empower (2).";
                color = Color.green;
            }

            public override void activate()
            {
                //TODO
            }
        }

        private class DarkEmbrace : Card
        {
            public DarkEmbrace(Character user) : base(user)
            {
                name = "Dark Embrace";
                effect = "Negate 2 damage.\nAlign (R, G): Instead, Absorb.";
                color = Color.blue;
            }

            public override void activate()
            {
                //TODO
            }
        }

        private class CaressingChaos : Card
        {
            public CaressingChaos(Character user) : base(user)
            {
                name = "Caressing Chaos";
                effect = "Negate 2 damage.\nBloodlust: Negate 4 additional damage.\nResidual: Gain 1 health for each of your Suspended cards.";
                color = Color.blue;
            }

            public override void activate()
            {
                //TODO
            }
        }

        private class CalloftheEnd : Card
        {
            public CalloftheEnd(Character user) : base(user)
            {
                name = "Call of the End";
                effect = "Dusk: You may sacrifice 1 health to Suspend 1 of your Standby cards.\nEndure (5): You may return a Suspended card to your hand.";
                color = Color.black;
            }

            public override void activate()
            {
                //TODO
            }
        }

        private class WitnessoftheEnd : Card
        {
            public WitnessoftheEnd(Character user) : base(user)
            {
                name = "Witness of the End";
                effect = "Dawn: You may swap 1 of your Suspended cards with 1 of your Standby cards. (Does not trigger Residual)";
                color = Color.black;
            }

            public override void activate()
            {
                //TODO
            }
        }

        private class HarbingersDecree : Card
        {
            public HarbingersDecree(Character user) : base(user)
            {
                name = "Harbinger’s Decree";
                effect = "tent\nEffect: Align (R, G, B, G): Declare a card type. Your opponent cannot play that card type for the next 2 turns.";
                color = Color.black; //TODO;
            }

            public override void activate()
            {
                //TODO
            }
        }

        private class BloodofKorek : Card
        {
            public BloodofKorek(Character user) : base(user)
            {
                name = "Blood of Korek";
                effect = "fense\nEffect: Align (R, B, R, G): Deal 2 damage. Deal additional damage equal to the difference between you and your opponent’s health totals.";
                color = Color.black; //TODO;
            }

            public override void activate()
            {
                //TODO
            }
        }
    }
}