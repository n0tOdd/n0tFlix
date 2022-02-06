using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediaBrowser.Controller.Channels;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using n0tFlix.Helpers.Downloader;

namespace n0tFlix.Plugin.TubiTV
{
    /// <summary>
    /// The main class for collecting data to our channel
    /// putting it here just to keep the IChannel source more clean
    /// </summary>
    public class ChannelWorkers
    {
        private readonly ILogger<ChannelWorkers> logger;
        private readonly ILoggerFactory loggerFactory;
        internal n0tHttpClient httpClient;
        public ChannelWorkers(ILoggerFactory loggerFactory)
        {
            this.loggerFactory = loggerFactory; 
            this.logger = loggerFactory.CreateLogger<ChannelWorkers>();
            httpClient = new n0tHttpClient(loggerFactory);
        }


        /// <summary>
        /// Gets the first page of the channel, this is premade with static variables because i couldnt find a dynamic way to collect this
        /// </summary>
        /// <param name="logger"></param>
        /// <returns></returns>
        internal async Task<ChannelItemResult> GetGenres(ILogger logger)
        {
            ChannelItemResult result = new ChannelItemResult();
            result.Items.Add(new ChannelItemInfo()
            {
                Id = "action",
                Name = "Action",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            result.TotalRecordCount++;
            result.Items.Add(new ChannelItemInfo()
            {
                Id = "anime",
                Name = "Anime",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            result.TotalRecordCount++;
            result.Items.Add(new ChannelItemInfo()
            {
                Id = "classics",
                Name = "Classics",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            result.TotalRecordCount++;

            result.Items.Add(new ChannelItemInfo()
            {
                Id = "comedy",
                Name = "Comedy",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            result.TotalRecordCount++;
            result.Items.Add(new ChannelItemInfo()
            {
                Id = "crime_tv".ToLower(),
                Name = "Crime TV",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            result.TotalRecordCount++;
            result.Items.Add(new ChannelItemInfo()
            {
                Id = "Documentary".ToLower(),
                Name = "Documentary",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            result.TotalRecordCount++;
            result.Items.Add(new ChannelItemInfo()
            {
                Id = "Docuseries".ToLower(),
                Name = "Docuseries",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            result.TotalRecordCount++;
            result.Items.Add(new ChannelItemInfo()
            {
                Id = "Drama".ToLower(),
                Name = "Drama",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            result.TotalRecordCount++;
            result.Items.Add(new ChannelItemInfo()
            {
                Id = "faith_and_spirituality".ToLower(),
                Name = "Faith",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            result.TotalRecordCount++;
            result.Items.Add(new ChannelItemInfo()
            {
                Id = "Family_Movies".ToLower(),
                Name = "Family Movies",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            result.TotalRecordCount++;
            result.Items.Add(new ChannelItemInfo()
            {
                Id = "foreign_films".ToLower(),
                Name = "Foreign Language Films",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            result.TotalRecordCount++;
            result.Items.Add(new ChannelItemInfo()
            {
                Id = "Foreign_Language_TV".ToLower(),
                Name = "Foreign Language TV",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            result.TotalRecordCount++;
            result.Items.Add(new ChannelItemInfo()
            {
                Id = "Horror".ToLower(),
                Name = "Horror",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            result.TotalRecordCount++;
            result.Items.Add(new ChannelItemInfo()
            {
                Id = "Indie_Films".ToLower(),
                Name = "Indie Films",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            result.TotalRecordCount++;
            result.Items.Add(new ChannelItemInfo()
            {
                Id = "Kids_Shows".ToLower(),
                Name = "Kids Shows",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            result.TotalRecordCount++;
            result.Items.Add(new ChannelItemInfo()
            {
                Id = "LGBT".ToLower(),
                Name = "LGBTQ",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            result.TotalRecordCount++;
            result.Items.Add(new ChannelItemInfo()
            {
                Id = "Lifestyle".ToLower(),
                Name = "Lifestyle",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            result.TotalRecordCount++;
            result.Items.Add(new ChannelItemInfo()
            {
                Id = "Martial_Arts".ToLower(),
                Name = "Martial Arts",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            result.TotalRecordCount++;
            result.Items.Add(new ChannelItemInfo()
            {
                Id = "music_musicals".ToLower(),
                Name = "Music & Concerts",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            result.TotalRecordCount++;
            result.Items.Add(new ChannelItemInfo()
            {
                Id = "Preschool".ToLower(),
                Name = "Preschool",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            result.TotalRecordCount++;
            result.Items.Add(new ChannelItemInfo()
            {
                Id = "Reality_TV".ToLower(),
                Name = "Reality TV",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            result.TotalRecordCount++;
            result.Items.Add(new ChannelItemInfo()
            {
                Id = "Romance".ToLower(),
                Name = "Romance",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            result.TotalRecordCount++;
            result.Items.Add(new ChannelItemInfo()
            {
                Id = "Sci_fi_and_Fantasy".ToLower(),
                Name = "SciFi And Fantasy",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            result.TotalRecordCount++;
            result.Items.Add(new ChannelItemInfo()
            {
                Id = "Stand_Up_Comedy".ToLower(),
                Name = "Stand Up Comedy",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            result.TotalRecordCount++;
            result.Items.Add(new ChannelItemInfo()
            {
                Id = "Thrillers".ToLower(),
                Name = "Thrillers",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            result.TotalRecordCount++;
            result.Items.Add(new ChannelItemInfo()
            {
                Id = "TV_Comedies".ToLower(),
                Name = "TV Comedies",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            result.TotalRecordCount++;
            result.Items.Add(new ChannelItemInfo()
            {
                Id = "TV_Dramas".ToLower(),
                Name = "TV Dramas",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            result.TotalRecordCount++;
            result.Items.Add(new ChannelItemInfo()
            {
                Id = "Westerns".ToLower(),
                Name = "Westerns",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            result.TotalRecordCount++;

            return result;
        }



        /// <summary>
        /// This function grabs all the items that belongs to selected genre
        /// </summary>
        /// <param name="GenreId"></param>
        /// <param name="logger"></param>
        /// <param name="memoryCache"></param>
        /// <returns></returns>
        internal async Task<ChannelItemResult> CollectGenreItemsAsync(string GenreId, ILogger logger, IMemoryCache memoryCache,CancellationToken cancelationToken)
        {
            if (memoryCache.TryGetValue("tubitv-categories-" + GenreId, out ChannelItemResult cachedValue))
            {
                logger.LogInformation("Function={function} FolderId={folderId} Cache Hit", nameof(CollectGenreItemsAsync), "tubitv-categories-" + GenreId);
                return cachedValue;
            }
            ChannelItemResult result = new ChannelItemResult();

           
            string json = await httpClient.GetStringAsync("https://tubitv.com/oz/containers/" + GenreId.ToLower().Replace(" ", "_") + "/content?parentId=&limit=100&isKidsModeEnabled=false",cancelationToken);
            dynamic test = JObject.Parse(json);
            List<GenreItems> list = new List<GenreItems>();

            foreach (dynamic jObject in test.contents)
            {
                if (jObject == null)
                {
                    continue;
                }
                try
                {
                    string id = jObject.Value.id;
                    string type = jObject.Value.type;
                    string title = jObject.Value.title;
                    string decription = jObject.Value.description;
                    List<string> Tags = new List<string>();
                    foreach (dynamic jj in jObject.Value.tags)
                        Tags.Add(jj.Value);
                    List<string> Genres = new List<string>();
                    foreach (dynamic jj in jObject.Value.tags)
                        Genres.Add(jj.Value);
                    List<string> Directors = new List<string>();
                    foreach (dynamic director in jObject.Value.directors)
                        Directors.Add(director.Value);
                    List<string> Actors = new List<string>();
                    foreach (dynamic actor in jObject.Value.actors)
                        Actors.Add(actor.Value);
                    List<string> PosterArt = new List<string>();
                    foreach (dynamic art in jObject.Value.posterarts)
                        PosterArt.Add(art.Value);
                    List<string> thumbnails = new List<string>();
                    foreach (dynamic art in jObject.Value.thumbnails)
                        thumbnails.Add(art.Value);
                    string language = jObject.Value.lang;
                    if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(title) || string.IsNullOrEmpty(decription) || string.IsNullOrEmpty(language))
                    {
                        continue;
                    }

                    GenreItems item = new GenreItems(id, type, title, decription, language, Tags, Directors, Actors, PosterArt, thumbnails);
                    list.Add(item);
                }
                catch (Exception e)
                {
                    logger.LogInformation("Error parsin value from dynamic json variable  CollectGenreItemsAsync");
                    continue;
                }
            }

            foreach (GenreItems i in list)
            {
                if (string.Equals(i.type, "v", StringComparison.OrdinalIgnoreCase))
                {
                    result.Items.Add(new ChannelItemInfo()
                    {
                        Id = i.id,
                        Genres = i.tags,
                        Name = i.title ?? "Unknown",
                        ImageUrl = i.Thumbnail.First(x => !string.IsNullOrEmpty(x)) ?? "https://upload.wikimedia.org/wikipedia/en/thumb/d/db/Tubi_logo.svg/1200px-Tubi_logo.svg.png",
                        FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                        MediaType = MediaBrowser.Model.Channels.ChannelMediaType.Video,
                        Type = ChannelItemType.Media,
                        OriginalTitle = i.title ?? "Unknown",
                        Overview = i.description ?? "Unknown",
                    });
                    result.TotalRecordCount++;
                }
                else if (string.Equals(i.type, "s", StringComparison.OrdinalIgnoreCase))
                {
                    result.Items.Add(new ChannelItemInfo()
                    {
                        Id = "series-0" + i.id,
                        Genres = i.tags,
                        Name = i.title ?? "Unknown",
                        SeriesName = i.title,
                        ImageUrl = i.Thumbnail.First(x => !string.IsNullOrEmpty(x)) ?? "https://upload.wikimedia.org/wikipedia/en/thumb/d/db/Tubi_logo.svg/1200px-Tubi_logo.svg.png",
                        FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                        MediaType = MediaBrowser.Model.Channels.ChannelMediaType.Video,
                        Type = ChannelItemType.Folder,
                        OriginalTitle = i.title ?? "Unknown",
                        Overview = i.description ?? "Unknown",
                    });
                    result.TotalRecordCount++;
                }
            }
            memoryCache.Set("tubitv-categories-" + GenreId, result, DateTimeOffset.Now.AddDays(1));
            return result;
        }

    }
}
