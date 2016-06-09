using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

using RSCoreInterface;
using RSPluginInterface;

namespace RSPELoader
{
    internal class PELoadedModule : ILoadedModule
    {
        internal PELoadedModule(Endianess endianess, Processor processor)
        {
            if(!PluginHelpers.IsValidNotUnknownEndianess(endianess)) {
                throw new ArgumentException();
            }
            if (!PluginHelpers.IsValidNotUnknownProcessor(processor)) {
                throw new ArgumentException();
            }
            _endianess = endianess;
            _processor = processor;
            return;
        }

        private Endianess _endianess;
        private Processor _processor;

        public Endianess Endianess
        {
            get { return _endianess; }
        }

        public Processor TargetProcessor
        {
            get { return _processor; }
        }

        public bool CanPassivate()
        {
            return false;
        }

        public IEnumerable<IVirtualAddress> EnumerateEntrypoints()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IModuleDescriptor> EnumerateReferencedModules()
        {
            throw new NotImplementedException();
        }

        public ISerializable Passivate()
        {
            return null;
        }
    }
}
