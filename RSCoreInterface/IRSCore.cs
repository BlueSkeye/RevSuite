using System.Collections.Generic;

namespace RSCoreInterface
{
    public interface IRSCore
    {
        /// <summary>Provides a way to enumerate descriptors for each loader
        /// kind known from the framework.</summary>
        /// <returns></returns>
        IEnumerable<ILoaderDescriptor> EnumerateKnownLoaders();
        /// <summary>Provides a fresh instance of a loader for the given
        /// descriptor.</summary>
        /// <param name="descriptor">A descriptor that has been returned by a
        /// previous call to <see cref="EnumerateKnownLoaders"/> method.</param>
        /// <returns>A new loader instance that is associated with a default
        /// address space.</returns>
        ILoader Instanciate(ILoaderDescriptor descriptor);
    }
}
