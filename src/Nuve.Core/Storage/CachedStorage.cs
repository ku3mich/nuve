using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Nuve.Core.Storage
{

    public class CachedStorage : IStorage, IVersionService
    {
        private readonly IStorage Storage;
        private readonly ConcurrentDictionary<string, Entry> Cache;
        private NamedLock NamedLock = new NamedLock();

        public CachedStorage(IStorage storage)
        {
            Storage = storage;
            var entries = storage.List().Select(s => Storage.GetEntry(s)).Select(s => new KeyValuePair<string, Entry>(s.Identifier, s));
            Cache = new ConcurrentDictionary<string, Entry>(entries, StringComparer.InvariantCultureIgnoreCase);
        }

        public Entry GetEntry(string identifier) => Cache.GetOrAdd(identifier, s => Storage.GetEntry(s));

        public Version Increase(string identifier, Version version)
        {
            var result = version.Increase();
            lock (NamedLock[identifier])
            {
                Cache.AddOrUpdate(identifier, i =>
                {
                    var e = new Entry(identifier, result);
                    Storage.SetEntry(e);
                    return e;
                },
                (i, e) =>
                {
                    if (e == null)
                        e = new Entry(identifier, result);
                    else
                        result = e.Increase(version);

                    Storage.SetEntry(e);
                    return e;
                });
            }

            return result;
        }

        public List<string> List() => Cache.Keys.ToList();

        public void SetEntry(Entry entry)
        {
            Entry Update()
            {
                Storage.SetEntry(entry);
                return entry;
            }

            Cache.AddOrUpdate(entry.Identifier, s => Update(), (s, e) => Update());
        }
    }
}