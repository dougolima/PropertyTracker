using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace PropertyTracker.Presentation.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SitesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }
    }
}
