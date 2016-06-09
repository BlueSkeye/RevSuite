using System;
using System.IO;

using RSCoreInterface;

namespace RSPluginInterface
{
    public static class PluginHelpers
    {
        public static bool IsValidNotUnknownEndianess(Endianess candidate)
        {
            switch (candidate) {
                case Endianess.BigEndian:
                case Endianess.LittleEndian:
                    return true;
                default:
                    return false;
            }
        }

        public static bool IsValidNotUnknownProcessor(Processor candidate)
        {
            switch (candidate) {
                case Processor.X86:
                case Processor.X64:
                    return true;
                default:
                    return false;
            }
        }

        public static ushort ReadUShort(byte[] from, ref int offset)
        {
            if (null == from) { throw new ArgumentNullException(); }
            if (0 > offset) { throw new ArgumentException(); }
            if (from.Length < (offset + sizeof(ushort))) {
                throw new ArgumentException();
            }
            return (ushort)(from[offset++] + (from[offset++] * 256));
        }
    }
}
