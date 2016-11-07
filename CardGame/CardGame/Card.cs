using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame
{
    public abstract class Card
    {
        private static int startID = 0;
        public sealed int UID
        {
            get
            {
                return UID;
            }
            set
            {
                UID = startID;
                ++startID;
            }
        }

        public bool isAwakening;

        public string name;

        public TYPE type;

        public void Effect(Board b);

        public void Residual(Board b);

        public void Depart(Board b);

        public void Alight(Board b);

        public override virtual int GetHashCode()
        {
            return UID;
        }
       
    }
    public enum TYPE
    {
        RED,
        BLUE,
        GREEN,
        INHERENT,
        CHARACTER
    }
}
