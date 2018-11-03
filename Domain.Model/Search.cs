using MongoDbGenericRepository.Models;
using System;

namespace PropertyTracker.Domain.Model
{
    public class Search : Document
    {
        public string Url { get; set; }

        public Guid SiteId { get; set; }
    }
}
