using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PropertyCrawler.Console
{
    public class Imovirtual : BaseSite
    {
        public Imovirtual(string url) : base()
        {
            this.Url = url;
        }

        public override List<Property> Parse(string html)
        {
            var properties = new List<Property>();

            HtmlDocument document = new HtmlDocument();
            string htmlString = this.RemoveTrash(html);
            document.LoadHtml(htmlString);
            HtmlNodeCollection collection = document.DocumentNode.SelectNodes("//div/div/div[1]/div/article");
            foreach (HtmlNode node in collection)
            {
                var property = new Property();
                property.InternalId = node.Attributes.First(x => x.Name.Equals("data-tracking-id")).Value;
                property.Url = node.SelectSingleNode("div[1]/header/h3/a").Attributes.First(x => x.Name.Equals("href")).Value;
                property.Value = node.SelectSingleNode("div[1]/ul/li[2]").InnerHtml;
                property.Title = node.SelectSingleNode("div[1]/header/h3/a/span/span").InnerHtml;
                property.Photo = node.SelectSingleNode("figure/a/span[1]").Attributes.First(x => x.Name.Equals("style")).Value;
                property.Photo = property.Photo.Replace(")", string.Empty).Replace("background-image:url(", string.Empty);
                property.SiteId = Guid.Empty;
                property.Checked = false;
                property.CreatedDate = DateTime.Now;
                properties.Add(property);
            }

            if (document.DocumentNode.SelectSingleNode("//*[@id='pagerForm']/ul/li[2]/a") != null)
            {
                var url = document.DocumentNode.SelectSingleNode("//*[@id='pagerForm']/ul/li[2]/a").Attributes.First(x => x.Name.Equals("href")).Value;
                html = this.GetHtml(HttpUtility.HtmlDecode(url));
                properties.AddRange(this.Parse(html));
            }

            return properties;
        }

        private string RemoveTrash(string html)
        {
            return html.Substring(html.IndexOf("<!DOCTYPE"));
        }
    }
}
