using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Warforged
{
    public class Edros : Character
    {
        private bool bolster2;
        private bool bonusEmp;

        public Edros() : base()
        {
            name = "Edros";
            title = "Envoy of Toren";
            bolster2 = false;
            bonusEmp = false;
            hand.Add(new HandofToren(this));
            hand.Add(new CelestialSurge(this));
            hand.Add(new PillarofLightning(this));
            hand.Add(new TorensFavored(this));
            standby.Add(new SkyBlessedShield(this));
            standby.Add(new RollingThunder(this));
            standby.Add(new PurgingLightning(this));
            standby.Add(new FaithUnquestioned(this));
            invocation.Add(new GraceofHeaven(this));
            invocation.Add(new ScornofThunder(this));
            invocation.Add(new CrashingSky(this));
            invocation.Add(new WrathofLightning(this));
            library.setupEdros(1);
        }

        public override void setupUIForOpponent(GameWindowLibrary lib)
        {
            lib.setupEdros(2);
        }

        /// Deal damage to another character
        public override void dealDamage()
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
                if (((Edros)this).bonusEmp)
                {
                    empower += 1;
                    ((Edros)this).bonusEmp = false;
                }
                if (((Edros)this).bolster2)
                {
                    bolster();
                    ((Edros)this).bolster2 = false;
                }
            }
        }

        // Offense
        private class PurgingLightning : Card
        {
            public PurgingLightning(Character user) : base(user)
            {
                name = "Purging Lightning";
                effect = "Deal 2 damage.\nBloodlust: Deal 2 additional damage";
                color = Color.red;
            }

            public override void activate()
            {
                user.damage += 2 + user.empower;
                user.empower = 0;
                if (user.bloodlust)
                {
                    user.damage += 2;
                }
            }
        }

        private class HandofToren : Card
        {
            public HandofToren(Character user) : base(user)
            {
                name = "Hand of Toren";
                effect = "Deal 1 damage.\nAlign (B, R, R): Deal 3 additional damage.";
                color = Color.red;
            }

            public override void activate()
            {
                user.damage += 1 + user.empower;
                user.empower = 0;
                if (user.hasAlign("BRR"))
                {
                    user.damage += 3;
                }
            }
        }

        private class RollingThunder : Card
        {
            public RollingThunder(Character user) : base(user)
            {
                name = "Rolling Thunder";
                effect = "Chain (R): Deal 3 damage.\nStrike: Bolster";
                color = Color.red;
            }

            public override void activate()
            {
                if (user.prevCard.color == Color.red)
                {
                    user.damage += 3 + user.empower;
                    user.empower = 0;
                }
                // Strike, and thus bolster, are deided after both card effects take place
                ((Edros)user).bolster2 = true;
            }
        }

        private class PillarofLightning : Card
        {
            public PillarofLightning(Character user) : base(user)
            {
                name = "Pillar of Lightning";
                effect = "Deal 2 damage.\nCounter(G): Seal (B)";
                color = Color.red;
            }

            public override void activate()
            {
                user.damage += 2 + user.empower;
                user.empower = 0;
                if (user.opponent.currCard.color == Color.green)
                {
                    user.sealColor(Color.blue);
                }
            }
        }

        private class CelestialSurge : Card
        {
            public CelestialSurge(Character user) : base(user)
            {
                name = "Celestial Surge";
                effect = "Deal 2 damage.\nStrike: Empower (1).";
                color = Color.red;
            }

            public override void activate()
            {
                user.damage += 2 + user.empower;
                user.empower = 0;
                ((Edros)user).bonusEmp = true;
            }
        }

        // Defense TODO
        private class SkyBlessedShield : Card
        {
            public SkyBlessedShield(Character user) : base(user)
            {
                name = "Sky Blessed Shield";
                effect = "Gain 2 health.\nEndure (3): Counter (R): Reflect.";
                color = Color.blue;
            }

            public override void activate()
            {
                user.heal += 2;
                if (user.hp <= 3 && user.opponent.currCard.color == Color.red)
                {
                    user.reflect = true;
                }
            }
        }

        private class TorensFavored : Card
        {
            private bool strove = false;
            public TorensFavored(Character user) : base(user)
            {
                name = "Toren's Favored";
                effect = "Strive(1): Negate 3 damage.";
                color = Color.blue;
            }

            public override void activate()
            {
                if (((TorensFavored)this).strove)
                {
                    user.negate += 3 + user.reinforce;
                    user.reinforce = 0;
                }
            }

            public override void declare()
            {
                // TODO MAJOR
                // Prompt for input
                // null should be accepted as input; that means they choose not to strive
                // store input in striveCard
                Card striveCard = user.library.waitForClickOrCancel("Strive 1 card");
                if (user.strive(striveCard))
                {
                    ((TorensFavored)this).strove = true;
                }
            }
        }

        // Utility TODO
        private class FaithUnquestioned : Card
        {
            private bool strove = false;
            Card offenseCard = null;
            Card standbyCard = null;
            Card defenseCard = null;
            public FaithUnquestioned(Character user) : base(user)
            {
                name = "Faith Unquestioned";
                effect = "Swap 1 Offense card in your hand with 1 Standby card.\nStrive(1): Send a Standby Defense card to your hand.";
                color = Color.green;
            }

            public override void activate()
            {
                user.swap(offenseCard, standbyCard);
                if (((FaithUnquestioned)this).strove)
                {
                    user.takeStandby(defenseCard);
                }
            }

            public override void declare()
            {
                // GUI should do checking for offense cards, or right here
                // TODO ASK FOR CARDS TO SWAP
                if(canSwap())
                {
                    user.library.setPromptText("Swap an offense card from your hand with a standbycard");
                    while(true)
                    {
                        Character.Card card1 = user.library.waitForClick();
                        user.library.highlight(card1, 255, 255, 0);
                        Character.Card card2 = user.library.waitForClick();
                        user.library.clearAllHighlighting();
                        if(user.hand.Contains(card1) && user.standby.Contains(card2))
                        {
                            offenseCard = card1;
                            standbyCard = card2;
                            break;
                        }
                        else if(user.hand.Contains(card2) && user.standby.Contains(card1))
                        {
                            offenseCard = card2;
                            standbyCard = card1;
                            break;
                        }
                    }
                }
                // ASK IF WANT TO STRIVE
                // ASK WHAT TO STRIVE
                bool canStrive = false;
                foreach(Character.Card c in user.standby)
                {
                    if(c.color == Color.blue)
                    {
                        canStrive = true;
                        break;
                    }
                }

                int blueCardsInStandby = 0;
                foreach (Character.Card card in user.standby)
                {
                    if (card.color == Color.blue)
                    {
                        blueCardsInStandby += 1;
                    }
                }
                while (canStrive && (blueCardsInStandby == 2 || standbyCard.color != Color.blue))
                {
                    Character.Card card = user.library.waitForClickOrCancel("Choose an inherent to strive");
                    if(card == null)
                    {
                        ((FaithUnquestioned)this).strove = false;
                        break;
                    }
                    else if (user.strive(card))
                    {
                        ((FaithUnquestioned)this).strove = true;
                        break;
                    }
                }
                // ASK WHAT TO TAKE
                while(((FaithUnquestioned)this).strove )
                {
                    user.library.setPromptText("Choose a blue standby card to send to your hand");
                    Character.Card card = user.library.waitForClick();
                    if(card.color == Color.blue && user.standby.Contains(card) && card != standbyCard)
                    {
                        ((FaithUnquestioned)this).defenseCard = card;
                        break;
                    }
                }
                user.library.setPromptText("");
            }

            private bool canSwap()
            {
                if (user.hand.Count == 0 || user.standby.Count == 0)
                {
                    return false;
                }
                foreach(Character.Card card in user.hand)
                {
                    if(card.color == Color.red)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        // Invocation TODO all of these
        private class WrathofLightning : Card
        {
            public WrathofLightning(Character user) : base(user)
            {
                name = "Wrath of Lightning";
                effect = "Depart: Deal damage equal to the amount of Standby Offense cards you have.";
                color = Color.black;
                active = false;
            }

            public override void depart()
            {
                int offenseCards = 0;
                foreach(Character.Card card in user.standby)
                {
                    if(card.color == Color.red)
                    {
                        offenseCards += 1;
                    }
                }
                user.damage += offenseCards;
            }
        }

        private class ScornofThunder : Card
        {
            public ScornofThunder(Character user) : base(user)
            {
                name = "Scorn of Thunder";
                effect = "Depart: Return up to 2 Standby cards to your hand.";
                color = Color.black;
                active = false;
            }

            public override void depart()
            {
            Character.Card card1 = null;
            Character.Card card2 = null;
            while (true)
                {
                    if (user.standby.Count > 0)
                    {
                        card1 = user.library.waitForClickOrCancel("Select up to 2 standby cards to send to your hand");
                        if(card1 != null && !user.standby.Contains(card1))
                        {
                            continue;
                        }
                        if(card1 == null)
                        {
                            break;
                        }
                        while (true)
                        {
                            if (user.standby.Count > 1)
                            {
                                card2 = user.library.waitForClickOrCancel("Select up to 1 more standby card to send to your hand");
                                if (card2 != null && !user.standby.Contains(card2))
                                {
                                    continue;
                                }
                                else
                                {
                                    goto EndLoop;
                                }
                            }
                            else
                            {
                                goto EndLoop;
                            }
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                EndLoop:
                if (card1 == null)
                {
                    return;
                }
                else if(card2 == null)
                {
                    user.hand.Add(card1);
                }
                else
                {
                    user.takeStandby(card1);
                    user.takeStandby(card2);
                }

            }
        }

        private class GraceofHeaven : Card
        {
            public GraceofHeaven(Character user) : base(user)
            {
                name = "Grace of Heaven";
                effect = "Depart: Gain health equal to amount of health your opponent is missing.";
                color = Color.black;
                active = false;
            }

            public override void depart()
            {
                user.heal += Math.Max(10-user.opponent.hp,0);
            }
        }

        private class CrashingSky : Card
        {
            public CrashingSky(Character user) : base(user)
            {
                name = "Crashing Sky";
                effect = "Choose 1:\nStrive (X)\nOR\nStrive(3): Deal 3 damage.";
                color = Color.red;
                setAwakening();
                active = false;
            }
            bool dealsOwnDamage = false;
            public override void activate()
            {
                if(dealsOwnDamage)
                {
                    user.damage += 3 + user.empower;
                    user.empower = 0;
                }
            }
            public override void declare()
            {
                List<string> texts = new List<string>();
                List<object> returns = new List<object>();
                texts.Add("0");
                returns.Add(0);
                int awakening = 0;
                for (int i = 0; i < 3; ++i)
                {
                    var card = user.invocation[i];
                    if (card.isAwakening || !card.active)
                    {
                        awakening += 1;
                        continue;
                    }

                    texts.Add((i + 1 - awakening) + "");
                    returns.Add(i + 1 - awakening);

                }
                int cards = (int)user.library.multiPrompt("How many cards do you want to Strive?", texts, returns);
                for(int i =0; i< cards;++i)
                {
                    while(true)
                    {
                        user.library.setPromptText("Please select "+(cards-i)+" card(s) to strive");
                        var card = user.library.waitForClick();
                        if(user.invocation.Contains(card))
                        {
                            user.strive(card);
                            user.library.updateUI(user, false);

                            break;
                        }
                    }
                }
                user.library.setPromptText("");
                if(cards == 3)
                {
                    dealsOwnDamage = true;
                }
            }
        }
    }
}
