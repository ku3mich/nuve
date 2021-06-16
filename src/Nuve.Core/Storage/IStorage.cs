using System.Collections.Generic;

namespace Nuve.Core.Storage
{
    public interface IStorage
    {
        Entry GetEntry(string identifier);
        void SetEntry(Entry entry);
        List<string> List();
    }
}
