using System.IO;

namespace Nuve.Core.Storage
{
    public interface IEntryWriter
    {
        void Write(Entry entry, Stream stream);
    }
}
