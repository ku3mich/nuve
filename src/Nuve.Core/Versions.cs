using System;
using System.Collections.Generic;

namespace Nuve.Core
{
    public class Versions : Dictionary<Version, DateTime>
    {
        public Versions() : base(VersionPartialComparer.Instance)
        {

        }
    }
}
