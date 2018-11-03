using System;
using System.ComponentModel.DataAnnotations;

namespace PropertyTracker.Application.DTO
{
    public class Search
    {
        public Guid Id { get; set; }

        [Required]
        public string Url { get; set; }

        [Required]
        public Guid SiteId { get; set; }
    }
}
