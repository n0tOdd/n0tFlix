using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using MediaBrowser.Model.Providers;
using Microsoft.Extensions.Logging;
using n0tFlix.Helpers.Downloader;

namespace SubtitleDebugger
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string searchwork = "Family Guy";
            string query = "q=" + "Family guy ";
            LoggerFactory ll = new LoggerFactory();
            n0tHttpClient client = new n0tHttpClient(ll);
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("q", searchwork);
            var post = new StringContent(query.Replace(" ", "+"));
            var source = await client.GetStringAsync("http://www.tvsubtitles.net/search.php",default, headers,default);
            string cont = source;
            var conf = AngleSharp.Configuration.Default;
            var browser = AngleSharp.BrowsingContext.New(conf);
            IDocument document = await browser.OpenAsync(x => x.Content(cont));
            var results = document.GetElementsByTagName("ul").Last();
            var links = results.GetElementsByTagName("a");
            List<RemoteSubtitleInfo> list = new List<RemoteSubtitleInfo>();
            foreach (var link in links)
            {
                try
                {
                    if (!link.InnerHtml.Contains(searchwork, StringComparison.OrdinalIgnoreCase))
                        continue;
                    string url = "http://www.tvsubtitles.net" + link.GetAttribute("href");
                    if (string.IsNullOrEmpty(url))
                        continue;
                    string res = await client.GetStringAsync(url,default);
                    document = await browser.OpenAsync(x => x.Content(res));
                    var desc = document.GetElementsByClassName("description").First();
                    var seasons = desc.GetElementsByTagName("a");
                    var correct = seasons.Where(x => x.TextContent.Equals("season 8", StringComparison.OrdinalIgnoreCase)).First();
                    string seasonurl = "http://www.tvsubtitles.net/" + correct.GetAttribute("href");
                    res = await client.GetStringAsync(seasonurl,default);
                    document = await browser.OpenAsync(x => x.Content(res));
                    var episodes = document.GetElementsByName("tbody").First().GetElementsByTagName("table").First().GetElementsByTagName("tr");
                    var thisone = episodes.Where(x => x.GetElementsByTagName("td").First().TextContent.Split("x").Last().Equals("8")).First();
                    var hrr = thisone.GetElementsByTagName("a").Where(x => x.GetAttribute("href").StartsWith("subtitle")).First();
                    string dllink = "http://www.tvsubtitles.net/" + hrr.GetAttribute("href");
                    res = await client.GetStringAsync(dllink,default);
                    document = await browser.OpenAsync(x => x.Content(res));
                    string title = document.QuerySelector("//*[@id=\"content\"]/div[4]/div/div[3]/table/tbody/tr[2]/td[3]").TextContent;
                    string author = document.QuerySelector("//*[@id=\"content\"]/div[4]/div/div[3]/table/tbody/tr[5]/td[3]").TextContent;

                    list.Add(new RemoteSubtitleInfo()
                    {
                        Name = title,
                        Format = "srt",
                        Id = dllink.Replace("subtitle", "download"),
                        Author = author,
                        ProviderName = "TvSubtitles"
                    });
                }
                catch (Exception ex)
                {

                }
            }
            //todo add så den henter ut riktig episode som vi trenger
          
        }
    }
}
