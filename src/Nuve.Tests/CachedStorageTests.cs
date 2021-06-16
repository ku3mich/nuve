using Nuve.Core.Storage;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Nuve.Tests
{
    public class CachedStorageTests
    {
        readonly CachedStorage Storage;

        public CachedStorageTests()
        {
            Storage = new CachedStorage(new InMemoryStorage());
        }

        [Fact]
        public void Add_Entry_Empty()
        {
            Storage.SetEntry(new Core.Entry("a"));
        }

        [Fact]
        public void Add_Entry_Version()
        {
            Storage.SetEntry(new Core.Entry("a", Version.Parse("1.1.1.1")));
        }

        [Fact]
        public void Add_Entry_IncreaseVersion()
        {
            Storage.SetEntry(new Core.Entry("a", Version.Parse("1.1.1.1")));
            var result = Storage.Increase("a", Version.Parse("1.1.1"));
            Assert.True(result == Version.Parse("1.1.1.2"));
        }

        [Fact]
        public void Add_Entry_IncreaseVersion_Multiple()
        {
            const int taskCount = 20;
            Storage.SetEntry(new Core.Entry("a", Version.Parse("1.1.1.1")));
            var tasks = Enumerable
               .Range(0, taskCount)
               .Select(s => Task.Factory.StartNew(() => Storage.Increase("a", Version.Parse("1.1.1"))))
               .ToArray();

            Task.WaitAll(tasks);
            var versions = tasks.Select(s => s.Result.ToString()).ToArray();

            var differentVersions = versions.Distinct().Count();

            Assert.True(differentVersions == taskCount);
        }
    }
}
