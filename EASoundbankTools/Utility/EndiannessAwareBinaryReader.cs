﻿using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EASoundbankTools.Utility
{
    public class EndiannessAwareBinaryReader : BinaryReader
    {
        public enum EndiannessType
        {
            Little,
            Big,
        }

        private EndiannessType _endianness = EndiannessType.Little;
        public EndiannessType Endianness
        {
            get
            {
                return _endianness;
            }
            set
            {
                _endianness = value;
            }
        }

        public EndiannessAwareBinaryReader(Stream input) : base(input)
        {
        }

        public EndiannessAwareBinaryReader(Stream input, Encoding encoding) : base(input, encoding)
        {
        }

        public EndiannessAwareBinaryReader(Stream input, Encoding encoding, bool leaveOpen) : base(
            input, encoding, leaveOpen)
        {
        }

        public EndiannessAwareBinaryReader(Stream input, EndiannessType endianness) : base(input)
        {
            _endianness = endianness;
        }

        public EndiannessAwareBinaryReader(Stream input, Encoding encoding, EndiannessType endianness) :
            base(input, encoding)
        {
            _endianness = endianness;
        }

        public EndiannessAwareBinaryReader(Stream input, Encoding encoding, bool leaveOpen,
            EndiannessType endianness) : base(input, encoding, leaveOpen)
        {
            _endianness = endianness;
        }

        public override short ReadInt16() => ReadInt16(_endianness);

        public short ReadInt16(EndiannessType endianness) => endianness == EndiannessType.Little
            ? BinaryPrimitives.ReadInt16LittleEndian(ReadBytes(sizeof(short)))
            : BinaryPrimitives.ReadInt16BigEndian(ReadBytes(sizeof(short)));

        public override ushort ReadUInt16() => ReadUInt16(_endianness);

        public ushort ReadUInt16(EndiannessType endianness) => endianness == EndiannessType.Little
            ? BinaryPrimitives.ReadUInt16LittleEndian(ReadBytes(sizeof(ushort)))
            : BinaryPrimitives.ReadUInt16BigEndian(ReadBytes(sizeof(ushort)));

        public override int ReadInt32() => ReadInt32(_endianness);

        public int ReadInt32(EndiannessType endianness) => endianness == EndiannessType.Little
            ? BinaryPrimitives.ReadInt32LittleEndian(ReadBytes(sizeof(int)))
            : BinaryPrimitives.ReadInt32BigEndian(ReadBytes(sizeof(int)));

        public override uint ReadUInt32() => ReadUInt32(_endianness);

        public uint ReadUInt32(EndiannessType endianness) => endianness == EndiannessType.Little
            ? BinaryPrimitives.ReadUInt32LittleEndian(ReadBytes(sizeof(uint)))
            : BinaryPrimitives.ReadUInt32BigEndian(ReadBytes(sizeof(uint)));

        public override long ReadInt64() => ReadInt64(_endianness);

        public long ReadInt64(EndiannessType endianness) => endianness == EndiannessType.Little
            ? BinaryPrimitives.ReadInt64LittleEndian(ReadBytes(sizeof(long)))
            : BinaryPrimitives.ReadInt64BigEndian(ReadBytes(sizeof(long)));

        public override ulong ReadUInt64() => ReadUInt64(_endianness);

        public ulong ReadUInt64(EndiannessType endianness) => endianness == EndiannessType.Little
            ? BinaryPrimitives.ReadUInt64LittleEndian(ReadBytes(sizeof(ulong)))
            : BinaryPrimitives.ReadUInt64BigEndian(ReadBytes(sizeof(ulong)));
    }
}
