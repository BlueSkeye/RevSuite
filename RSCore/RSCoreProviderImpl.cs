using System;
using System.Collections.Generic;
using System.IO;
using RSCoreInterface;

using RSPluginInterface;
// TODO : Remove this namespace AND the reference to the library?
using RSPELoader;
using System.Security.Cryptography;

namespace RSCore
{
    /// <summary>This class is our concrete implementation of the suite in
    /// the contexxt of a single process. We might consider wrapping it in
    /// another class if cross process or even remote access is required
    /// later.</summary>
    public class RSCoreProviderImpl : RSCoreProvider, IRSCore
    {
        /// <summary>Enumerate a descriptor for each known loader.</summary>
        /// <returns></returns>
        public IEnumerable<ILoaderDescriptor> EnumerateKnownLoaders()
        {
            foreach(ILoaderDescriptor item in _knownLoaders.Keys) {
                yield return item;
            }
            yield break;
        }

        /// <summary>Performs any required initialization tasks.</summary>
        private void Initialize()
        {
            // TODO : We should dynamically seek for available loaders.
            ILoaderDescriptorImplementor descriptor;
            RegisterLoader((descriptor = PELoaderDescriptor.Get), descriptor.Instanciator);
            return;
        }

        /// <summary>Provide a fresh instance of a loader which descriptor
        /// has previously been retrieved with the <see cref="EnumerateKnownLoaders"/>
        /// method.</summary>
        /// <param name="descriptor">Descriptor for the loader to be instanciated.
        /// </param>
        /// <returns></returns>
        public ILoader Instanciate(ILoaderDescriptor descriptor)
        {
            LoaderInstanciatorDelegate instanciator;
            if (!_knownLoaders.TryGetValue(descriptor, out instanciator)) {
                throw new ArgumentException();
            }
            return instanciator(null);
        }

        /// <summary>Register a loader in the collection of known ones.
        /// Currently, this is a core only feature. There is no way for the
        /// suite to be instructed by the client software to do this on the
        /// fly.</summary>
        /// <param name="descriptor">Registered loader descriptor (mandatory).
        /// </param>
        /// <param name="instanciator">An instanciator delegate that can create
        /// a fresh instance of the loader.</param>
        internal void RegisterLoader(ILoaderDescriptor descriptor,
            LoaderInstanciatorDelegate instanciator)
        {
            if (null == descriptor) { throw new ArgumentNullException(); }
            if (null == instanciator) { throw new ArgumentNullException(); }
            if (_knownLoaders.ContainsKey(descriptor)) {
                throw new InvalidOperationException();
            }
            _knownLoaders.Add(descriptor, instanciator);
            return;
        }

        public IEnumerable<IAddressSpaceDescriptor> EnumerateAddressSpaceType()
        {
            throw new NotImplementedException();
        }

        public IAddressSpace GetAddressSpace(Oid identifier)
        {
            throw new NotImplementedException();
        }

        private Dictionary<ILoaderDescriptor, LoaderInstanciatorDelegate> _knownLoaders =
            new Dictionary<ILoaderDescriptor, LoaderInstanciatorDelegate>();
    }
}
