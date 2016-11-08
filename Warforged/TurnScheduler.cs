using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame
{
    public class TurnScheduler
    {
        public List<Dictionary<PHASES, List<MyMethod>>> turnMethods = new List<Dictionary<PHASES, List<MyMethod>>>();
        public int currentTurn = 0;
        public TurnScheduler()
        {
            newTurn();
        }

        public void schedule(int turn,PHASES phase, Delegate d, params object[] parameters)
        {
            while(turnMethods.Count < turn)
            {
                newTurn();
            }
            turnMethods[turn][phase].Add(new MyMethod(d,parameters));
        }

        public void newTurn()
        {
            var dict = new Dictionary<PHASES, List<MyMethod>>();
            dict.Add(PHASES.DAWN, new List<MyMethod>());
            dict.Add(PHASES.DECLARATION, new List<MyMethod>());
            dict.Add(PHASES.MAIN, new List<MyMethod>());
            dict.Add(PHASES.DUSK, new List<MyMethod>());
            turnMethods.Add(dict);
        }

        public void doTurn(PHASES phase)
        {
            while (turnMethods.Count < currentTurn)
            {
                newTurn();
            }
            Dictionary<PHASES, List<MyMethod>> dict = turnMethods[currentTurn];
            List<MyMethod> lst = dict[phase];
            foreach (MyMethod d in lst)
            {
                d.func.DynamicInvoke(d.parameters);
            }
        }
    }
    public enum PHASES
    {
        DAWN,
        DECLARATION,
        MAIN,
        DUSK,
    }
}
