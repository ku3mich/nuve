using System;
using System.Collections.Concurrent;

namespace Nuve.Core.Storage
{
    class NamedLock
    {
        readonly ConcurrentDictionary<string, object> _dictionary = new ConcurrentDictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);
        public object this[string name] => _dictionary.GetOrAdd(name, _ => new object());
    }
}