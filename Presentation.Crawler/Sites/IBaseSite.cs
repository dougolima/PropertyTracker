using PropertyTracker.Application.DTO;
using System.Collections.Generic;

namespace PropertyCrawler.Sites
{
    public interface IBaseSite
    {
        string GetHtml(string url);

        List<Property> ParseProperties(string html);

        string ParseNextPageUrl(string html);
    }
}