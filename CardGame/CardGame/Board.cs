using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame
{
    public class Board
    {
        public Player GoodPlayer = new Player();

        public Player EvilPlayer = new Player();

        public Card GoodStandby1 = null;
        public Card GoodStandby2 = null;
        public Card GoodStandby3 = null;
        public Card GoodStandby4 = null;

        public Card EvilStandby1 = null;
        public Card EvilStandby2 = null;
        public Card EvilStandby3 = null;
        public Card EvilStandby4 = null;

        public HashSet<Card> GoodHand = new HashSet<Card>();
        public HashSet<Card> EvilHand = new HashSet<Card>();

        public HashSet<Card> GoodSuspended = new HashSet<Card>();
        public HashSet<Card> EvilSuspended = new HashSet<Card>();

        public Card GoodLineup1 = null;
        public Card GoodLineup2 = null;
        public Card GoodLineup3 = null;
        public Card GoodLineup4 = null;

        public Card EvilLineup1 = null;
        public Card EvilLineup2 = null;
        public Card EvilLineup3 = null;
        public Card EvilLineup4 = null;

        public Card GoodCharacter = null;
        public Card EvilCharacter = null;

        public Card GoodInPlay = null;
        public Card EvilInPlay = null;

        public Dictionary<Card, List<Card>> GoodLink1 = new Dictionary<Card, List<Card>>();
        public Dictionary<Card, List<Card>> GoodLink2 = new Dictionary<Card, List<Card>>();
        public Dictionary<Card, List<Card>> GoodLink3 = new Dictionary<Card, List<Card>>();
        public Dictionary<Card, List<Card>> GoodLink4 = new Dictionary<Card, List<Card>>();

        public Dictionary<Card, List<Card>> EvilLink1 = new Dictionary<Card, List<Card>>();
        public Dictionary<Card, List<Card>> EvilLink2 = new Dictionary<Card, List<Card>>();
        public Dictionary<Card, List<Card>> EvilLink3 = new Dictionary<Card, List<Card>>();
        public Dictionary<Card, List<Card>> EvilLink4 = new Dictionary<Card, List<Card>>();

        /**
         * Moves empwowerment and reinforce to damage and negation given the correct play
         */
        public void calcEmpowerReinforce()
        {
            if(GoodInPlay !=null && GoodInPlay.type == TYPE.RED)
            {
                GoodPlayer.damage += GoodPlayer.empower;
            }

            if (EvilInPlay != null && EvilInPlay.type == TYPE.RED)
            {
                EvilPlayer.damage += EvilPlayer.empower;
            }

            if (GoodInPlay != null && GoodInPlay.type == TYPE.BLUE)
            {
                GoodPlayer.negatedDamage += GoodPlayer.reinforce;
            }

            if (EvilInPlay != null && EvilInPlay.type == TYPE.BLUE)
            {
                EvilPlayer.negatedDamage += EvilPlayer.reinforce;
            }

            GoodPlayer.empower = 0;
            GoodPlayer.reinforce = 0;
            EvilPlayer.empower = 0;
            EvilPlayer.reinforce = 0;
        }

        /**
         * Calculates damage for each player.
         */
        public void calcDamage()
        {

            int negatePierce = 0;
            int totalDamage = 0;
            if(GoodPlayer.safegaurd && EvilPlayer.safegaurd)
            {
                GoodPlayer.didNegate = EvilPlayer.damage > 0;
                GoodPlayer.didDamage = false;
                GoodPlayer.didHeal = GoodPlayer.healed > 0;

                EvilPlayer.didNegate = GoodPlayer.damage > 0;
                EvilPlayer.didDamage = false;
                EvilPlayer.didHeal = EvilPlayer.healed > 0;

                GoodPlayer.health += GoodPlayer.healed;

                EvilPlayer.health += EvilPlayer.healed;

                return;
            }
            if (GoodPlayer.reflect && EvilPlayer.reflect)
            {
                negatePierce = Math.Max(EvilPlayer.negatedDamage - GoodPlayer.pierced, 0);
                totalDamage = Math.Max(EvilPlayer.damage - (Math.Max(GoodPlayer.negatedDamage - EvilPlayer.pierced, 0)) + GoodPlayer.damage - negatePierce, 0);

                EvilPlayer.didNegate = negatePierce > 0 && totalDamage > 0;


                negatePierce = Math.Max(GoodPlayer.negatedDamage - EvilPlayer.pierced, 0);
                totalDamage = Math.Max(GoodPlayer.damage - (Math.Max(EvilPlayer.negatedDamage - GoodPlayer.pierced, 0))  + EvilPlayer.damage - negatePierce, 0);

                GoodPlayer.didNegate = negatePierce > 0 && totalDamage > 0;

                GoodPlayer.health += GoodPlayer.healed;
                EvilPlayer.health += EvilPlayer.healed;
                GoodPlayer.didHeal = GoodPlayer.healed > 0;
                EvilPlayer.didHeal = EvilPlayer.healed > 0;

                if (GoodPlayer.undying)
                {
                    GoodPlayer.health = Math.Max(GoodPlayer.health, 1);
                }

                if (EvilPlayer.undying)
                {
                    EvilPlayer.health = Math.Max(EvilPlayer.health, 1);
                }
                return;
            }
            if(GoodPlayer.reflect && GoodPlayer.safegaurd)
            {
                negatePierce = Math.Max(EvilPlayer.negatedDamage - GoodPlayer.pierced, 0);
                totalDamage= Math.Max(GoodPlayer.damage - negatePierce, 0);

                if (EvilPlayer.absorb)
                {
                    EvilPlayer.healed += totalDamage;
                    totalDamage = 0;
                }
                EvilPlayer.health -= (totalDamage - EvilPlayer.healed);

                GoodPlayer.didDamage = totalDamage > 0;

                GoodPlayer.didNegate = EvilPlayer.damage > 0;
                GoodPlayer.didHeal = GoodPlayer.healed > 0;
                EvilPlayer.didHeal = EvilPlayer.healed > 0;
                EvilPlayer.didNegate = negatePierce != 0 && GoodPlayer.damage != 0;
                EvilPlayer.didDamage = false;

                if (GoodPlayer.undying)
                {
                    GoodPlayer.health = Math.Max(GoodPlayer.health, 1);
                }

                if (EvilPlayer.undying)
                {
                    EvilPlayer.health = Math.Max(EvilPlayer.health, 1);
                }
                return;
            }

            if (EvilPlayer.reflect && EvilPlayer.safegaurd)
            {
                negatePierce = Math.Max(GoodPlayer.negatedDamage - EvilPlayer.pierced, 0);
                totalDamage = Math.Max(EvilPlayer.damage - negatePierce, 0);

                if (GoodPlayer.absorb)
                {
                    GoodPlayer.healed += totalDamage;
                    totalDamage = 0;
                }

                GoodPlayer.health -= (totalDamage - GoodPlayer.healed);

                EvilPlayer.didDamage = totalDamage > 0;

                EvilPlayer.didNegate = GoodPlayer.damage > 0;
                EvilPlayer.didHeal = EvilPlayer.healed > 0;
                GoodPlayer.didHeal = GoodPlayer.healed > 0;
                GoodPlayer.didNegate = negatePierce != 0 && EvilPlayer.damage != 0;
                GoodPlayer.didDamage = false;

                if (GoodPlayer.undying)
                {
                    GoodPlayer.health = Math.Max(GoodPlayer.health, 1);
                }

                if (EvilPlayer.undying)
                {
                    EvilPlayer.health = Math.Max(EvilPlayer.health, 1);
                }
                return;
            }
            if(GoodPlayer.reflect)
            {
                negatePierce = Math.Max( GoodPlayer.negatedDamage - EvilPlayer.pierced,0);
                int totalDamageReflected = Math.Max(EvilPlayer.damage - negatePierce,0);
                GoodPlayer.didNegate = negatePierce != 0 && EvilPlayer.damage != 0;

                negatePierce = Math.Max(EvilPlayer.negatedDamage - GoodPlayer.pierced, 0);
                int finalDamageEvil = Math.Max(GoodPlayer.damage + totalDamageReflected - negatePierce,0);
                if(EvilPlayer.absorb)
                {
                    EvilPlayer.healed += finalDamageEvil;
                    finalDamageEvil = 0;
                }
                EvilPlayer.health -= (finalDamageEvil-EvilPlayer.healed);
                GoodPlayer.health -= -GoodPlayer.healed;

                GoodPlayer.didDamage = finalDamageEvil > 0;
                GoodPlayer.didHeal = GoodPlayer.healed > 0;
                EvilPlayer.didHeal = EvilPlayer.healed > 0;
                EvilPlayer.didNegate = negatePierce != 0 && finalDamageEvil != 0;

                if (GoodPlayer.undying)
                {
                    GoodPlayer.health = Math.Max(GoodPlayer.health, 1);
                }

                if (EvilPlayer.undying)
                {
                    EvilPlayer.health = Math.Max(EvilPlayer.health, 1);
                }
                return;
            }
            if(EvilPlayer.reflect)
            {
                negatePierce = Math.Max(EvilPlayer.negatedDamage - GoodPlayer.pierced, 0);
                int totalDamageReflected = Math.Max(GoodPlayer.damage - negatePierce, 0);
                EvilPlayer.didNegate = negatePierce != 0 && GoodPlayer.damage != 0;

                negatePierce = Math.Max(GoodPlayer.negatedDamage - EvilPlayer.pierced, 0);
                int finalDamageGood = Math.Max(EvilPlayer.damage + totalDamageReflected - negatePierce, 0);
                if (GoodPlayer.absorb)
                {
                    GoodPlayer.healed += finalDamageGood;
                    finalDamageGood = 0;
                }
                GoodPlayer.health -= (finalDamageGood - GoodPlayer.healed);
                EvilPlayer.health -= -EvilPlayer.healed;

                EvilPlayer.didDamage = finalDamageGood > 0;
                EvilPlayer.didHeal = EvilPlayer.healed > 0;
                GoodPlayer.didHeal = GoodPlayer.healed > 0;
                GoodPlayer.didNegate = negatePierce != 0 && finalDamageGood != 0;

                if (GoodPlayer.undying)
                {
                    GoodPlayer.health = Math.Max(GoodPlayer.health, 1);
                }

                if (EvilPlayer.undying)
                {
                    EvilPlayer.health = Math.Max(EvilPlayer.health, 1);
                }
                return;
            }

            negatePierce = Math.Max(EvilPlayer.negatedDamage - GoodPlayer.pierced, 0);
            totalDamage = Math.Max(GoodPlayer.damage - negatePierce, 0);

            if (EvilPlayer.absorb)
            {
                EvilPlayer.healed += totalDamage;
                totalDamage = 0;
            }
            EvilPlayer.health -= (totalDamage - EvilPlayer.healed);
            GoodPlayer.didDamage = totalDamage > 0;
            EvilPlayer.didNegate = negatePierce > 0 && GoodPlayer.damage > 0;

            negatePierce = Math.Max(GoodPlayer.negatedDamage - EvilPlayer.pierced, 0);
            totalDamage = Math.Max(EvilPlayer.damage - negatePierce, 0);

            if (GoodPlayer.absorb)
            {
                GoodPlayer.healed += totalDamage;
                totalDamage = 0;
            }

            GoodPlayer.health -= (totalDamage - GoodPlayer.healed);
            EvilPlayer.didDamage = totalDamage > 0;
            GoodPlayer.didNegate = negatePierce > 0 && EvilPlayer.damage > 0;

            GoodPlayer.didHeal = GoodPlayer.healed > 0;
            EvilPlayer.didHeal = EvilPlayer.healed > 0;

            if(GoodPlayer.undying)
            {
                GoodPlayer.health = Math.Max(GoodPlayer.health,1);
            }

            if (EvilPlayer.undying)
            {
                EvilPlayer.health = Math.Max(EvilPlayer.health, 1);
            }
        }

        public void checkWin()
        {
            if(GoodPlayer.health <=0 && EvilPlayer.health <=0)
            {
                Console.WriteLine("Tie");
            }
            else if (GoodPlayer.health <= 0)
            {
                Console.WriteLine("Player 2 Victory!");
            }
            else if (EvilPlayer.health <= 0)
            {
                Console.WriteLine("Player 1 Victory!");
            }
        }
    }
}
