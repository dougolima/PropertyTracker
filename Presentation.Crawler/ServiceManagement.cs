using MongoDB.Driver;
using PropertyCrawler.Interfaces;
using PropertyCrawler.Sites;
using PropertyTracker.Application.Services.Interfaces;
using PropertyTracker.Application.Services.Services;
using PropertyTracker.Data.Repository;
using PropertyTracker.Data.Repository.Interfaces;
using System;
using Topshelf;
using Unity;
using Unity.Lifetime;

namespace PropertyTracker.Presentation.Crawler
{
    public class ServiceManagement : ServiceControl
    {
        public UnityContainer container { get; }

        public ServiceManagement()
        {
            var container = new UnityContainer();

            container.RegisterType<ICrawlerEngine, CrawlerEngine>();

            container.RegisterType<IGroupService, GroupService>();
            container.RegisterType<ISearchService, SearchService>();
            container.RegisterType<IPropertyService, PropertyService>();

            container.RegisterType<IGroupRepository, GroupRepository>();
            container.RegisterType<ISearchRepository, SearchRepository>();
            container.RegisterType<ISiteRepository, SiteRepository>();
            container.RegisterType<IPropertyRepository, PropertyRepository>();

            container.RegisterType<IBaseSite, SiteExample>(SiteExample.Id, new ContainerControlledLifetimeManager());

            container.RegisterInstance(new MongoClient().GetDatabase("crawler"));

            this.container = container;
        }

        public bool Start(HostControl hostControl)
        {
            Console.WriteLine($"[{nameof(ServiceManagement)}] - [{nameof(Start)}] - Crawler starting.");

            var crawler = this.container.Resolve<ICrawlerEngine>();

            crawler.Process(this.container).Wait();

            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            Console.WriteLine($"[{nameof(ServiceManagement)}] - [{nameof(Start)}] - Crawler stopped.");
            return true;
        }
    }
}
