using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookMid.UI
{
    public abstract class TabManager
    {
        public List<Tab> Tabs;

        public TabManager()
        {
            Tabs = new List<Tab>();
        }

        public abstract Tab AddNewPanel(string name);
    }
}
