using System;

namespace Nuve.Core
{
    public static class VersionExtensions
    {
        public static Version Increase(this Version v) => new Version(Zerofy(v.Major), Zerofy(v.Minor), Zerofy(v.Build), Zerofy(v.Revision) + 1);
        private static int Zerofy(int i) => i == -1 ? 0 : i;
        public static Version Normalize(this Version v) => new Version(Zerofy(v.Major), Zerofy(v.Minor), Zerofy(v.Build), Zerofy(v.Revision));
    }
}
