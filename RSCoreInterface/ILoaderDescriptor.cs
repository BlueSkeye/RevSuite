using System;

namespace RSCoreInterface
{
    public interface ILoaderDescriptor
    {
        /// <summary>Retrieve an instance of this loader. It is up to the core
        /// engine to decide wether to instanciate a new one or to return an
        /// already existing instance.</summary>
        /// <returns>An instance of th loader this descriptor describes.</returns>
        ILoader Get();
        /// <summary>A human redable short description of this kind of loaders.
        /// </summary>
        string Name { get; }
        /// <summary>Informs the caller whether this kind of loaders supports non
        /// default address space. A default address space is expected to support
        /// loading at least one module. Not supporting non default address space
        /// doesn't preclude the loader to be able to loade several modules. However
        /// the caller MUST NOT make any assumption about it.
        /// WARNING : The instance MUST always return the same vaue for this property
        /// that MUST be considered immutable.</summary>
        bool SupportsNonDefaultAddressSpace { get; }
    }
}
