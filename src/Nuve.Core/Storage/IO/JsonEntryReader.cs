using Newtonsoft.Json;
using System.IO;

namespace Nuve.Core.Storage
{
    public class JsonWriterReader : IEntryReader
    {
        private readonly JsonSerializer JsonSerializer;

        public JsonWriterReader(JsonSerializer jsonSerializer)
        {
            JsonSerializer = jsonSerializer;
        }

        public Entry Read(Stream stream)
        {
            using (var r = stream.CreateLeaveOpenReader())
            using (var j = new JsonTextReader(r))
            {
                return JsonSerializer.Deserialize<Entry>(j);
            }
        }
    }
}
