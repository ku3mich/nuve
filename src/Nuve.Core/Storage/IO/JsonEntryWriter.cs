using Newtonsoft.Json;
using System.IO;

namespace Nuve.Core.Storage
{
    public class JsonWriterWriter : IEntryWriter
    {
        private readonly JsonSerializer JsonSerializer;

        public JsonWriterWriter(JsonSerializer jsonSerializer)
        {
            JsonSerializer = jsonSerializer;
        }

        public void Write(Entry s, Stream stream)
        {
            using (var r = stream.CreateLeaveOpenWriter())
            using (var j = new JsonTextWriter(r))
            {
                JsonSerializer.Serialize(j, s, typeof(Entry));
            }
        }
    }
}
