using System;

namespace PropertyTracker.Application.DTO
{
    public class Property
    {
        public Guid Id { get; set; }

        public string ExternalId { get; set; }

        public Guid SiteId { get; set; }

        public Guid SearchId { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public DateTime? DeletedDate { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public string Value { get; set; }

        public string Photo { get; set; }

        public bool Visited { get; set; }

        public bool Deleted { get; set; }
    }
}
