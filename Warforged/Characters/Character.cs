using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Media;

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
        protected Color seal;
        // Information about cards
        public Card currCard{get; protected set;}
        public List<Card> standby{get; protected set;}
        public List<Card> hand{get; protected set;}
        public List<Card> invocation{get; protected set;}
        public Card prevCard{get; protected set;}
        // Represents the opposing character
        // Should be fine this way since the game is 1v1
        public Character opponent{get; protected set;}

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
            seal = Color.black;

            standby = new List<Card>();
            hand = new List<Card>();
            invocation = new List<Card>();
        }

        public void bolster()
        {
            for (int i = 0; i < invocation.Count; i++)
            {
                if (!invocation[i].active)
                {
                    invocation[i].active = true;
                    if (invocation[i].color != Color.black)
                    {
                        hand.Add(invocation[i]);
                        invocation.Remove(invocation[i]);
                    }
                    break;
                }
            }
        }
        // Bolster effects will be taken care of in each individual function that would cause the bolster (e.g. Edros bolsters in the dealDamage method)

        /// Take damage and double check that damage numbers are within the allowed range
        /// Behaves just about exactly as expected
        public virtual void takeDamage(int dmg)
        {
            if (dmg < 0)
            {
                dmg = 0;
            }
            hp -= dmg;
        }

        /// These next five methods should ALWAYS be called in order.

        /// Resets all fields that relay information about the current turn.
        /// Also removes overheal from further back than last turn
        public virtual void dawn()
        {
            negate = 0;
            damage = 0;
            heal = 0;
            reflect = false;
            absorb = false;
            if (hp > 10)
            {
                hp -= Math.Min(hp-10, overheal);
            }
            overheal = 0;
            if (hp > 10)
            {
                overheal = hp - 10;
            }
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
            if (card.color == seal)
            {
                return false;
            }
            currCard = card;
            hand.Remove(card);
            return true;
        }

        /// After both players have played their cards, activate this method.
        /// It activates the effects of the cards (i.e. buffing the characters)
        /// then calculates damages and healing.
        public virtual void declarePhase()
        {
            seal = Color.black; // Reset any seal from last turn
            // Declarations should happen BEFORE activateCard(), since activateCard()reads current information and declarations should happen before card calculations.
            currCard.declare();
            activateCard();
        }

        public virtual void damagePhase()
        {
            dealDamage();
            healSelf();
            prevCard = currCard;
            rotate();
        }

        /// Calculates if the character dealt or negated damage this turn
        /// Probably will be overwritten a ton
        /// e.g. If a card effect happens at dusk, then make a new field in the child class
        /// to represent if that effect is happening, then make it happen
        public virtual void dusk()
        {
            bloodlust = (damage > opponent.negate) ? true : false;
            stalwart = (opponent.damage > 0 && negate > 0) ? true : false;
        }

        /// Strive an active inherent.
        /// The parameter can be null. It will fail the second check.
        /// returns true if something was strived
        /// returns false otherwise
        public virtual bool strive(Card card)
        {
            if (invocation.Count == 0)
            {
                return false;
            }
            if (!invocation.Contains(card))
            {
                return false;
            }
            if (!card.active)
            {
                return false;
            }
            card.active = false;
            card.depart();
            return true;
        }

        /// Definitely doesn't need to be its own function
        public virtual void healSelf()
        {
            hp += heal;
        }

        /// Deal damage to another character
        /// This happens AFTER the cards are played and effects activated.
        /// The character has the stats, now they strike.
        public virtual void dealDamage()
        {
            // Will probably need more logic in the future
            int tempdamage = damage - opponent.negate;
            if (opponent.reflect)
            {
                takeDamage(tempdamage);
            }
            else if (opponent.absorb)
            {
                opponent.heal += tempdamage;
            }
            else
            {
                opponent.takeDamage(tempdamage);
            }
        }

        // Pretty simple; activates the current card
        public void activateCard()
        {
            currCard.activate();
        }

        /// Rotate the cards
        /// Active goes to left end of standby
        /// Right end of standby goes to hand
        public void rotate()
        {
            if (currCard.isAwakening)
            {
                currCard.active = false;
                invocation.Add(currCard);
                currCard = null;
                return;
            }
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
                    if (found)
                    {
                        return true;
                    }
                }
                return false;
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


        /// Seals a certain card type for the opponent next turn
        public void sealColor(Color color)
        {
            opponent.seal = color;
        }

        /// Swap a standby card with a card in your hand
        public void swap(Card inHand, Card inStandby)
        {
            if (!hand.Contains(inHand) || !standby.Contains(inStandby))
            {
                return;
            }
            standby.Insert(standby.IndexOf(inStandby), inHand);
            hand.Add(inStandby);
            standby.Remove(inStandby);
            hand.Remove(inHand);
        }

        /// Send a standby card to your hand
        public void takeStandby(Card card)
        {
            if (!standby.Contains(card))
            {
                return;
            }
            hand.Add(card);
            standby.Remove(card);
        }

        /* Nested class representing a generic card */
        public abstract class Card
        {
            public string name{get; protected set;}
            public string effect{get; protected set;}
            public Color color{get; protected set;}
            public bool active = true;
            public bool isAwakening{get; private set;} = false;
            // Nothing other than cards need this reference,
            // since cards are only called through the reference in the first place
            protected Character user;

            //Used in the UI. This is the image assosiated with the card.
            public Brush CardImage { get; protected set; }

            //A variable used to easily get the current directory of card images
            public static string ImageDir = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + System.IO.Path.DirectorySeparatorChar + "CardImages" + System.IO.Path.DirectorySeparatorChar;


            protected Card(Character user)
            {
                this.user = user;
            }

            protected void setAwakening()
            {
                isAwakening = true;
            }

            /// Both players should have locked in cards when this happens
            public virtual void activate() { }

            /// Happens when an Inherent is deactivated
            public virtual void depart() { }

            /// Happens when a card is suspended
            public virtual void residual() { }

            /// Covers anything that the card needs to do before effects happen
            /// i.e. Declaration effects (including swaps, strive)
            public virtual void declare() { }
        }
    }
}

