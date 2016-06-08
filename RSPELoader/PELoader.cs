using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using RSCoreInterface;

namespace RSPELoader
{
    public class PELoader : ILoader
    {
        public bool SupportsNonDefaultAddressSpace
        {
            get { return true; }
        }

        internal static PELoader Instanciate(object context)
        {
            return new PELoader();
        }

        public void AllocateAddressSpace(ICollection<VirtualAddressRange> adddresses)
        {
            throw new NotImplementedException();
        }

        public bool CanLoad(Stream candidate)
        {
            throw new NotImplementedException();
        }

        public ILoadedModule Load(ISerializable context, Stream candidate)
        {
            throw new NotImplementedException();
        }

        public ILoadedModule Reload(object token)
        {
            throw new NotImplementedException();
        }

        public void Unload()
        {
            throw new NotImplementedException();
        }
    }
}
