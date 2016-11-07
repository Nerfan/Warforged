using System;
using System.Collections.Generic;

namespace Warforged
{
    public abstract class Character
    {
        public string name{get; protected set;}
        public string title{get; protected set;}
        public int hp{get; protected set;}
        /* Information about the current turn */
        public int negate{get; set;}
        public int damage{get; set;}
        public int heal{get; set;}
        public int empower{get; set;}
        public int reinforce{get; set;}
        public Card currCard{get; protected set;}
        public List<Card> standby{get; protected set;}
        public List<Card> hand{get; protected set;}
        public List<Card> invocation{get; protected set;}
        public bool stalwart{get; protected set;}
        public bool bloodlust{get; protected set;}
        public Card prevCard{get; protected set;}

        public Character()
        {
            hp = 10;
            negate = 0;
            damage = 0;
            heal = 0;
            empower = 0;
            reinforce = 0;
            standby = new List<Card>();
            hand = new List<Card>();
            invocation = new List<Card>();
            stalwart = false;
            bloodlust = false;
        }

        public void takeDamage(int dmg)
        {
            hp -= dmg;
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
            card.activate(this);
            currCard = card;
            return true;
        }

        /* Nested class representing a generic card */
        public abstract class Card
        {
            public enum Color
            {
                red,
                green,
                blue,
                black
            }
            public string name{get; protected set;}
            public string effect{get; protected set;}
            public Color color{get; protected set;}

            public abstract void activate(Character user);
        }
    }
}

