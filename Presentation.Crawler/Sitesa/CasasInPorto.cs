using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PropertyCrawler.Console
{
    public class CasasInPorto : BaseSite
    {
        public CasasInPorto(string url) : base()
        {
            this.Url = url;
        }

        public override List<Property> Parse(string html)
        {
            var properties = new List<Property>();

            HtmlDocument document = new HtmlDocument();
            string htmlString = this.RemoveTrash(html);
            document.LoadHtml(htmlString);
            HtmlNodeCollection collection = document.DocumentNode.SelectNodes("//*[@id='products']/div");
            foreach (HtmlNode node in collection)
            {
                var property = new Property();
                property.InternalId = node.SelectSingleNode("div/div/p[3]").InnerHtml.Replace("Ref.:Ap_inPorto_", string.Empty);
                property.Url = node.SelectSingleNode("div/a").Attributes.First(x => x.Name.Equals("href")).Value;
                property.Value = node.SelectSingleNode("div/div/p[2]").InnerHtml;
                property.Title = node.SelectSingleNode("div/div/h4").InnerHtml;
                property.Photo = node.SelectSingleNode("div/a/div")
                                     .Attributes
                                     .First(x => x.Name.Equals("style"))
                                     .Value
                                     .Replace("background-image: url( &quot;", string.Empty)
                                     .Replace("&quot; ); background-repeat: no-repeat; background-position: center; background-size:100% auto;", string.Empty);
                property.SiteId = Guid.Empty;
                property.Checked = false;
                property.CreatedDate = DateTime.Now;
                properties.Add(property);
            }

            var currentPage = int.Parse(document.DocumentNode.SelectSingleNode("//*[@id='wrap']/div[4]/nav/ul/li[@class='active']/span").InnerHtml);

            if (document.DocumentNode.SelectSingleNode($"//*[@id='wrap']/div[4]/nav/ul/li/a[text()='{currentPage + 1}']") != null)
            {
                var url = document.DocumentNode.SelectSingleNode($"//*[@id='wrap']/div[4]/nav/ul/li/a[text()='{currentPage + 1}']").Attributes.First(x => x.Name.Equals("href")).Value;
                html = this.GetHtml("http://www.casasinporto.pt/" + HttpUtility.HtmlDecode(url));
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
