using Nuve.Core;
using Nuve.Core.Storage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nuve.Tests
{
    class InMemoryStorage : IStorage
    {
        readonly Dictionary<string, Entry> Storage = new Dictionary<string, Entry>(StringComparer.InvariantCultureIgnoreCase);
        public Entry GetEntry(string identifier) => Storage.ContainsKey(identifier) ? Storage[identifier] : null;
        public List<string> List() => Storage.Keys.ToList();
        public void SetEntry(Entry entry) => Storage[entry.Identifier] = entry;
    }
}
