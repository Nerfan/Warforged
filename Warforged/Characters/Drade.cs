using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warforged.Characters
{
    public class Drade : Character
    {
        public override void setupUIForOpponent(GameWindowLibrary lib)
        {

            //TODO: lib.setupDrade(2);
        }
        public Drade() :base()
        {
            name = "Drade";
            title = "the Oathbringer";
            
            hand.Add(new SacredSpear(this));
            hand.Add(new HallowedStrike(this));
            hand.Add(new OathofCourage(this));
            hand.Add(new RetherecsBlessing(this));
            standby.Add(new DivineArmor(this));
            standby.Add(new OathofGlory(this));
            standby.Add(new OathofVigilance(this));
            standby.Add(new OathofVirtue(this));
            invocation.Add(new ASolemnDuty(this));
            invocation.Add(new UnwaveringPiety(this));
            invocation.Add(new Sanctuary(this));
            invocation.Add(new Judgment(this));
            /*library.setupDrade(1);*/
        }

        private class ASolemnDuty : Card
        {
            public ASolemnDuty(Character user) : base(user)
            {
                name = "A Solemn Duty";
                effect = "Whenever you play a counter type of your opponent's card, you may swap an Intent card with a Suspended Intent card.";
                color = Color.black;
            }

            public override void activate()
            {

            }
        }

        private class DivineArmor : Card
        {
            public DivineArmor(Character user) : base(user)
            {
                name = "Divine Armor";
                effect = "Return up to 2 Suspended cards to your hand and then negate 2 damage.";
                color = Color.blue;
            }

            public override void activate()
            {

            }

            public override void declare()
            {

            }
        }

        private class HallowedStrike : Card
        {
            public HallowedStrike(Character user) : base(user)
            {
                name = "Hallowed Strike";
                effect = "Return up to 2 Suspended cards to your hand and then deal 2 damage.";
                color = Color.red;
            }

            public override void activate()
            {

            }

            public override void declare()
            {

            }
        }


        private class Judgment : Card
        {
            public Judgment(Character user) : base(user)
            {
                name = "Judgment";
                effect = "Strive(1): Return X Suspended cards to your hand and then deal 2 damage.\n Strive(+1): Pierce(X).";
                color = Color.red;
                setAwakening();
            }

            public override void activate()
            {

            }

            public override void declare()
            {

            }
        }

        private class RetherecsBlessing : Card
        {
            public RetherecsBlessing(Character user) : base(user)
            {
                name = "Retherec's Blessing";
                effect = "Suspend an Intent card. Return up to 1 Suspended Intent card to your hand and then negate 1 damage.\nStrive(1): Return an additional Suspended card.";
                color = Color.blue;
            }

            public override void activate()
            {

            }

            public override void declare()
            {

            }
        }

        private class SacredSpear : Card
        {
            public SacredSpear(Character user) : base(user)
            {
                name = "Sacred Spear";
                effect = "Suspend an Intent card. Return up to 1 Suspended Intent card to your hand and then deal 1 damage.\nStrive(1): Return an additional Suspended card.";
                color = Color.red;
            }

            public override void activate()
            {

            }

            public override void declare()
            {

            }
        }

        private class Sanctuary : Card
        {
            public Sanctuary(Character user) : base(user)
            {
                name = "Sanctuary";
                effect = "Strive(1): Return X Suspended cards to your hand and then negate 2 damage.\n Strive(+1): Gain 2 health for every returned card.";
                color = Color.blue;
                setAwakening();
            }

            public override void activate()
            {

            }

            public override void declare()
            {

            }
        }

        private class UnwaveringPiety : Card
        {
            public UnwaveringPiety(Character user) : base(user)
            {
                name = "Unwavering Piety";
                effect = "If you have 3 or more Suspended Intent cards, send a Standby card to your hand.";
                color = Color.black;
            }

            public override void activate()
            {

            }
        }

        private class OathofCourage : Card
        {
            public OathofCourage(Character user) : base(user)
            {
                name = "Oath of Courage";
                effect = "Send a standby card to your hand. You may Suspend this card.\nRecal:Imbue(R): Chain(B): Lifesteal. Imbue(B): Chain(R): Suspend 2 other Intent cards.";
                color = Color.green;
            }

            public override void activate()
            {

            }
            public override void declare()
            {

            }
            public override void residual()
            {

            }
        }

        private class OathofGlory : Card
        {
            public OathofGlory(Character user) : base(user)
            {
                name = "Oath of Glory";
                effect = "Send any standby offense cards to your hand. Residual: Reinforce (2).\nRecal:Imbue(R): Deal 1 additional damage for each Suspended Intent card you have.";
                color = Color.green;
            }

            public override void activate()
            {

            }
            public override void declare()
            {

            }
            public override void recall()
            {

            }
            public override void residual()
            {

            }
        }

        private class OathofVigilance : Card
        {
            public OathofVigilance(Character user) : base(user)
            {
                name = "Oath of Vigilance";
                effect = "Send a standby card to your hand. You may Suspend this card.\n[Recal:Imbue(R): Strike: Suspend 2 other Intent cards. Imbue(B): Bloodlust: Absorb.]";
                color = Color.green;
            }

            public override void activate()
            {

            }
            public override void declare()
            {

            }
            public override void residual()
            {

            }
        }

        private class OathofVirtue : Card
        {
            public OathofVirtue(Character user) : base(user)
            {
                name = "Oath of Virtue";
                effect = "Send any standby defense cards to your hand. Residual: Empower(2).\nRecal:Imbue(R): Negate 1 additional damage for each Card in you hand.";
                color = Color.green;
            }

            public override void activate()
            {

            }
            public override void declare()
            {

            }
            public override void recall()
            {

            }
            public override void residual()
            {

            }
        }
    }
}
