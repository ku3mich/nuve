using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Nuve.Core;
using Nuve.Core.Storage;

namespace Nuve.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EntryController : ControllerBase
    {
        private readonly IStorage Storage;

        public EntryController(IStorage storage)
        {
            Storage = storage;
        }

        [HttpGet]
        [Route("{id}")]
        public Entry Get(string id) => Storage.GetEntry(id);


        [HttpGet]
        [Route("list")]
        public IEnumerable<Entry> List() => Storage.List().Select(s => Storage.GetEntry(s));
    }
}
