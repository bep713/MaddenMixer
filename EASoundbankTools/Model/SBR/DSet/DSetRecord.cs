using System;
using System.Collections.Generic;
using System.Text;

namespace EASoundbankTools.Model.SBR
{
    public class DSetRecord
    {
        public List<DSetField> Fields;

        public DSetRecord()
        {
            Fields = new List<DSetField>();
        }

        public DSetField GetFieldByName(string name)
        {
            return Fields.Find(x => x.Name.Equals(name));
        }

        public ulong? GetFieldValueByName(string name)
        {
            return Fields.Find(x => x.Name.Equals(name))?.Value;
        }
    }
}
