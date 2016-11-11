using System;
using System.Windows.Media;

namespace Warforged
{
    public class AdriusAspirer : Character
    {
        public AdriusAspirer() : base()
        {
            name = "Adrius";
            title = "The Aspirer";
            hand.Add(new ShatteringBlow(this));
        }

        public override void setupUIForOpponent(GameWindowLibrary lib)
        {
            //TODO: Call lib.setupAdrius(2);
        }

        /* CARDS */
        private class ShatteringBlow : Card
        {
            public ShatteringBlow(Character user) : base(user)
            {
                name = "Shattering Blow";
                effect = "Deal 1 damage.";
                color = Color.red;
            }

            public override void activate()
            {
                user.damage += 1;
            }
        }
    }
}

