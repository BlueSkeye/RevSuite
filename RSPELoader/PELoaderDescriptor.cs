using System;

using RSCoreInterface;
using RSPluginInterface;

namespace RSPELoader
{
    public class PELoaderDescriptor : ILoaderDescriptorImplementor, ILoaderDescriptor
    {
        private PELoaderDescriptor()
        {
            return;
        }

        public static PELoaderDescriptor Get
        {
            get { return _instance; }
        }

        public LoaderInstanciatorDelegate Instanciator
        {
            get { return PELoader.Instanciate; }
        }

        public string Name
        {
            get { return HumanReadableName; }
        }

        public bool SupportsNonDefaultAddressSpace
        {
            get { return false; }
        }

        private const string HumanReadableName = "PE Loader";
        private static PELoaderDescriptor _instance = new PELoaderDescriptor();
    }
}
