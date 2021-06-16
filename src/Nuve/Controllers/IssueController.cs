using System;
using Microsoft.AspNetCore.Mvc;
using Nuve.Core;

namespace Nuve.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class IssueController : ControllerBase
    {
        private readonly IVersionService VersionService;

        public IssueController(IVersionService versionService)
        {
            VersionService = versionService;
        }

        [HttpPost]
        [Route("{id}")]
        public ActionResult<string> Post(string id, string version)
        {
            if (!Version.TryParse(version, out var v))
            {
                v = new Version(1, 0, 0, 0);
            }
            else
            {
                v = v.Normalize();
            }

            var result = VersionService.Increase(id, v);
            return result.ToString();
        }
    }
}
