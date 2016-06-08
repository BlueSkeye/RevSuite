using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RSCoreInterface
{
    public interface ILoadedModule
    {
        Endianess Endianess { get; }
        Processor TargetProcessor { get; }
        /// <summary>Tells the invoker whether this current loaded module
        /// instance can be stored on a persistent storage.</summary>
        /// <returns>true if passivation is supported for this instance,
        /// false otherwise.</returns>
        /// <seealso cref="Passivate"/>
        bool CanPassivate();
        IEnumerable<IModuleDescriptor> EnumerateReferencedModules();
        IEnumerable<IVirtualAddress> EnumerateEntrypoints();
        /// <summary>Store the instance in a persistent storage.
        /// WARNING : This method can be invoked by the framework even if
        /// a previous call to <see cref="CanPassivate"/> returned false.
        /// </summary>
        /// <returns>Either an opaque object that must be serializable if
        /// passivation succeeded or a null reference if either it failed
        /// or the loaded module can't be passivated anyway due to a previous
        /// call to <see cref="CanPassivate"/> having returned false.</returns>
        ISerializable Passivate();
    }
}
