using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

namespace RSCoreInterface
{
    /// <summary>We expect the following duties from a loader when
    /// presented with a file content :
    /// - Detect if it can handle the file format
    /// - Translate file content into a memory representation.
    /// - Enumerate entry points if any for executable files.
    /// - Detect endianess.
    /// - Detect target processor for executable files.
    /// - Enumerate additional artifacts that are referenced by the
    /// file content.</summary>
    public interface ILoader
    {
        /// <summary>Informs the caller whether this loader supports non default
        /// address space. A default address space is expected to support loading
        /// at least one module. Not supporting non default address space doesn't
        /// preclude the loader to be able to loade several modules. However the
        /// caller MUST NOT make any assumption about it.
        /// WARNING : The value returned by this instance MUST be the same than the
        /// one</summary>
        bool SupportsNonDefaultAddressSpace { get; }

        /// <summary>Bind a specific address space to this loader. This method
        /// MUST be invoked before any module is loaded, otherwise the loader
        /// MUST throw an <see cref="InvalidOperationException"/>.
        /// On the other hand not every loadder support non default address space.
        /// Any loader is allowed to throw an <see cref="NotSupportedException"/>
        /// whenever it feels the address collection doesn't fit it's capabilities.
        /// <seealso cref="SupportsNonDefaultAddressSpace"/></summary>
        /// <param name="adddresses">A collection of wirtual address ranges.</param>
        void AllocateAddressSpace(ICollection<VirtualAddressRange> adddresses);
        /// <summary>Check the given readonly stream and decide whether the loader
        /// can manage its duties on this content. The loader is not expected to
        /// perform any actual loading at this stage.</summary>
        /// <param name="candidate">A candidate stream.</param>
        /// <returns>true if the instance feels it is able to handle this stream,
        /// false otherwise.</returns>
        bool CanLoad(Stream candidate);
        /// <summary>Load a module from the given readonly stream. The loader is
        /// guaranteed this method won't be invoked unless it returned true from
        /// the <see cref="CanLoad"/>method invoked on this exact same stream.
        /// </summary>
        /// <param name="context">A context that is provided by the invoker. This
        /// context is returned by the loader as a property for various objects.
        /// WARNING : The implementation of the Load method is allowed to get a
        /// serialized representation of this object at any time. Hence, the caller
        /// MUST consider it transdered ownership of this object to the loader as
        /// soon as it invokes the <see cref="Load"/> method. Any further modification
        /// ot he context object by the caller may be lost and lead to unexpected
        /// behavior.
        /// The intended use case is as follows :
        /// - The caller asks the loader to load a module and provides context for
        /// it.
        /// - The caller passivates the loaded module.
        /// - The passivated module is later reloaded using the passivation token.
        /// - The reloaded module now uses a context that the caller can use in
        /// order to decide whether the reloaded module is still relevant relatively
        /// to the context.
        /// The relevance criteria is entirely under control of the caller.</param>
        /// <param name="candidate">The stream to load from. The loader is
        /// guaranteed that the stream will remain open until either the
        /// <see cref="Passivate"/> method is invoked on the returned instance or
        /// the <see cref="Unload"/>method is invoked on the loader itself.</param>
        /// <returns>An instance that stands for the input content.</returns>
        ILoadedModule Load(ISerializable context, Stream candidate);
        /// <summary></summary>
        /// <param name="token">An opaque object that has been acquired during a
        /// previous invocation of the <see cref="Passivate"/> method on a loaded
        /// module that has been returned by this same loader.</param>
        /// <remarks>Being serializable, the loader must not expect the token to
        /// be <see cref="ReferenceEquals"/> comparable with the one it returned.
        /// More specifically, passivation token are execpted to remain meaningfull
        /// across server power off/power on cycles.</remarks>
        /// <returns></returns>
        ILoadedModule Reload(object token);
        /// <summary>Invoked by the framework prior to shutting down the loader.
        /// On return from this method, the loader must assume none of the input
        /// streams that were used to create <see cref="ILoadedModule"/> instances
        /// are still available.</summary>
        void Unload();
    }
}
