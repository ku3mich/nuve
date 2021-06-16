using System;
using System.Collections.Generic;

namespace Nuve.Core
{
    public class VersionPartialComparer : IEqualityComparer<Version>
    {
        public bool Equals(Version x, Version y)
        {
            if (x == y) return true;
            if (x == null || y == null) return false;
            return x.Major == y.Major && x.Minor == y.Minor && x.Build == y.Build;
        }

        public int GetHashCode(Version obj)
        {
            if (obj == null)
                return 0;

            int accumulator = 0;

            accumulator |= (obj.Major & 0x0000000F) << 28;
            accumulator |= (obj.Minor & 0x000000FF) << 20;
            accumulator |= (obj.Build & 0x000000FF) << 12;

            return accumulator;
        }

        public static readonly VersionPartialComparer Instance = new VersionPartialComparer();
    }
}
