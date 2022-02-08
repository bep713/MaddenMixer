using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace EASoundbankTools.Model.SBR
{
    public class DSetField
    {
        public string Name { get; set; }
        public ulong Value { get; set; }

        public T GetValue<T>()
        {
            return (T) Convert.ChangeType(Value, typeof(T));
        }
    }
}
