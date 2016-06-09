using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using RSCoreInterface;

namespace RSPluginInterface
{
    /// <summary>A default implementation for a flat address space starting
    /// at virtual address space 0.</summary>
    public class FlatAddressSpace : IAddressSpace
    {
        public FlatAddressSpace(uint pageSize = 0)
        {
            _pageSize = pageSize;
            return;
        }

        /// <summary>Allocate an array of byte that will encompass the given
        /// range (at/size) and will be <see cref="_pageSize"/> aligned if
        /// <see cref="_pageSize"/> is defined for this instance.</summary>
        /// <param name="at"></param>
        /// <param name="size"></param>
        /// <param name="offset">On return this parameter is updated to match
        /// the offset within the returned array of the mapping.</param>
        /// <returns></returns>
        private NativeArray AllocateMapping(ulong at, uint size, out uint offset)
        {
            uint allocationSize;
            if (int.MaxValue < size) { throw new NotSupportedException(); }
            if (0 == _pageSize) {
                offset = 0;
                allocationSize = size;
            }
            else {
                uint baseAllocationDelta = (uint)(at % _pageSize);
                ulong baseAllocation = (0 == baseAllocationDelta)
                    ? at
                    : at - baseAllocationDelta;
                allocationSize = baseAllocationDelta + size;
                if (int.MaxValue > allocationSize) { throw new NotSupportedException(); }
                offset = baseAllocationDelta;
            }
            return new NativeArray(Marshal.AllocCoTaskMem((int)allocationSize), allocationSize);
        }

        bool IAddressSpace.IsSealed
        {
            get { return _sealed; }
        }

        public unsafe byte* MapAddress(ulong at, uint size)
        {
            uint offset;
            if (0 == at) {
                // Special case where we want to map address 0.
                NativeArray mapping = (_mappedAddresses[0] = AllocateMapping(at, size, out offset));
                return (byte*)mapping.At.ToPointer();
            }
            KeyValuePair<ulong, NativeArray> existingMapping = NonExistingMapping;
            foreach (KeyValuePair<ulong, NativeArray> alreadyMapped in _mappedAddresses) {
                if (alreadyMapped.Key > at) { break; }
                if ((alreadyMapped.Key  + alreadyMapped.Value.Size - 1) < (at + size)) {
                    // We must reallocate the mapping.
                    throw new NotImplementedException();
                }
            }
            // TODO : Check accurracy for ReferenceEqual on a structure.
            if (object.ReferenceEquals(existingMapping, NonExistingMapping)) {
            }
            else {
                // We already have a mapping. Is it big enough ?
            }
            throw new NotImplementedException();
        }

        void IAddressSpace.Seal()
        {
            _sealed = true;
        }

        private static KeyValuePair<ulong, NativeArray> NonExistingMapping =
            new KeyValuePair<ulong, NativeArray>(0, new NativeArray(IntPtr.Zero, 0));
        // TODO : This is inefficien. Seek for another algorithm that allow for
        // faster searches.
        private Dictionary<ulong, NativeArray> _mappedAddresses =
            new Dictionary<ulong, NativeArray>();
        private uint _pageSize;
        private bool _sealed;

        private struct NativeArray
        {
            internal NativeArray(IntPtr at, uint size)
            {
                At = at;
                Size = size;
            }

            internal IntPtr At { get; private set; }
            internal uint Size { get; private set; }
        }
    }
}
