using PropertyCrawler.Interfaces;
using PropertyCrawler.Sites;
using PropertyTracker.Application.DTO;
using PropertyTracker.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity;

namespace PropertyTracker.Presentation.Crawler
{
    public class CrawlerEngine : ICrawlerEngine
    {
        private IGroupService groupService { get; }
        private ISearchService searchService { get; }
        private IPropertyService propertyService { get; }

        public CrawlerEngine(IGroupService groupService,
                            ISearchService searchService,
                            IPropertyService propertyService)
        {
            this.groupService = groupService;
            this.searchService = searchService;
            this.propertyService = propertyService;
        }

        public async Task Process(UnityContainer container)
        {
            var groups = await this.groupService.GetAllAsync();
            Console.WriteLine($"[{nameof(CrawlerEngine)}] - [{nameof(Process)}] - {groups.Count} groups to process.");

            foreach (var group in groups)
            {
                Console.WriteLine($"[{nameof(CrawlerEngine)}] - [{nameof(Process)}] - Processing group '{group.Name}' from user '{group.UserId}'.");

                var searchs = await this.searchService.GetAll(group.Id);

                foreach (var search in searchs)
                {
                    IBaseSite baseSite;
                    try
                    {
                        baseSite = container.Resolve<IBaseSite>(search.SiteId.ToString());
                    }
                    catch (Exception)
                    {
                        Console.WriteLine($"[{nameof(CrawlerEngine)}] - [{nameof(Process)}] - Parser for site '{search.SiteId}' was not found.");
                        continue;
                    }

                    Console.WriteLine($"[{nameof(CrawlerEngine)}] - [{nameof(Process)}] - Processing search '{search.Url}' from group '{group.UserId}'.");

                    var oldProperties = await this.propertyService.GetAllAsync(search.Id);

                    List<Property> newProperties = new List<Property>();

                    var url = search.Url;
                    do
                    {
                        var html = baseSite.GetHtml(url);

                        if (string.IsNullOrEmpty(html))
                        {
                            break;
                        }

                        var parsedProperties = baseSite.ParseProperties(html);

                        if (parsedProperties.Count == 0)
                        {
                            break;
                        }

                        newProperties.AddRange(parsedProperties);

                        url = baseSite.ParseNextPageUrl(html);

                    } while (!string.IsNullOrEmpty(url));

                    Console.WriteLine($"[{nameof(CrawlerEngine)}] - [{nameof(Process)}] - It was parsed '{newProperties.Count}' properties from ur '{search.Url}'.");

                    var (inserts, updates, deletes) = await this.SaveProperties(newProperties, oldProperties, search);

                    Console.WriteLine($"[{nameof(CrawlerEngine)}] - [{nameof(Process)}] - Added '{inserts}' properties, updated '{updates}' properties and deleted '{deletes}' properties .");
                }
            }
        }

        public async Task<(int totalInserts, int totalUpdates, int totalDeletes)> SaveProperties(
            List<Property> newProperties,
            List<Property> oldProperties,
            Search search)
        {
            int inserts = 0, updates = 0, deletes = 0;

            try
            {
                inserts = await InsertProperties(newProperties, oldProperties, search);
                updates = await UpdateProperties(newProperties, oldProperties);
                deletes = await DeleteProperties(newProperties, oldProperties);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[{nameof(CrawlerEngine)}] - [{nameof(Process)}] - Error saving properties from site '{search.SiteId}': {ex.Message}.");
            }

            return (inserts, updates, deletes);
        }

        private async Task<int> DeleteProperties(List<Property> newProperties, List<Property> oldProperties)
        {
            var deletes = 0;
            foreach (var prop in oldProperties.Where(o => !newProperties.Any(n => n.ExternalId.Equals(o.ExternalId))))
            {
                prop.Deleted = true;
                prop.DeletedDate = DateTime.Now;

                await this.propertyService.UpsertAsync(prop);

                deletes++;
            }

            return deletes;
        }

        private async Task<int> UpdateProperties(List<Property> newProperties, List<Property> oldProperties)
        {
            var updates = 0;

            foreach (var prop in newProperties.Where(n => oldProperties.Any(o => o.ExternalId.Equals(n.ExternalId))))
            {
                var oldProp = oldProperties.First(o => o.ExternalId.Equals(prop.ExternalId));

                prop.Id = oldProp.Id;
                prop.SiteId = oldProp.SiteId;
                prop.SearchId = oldProp.SearchId;
                prop.Visited = oldProp.Visited;
                prop.Deleted = oldProp.Visited;
                prop.UpdatedDate = DateTime.Now;
                prop.DeletedDate = oldProp.DeletedDate;

                await this.propertyService.UpsertAsync(prop);

                updates++;
            }

            return updates;
        }

        private async Task<int> InsertProperties(List<Property> newProperties, List<Property> oldProperties, Search search)
        {
            var inserts = 0;

            foreach (var prop in newProperties.Where(n => !oldProperties.Any(o => o.ExternalId.Equals(n.ExternalId))))
            {
                prop.Id = Guid.NewGuid();
                prop.SiteId = search.SiteId;
                prop.SearchId = search.Id;
                prop.Visited = false;
                prop.Deleted = false;
                prop.UpdatedDate = null;
                prop.DeletedDate = null;

                await this.propertyService.UpsertAsync(prop);

                inserts++;
            }

            return inserts;
        }
    }
}
