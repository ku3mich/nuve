using System.IO;
using System.Text;


namespace Nuve.Core.Storage
{
    internal static class StreamExtensions
    {
        public static StreamReader CreateLeaveOpenReader(this Stream stream) => new StreamReader(stream, Encoding.UTF8, true, 4096, true);
        public static StreamWriter CreateLeaveOpenWriter(this Stream stream) => new StreamWriter(stream, Encoding.UTF8, 4096, true);
    }
}
