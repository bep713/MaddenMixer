using System;
using System.Collections.Generic;
using System.Text;

// Thank you to NicknineTheEagle
// https://github.com/NicknineTheEagle/Frostbite-Scripts/blob/master/frostbite3/sbr.py

namespace EASoundbankTools.Model.SBR
{
    public class DSetFieldDefinition
    {
        public enum FieldType
        {
            Boolean = 0,
            Int32 = 1,
            UInt32 = 2,
            Int64 = 3,
            UInt64 = 4,
            Float32 = 5,
            Float64 = 6,
            String = 7,
            Pointer = 8
        };

        public uint Id { get; set; }
        public byte DataTypeRaw { get; set; }
        public byte StoreType { get; set; }
        public ushort StoreParam1 { get; set; }
        public ulong StoreParam2 { get; set; }
        public uint TableOffset { get; set; }
        public uint Unknown { get; set; }
        public string Name 
        { 
            get
            {
                byte[] idBytes = BitConverter.GetBytes(Id);
                byte[] filteredBytes = Array.FindAll(idBytes, x => x != 0x2E);
                Array.Reverse(filteredBytes);
                return Encoding.Default.GetString(filteredBytes);
            }
        }
        public FieldType DataType
        {
            get
            {
                return (FieldType)DataTypeRaw;
            }
        }
    }
}
