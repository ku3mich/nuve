using System;

namespace Nuve.Core
{
    public interface IVersionService
    {
        Version Increase(string identifier, Version version);
    }
}
