using HtmlAgilityPack;
using PropertyTracker.Application.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PropertyCrawler.Sites
{
    public class SiteExample : BaseSite
    {
        public const string Id = "17ac71f3-8776-35ff-865b-580506e3b7e6";

        public SiteExample() : base() { }

        public override List<Property> ParseProperties(string html)
        {
            List<Property> parseFunc()
            {
                var properties = new List<Property>();

                HtmlDocument document = new HtmlDocument();
                document.LoadHtml(this.RemoveTrash(html));
                HtmlNodeCollection collection = document.DocumentNode.SelectNodes("//*[@id='products']/div");
                foreach (HtmlNode node in collection)
                {
                    var property = new Property();
                    property.ExternalId = node.SelectSingleNode("div/div/p[3]").InnerHtml.Replace("Ref.:Ap_inPorto_", string.Empty);
                    property.Url = node.SelectSingleNode("div/a").Attributes.First(x => x.Name.Equals("href")).Value;
                    property.Value = node.SelectSingleNode("div/div/p[2]").InnerHtml;
                    property.Title = node.SelectSingleNode("div/div/h4").InnerHtml;
                    property.Photo = node.SelectSingleNode("div/a/div")
                                         .Attributes
                                         .First(x => x.Name.Equals("style"))
                                         .Value
                                         .Replace("background-image: url( &quot;", string.Empty)
                                         .Replace("&quot; ); background-repeat: no-repeat; background-position: center; background-size:100% auto;", string.Empty);
                    properties.Add(property);
                }
                return properties;
            }

            return base.ParseProperties(parseFunc, Id);
        }

        private string RemoveTrash(string html)
        {
            return html.Substring(html.IndexOf("<!DOCTYPE"));
        }

        public override string ParseNextPageUrl(string html)
        {
            string parseFunc()
            {
                HtmlDocument document = new HtmlDocument();
                document.LoadHtml(this.RemoveTrash(html));

                var currentPage = int.Parse(document.DocumentNode.SelectSingleNode("//*[@id='wrap']/div[4]/nav/ul/li[@class='active']/span").InnerHtml);
                var button = document.DocumentNode.SelectSingleNode($"//*[@id='wrap']/div[4]/nav/ul/li/a[text()='{currentPage + 1}']");

                return button != null ?
                        $"http://www.siteexample.pt/{HttpUtility.HtmlDecode(button.Attributes.First(x => x.Name.Equals("href")).Value)}" :
                        string.Empty;
            }

            return base.ParseNextPageUrl(parseFunc, Id);
        }
    }
}
