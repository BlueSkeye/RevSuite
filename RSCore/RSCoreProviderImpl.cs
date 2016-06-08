using System;
using System.Collections.Generic;
using System.IO;
using RSCoreInterface;

using RSPluginInterface;
// TODO : Remove this namespace AND the reference to the library?
using RSPELoader;

namespace RSCore
{
    public class RSCoreProviderImpl : RSCoreProvider, IRSCore
    {
        public IEnumerable<ILoaderDescriptor> EnumerateKnownLoaders()
        {
            foreach(ILoaderDescriptor item in _knownLoaders.Keys) {
                yield return item;
            }
            yield break;
        }

        private void Initialize()
        {
            // TODO : We should dynamically seek for available loaders.
            ILoaderDescriptorImplementor descriptor;
            RegisterLoader((descriptor = PELoaderDescriptor.Get), descriptor.Instanciator);
            return;
        }

        public ILoader Instanciate(ILoaderDescriptor descriptor)
        {
            LoaderInstanciatorDelegate instanciator;
            if (!_knownLoaders.TryGetValue(descriptor, out instanciator)) {
                throw new ArgumentException();
            }
            return instanciator(null);
        }

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

        private Dictionary<ILoaderDescriptor, LoaderInstanciatorDelegate> _knownLoaders =
            new Dictionary<ILoaderDescriptor, LoaderInstanciatorDelegate>();
    }
}
