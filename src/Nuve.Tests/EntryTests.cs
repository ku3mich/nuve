using Nuve.Core;
using System;
using Xunit;

namespace Nuve.Tests
{
    public class EntryTests
    {
        [Fact]
        public void Entry_Empty()
        {
            var entry = new Entry("a");
            var version = entry.Increase(Version.Parse("1.1"));
            Assert.True(version == Version.Parse("1.1.0.1"));
        }

        [Fact]
        public void Entry_NotEmpty()
        {
            var entry = new Entry("a");
            entry.Add(Version.Parse("1.1.1.2"));

            var version = entry.Increase(Version.Parse("1.1.1.1"));
            Assert.True(version == Version.Parse("1.1.1.3"));
        }

        [Fact]
        public void Entry_Throws_IfSameVersionAdded()
        {
            var entry = new Entry("a");
            entry.Add(Version.Parse("1.1.1.2"));
            Assert.Throws<ArgumentException>(() => entry.Add(Version.Parse("1.1.1.3")));
        }

        [Fact]
        public void Entry_VersionAnotherBranch_Empty()
        {
            var entry = new Entry("a");
            entry.Add(Version.Parse("1.1.1.2"));
            var version = entry.Increase(Version.Parse("1.2.1.1"));
            Assert.True(version == Version.Parse("1.2.1.2"));
        }

        [Fact]
        public void Entry_VersionAnotherBranch_Exists()
        {
            var entry = new Entry("a");
            entry.Add(Version.Parse("1.1.1.2"));
            entry.Add(Version.Parse("1.1.2.3"));
            var version = entry.Increase(Version.Parse("1.1.1"));
            Assert.True(version == Version.Parse("1.1.1.3"));
        }
    }
}
