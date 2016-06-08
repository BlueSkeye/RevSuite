using System;

namespace RSCoreInterface
{
    public struct VirtualAddressRange
    {
        public VirtualAddressRange(IVirtualAddress start, IVirtualAddress end)
        {
            if (null == start) { throw new ArgumentNullException(); }
            if (null == end) { throw new ArgumentNullException(); }
            if (+1 == start.CompareTo(end)) { throw new ArgumentException(); }
            Start = start;
            End = end;
            return;
        }

        public IVirtualAddress Start { get; private set; }
        public IVirtualAddress End { get; private set; }
    }
}
