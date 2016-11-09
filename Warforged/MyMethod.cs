using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warforged
{
    public class MyMethod
    {
        public Delegate func = null;
        public object[] parameters = new object[0];

        public MyMethod(Delegate func, object[] parameters)
        {
            this.func = func;
            this.parameters = parameters;
        }
    }
}
