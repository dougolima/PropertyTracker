using NReco.PhantomJS;
using PropertyTracker.Application.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PropertyCrawler.Sites
{
    public abstract class BaseSite : IBaseSite
    {
        private PhantomJS phantomJS;

        public BaseSite()
        {
            this.phantomJS = new PhantomJS();
        }

        public string GetHtml(string url)
        {
            using (var outFs = new MemoryStream())
            {
                try
                {
                    phantomJS.RunScript(@"
						var system = require('system');
						var page = require('webpage').create();
						page.open(system.args[1], function() {
							system.stdout.writeLine(page.content);
							phantom.exit();
						});
					", new string[] {
                        url
                    }, null, outFs);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[{nameof(BaseSite)}] - [{nameof(GetHtml)}] -Error getting html from '{url}'. Exception: {ex.Message}");
                    return string.Empty;
                }
                finally
                {
                    phantomJS.Abort();
                }

                return Encoding.UTF8.GetString(outFs.ToArray());
            }
        }

        public abstract string ParseNextPageUrl(string html);

        public abstract List<Property> ParseProperties(string html);

        public string ParseNextPageUrl(Func<string> func, string id)
        {
            try
            {
                return func();
            }
            catch (Exception)
            {
                Console.WriteLine($"[{nameof(BaseSite)}] - [{nameof(ParseNextPageUrl)}] - Error parsing html from site '{id}'.");
            }

            return string.Empty;
        }

        public List<Property> ParseProperties(Func<List<Property>> func, string id)
        {
            try
            {
                return func();
            }
            catch (Exception)
            {
                Console.WriteLine($"[{nameof(BaseSite)}] - [{nameof(ParseProperties)}] - Error parsing html from site '{id}'.");
            }

            return new List<Property>();
        }
    }
}
