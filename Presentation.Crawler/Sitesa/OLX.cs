using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PropertyCrawler.Console
{
    public class OLX : BaseSite
    {
        public OLX(string url) : base()
        {
            this.Url = url;
        }

        public override List<Property> Parse(string html)
        {
            var properties = new List<Property>();

            HtmlDocument document = new HtmlDocument();
            string htmlString = this.RemoveTrash(html);
            document.LoadHtml(htmlString);
            HtmlNodeCollection collection = document.DocumentNode.SelectNodes("//*[@id='offers_table']/tbody/tr[@class='wrap']");
            foreach (HtmlNode node in collection)
            {
                var property = new Property();
                //property.InternalId = node.Attributes.First(x => x.Name.Equals("data-adid")).Value;
                //property.Url = "https://www.idealista.pt" + node.SelectSingleNode("div/div[2]/a").Attributes.First(x => x.Name.Equals("href")).Value;
                //property.Value = node.SelectSingleNode("div/div[2]/div[2]/span").InnerHtml;
                property.Title = node.SelectSingleNode("td/div/table/tbody/tr[1]/td[2]/div/h3/a/strong").InnerHtml;
                //property.Photo = node.SelectSingleNode("div/div[1]/div[3]/div/div[3]/div[2]/img").Attributes.First(x => x.Name.Equals("src")).Value;
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
