using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediaBrowser.Controller.Channels;
using MediaBrowser.Model.Channels;
using MediaBrowser.Model.Dto;
using MediaBrowser.Model.Entities;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using n0tFlix.Helpers.Downloader;
using n0tFlix.Plugin.NRK.Models;

namespace n0tFlix.Plugin.NRK
{
    public class Worker
    {
        private readonly n0tHttpClient httpClient;
        private readonly ILoggerFactory loggerFactory;
        private readonly ILogger<Worker> logger;

        public Worker(ILoggerFactory loggerFactory)
        {
            httpClient = new n0tHttpClient(loggerFactory);
            this.loggerFactory = loggerFactory;
            this.logger = loggerFactory.CreateLogger<Worker>();
        }
        /// <summary>
        /// Henter alle channel typene vi bruker som start side da man åpner pluginet
        /// gir en søt liten oversikt over ting som man kanskje vil søke opp
        /// </summary>
        /// <returns><see cref="ChannelItemResult"/> containing the types of categories.</returns>
        internal async Task<ChannelItemResult> GetChannelCategoriesAsync(ILogger logger, IMemoryCache memoryCache,CancellationToken cancellationToken)
        {
            logger.LogError("GetChannelCategoriesAsync");
            if (memoryCache.TryGetValue("nrk-categories", out ChannelItemResult cachedValue))
            {
                logger.LogInformation("Function={function} FolderId={folderId} Cache Hit", nameof(GetChannelCategoriesAsync), "nrk-categories");
                return cachedValue;
            }
            else
            {
                logger.LogError("Grabbing categories");
                var root = System.Text.Json.JsonSerializer.Deserialize<Categories.root>(await  httpClient.GetStringAsync("https://psapi.nrk.no/tv/pages", cancellationToken));
                ChannelItemResult result = new ChannelItemResult();
                foreach (var v in root.pageListItems)
                {
                    result.Items.Add(new ChannelItemInfo
                    {
                        Id = "https://psapi.nrk.no/tv/pages/" + v.id,
                        Name = v.title,
                        FolderType = ChannelFolderType.Container,
                        Type = ChannelItemType.Folder,
                        MediaType = ChannelMediaType.Video,
                        HomePageUrl = "https://tv.nrk.no" + v.links.self.href,
                        ImageUrl = v.image.webimages[0].uri ?? v.image.webimages[1].uri ?? v.image.webimages[2].uri ?? v.image.webimages[3].uri ?? v.image.webimages[4].uri
                    });
                    result.TotalRecordCount++;
                }
                logger.LogError("Collected " + result.TotalRecordCount.ToString() + " categories for this channel");
                memoryCache.Set("nrk-categories", result, DateTimeOffset.Now.AddDays(7));
                return result;
            }
        }

        /// <summary>
        /// this function here grabs all the content awailable for selected category
        /// </summary>
        /// <param name="query"></param>
        /// <param name="logger"></param>
        /// <param name="memoryCache"></param>
        /// <returns></returns>
        internal async Task<ChannelItemResult> GetCategoryItemsAsync(InternalChannelItemQuery query, ILogger logger, IMemoryCache memoryCache, CancellationToken cancellationToken)
        {
            logger.LogError("GetCategoryItemsAsync");
            if (memoryCache.TryGetValue("nrk-categories-" + query.FolderId, out ChannelItemResult cachedValue))
            {
                logger.LogInformation("Function={function} FolderId={folderId} Cache Hit", nameof(GetCategoryItemsAsync), query.FolderId);
                return cachedValue;
            }
            else
            {
                logger.LogInformation("Function={function} FolderId={folderId} web download", nameof(GetCategoryItemsAsync), query.FolderId);
                
                string json = await httpClient.GetStringAsync(query.FolderId,cancellationToken);
                var root = System.Text.Json.JsonSerializer.Deserialize<CategoryItems.root>(json);
                ChannelItemResult result = new ChannelItemResult();
                foreach (var v in root.sections)
                {
                    foreach (var p in v.included.plugs)
                    {
                        try

                        {
                            string mainurl = string.Empty;
                            if (p.targetType == "series")
                            {
                                result.Items.Add(new ChannelItemInfo
                                {
                                    Id = "https://psapi.nrk.no/tv/catalog" + p.series.links.self.href,
                                    Name = p.displayContractContent.contentTitle,
                                    ImageUrl = p.displayContractContent.displayContractimage.webimages[0].uri ?? p.displayContractContent.displayContractimage.webimages[1].uri,
                                    FolderType = ChannelFolderType.Container,
                                    Type = ChannelItemType.Folder,

                                    SeriesName = p.displayContractContent.contentTitle,
                                    MediaType = ChannelMediaType.Video,
                                    HomePageUrl = "htps://tv.nrk.no" + p.series.links.self.href,
                                    Overview = p.displayContractContent.description,
                                });
                                result.TotalRecordCount++;
                            }
                            else if (p.targetType == "standaloneProgram")
                            {
                                result.Items.Add(new ChannelItemInfo
                                {
                                    Id = "https://psapi.nrk.no" + p.standaloneProgram.links.playback.href,
                                    Name = p.displayContractContent.contentTitle,
                                    ImageUrl = p.displayContractContent.displayContractimage.webimages[0].uri ?? p.displayContractContent.displayContractimage.webimages[1].uri,
                                    FolderType = ChannelFolderType.Container,
                                    Type = ChannelItemType.Media,
                                    //           RunTimeTicks = TimeSpan.Parse(p.StandaloneProgram.Duration).Ticks,
                                    Overview = p.displayContractContent.description,
                                    MediaType = ChannelMediaType.Video,
                                    HomePageUrl = "htps://tv.nrk.no" + p.standaloneProgram.links.self.href,
                                });
                                result.TotalRecordCount++;
                            }
                            else if (p.targetType == "episode")
                            {
                                result.Items.Add(new ChannelItemInfo
                                {
                                    Id = "https://psapi.nrk.no" + p.episode.links.playback.href,
                                    Name = p.displayContractContent.contentTitle,
                                    ImageUrl = p.displayContractContent.displayContractimage.webimages[0].uri ?? p.displayContractContent.displayContractimage.webimages[1].uri ?? p.displayContractContent.fallbackimage.webimages[0].uri,
                                    FolderType = ChannelFolderType.Container,
                                    //           RunTimeTicks = TimeSpan.Parse(p.Episode.Duration).Ticks,
                                    Type = ChannelItemType.Media,
                                    Overview = p.displayContractContent.description,
                                    MediaType = ChannelMediaType.Video,
                                    HomePageUrl = "htps://tv.nrk.no" + p.episode.links.self.href,
                                });
                                result.TotalRecordCount++;
                            }
                        }
                        catch (Exception e)
                        {
                            logger.LogError("Error trying to parse all category items from the nrk web tv channel");
                            logger.LogError(e.Message);
                        }
                    }
                }
                memoryCache.Set("nrk-categories-" + query.FolderId, result, DateTimeOffset.Now.AddDays(7));
                return result;
            }
        }

        /// <summary>
        /// If one of the items in the category is a series with multiple seasons this function here grabs season info for us
        /// </summary>
        /// <param name="query"></param>
        /// <param name="logger"></param>
        /// <param name="memoryCache"></param>
        /// <returns></returns>
        internal async Task<ChannelItemResult> GetSeasonInfoAsync(InternalChannelItemQuery query, ILogger logger, IMemoryCache memoryCache)
        {
            logger.LogError("GetSeasonInfoAsync");

            if (memoryCache.TryGetValue("nrk-categories-seasoninfo-" + query.FolderId, out ChannelItemResult cachedValue))
            {
                logger.LogInformation("Function={function} FolderId={folderId} Cache Hit", nameof(GetSeasonInfoAsync), "nrk-categories-seasoninfo-" + query.FolderId);
                return cachedValue;
            }
            else
            {
                logger.LogInformation("Function={function} FolderId={folderId} web download", nameof(GetSeasonInfoAsync), "nrk-categories-seasoninfo-" + query.FolderId);
                HttpClient httpClient = new HttpClient();
                string json = await httpClient.GetStringAsync(query.FolderId);
                var root = System.Text.Json.JsonSerializer.Deserialize<SeasonInfo.root>(json);
                ChannelItemResult result = new ChannelItemResult();

                foreach (var emb in root.Embedded.Seasons)
                {
                    ChannelItemInfo info = new ChannelItemInfo()
                    {
                        FolderType = ChannelFolderType.Container,
                        SeriesName = root.Sequential.Titles.Title,
                        Name = emb.Titles.Title,
                        Overview = root.Sequential.Titles.Subtitle,
                        HomePageUrl = "https://tv.nrk.no" + emb.Links.Series.Href,
                        Id = "https://psapi.nrk.no" + emb.Links.Self.Href,
                        MediaType = ChannelMediaType.Video,
                        Type = ChannelItemType.Folder
                    };
                    result.Items.Add(info);
                    result.TotalRecordCount++;
                }
                memoryCache.Set("nrk-categories-seasoninfo-" + query.FolderId, result, DateTimeOffset.Now.AddDays(7));
                return result;
            }
        }

        /// <summary>
        /// This function here grabs all the episodes in the selected season
        /// </summary>
        /// <param name="query"></param>
        /// <param name="logger"></param>
        /// <param name="memoryCache"></param>
        /// <returns></returns>
        internal async Task<ChannelItemResult> GetEpisodeInfoAsync(InternalChannelItemQuery query, ILogger logger, IMemoryCache memoryCache)
        {
            logger.LogError("GetEpisodeInfoAsync");

            if (memoryCache.TryGetValue("nrk-episodeinfo-" + query.FolderId, out ChannelItemResult cachedValue))
            {
                logger.LogInformation("Function={function} FolderId={folderId} Cache Hit", nameof(GetSeasonInfoAsync), "nrk-episodeinfo-" + query.FolderId);
                return cachedValue;
            }
            else
            {
                logger.LogInformation("Function={function} FolderId={folderId} web download", nameof(GetCategoryItemsAsync), "nrk-episodeinfo-" + query.FolderId);
                HttpClient httpClient = new HttpClient();
                string json = await httpClient.GetStringAsync(query.FolderId);
                var root = System.Text.Json.JsonSerializer.Deserialize<EpisodeInfo.root>(json); 
                ChannelItemResult result = new ChannelItemResult();

                foreach (var ep in root.Embedded.Episodes)
                {
                    ChannelItemInfo info = new ChannelItemInfo()
                    {
                        FolderType = ChannelFolderType.Container,
                        SeriesName = root.Titles.Title,
                        Name = ep.Titles.Title,
                        ImageUrl = ep.image[0].Url ?? ep.image[1].Url ?? ep.image[2].Url ?? ep.image[3].Url ?? ep.image[4].Url ?? ep.image[5].Url,
                        Overview = ep.Titles.Subtitle,
                        HomePageUrl = ep.Links.Share.Href,
                        Id = "https://psapi.nrk.no" + ep.Links.Playback.Href,
                        MediaType = ChannelMediaType.Video,
                        Type = ChannelItemType.Media
                    };
                    result.Items.Add(info);
                    result.TotalRecordCount++;
                }
                memoryCache.Set("nrk-episodeinfo-" + query.FolderId, result, DateTimeOffset.Now.AddDays(7));
                return result;
            }
            return null;
        }

        /// <summary>
        /// This function here is to collect the headliners from the server, we are using this insted of latest content because the api dosnt give us latest content atm
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="memoryCache"></param>
        /// <returns></returns>
        internal async Task<IEnumerable<ChannelItemInfo>> GetHeadlinersInfoAsync(ILogger logger, IMemoryCache memoryCache)
        {
            logger.LogError("Grabbing latest headliners");
            HttpClient httpClient = new HttpClient();
            string json = await httpClient.GetStringAsync("https://psapi.nrk.no/tv/headliners/default");
            var root = System.Text.Json.JsonSerializer.Deserialize<HeadlinersInfo.root>(json); 
            List<ChannelItemInfo> list = new List<ChannelItemInfo>();
            foreach (var head in root.Headliners)
            {
                list.Add(new ChannelItemInfo()
                {
                    Name = head.Title,
                    Id = "https://psapi.nrk.no" + head.Links.Seriespage.Href,
                    Overview = head.SubTitle,
                    ImageUrl = head.images[0].Uri ?? head.images[1].Uri ?? head.images[2].Uri ?? head.images[3].Uri ?? head.images[4].Uri ?? head.images[5].Uri,
                    FolderType = ChannelFolderType.Container,
                    Type = ChannelItemType.Folder,
                    SeriesName = head.Title,
                    MediaType = ChannelMediaType.Video,
                });
            }
            return null;
        }

        /// <summary>
        /// This function here grabs the mediasource (stream link) to the selected media
        /// </summary>
        /// <param name="id"></param>
        /// <param name="logger"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        internal async Task<IEnumerable<MediaSourceInfo>> GetMediaSourceInfo(string id, ILogger logger, CancellationToken cancellationToken)
        {
            logger.LogError("Grabbing stream data for " + id);
            HttpClient httpClient = new HttpClient();
            string json = await httpClient.GetStringAsync(id);
            
            var root = System.Text.Json.JsonSerializer.Deserialize<PlayBackInfo.root>(json);
            List<MediaStream> mediaStreams = new List<MediaStream>();

            return new List<MediaSourceInfo>()
            {
                 new MediaSourceInfo()
                 {
                     Path = root.Playable.Assets.First().Url,
                     Protocol = MediaBrowser.Model.MediaInfo.MediaProtocol.File,
                     Id = root.Id,
               //      MediaStreams = mediaStreams,
                     //Todo find a way to parse the duration correctly
                     //RunTimeTicks = TimeSpan.Parse(root.Playable.Duration).Ticks,
                      IsRemote = true,
                     EncoderProtocol = MediaBrowser.Model.MediaInfo.MediaProtocol.File,
                     VideoType = MediaBrowser.Model.Entities.VideoType.VideoFile
                 },
            };
        }
    }
}