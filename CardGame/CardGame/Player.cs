using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame
{
    public class Player
    {

        public int health = 10;

        public int healed
        {
            get
            {
                return healed;
            }
            set
            {
                if (value < 0)
                {
                    healed = 0;
                }
                else
                {
                    healed = value;
                }
            }
        }

        public int empower
        {
            get
            {
                return empower;
            }
            set
            {
                if (value < 0)
                {
                    empower = 0;
                }
                else
                {
                    empower = value;
                }
            }
        }

        public int reinforce
        {
            get
            {
                return reinforce;
            }
            set
            {
                if (value < 0)
                {
                    reinforce = 0;
                }
                else
                {
                    reinforce = value;
                }
            }
        }
            

        public int damage
        {
            get
            {
                return damage;
            }
            set
            {
                if (value < 0)
                {
                    damage = 0;
                }
                else
                {
                    damage = value;
                }
            }
            
        }

        public int damageTaken
        {
            get
            {
                return damageTaken;
            }
            set
            {
                if (value < 0)
                {
                    damageTaken = 0;
                }
                else
                {
                    damageTaken = value;
                }
            }

        }

        public int negatedDamage
        {
            get
            {
                return negatedDamage;
            }
            set
            {
                if(value < 0)
                {
                    negatedDamage = 0;
                }
                else
                {
                    negatedDamage = value;
                }
            }
        }

        public int pierced
        {
            get
            {
                return pierced;
            }
            set
            {
                if (value < 0)
                {
                    pierced = 0;
                }
                else
                {
                    pierced = value;
                }
            }
        }

        public bool didDamage = false;
        public bool didNegate = false;
        public bool didHeal = false;

        public bool safegaurd = false;

        public bool reflect = false;

        public bool absorb = false;

        public bool undying = false;

        

        public List<TYPE> seal = new List<TYPE>();
        public List<Card> playHistory = new List<Card>();


    }
}
