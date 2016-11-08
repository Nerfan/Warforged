using System;

namespace Warforged
{
    public class AdriusAspirer : Character
    {
        public AdriusAspirer() : base()
        {
            name = "Adrius";
            title = "The Aspirer";
            hand.Add(new ShatteringBlow());
        }

        /* CARDS */
        private class ShatteringBlow : Card
        {
            public ShatteringBlow()
            {
                name = "Shattering Blow";
                effect = "Deal 1 damage.";
                color = Color.red;
            }

            public override void activate(Character user)
            {
                user.damage = 1 + user.empower;
            }
        }
    }
}

