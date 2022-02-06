using MediaBrowser.Controller.Channels;
using MediaBrowser.Controller.Providers;
using MediaBrowser.Model.Drawing;
using MediaBrowser.Model.Dto;
using MediaBrowser.Model.Entities;
using MediaBrowser.Model.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace n0tFlix.Plugin.NRK
{
    public class Channel : IChannel, IRequiresMediaInfoCallback, ISupportsLatestMedia, ISupportsMediaProbe, IScheduledTask
    {
        #region Just some variables that uses the Plugin.cs file variables

        public string Name => Plugin.Instance.Name;
        public string Description => Plugin.Instance.Description;
        public string DataVersion => Plugin.Instance.Version.ToString();
        public string HomePageUrl => Plugin.Instance.HomePageURL;
        public string Key => Plugin.Instance.Name;
        public string Category => Plugin.Instance.Name;

        #endregion Just some variables that uses the Plugin.cs file variables

        #region Access controller function/settings

        /// <summary>
        /// Gotta be kid friendly here now
        /// </summary>
        public ChannelParentalRating ParentalRating => ChannelParentalRating.GeneralAudience;

        /// <summary>
        /// Checks if the user has access to the channel
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool IsEnabledFor(string userId)
        {
            //todo make a user access check system here
            return true;
        }

        #endregion Access controller function/settings

        private readonly ILogger logger;
        private readonly Worker worker;
        private IMemoryCache memoryCache;

        public Channel(ILoggerFactory loggerFactory, IMemoryCache memoryCache)
        {
            this.logger = loggerFactory.CreateLogger<Channel>();
            worker = new Worker(loggerFactory);
            this.memoryCache = memoryCache;
            this.logger.LogError(GetType().Namespace + " initialised and ready for use");
        }

        public InternalChannelFeatures GetChannelFeatures()
        {
            return new InternalChannelFeatures()
            {
                MediaTypes = new List<MediaBrowser.Model.Channels.ChannelMediaType>()
                {
                    MediaBrowser.Model.Channels.ChannelMediaType.Video,
                    MediaBrowser.Model.Channels.ChannelMediaType.Audio,
                    MediaBrowser.Model.Channels.ChannelMediaType.Photo,
                },
                ContentTypes = new List<MediaBrowser.Model.Channels.ChannelMediaContentType>()
                 {
                    MediaBrowser.Model.Channels.ChannelMediaContentType.Clip,
                     MediaBrowser.Model.Channels.ChannelMediaContentType.Episode,
                      MediaBrowser.Model.Channels.ChannelMediaContentType.Movie,
                     MediaBrowser.Model.Channels.ChannelMediaContentType.Clip,
                     MediaBrowser.Model.Channels.ChannelMediaContentType.Podcast,
                     MediaBrowser.Model.Channels.ChannelMediaContentType.Song,
                     MediaBrowser.Model.Channels.ChannelMediaContentType.Trailer
                }, //<=== just adding all to have them, probably only need one but hey we can remove that when we get there
                DefaultSortFields = new List<MediaBrowser.Model.Channels.ChannelItemSortField>()
                 {
                      MediaBrowser.Model.Channels.ChannelItemSortField.Name,
                       MediaBrowser.Model.Channels.ChannelItemSortField.DateCreated,
                        MediaBrowser.Model.Channels.ChannelItemSortField.Runtime,
                     MediaBrowser.Model.Channels.ChannelItemSortField.CommunityPlayCount,
                     MediaBrowser.Model.Channels.ChannelItemSortField.CommunityRating,
                     MediaBrowser.Model.Channels.ChannelItemSortField.PremiereDate,
                      MediaBrowser.Model.Channels.ChannelItemSortField.PlayCount
                 }, //<== again adding all, probably dont need it, but hey the luxuary problems in life =D
                MaxPageSize = 25, //<== this one here is n0t working, i downloaded 1000s of youtube pages at once and it became a big a list without any more pages :(
                SupportsContentDownloading = true,
                SupportsSortOrderToggle = true,
                AutoRefreshLevels = 4, //<== i would love to know whats a good value to put here bro
            };
        }

        public async Task<ChannelItemResult> GetChannelItems(InternalChannelItemQuery query, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(query.FolderId))
                return await worker.GetChannelCategoriesAsync(logger, memoryCache, cancellationToken);
            else if (query.FolderId.StartsWith("https://psapi.nrk.no/tv/pages/"))
                return await worker.GetCategoryItemsAsync(query, logger, memoryCache,cancellationToken);
            else if (query.FolderId.StartsWith("https://psapi.nrk.no/tv/catalog/series/") && !query.FolderId.Contains("seasons"))
                return await worker.GetSeasonInfoAsync(query, logger, memoryCache, cancellationToken);
            else if (query.FolderId.StartsWith("https://psapi.nrk.no/tv/catalog/series/") && query.FolderId.Contains("seasons"))
                return await worker.GetEpisodeInfoAsync(query, logger, memoryCache, cancellationToken);

            logger.LogInformation("This should not happen, we cant find any folderid to use " + query.FolderId);
            return null;
        }

        #region Channel Image configuration

        /// <summary>
        /// 850width and 475 Height are the dimensions that give me the best logo here, else its gonna be all kinds of coco
        /// </summary>
        /// <param name="type"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<DynamicImageResponse> GetChannelImage(ImageType type, CancellationToken cancellationToken)
        {
            switch (type)
            {
                case ImageType.Thumb: //Gives back your logo png from the ManifestResource
                    {
                        var path = GetType().Namespace + ".Images.logo.jpg";

                        return await Task.FromResult(new DynamicImageResponse
                        {
                            Format = ImageFormat.Jpg,
                            HasImage = true,

                            Stream = GetType().Assembly.GetManifestResourceStream(path)
                        });
                    }
                default:
                    throw new ArgumentException("Unsupported Image type: " + type);
            }
        }

        /// <summary>
        /// This beutifull little bastard tells us what kind of pictures to support,
        /// you should probably enable more types, im just lazy
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ImageType> GetSupportedChannelImages()
            => new List<ImageType>
            {
                ImageType.Thumb
    };

        #endregion Channel Image configuration

        public async Task<IEnumerable<MediaSourceInfo>> GetChannelItemMediaInfo(string id, CancellationToken cancellationToken)
            => await this.worker.GetMediaSourceInfo(id, logger, cancellationToken);

        public async Task<IEnumerable<ChannelItemInfo>> GetLatestMedia(ChannelLatestMediaSearch request, CancellationToken cancellationToken)
         => await worker.GetHeadlinersInfoAsync(logger, memoryCache,cancellationToken);

        /// <summary>
        /// This is the execution of our schedueled tasks
        /// this one only grabs the content for the channel on server startup so we have it in our memorycache for faster loading
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="progress"></param>
        /// <returns></returns>
        public async Task Execute(CancellationToken cancellationToken, IProgress<double> progress)
        {
            //Grabs all categories so we have them in the memorycache
            var results = await worker.GetChannelCategoriesAsync(logger, memoryCache,cancellationToken);
            //Grabs all the category content so we have that in memorycache too
            foreach (var result in results.Items)
            {
                await worker.GetCategoryItemsAsync(new InternalChannelItemQuery() { FolderId = result.Id }, logger, memoryCache,cancellationToken);
            }
        }

        /// <summary>
        /// The triggers for when the scheduled tasks should be run, only on startup here
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TaskTriggerInfo> GetDefaultTriggers()
        {
            yield return new TaskTriggerInfo
            {
                Type = TaskTriggerInfo.TriggerStartup
            };
        }
    }
}