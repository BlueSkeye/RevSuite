
namespace RSCoreInterface
{
    /// <summary>An address space is roughly equivalent to a process.
    /// Every loader acts on a single address space.</summary>
    public interface IAddressSpace
    {
        /// <summary>Tells whether the address space is sealed ornot.</summary>
        bool IsSealed { get; }
        /// <summary>Seals the address space, preventing further addition
        /// of any chunk. Sealing is irreversible.</summary>
        void Seal();
    }
}
