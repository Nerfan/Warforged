using System;
namespace Warforged
{
    [Serializable]
    public class Adrius : Character
    {
        private enum Form
        {
            Aspier,
            Bearer,
            Incarnate
        }

        private Form form;

        public Adrius() : base()
        {
            name = "Adrius";
            title = "The Aspirer";
            form = Form.Aspier;
        }

        public override void dawn()
        {
            base.dawn();
            if (form == Form.Incarnate)
            {
                bolster();
            }
        }

        // TODO lower form bolsters and inherents

        public override int dealDamage()
        {
            if (base.dealDamage && form == Form.Aspier)
            {
                bolster();
            }
        }
        /// Inherents are handled in here.
        /// We make sure all conditions are met, then "ascend" to play the card.
        /// After the card is played, we go back to the old form.
        public override void declarePhase()
        {
            Form currform = form;
            foreach (Card inherent in invocation)
            {
                if (inherent.active)
                {
                    if (inherent.name == "Ruby Heart" && hasAlign("GB") && prevCard.color == Color.blue && currCard.color == Color.red)
                    {
                        if (form == Form.Aspier)
                        {
                            form = Form.Bearer;
                        }
                        else if (form == Form.Bearer)
                        {
                            form = Form.Incarnate;
                        }
                    }
                    if (inherent.name == "Emerald Core" && hasAlign("BRRB") && currCard.color == Color.green)
                    {
                        if (form == Form.Aspier)
                        {
                            form = Form.Bearer;
                        }
                        else if (form == Form.Bearer)
                        {
                            form = Form.Incarnate;
                        }
                    }
                    if (inherent.name == "Sapphire Mantle" && hasAlign("RG") && bloodlust && currCard.color == Color.blue)
                    {
                        if (form == Form.Aspier)
                        {
                            form = Form.Bearer;
                        }
                        else if (form == Form.Bearer)
                        {
                            form = Form.Incarnate;
                        }
                    }
                }
            }
            base.declarePhase();
            form = currform;
        }

        public override void damagePhase()
        {
            bool canSurpass = (hp <= opponent.hp);
            base.damagePhase();
            if (opponent.damage > 0 && negate > 0 && form == Form.Aspier)
            {
                bolster();
            }
            if (canSurpass && hp > opponent.hp && form == Form.Bearer)
            {
                bolster();
            }
        }

        /// Utility method becuase several cards use this
        private int activeInherents()
        {
            int count = 0;
            foreach (Card inherent in invocation)
            {
                if (inherent.active)
                {
                    count += 1;
                }
            }
            return count;
        }

        private class FistofRuin : Card
        {
            public FistofRuin() : base()
            {
                name = "Fist of Ruin";
                effect = "Deal 2 damage.\nBearer: Pierce (2).\nIncarnate: Deal additional damage equal to the amount of active Inherents you have.";
                color = Color.red;
            }

            public override void activate()
            {
                // Base
                user.addDamage(2);
                if (((Adrius)user).form >= Form.Bearer)
                {
                    // Bearer
                    user.pierce += 2;
                }
                if (((Adrius)user).form >= Form.Incarnate)
                {
                    // Incarnate
                    user.addDamage(((Adrius)user).activeInherents());
                }
            }
        }

        private class ShatteringBlow : Card
        {
            public ShatteringBlow() : base()
            {
                name = "Shattering Blow";
                effect = "Deal 1 damage.\nBearer: Counter (G): Bolster.\nIncarnate: Empower equal to the amount of active Inherents you have.";
                color = Color.red;
            }

            public override void activate()
            {
                // Base
                user.addDamage(1);
                if (((Adrius)user).form >= Form.Bearer)
                {
                    // Bearer
                    if (user.opponent.currCard.color == Color.green)
                    {
                        user.bolster();
                    }
                }
                if (((Adrius)user).form >= Form.Incarnate)
                {
                    // Incarnate
                    user.empower += ((Adrius)user).activeInherents();
                }
            }
        }

        private class TremoringImpact : Card
        {
            public TremoringImpact() : base()
            {
                name = "Tremoring Impact";
                effect = "Deal 2 damage.\nBearer: Bloodlust: Empower (1).\nIncarnate: Double your Empower effects this turn.";
                color = Color.red;
            }

            public override void activate()
            {
                if (((Adrius)user).form >= Form.Bearer)
                {
                    // Bearer
                    if (user.bloodlust)
                    {
                        user.empower += 1;
                    }
                }
                if (((Adrius)user).form >= Form.Incarnate)
                {
                    // Incarnate
                    user.damage += user.currEmpower;
                }
                // Base
                // This goes after becuase it "uses up" the empower
                user.addDamage(2);
            }
        }

        private class EarthPiercer : Card
        {
            public EarthPiercer() : base()
            {
                name = "Earth Piercer";
                effect = "Deal 1 damage.\nBearer: Strike: Empower (2). Reinforce (2).\nIncarnate: Pierce (4).";
                color = Color.red;
            }

            public override void activate()
            {
                // Base
                user.addDamage(1);
                if (((Adrius)user).form >= Form.Incarnate)
                {
                    // Incarnate
                    user.pierce += 4;
                }
                if (((Adrius)user).form >= Form.Bearer)
                {
                    // Bearer
                    // Last because this relies on pierce
                    if (!(user.opponent.absorb || user.opponent.reflect || (user.opponent.negate - user.pierce) >= user.damage))
                    {
                        user.empower += 2;
                        user.reinforce += 2;
                    }
                }
            }
        }

        private class HerosResolution : Card
        {
            private Card card1;
            private List<Card> incarnateCards;
            public HerosResolution() : base()
            {
                name = "Hero’s Resolution";
                effect = "Counter (B): Bolster\nBearer: Send a Standby card to your hand.\nIncarnate: Send Standby cards to your hand equal to the amount of active Inherents you have.";
                color = Color.green;
                incarnateCards = new List<Card>(3);
            }

            public override void activate()
            {
                // Base
                if (user.opponent.currCard.color == Color.blue)
                {
                    user.bolster;
                }
                if (((Adrius)user).form >= Form.Bearer)
                {
                    // Bearer
                    user.takeStandby(card1);
                    card1 = null;
                }
                if (((Adrius)user).form >= Form.Incarnate)
                {
                    // Incarnate
                    foreach (Card card in incarnateCards)
                    {
                        user.takeStandby(card);
                    }
                    incarnateCards.Clear();
                }
            }

            public override void declare()
            {
                if (((Adrius)user).form >= Form.Bearer)
                {
                    // Bearer
                    Game.library.setPromptText("Select a standby card to send to your hand.");
                    while (true)
                    {
                         card1 = Game.library.waitForClick();
                        if (user.standby.Contains(card1))
                        {
                            break;
                        }
                    }
                    Game.library.setPromptText("");
                }
                if (((Adrius)user).form >= Form.Incarnate)
                {
                    // Incarnate
                    for (int i = 0; i < user.activeInherents(); i++)
                    {
                        Game.library.setPromptText("Select a standby card to send to your hand."); // TODO format this with number
                        while (true)
                        {
                            Card card = Game.library.waitForClick();
                            if (user.standby.Contains(card) && !incarnateCards.Contains(card))
                            {
                                incarnateCards.Add(card);
                                Game.library.highlight(card, 255, 255, 0);
                                break;
                            }
                        }
                    }
                    Game.library.clearAllHighlighting();
                    Game.library.setPromptText("");
                }
            }
        }

        private class UnyieldingFaith : Card
        {
            private List<Card> stdbyCards, handCards;
            public UnyieldingFaith() : base()
            {
                name = "Unyielding Faith";
                effect = "You may swap a Standby card with a card in your hand.\nBearer: Chain (R): You may swap up to 2 additional Standby cards.\nIncarnate: Swap up to 4 Standby cards with 4 cards in your hand.";
                color = Color.green;
                stdbyCards = new List<Card>(4);
                handCards = new List<Card>(4);
            }

            public override void activate()
            {
                for (int i = 0; i < stdbyCards.Count; i++)
                {
                    user.swap(stdbyCards[i], handCards[i]);
                }
                stdbyCards.Clear();
                handCards.Clear();
            }

            public override void declare()
            {
                // TODO this is a LOT of stuff, so something is definitely broken here.
                while (true)
                {
                    Card card = Game.library.waitForClickOrCancel("Select a Standby card to swap.");
                    if (user.standby.Contains(card))
                    {
                        stdbyCards.Add(card);
                        break;
                    }
                    else if (card == null)
                    {
                        break;
                    }
                }
                if (card != null)
                {
                    Game.library.setPromptText("Select a card from your hand to swap.");
                    while (true)
                    {
                        card = Game.library.waitForClick();
                        if (user.hand.Contains(card))
                        {
                            handCards.Add(card);
                            break;
                        }
                    }
                }
                if (((Adrius)user).form >= Form.Bearer && user.prevCard.color == Color.red)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        while (true)
                        {
                            Card card = Game.library.waitForClickOrCancel("Select a Standby card to swap.");
                            if (user.standby.Contains(card))
                            {
                                stdbyCards.Add(card);
                                break;
                            }
                            else if (card == null)
                            {
                                break;
                            }
                        }
                        if (card != null)
                        {
                            Game.library.setPromptText("Select a card from your hand to swap.");
                            while (true)
                            {
                                card = Game.library.waitForClick();
                                if (user.hand.Contains(card))
                                {
                                    handCards.Add(card);
                                    break;
                                }
                            }
                        }
                    }
                }
                if (((Adrius)user).form >= Form.Incarnate)
                {
                    for (int i = 0; i < 4-stdbyCards.Count; i++)
                    {
                        while (true)
                        {
                            Card card = Game.library.waitForClickOrCancel("Select a Standby card to swap.");
                            if (user.standby.Contains(card))
                            {
                                stdbyCards.Add(card);
                                break;
                            }
                            else if (card == null)
                            {
                                break;
                            }
                        }
                        if (card != null)
                        {
                            Game.library.setPromptText("Select a card from your hand to swap.");
                            while (true)
                            {
                                card = Game.library.waitForClick();
                                if (user.hand.Contains(card))
                                {
                                    handCards.Add(card);
                                    break;
                                }
                            }
                        }
                    }
                }
                Game.library.setPromptText("");
            }
        }

        private class WillUnbreakable : Card
        {
            public WillUnbreakable() : base()
            {
                name = "Will Unbreakable";
                effect = "Negate 2 damage.\nUndying.\nBearer: Counter (R): Insead, Absorb.\nIncarnate: Align (R, G): Gain 4 health.";
                color = Color.blue;
            }

            public override void activate()
            {
                // Base
                user.undying = true;
                if (((Adrius)user).form >= Form.Bearer)
                {
                    // Bearer
                    if (user.opponent.currCard.color == Color.red)
                    {
                        user.absorb = true;
                    }
                    else
                    {
                        user.addNegate(2);
                    }
                }
                else
                {
                    user.addNegate(2);
                }
                if (((Adrius)user).form >= Form.Incarnate)
                {
                    // Incarnate
                    if (user.hasAlign("RG"))
                    {
                        user.heal += 4;
                    }
                }
            }
        }

        private class SurgingHope : Card
        {
            public SurgingHope() : base()
            {
                name = "Surging Hope";
                effect = "Negate 2 damage.\nBearer: Bloodlust: Negate 2 additional damage.\nIncarnate: Safeguard.\nEmpower equal to the amount of damage negated.";
                color = Color.blue;
            }

            public override void activate()
            {
                // Base
                user.addNegate(2);
                if (((Adrius)user).form >= Form.Bearer)
                {
                    // Bearer
                    if (user.bloodlust)
                    {
                        user.addNegate(2);
                    }
                }
                if (((Adrius)user).form >= Form.Incarnate)
                {
                    // Incarnate
                    int negated = user.opponent.damage;
                    if (negated > user.negate)
                    {
                        negated = user.negate;
                    }
                    user.empower += negated;
                }
            }
        }



        private class RubyHeart : Card
        {
            public RubyHeart() : base()
            {
                name = "Ruby Heart";
                effect = "Align (G, B): Chain (B): This turn, your Offense cards are Ascended.";
                color = Color.black;
                active = false;
            }
        }

        private class EmeraldCore : Card
        {
            public EmeraldCore() : base()
            {
                name = "Emerald Core";
                effect = "Align (B, R, R, B): This turn, your Intent cards are Ascended.";
                color = Color.black;
                active = false;
            }
        }

        private class SapphireMantle : Card
        {
            public SapphireMantle() : base()
            {
                name = "Sapphire Mantle";
                effect = "Align (R, G): Bloodlust: This turn, your Defense cards are Ascended.";
                color = Color.black;
                active = false;
            }
        }

        private class Ascendance: Card
        {
            public Ascendance() : base()
            {
                name = "Ascendance (First Form)";
                effect = "Effect: Strive (3): Gain 5 health.\nAspirer: Ascend to Bearer.\nBearer: Ascend to Incarnate and Shift this card.";
                color = Color.blue;
            }

            public override void activate()
            {
                foreach (Card inherent in user.invocation)
                {
                    user.strive(inherent);
                }
                if (((Adrius)user).form == Form.Aspier)
                {
                    ((Adrius)user).form = Form.Bearer);
                }
                if (((Adrius)user).form == Form.Bearer)
                {
                    ((Adrius)user).form = Form.Incarnate);
                }
                // TODO shift this card
            }
        }

        private class DivineCataclysm: Card
        {
            public DivineCataclysm() : base()
            {
                name = "Divine Cataclysm (Incarnate Form)";
                effect = "Effect: Strive (3): Deal damage equal to the sum of you and your opponent’s health.";
                color = Color.red;
            }

            public override void activate()
            {
                foreach (Card inherent in user.invocation)
                {
                    user.strive(inherent);
                }
                user.addDamage(user.hp + user.opponent.hp);
            }
        }
    }
}
