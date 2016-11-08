using System;
using System.Collections.Generic;

namespace Warforged
{
    public enum Color
    {
        red,
        green,
        blue,
        black
    }

    public abstract class Character
    {
        // Overall information
        public string name{get; protected set;}
        public string title{get; protected set;}
        public int hp{get; protected set;}
        // Information about the current turn
        public int negate{get; set;}
        public int damage{get; set;}
        public int heal{get; set;}
        public int empower{get; set;}
        public int reinforce{get; set;}
        public bool reflect{get; set;}
        public bool absorb{get; set;}
        // Information about previous turn
        public bool stalwart{get; protected set;}
        public bool bloodlust{get; protected set;}
        protected int overheal;
        // Information about cards
        public Card currCard{get; protected set;}
        public List<Card> standby{get; protected set;}
        public List<Card> hand{get; protected set;}
        public List<Card> invocation{get; protected set;} // Not bolstered
        // Fill with invocation cards
        public List<Card> invoked{get; protected set;} // Bosltered
        // Fill with blank cards to begin with
        public Card prevCard{get; protected set;}
        // Represents the opposing character
        // Should be fine tihs way sicne the game is 1v1
        protected Character opponent;

        public void setOpponent(Character opponent)
        {
            this.opponent = opponent;
        }

        public Character()
        {
            hp = 10;
            negate = 0;
            damage = 0;
            heal = 0;
            empower = 0;
            reinforce = 0;
            reflect = false;
            absorb = false;
            stalwart = false;
            bloodlust = false;
            overheal = 0;
            standby = new List<Card>();
            hand = new List<Card>();
            invocation = new List<Card>();
            invoked = new List<Card>();
        }

        public void bolster()
        {
            // Check for first non-invoked card and do that one
            // i.e. while invoked.contains(invocation[i])
            // Move cards out of the invocation to the invoked
        }
        // Bolster effects will be taken care of in each individual function that would cause the bolster (e.g. Edros bolsters in the dealDamage method)

        public virtual void takeDamage(int dmg)
        {
            if (dmg < 0)
            {
                dmg = 0;
            }
            hp -= dmg;
        }

        public virtual void dawn()
        {
            negate = 0;
            damage = 0;
            heal = 0;
            empower = 0;
            reinforce = 0;
            reflect = false;
            absorb = false;
            if (hp > 10)
            {
                hp -= Math.Min(hp-10, overheal);
            }
            hp -= overheal;
            overheal = 0;
            if (hp > 10)
            {
                overheal = hp - 10;
            }
        }

        public virtual void mainPhase()
        {
            activateCard();
            dealDamage();
            healSelf();
            prevCard = currCard;
            rotate();
        }

        public virtual void dusk()
        {
            bloodlust = (damage > opponent.negate) ? true : false;
            stalwart = (opponent.damage > 0 && negate > 0) ? true : false;
        }


        public virtual void healSelf()
        {
            hp += heal;
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
            }
        }

        public void activateCard()
        {
            // Somehow will need to check opponent's card
            // (countertyping, etc.)
            currCard.activate(this);
        }

        /// Play a card from your hand
        /// If your hand does not contain the card, the method returns false
        /// Returns true otherwise
        public bool playCard(Card card)
        {
            if (!hand.Contains(card))
            {
                return false;
            }
            currCard = card;
            hand.Remove(card);
            return true;
        }

        /// Rotate the cards
        /// Active goes to left end of standby
        /// Right end of standby goes to hand
        public void rotate()
        {
            standby.Insert(0, currCard);
            currCard = null;
            if (standby.Count > 4) // Rotate if there are more than 4 cards
            {
                hand.Add(standby[standby.Count-1]);
                standby.Remove(hand[hand.Count-1]);
            }
        }

        /// Tells whether or not a player has an align on their standby
        /// align is formatted without any spaces, just letters
        /// e.g. "RBB"
        /// align MUST be at least 2 characters
        /// index should never be specified outside this function.
        public bool hasAlign(string align, int index=0)
        {
            Color color;
            if (align[0] == 'B')
            {
                color = Color.blue;
            }
            else if (align[0] == 'R')
            {
                color = Color.red;
            }
            else if (align[0] == 'G')
            {
                color = Color.green;
            }
            else // Should never happen
            {
                color = Color.black;
            }

            if (index == 0)
            {
                // Safety check
                if (align.Length < 2)
                {
                    return false;
                }
                bool found = false;
                for (int i = 0; i < standby.Count-1; i++)
                {
                    if (standby[i].color == color)
                    {
                        found = hasAlign(align.Substring(1), i+1);
                    }
                }
                return found;
            }
            // Stop recursion if the align breaks
            if (standby[index].color != color)
            {
                return false;
            }
            // Also stop if we're at the last character
            if (standby[index].color == color && align.Length == 1)
            {
                return true;
            }
            // If we're good on the current character, go to the next one
            return (hasAlign(align.Substring(1), index+1));
        }



        /* Nested class representing a generic card */
        public abstract class Card
        {
            public string name{get; protected set;}
            public string effect{get; protected set;}
            public Color color{get; protected set;}

            /// Both players should have locked in cards when this happens
            public abstract void activate(Character user);

            public virtual void depart() { }

            public virtual void residual() { }
        }
    }
}

