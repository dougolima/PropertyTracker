using MongoDbGenericRepository.Models;
using System;
using System.Collections.Generic;

namespace PropertyTracker.Domain.Model
{
    public class Group : Document
    {
        public Guid UserId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<Search> Searchs { get; set; }

        public bool Active { get; set; }
    }
}
