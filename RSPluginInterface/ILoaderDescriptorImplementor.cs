using RSCoreInterface;

namespace RSPluginInterface
{
    public interface ILoaderDescriptorImplementor : ILoaderDescriptor
    {
        LoaderInstanciatorDelegate Instanciator { get; }
    }
}
