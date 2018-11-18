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
    public class SearchsController : ControllerBase
    {
        private ISearchService service { get; }

        public SearchsController(ISearchService searchService)
        {
            this.service = searchService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<Search>))]
        public async Task<IActionResult> GetAll([FromQuery][Required] Guid groupId)
        {
            return Ok(await this.service.GetAll(groupId));
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("{groupId}/{searchId}")]
        public async Task<IActionResult> Delete(
            [FromRoute][Required] Guid groupId,
            [FromRoute][Required] Guid searchId)
        {
            var site = await this.service.GetAsync(groupId, searchId);

            if (site == null)
            {
                return NotFound();
            }

            await this.service.DeleteAsync(groupId, searchId);

            return Ok();
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(Search))]
        [ProducesResponseType(404)]
        [Route("{groupId}/{searchId}", Name = "GetSearchById")]
        public async Task<IActionResult> Get(
            [FromRoute][Required] Guid groupId,
            [FromRoute][Required] Guid searchId)
        {
            var site = await this.service.GetAsync(groupId, searchId);

            if (site == null)
            {
                return NotFound();
            }

            return Ok(site);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Post(
            [FromQuery][Required] Guid groupId,
            [FromBody][Required] Search search)
        {
            if (!ModelState.IsValid && (await this.service.IsValidAsync(groupId, search.SiteId)))
            {
                return BadRequest();
            }

            if (search.Id.Equals(Guid.Empty))
            {
                search.Id = Guid.NewGuid();
            }

            await this.service.CreateAsync(groupId, search);

            return CreatedAtRoute("GetSearchById", new { groupId, searchId = search.Id }, null);
        }
    }
}
