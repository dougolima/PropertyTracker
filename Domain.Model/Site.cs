using System;

namespace PropertyTracker.Domain.Model
{
    public class Site
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public bool Active { get; set; }
    }
}
