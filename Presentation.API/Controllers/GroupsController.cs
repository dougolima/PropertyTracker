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
        private IGroupService service { get; }

        public GroupsController(IGroupService service)
        {
            this.service = service;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<Group>))]
        public async Task<IActionResult> GetAll([Required] Guid userId)
        {
            return Ok(await this.service.GetAll(userId));
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Post(Group group)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (group.Id.Equals(Guid.Empty))
            {
                group.Id = Guid.NewGuid();
            }

            await this.service.Create(group);

            return CreatedAtRoute("GetGroupById", new { id = group.Id }, null);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(Group))]
        [ProducesResponseType(404)]
        [Route("{id}", Name = "GetGroupById")]
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
