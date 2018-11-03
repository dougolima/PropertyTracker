using MongoDbGenericRepository.Models;

namespace PropertyTracker.Domain.Model
{
    public class Site : Document
    {
        public string Name { get; set; }

        public bool Active { get; set; }
    }
}
