using Microsoft.AspNetCore.Mvc;
using PropertyTracker.Application.DTO;
using PropertyTracker.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<Site>))]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await this.service.GetAllAsync());
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(Site))]
        [ProducesResponseType(404)]
        [Route("{id}", Name = "GetSiteById")]
        public async Task<IActionResult> Get([FromRoute][Required] Guid id)
        {
            var site = await this.service.GetAsync(id);

            if (site == null)
            {
                return NotFound();
            }

            return Ok(site);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Post([FromBody][Required] Site site)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (site.Id.Equals(Guid.Empty))
            {
                site.Id = Guid.NewGuid();
            }

            await this.service.CreateAsync(site);

            return CreatedAtRoute("GetSiteById", new { id = site.Id }, null);
        }
    }
}
