using System;

namespace Warforged
{
    public class Edros : Character
    {
        private bool extra;

        public Edros() : base()
        {
            name = "Edros";
            title = "Envoy of Toren";
            extra = false;
            hand.Add(new HandofToren());
            standby.Add(new SkyBlessedShield());
            standby.Add(new SkyBlessedShield());
            standby.Add(new PurgingLightning());
            standby.Add(new SkyBlessedShield());
        }

        /// Deal damage to another character
        public virtual void dealDamage()
        {
            // Will probably need more logic in the future
            int tempdamage = damage - opponent.negate;
            if (opponent.reflect) {
                takeDamage(tempdamage);
            }
            else if (opponent.absorb) {
                opponent.heal += tempdamage;
            }
            else
            {
                opponent.takeDamage(tempdamage);
                bolster();
                if (((Edros)this).extra)
                {
                    bolster();
                    ((Edros)this).extra = false;
                }
            }
        }

        // Offense
        private class PurgingLightning : Card
        {
            public PurgingLightning()
            {
                name = "Purging Lightning";
                effect = "Deal 2 damage.\nBloodlust: Deal 2 additional damage";
                color = Color.red;
            }

            public override void activate(Character user)
            {
                user.damage += 2;
                if (user.bloodlust)
                {
                    user.damage += 2;
                }
            }
        }

        private class HandofToren : Card
        {
            public HandofToren()
            {
                name = "Hand of Toren";
                effect = "Deal 1 damage.\nAlign (B, R, R): Deal 3 additional damage.";
                color = Color.red;
            }

            public override void activate(Character user)
            {
                user.damage += 1;
                if (user.hasAlign("BRR"))
                {
                    user.damage += 3;
                }
            }
        }

        private class RollingThunder : Card
        {
            public RollingThunder()
            {
                name = "Rolling Thunder";
                effect = "Chain (R): Deal 3 damage.\nStrike: Bolster";
                color = Color.red;
            }

            public override void activate(Character user)
            {
                if (user.prevCard.color == Color.red)
                {
                    user.damage += 3;
                }
                // Strike, and thus bolster, are deided after both card effects take place
                ((Edros)user).extra = true;
            }
        }

        private class PillarofLightning : Card
        {
            public PillarofLightning()
            {
                name = "Pillar of Lightning";
                effect = "Deal 2 damage.\nCounter(G): Seal (B)";
                color = Color.red;
            }

            public override void activate(Character user)
            {
                user.damage += 2;
                // TODO: Seal mechanics
            }
        }

        // Defense
        private class SkyBlessedShield : Card
        {
            public SkyBlessedShield()
            {
                name = "Sky Blessed Shield";
                effect = "Gain 2 health.\nEndure (3): Counter (R): Reflect.";
                color = Color.blue;
            }

            public override void activate(Character user)
            {
                user.heal += 2;
                if (user.hp <= 3 && user.opponent.currCard.Color == color.red)
                {
                    user.reflect = true;
                }
            }
        }

        private class TorensFavored : Card
        {
            public TorensFavored()
            {
                name = "Toren's Favored";
                effect = "Strive(1): Negate 3 damage.";
                color = Color.blue;
            }

            public override void activate(Character user)
            {
                //TODO
            }
        }
        //
        // Utility
        private class FaithUnquestioned : Card
        {
            public FaithUnquestioned()
            {
                name = "Faith Unquestioned";
                effect = "Swap 1 Offense card in your hand with 1 Standby card.\nStrive(1): Send a Standby Defense card to your hand.";
                color = Color.green;
            }

            public override void activate(Character user)
            {
                //TODO
            }
        }
    }
}
