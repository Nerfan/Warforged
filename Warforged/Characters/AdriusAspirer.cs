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

        /* CARDS */
        private class ShatteringBlow : Card
        {
            public ShatteringBlow(Character user) : base(user)
            {
                name = "Shattering Blow";
                effect = "Deal 1 damage.";
                color = Color.red;
                CardImage = new SolidColorBrush(System.Windows.Media.Color.FromRgb(255,0,0));
            }

            public override void activate()
            {
                user.damage += 1;
            }
        }
    }
}

