using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookMid.UI
{
    public interface IProp
    {
        Type GetValueType();
        object AsObject { get; set; }
    }
}
