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
    public class GroupsController : ControllerBase
    {
        private IGroupService groupService { get; }

        public GroupsController(IGroupService groupService)
        {
            this.groupService = groupService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<Group>))]
        public async Task<IActionResult> GetAll([FromQuery][Required] Guid userId)
        {
            return Ok(await this.groupService.GetAll(userId));
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Post([FromBody][Required] Group group)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (group.Id.Equals(Guid.Empty))
            {
                group.Id = Guid.NewGuid();
            }

            await this.groupService.Create(group);

            return CreatedAtRoute("GetGroupById", new { id = group.Id }, null);
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("{id}")]
        public async Task<IActionResult> Put([FromBody][Required] Group group, [FromRoute][Required] Guid id)
        {
            if (!ModelState.IsValid || !id.Equals(group.Id))
            {
                return BadRequest();
            }

            await this.groupService.Update(group);

            return Ok();
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute][Required] Guid id)
        {
            var site = await this.groupService.Get(id);

            if (site == null)
            {
                return NotFound();
            }

            await this.groupService.Delete(id);

            return Ok();
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(Group))]
        [ProducesResponseType(404)]
        [Route("{id}", Name = "GetGroupById")]
        public async Task<IActionResult> Get([FromRoute][Required] Guid id)
        {
            var site = await this.groupService.Get(id);

            if (site == null)
            {
                return NotFound();
            }

            return Ok(site);
        }
    }
}
