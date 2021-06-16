using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Nuve.Core.Storage
{
    public class FileStorage : IStorage
    {
        private readonly StorageSettings Settings;
        private readonly ILogger Logger;
        private readonly IEntryReader EntryReader;
        private readonly IEntryWriter EntryWriter;

        public FileStorage(IOptionsSnapshot<StorageSettings> settings, ILogger<FileStorage> logger, IEntryReader entryReader, IEntryWriter entryWriter)
        {
            Settings = settings.Value;
            Logger = logger;
            EntryReader = entryReader;
            EntryWriter = entryWriter;
            if (!Directory.Exists(Settings.Location))
            {
                Directory.CreateDirectory(Settings.Location);
            }
        }

        public Entry GetEntry(string identifier)
        {
            Logger.LogDebug($"reading: {identifier}");
            var fileName = Path.Combine(Settings.Location, identifier.ToLower() + ".json");
            if (!File.Exists(fileName))
                return null;

            using (var s = new FileStream(fileName, FileMode.Open))
            {
                return EntryReader.Read(s);
            }
        }

        public List<string> List() => Directory.GetFiles(Settings.Location, "*.json").Select(s => Path.GetFileNameWithoutExtension(s).ToLower()).ToList();

        public void SetEntry(Entry entry)
        {
            Logger.LogDebug($"writing: {entry.Identifier}");
            var fileName = Path.Combine(Settings.Location, entry.Identifier.ToLower() + ".json");
            using (var s = new FileStream(fileName, FileMode.Create))
            {
                EntryWriter.Write(entry, s);
            }
        }
    }
}
