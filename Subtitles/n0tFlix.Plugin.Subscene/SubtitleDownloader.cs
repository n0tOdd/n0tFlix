using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using n0tFlix.Plugin.Subscene.Configuration;
using MediaBrowser.Common.Extensions;
using MediaBrowser.Controller.Providers;
using MediaBrowser.Controller.Subtitles;
using MediaBrowser.Model.Entities;
using MediaBrowser.Model.Providers;
using Microsoft.Extensions.Logging;
using MediaBrowser.Common;
using MediaBrowser.Model.Globalization;
using MediaBrowser.Model.Serialization;
using n0tFlix.Subtitles.Subscene;
using System.IO.Compression;
using System.Web;
using System.Xml;

namespace n0tFlix.Plugin.Subscene
{
    /// <summary>
    /// The open subtitle downloader.
    /// </summary>
    public class SubsceneDownloader : ISubtitleProvider
    {
        private const string Domain = "https://subscene.com";
    private const string SubtitleUrl = "/subtitles/{0}/{1}";
    private const string SearchUrl = "/subtitles/searchbytitle?query={0}&l=";
    private const string XmlTag = "<?xml version=\"1.0\" ?>";

    public string Name => "Subscene";

    public IEnumerable<VideoContentType> SupportedMediaTypes =>
        new List<VideoContentType>()
        {
                VideoContentType.Movie,
                VideoContentType.Episode
        };

    public int Order => 3;


    private readonly HttpClient _httpClient;
    private readonly ILogger<SubsceneDownloader> _logger;
    private readonly IApplicationHost _appHost;
    private readonly ILocalizationManager _localizationManager;
    private readonly IJsonSerializer _jsonSerializer;

    public SubsceneDownloader(IHttpClientFactory httpClientFactory, ILogger<SubsceneDownloader> logger, IApplicationHost appHost
        , ILocalizationManager localizationManager, IJsonSerializer jsonSerializer)
    {
        _httpClient = httpClientFactory.CreateClient();
        _logger = logger;
        _appHost = appHost;
        _localizationManager = localizationManager;
        _jsonSerializer = jsonSerializer;
    }


    public async Task<SubtitleResponse> GetSubtitles(string id, CancellationToken cancellationToken)
    {
        var ids = id.Split(new[] { "___" }, StringSplitOptions.RemoveEmptyEntries);
        var url = ids[0].Replace("__", "/");
        var lang = ids[1];

        _logger?.LogInformation($"Subscene= Request for subtitle= {url}");

        var html = await GetHtml(Domain, url);
        if (string.IsNullOrWhiteSpace(html))
            return new SubtitleResponse();

        var startIndex = html.IndexOf("<div class=\"download\">");
        var endIndex = html.IndexOf("</div>", startIndex);

        var downText = html.SubStr(startIndex, endIndex);
        startIndex = downText.IndexOf("<a href=\"");
        endIndex = downText.IndexOf("\"", startIndex + 10);

        var downloadLink = downText.SubStr(startIndex + 10, endIndex - 1);

        _logger?.LogDebug($"Subscene= Downloading subtitle= {downloadLink}");


        var ms = new MemoryStream();
        var fileExt = string.Empty;
        try
        {
            using (var response = await _httpClient.GetAsync($"{Domain}/{downloadLink}").ConfigureAwait(false))
            {
                

                var archive = new ZipArchive(response.Content.ReadAsStream());

                var item = (archive.Entries.Count > 1
                    ? archive.Entries.FirstOrDefault(a => a.FullName.ToLower().Contains("utf"))
                    : archive.Entries.First()) ?? archive.Entries.First();

                await item.Open().CopyToAsync(ms).ConfigureAwait(false);
                ms.Position = 0;

                fileExt = item.FullName.Split('.').LastOrDefault();

                if (string.IsNullOrWhiteSpace(fileExt))
                {
                    fileExt = "srt";
                }
            }
        }
        catch (Exception e)
        {
            //
        }

        return new SubtitleResponse
        {
            Format = fileExt,
            Language = NormalizeLanguage(lang),
            Stream = ms
        };
    }

    public async Task<IEnumerable<RemoteSubtitleInfo>> Search(SubtitleSearchRequest request,
        CancellationToken cancellationToken)
    {
        var prov = request.ProviderIds?.FirstOrDefault(p =>
            p.Key.ToLower() == "imdb" || p.Key.ToLower() == "tmdb" || p.Key.ToLower() == "tvdb");

        if (prov == null)
            return new List<RemoteSubtitleInfo>();

        if (request.ContentType == VideoContentType.Episode &&
            (request.ParentIndexNumber == null || request.IndexNumber == null))
            return new List<RemoteSubtitleInfo>();

        var title = request.ContentType == VideoContentType.Movie
            ? request.Name
            : request.SeriesName;

        var res = await Search(title, request.ProductionYear, request.Language, request.ContentType, prov.Value.Value,
            request.ParentIndexNumber ?? 0, request.IndexNumber ?? 0);

        _logger?.LogDebug($"Subscene= result found={res?.Count()}");
        return res;
    }

    public async Task<IEnumerable<RemoteSubtitleInfo>> Search(string title, int? year, string lang,
        VideoContentType contentType, string movieId, int season, int episode)
    {
        _logger?.LogInformation(
            $"Subscene= Request subtitle for '{title}', language={lang}, year={year}, movie Id={movieId}, Season={season}, Episode={episode}");

        var res = new List<RemoteSubtitleInfo>();
            res = contentType == VideoContentType.Movie
                ? await SearchMovie(title, year, lang, movieId)
                : await SearchTV(title, year, lang, movieId, season, episode);

            if (!res.Any())
                return res;
        

        res.RemoveAll(l => string.IsNullOrWhiteSpace(l.Name));

        res = res.GroupBy(s => s.Id)
            .Select(s => new RemoteSubtitleInfo()
            {
                Id = s.First().Id,
                Name = $"{s.First().ProviderName} ({s.First().Author})",
                Author = s.First().Author,
                ProviderName = "Subscene",
                Comment = string.Join("<br/>", s.Select(n => n.Name)),
                Format = "srt"
            }).ToList();
        return res.OrderBy(s => s.Name);
    }

    private async Task<List<RemoteSubtitleInfo>> SearchMovie(string title, int? year, string lang, string movieId)
    {
        var res = new List<RemoteSubtitleInfo>();

        if (!string.IsNullOrWhiteSpace(movieId))
        {
            //   var mDb = new MovieDb(_jsonSerializer, _httpClient, _appHost);
            // var info = await mDb.GetMovieInfo(movieId);

            //                if (info != null)
            //              {
            //                year = info.release_date.Year;
            //              title = info.Title;
            //            _logger?.LogInformation($"Subscene= Original movie title=\"{info.Title}\", year={info.release_date.Year}");
            //      }
        }

        #region Search subscene

        _logger?.LogDebug($"Subscene= Searching for site search \"{title}\"");
        var url = string.Format(SearchUrl, HttpUtility.UrlEncode(title));
        var html = await GetHtml(Domain, url);

        if (string.IsNullOrWhiteSpace(html))
        {
            return res;
        }

        var xml = new XmlDocument();
        xml.LoadXml($"{XmlTag}{html}");

        var xNode = xml.SelectSingleNode("//div[@class='search-result']");
        if (xNode == null)
            return res;

        var ex = xNode?.SelectSingleNode("h2[@class='exact']")
                 ?? xNode?.SelectSingleNode("h2[@class='close']")
                 ?? xNode?.SelectSingleNode("h2[@class='popular']");

        if (ex == null)
            return res;

        xNode = xNode.SelectSingleNode("ul");
        if (xNode == null)
            return res;
        var sItems = xNode.SelectNodes(".//a");

        foreach (XmlNode item in sItems)
        {
            var sYear = item.InnerText.Split('(', ')')[1];
            if (year.Value != Convert.ToInt16(sYear))
                continue;

            var link = item.Attributes["href"].Value;
            link += $"/{MapLanguage(lang)}";
            html = await GetHtml(Domain, link);
            break;
        }

        #endregion

        #region Extract subtitle links

        xml = new XmlDocument();
        xml.LoadXml($"{XmlTag}{html}");

        var repeater = xml.SelectNodes("//table/tbody/tr");

        if (repeater == null)
        {
            return res;
        }

        foreach (XmlElement node in repeater)
        {
            var name = RemoveExtraCharacter(node.SelectSingleNode(".//a")?.SelectNodes("span").Item(1)
                ?.InnerText);

            if (string.IsNullOrWhiteSpace(name))
                continue;

            var id = (node.SelectSingleNode(".//a")?.Attributes["href"].Value + "___" + lang)
                .Replace("/", "__");
            var item = new RemoteSubtitleInfo
            {
                Id = id,
                Name = RemoveExtraCharacter(node.SelectSingleNode(".//a")?.SelectNodes("span").Item(1)
                    ?.InnerText),
                Author = RemoveExtraCharacter(node.SelectSingleNode("td[@class='a6']")?.InnerText),
                ProviderName = RemoveExtraCharacter(node.SelectSingleNode("td[@class='a5']")?.InnerText),
                ThreeLetterISOLanguageName = NormalizeLanguage(lang),
                IsHashMatch = true
            };
            res.Add(item);
        }

        #endregion

        return res;
    }

    private readonly string[] _seasonNumbers =
        {"", "First", "Second", "Third", "Fourth", "Fifth", "Sixth", "Seventh", "Eighth", "Ninth"};

    private async Task<List<RemoteSubtitleInfo>> SearchTV(string title, int? year, string lang, string movieId,
        int season, int episode)
    {
        var res = new List<RemoteSubtitleInfo>();

        var mDb = new MovieDb(_jsonSerializer, _httpClient, _appHost);
        var info = await mDb.GetTvInfo(movieId);

        if (info == null)
            return new List<RemoteSubtitleInfo>();

        #region Search TV Shows

        title = info.Name;

        _logger?.LogDebug($"Subscene= Searching for site search \"{title}\"");
        var url = string.Format(SearchUrl, HttpUtility.UrlEncode($"{title} - {_seasonNumbers[season]} Season"));
        var html = await GetHtml(Domain, url);

        if (string.IsNullOrWhiteSpace(html))
            return res;

        var xml = new XmlDocument();
        xml.LoadXml($"{XmlTag}{html}");

        var xNode = xml.SelectSingleNode("//div[@class='search-result']");
        if (xNode == null)
            return res;

        var ex = xNode?.SelectSingleNode("h2[@class='exact']")
                 ?? xNode?.SelectSingleNode("h2[@class='close']");
        if (ex == null)
            return res;

        xNode = xNode.SelectSingleNode("ul");
        if (xNode == null)
            return res;

        var sItems = xNode.SelectNodes(".//a");
        foreach (XmlNode item in sItems)
        {
            if (!item.InnerText.StartsWith($"{title} - {_seasonNumbers[season]} Season"))
                continue;

            var link = item.Attributes["href"].Value;
            link += $"/{MapLanguage(lang)}";
            html = await GetHtml(Domain, link);
            break;
        }

        #endregion

        #region Extract subtitle links

        xml = new XmlDocument();
        xml.LoadXml($"{XmlTag}{html}");

        var repeater = xml.SelectNodes("//table/tbody/tr");

        if (repeater == null)
        {
            return res;
        }

        foreach (XmlElement node in repeater)
        {
            var name = RemoveExtraCharacter(node.SelectSingleNode(".//a")?.SelectNodes("span").Item(1)
                ?.InnerText);

            if (string.IsNullOrWhiteSpace(name))
                continue;

            var id = (node.SelectSingleNode(".//a")?.Attributes["href"].Value + "___" + lang)
                .Replace("/", "__");
            var item = new RemoteSubtitleInfo
            {
                Id = id,
                Name = RemoveExtraCharacter(node.SelectSingleNode(".//a")?.SelectNodes("span").Item(1)
                    ?.InnerText),
                Author = RemoveExtraCharacter(node.SelectSingleNode("td[@class='a6']")?.InnerText),
                ProviderName = RemoveExtraCharacter(node.SelectSingleNode("td[@class='a5']")?.InnerText),
                ThreeLetterISOLanguageName = NormalizeLanguage(lang),
                IsHashMatch = true
            };
            res.Add(item);
        }

        #endregion

        var eTitle = $"S{season.ToString().PadLeft(2, '0')}E{episode.ToString().PadLeft(2, '0')}";
        res.RemoveAll(s => !s.Name.Contains(eTitle));

        return res;
    }

    private string RemoveExtraCharacter(string text) =>
        text?.Replace("\r\n", "")
            .Replace("\t", "")
            .Trim();

    private async Task<string> GetHtml(string domain, string path)
    {
        var html = await Tools.RequestUrl(domain, path, HttpMethod.Get).ConfigureAwait(false);

        var scIndex = html.IndexOf("<script");
        while (scIndex >= 0)
        {
            var scEnd = html.IndexOf("</script>", scIndex + 1);
            var end = scEnd - scIndex + 9;
            html = html.Remove(scIndex, end);
            scIndex = html.IndexOf("<script");
        }

        scIndex = html.IndexOf("&#");
        while (scIndex >= 0)
        {
            var scEnd = html.IndexOf(";", scIndex + 1);
            var end = scEnd - scIndex + 1;
            var word = html.Substring(scIndex, end);
            html = html.Replace(word, System.Net.WebUtility.HtmlDecode(word));
            scIndex = html.IndexOf("&#");
        }

        html = html.Replace("&nbsp;", "");
        html = html.Replace("&amp;", "Xamp;");
        html = html.Replace("&", "&amp;");
        html = html.Replace("Xamp;", "&amp;");
        html = html.Replace("--->", "---");
        html = html.Replace("<---", "---");
        html = html.Replace("<--", "--");
        html = html.Replace("Xamp;", "&amp;");
        html = html.Replace("<!DOCTYPE html>", "");
        return html;
    }

    private string NormalizeLanguage(string language)
    {
        if (string.IsNullOrWhiteSpace(language))
        {
            var culture = _localizationManager?.FindLanguageInfo(language);
            if (culture != null)
            {
                return culture.ThreeLetterISOLanguageName;
            }
        }

        return language;
    }

    private string MapLanguage(string lang)
    {
        switch (lang.ToLower())
        {
            case "per":
                lang = "farsi_persian";
                break;
            case "ara":
                lang = "arabic";
                break;
            case "eng":
                lang = "english";
                break;
            case "bur":
                lang = "burmese";
                break;
            case "dan":
                lang = "danish";
                break;
            case "dut":
                lang = "dutch";
                break;
            case "heb":
                lang = "hebrew";
                break;
            case "ind":
                lang = "indonesian";
                break;
            case "kor":
                lang = "korean";
                break;
            case "may":
                lang = "malay";
                break;
            case "spa":
                lang = "spanish";
                break;
            case "vie":
                lang = "vietnamese";
                break;
            case "tur":
                lang = "turkish";
                break;
            case "ben":
                lang = "bengali";
                break;
            case "bul":
                lang = "bulgarian";
                break;
            case "hrv":
                lang = "croatian";
                break;
            case "fin":
                lang = "finnish";
                break;
            case "fre":
                lang = "french";
                break;
            case "ger":
                lang = "german";
                break;
            case "gre":
                lang = "greek";
                break;
            case "hun":
                lang = "hungarian";
                break;
            case "ita":
                lang = "italian";
                break;
            case "kur":
                lang = "kurdish";
                break;
            case "mac":
                lang = "macedonian";
                break;
            case "mal":
                lang = "malayalam";
                break;
            case "nno":
                lang = "norwegian";
                break;
            case "nob":
                lang = "norwegian";
                break;
            case "nor":
                lang = "norwegian";
                break;
            case "por":
                lang = "portuguese";
                break;
            case "rus":
                lang = "russian";
                break;
            case "srp":
                lang = "serbian";
                break;
            case "sin":
                lang = "sinhala";
                break;
            case "slv":
                lang = "slovenian";
                break;
            case "swe":
                lang = "swedish";
                break;
            case "tha":
                lang = "thai";
                break;
            case "urd":
                lang = "urdu";
                break;
            case "pob":
                lang = "brazillian-portuguese";
                break;
        }

        return lang;
    }
}
}
