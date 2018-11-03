using Microsoft.AspNetCore.Mvc;
using PropertyTracker.Application.DTO;
using PropertyTracker.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PropertyTracker.Presentation.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SitesController : ControllerBase
    {
        private ISiteService service { get; }

        public SitesController(ISiteService service)
        {
            this.service = service;
        }

        // GET api/values
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<Site>))]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await this.service.GetAll());
        }

        // GET api/values
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(Site))]
        [ProducesResponseType(404)]
        [Route("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var site = await this.service.Get(id);

            if (site == null)
            {
                return NotFound();
            }

            return Ok(site);
        }
    }
}
