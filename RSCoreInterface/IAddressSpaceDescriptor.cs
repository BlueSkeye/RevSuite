using System.Security.Cryptography;

namespace RSCoreInterface
{
    public interface IAddressSpaceDescriptor
    {
        /// <summary>A human readable description of this address space.
        /// </summary>
        string Description { get; }
        /// <summary>The Oid MUST be persistent on the long term.</summary>
        Oid Id { get; }
    }
}
