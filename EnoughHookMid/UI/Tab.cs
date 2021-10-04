using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookMid.UI
{
    public abstract class Tab
    {
        public List<IProp> Props;
        public string Name;

        public Tab()
        {
            Props = new List<IProp>();
        }

        public abstract IProp AddNewProp(string name, object value);
    }
}
