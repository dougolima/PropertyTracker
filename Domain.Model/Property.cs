using MongoDbGenericRepository.Models;
using System;

namespace PropertyTracker.Domain.Model
{
    public class Property : Document
    {
        public string ExternalId { get; set; }

        public Guid SearchId { get; set; }

        public Guid SiteId { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? DeletedDate { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public string Value { get; set; }

        public string Photo { get; set; }

        public bool Visited { get; set; }

        public bool Deleted { get; set; }

        public string Comments { get; set; }
    }
}
