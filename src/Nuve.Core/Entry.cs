using System;
using System.Linq;

namespace Nuve.Core
{
    public class Entry
    {
        public string Identifier { get; set; }
        public Versions Versions { get; set; }

        public Entry()
        {
            Versions = new Versions();
        }

        public Entry(string identifier) : this()
        {
            Identifier = identifier;
        }

        public Entry(string identifier, Version version) : this(identifier)
        {
            Add(version);
        }

        public Version Increase(Version v)
        {
            Version newVersion;
            var found = Versions.Keys.FirstOrDefault(s => VersionPartialComparer.Instance.Equals(s, v));
            if (found != null)
            {
                newVersion = found.Increase();
                Versions.Remove(found);
            }
            else
                newVersion = v.Increase();

            Add(newVersion);

            return newVersion;
        }

        public void Add(Version v) => Versions.Add(v, DateTime.UtcNow);
    }
}
