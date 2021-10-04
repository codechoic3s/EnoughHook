using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookMid.UI
{
    public abstract class Prop<T> : IProp
    {
        public T Value;
        public string Name;

        private Type _type;

        public Prop()
        {
            _type = typeof(T);
        }

        public object AsObject { get => Value; set { Value = (T)value; } }

        public Type GetValueType()
        {
            return _type;
        }
    }
}
