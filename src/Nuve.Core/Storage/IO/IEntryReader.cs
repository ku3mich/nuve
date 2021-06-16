using System.IO;

namespace Nuve.Core.Storage
{
    public interface IEntryReader
    {
        Entry Read(Stream stream);
    }
}
