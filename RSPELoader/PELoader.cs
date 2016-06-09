using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using RSCoreInterface;

using RSPluginInterface;

namespace RSPELoader
{
    public class PELoader : ILoader
    {
        public PELoader()
        {
            return;
        }

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
            int bufferSize = 4096;

            while (true) {
                byte[] firstPage = new byte[bufferSize];
                try {
                    candidate.Position = 0;
                    // Don't care with returned length. If not enough bytes, this
                    // will fail.
                    candidate.Read(firstPage, 0, firstPage.Length);
                    if ((byte)'M' != firstPage[0]) { return false; }
                    if ((byte)'Z' != firstPage[1]) { return false; }
                    // TODO : Should we check for additional bytes 0x90 0x90
                    int offset = PEOffset;
                    // TODO : Is this really an ushort or something bigger ?
                    int peHeaderOffset = PluginHelpers.ReadUShort(firstPage, ref offset);
                    // Try again with a biger buffer if needed.
                    if (peHeaderOffset > bufferSize - sizeof(uint)) { continue; }
                    offset = peHeaderOffset;
                    if ((byte)'P' != firstPage[offset++]) { return false; }
                    if ((byte)'E' != firstPage[offset++]) { return false; }
                    if (0 != firstPage[offset++]) { return false; }
                    if (0 != firstPage[offset++]) { return false; }
                    return true;
                }
                catch { return false; }
            }
        }

        public ILoadedModule Load(ISerializable context, Stream candidate)
        {
            // Get and initialize an address space;
            IAddressSpace addressSpace = new FlatAddressSpace();

            // Map the file to the address space

            // Instanciate and initialize result object.
            Endianess endianess = Endianess.Unknown;
            Processor processor = Processor.Unknown;
            return new PELoadedModule(endianess, processor);
        }

        public ILoadedModule Reload(object token)
        {
            throw new NotImplementedException();
        }

        public void Unload()
        {
            throw new NotImplementedException();
        }

        private const int PEOffset = 0x3C;
    }
}
