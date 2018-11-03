using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PropertyTracker.Application.DTO
{
    public class Group
    {
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public List<Search> Searchs { get; set; }

        [Required]
        public bool Active { get; set; }
    }
}
