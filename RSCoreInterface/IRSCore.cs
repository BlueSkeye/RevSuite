using System.Collections.Generic;
using System.Security.Cryptography;

namespace RSCoreInterface
{
    public interface IRSCore
    {
        /// <summary>Enumerate a descriptor for each address space
        /// descriptor that can is known.</summary>
        /// <returns></returns>
        IEnumerable<IAddressSpaceDescriptor> EnumerateAddressSpaceType();
        /// <summary>Provides a way to enumerate descriptors for each loader
        /// kind known from the framework.</summary>
        /// <returns></returns>
        IEnumerable<ILoaderDescriptor> EnumerateKnownLoaders();
        /// <summary>Get an instance of and address space having the given
        /// identifier. The identifier must be one of those that has previously
        /// been retrieved through an <see cref="EnumerateAddressSpaceType"/>
        /// method call.</summary>
        /// <returns></returns>
        IAddressSpace GetAddressSpace(Oid identifier);
        /// <summary>Provides a fresh instance of a loader for the given
        /// descriptor.</summary>
        /// <param name="descriptor">A descriptor that has been returned by a
        /// previous call to <see cref="EnumerateKnownLoaders"/> method.</param>
        /// <returns>A new loader instance that is associated with a default
        /// address space.</returns>
        ILoader Instanciate(ILoaderDescriptor descriptor);
    }
}
