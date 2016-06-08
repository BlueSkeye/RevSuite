using System;

namespace RSCoreInterface
{
    /// <summary>A virtual address that is only relevant within an
    /// address space.</summary>
    public interface IVirtualAddress : IComparable<IVirtualAddress>
    {
    }
}
