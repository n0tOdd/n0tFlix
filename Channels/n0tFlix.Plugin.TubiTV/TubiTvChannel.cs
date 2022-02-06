using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediaBrowser.Controller.Channels;
using MediaBrowser.Controller.Providers;
using MediaBrowser.Model.Drawing;
using MediaBrowser.Model.Entities;
using MediaBrowser.Model.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace n0tFlix.Plugin.TubiTV
{
    public class TubiTvChannel : IChannel, IRequiresMediaInfoCallback, ISupportsLatestMedia, ISupportsMediaProbe, IScheduledTask
    {
        #region Just some variables that we already configured in PluginConfiguration.cs and TubiTv.cs

        public string Name => TubiTv.Instance.Name;
        public string Description => TubiTv.Instance.Description;
        public string DataVersion => TubiTv.Instance.Version.ToString();
        public string HomePageUrl => TubiTv.Instance.Configuration.BaseUrl;
        public string Key => TubiTv.Instance.Name;
        public string Category => TubiTv.Instance.Configuration.ChannelCategory;

        #endregion Just some variables that we already configured in PluginConfiguration.cs and TubiTv.cs

        private readonly ILoggerFactory loggerFactory;
        private readonly ILogger<TubiTvChannel> logger;
        private readonly IMemoryCache memoryCache;
        private readonly ChannelWorkers workers;
        public TubiTvChannel(ILoggerFactory loggerFactory, IMemoryCache memoryCache)
        {
            this.loggerFactory = loggerFactory;
            this.logger = this.loggerFactory.CreateLogger<TubiTvChannel>();
            this.memoryCache = memoryCache;
            workers = new ChannelWorkers(loggerFactory);
            this.logger.LogDebug(GetType().Namespace + " initialised and ready for use");
        }

        #region General channel functions i almost never need to touch
        public InternalChannelFeatures GetChannelFeatures()
        {
            return new InternalChannelFeatures()
            {
                MediaTypes = new List<MediaBrowser.Model.Channels.ChannelMediaType>()
                {
                    MediaBrowser.Model.Channels.ChannelMediaType.Video,
                },
                ContentTypes = new List<MediaBrowser.Model.Channels.ChannelMediaContentType>()
                 {
                    MediaBrowser.Model.Channels.ChannelMediaContentType.Clip,
                     MediaBrowser.Model.Channels.ChannelMediaContentType.Episode,
                      MediaBrowser.Model.Channels.ChannelMediaContentType.Movie,
                     MediaBrowser.Model.Channels.ChannelMediaContentType.Trailer
                },
                DefaultSortFields = new List<MediaBrowser.Model.Channels.ChannelItemSortField>()
                 {
                      MediaBrowser.Model.Channels.ChannelItemSortField.Name,
                       MediaBrowser.Model.Channels.ChannelItemSortField.DateCreated,
                        MediaBrowser.Model.Channels.ChannelItemSortField.Runtime,
                     MediaBrowser.Model.Channels.ChannelItemSortField.CommunityPlayCount,
                     MediaBrowser.Model.Channels.ChannelItemSortField.CommunityRating,
                     MediaBrowser.Model.Channels.ChannelItemSortField.PremiereDate,
                      MediaBrowser.Model.Channels.ChannelItemSortField.PlayCount
                 },
                MaxPageSize = 100, //Items per channel page
                SupportsContentDownloading = true,
                SupportsSortOrderToggle = true,
                AutoRefreshLevels = 4, //<== i would love to know whats a good value to put here bro
            };
        }


        /// <summary>
        /// Supported channel images
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ImageType> GetSupportedChannelImages()
            => new List<ImageType>
            {ImageType.Thumb};

        /// <summary>
        /// I tink this is the function that gives us the channel logo at the main page,
        /// but if anyone knows for sure please give me a hint on github
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
                        var path = GetType().Namespace + ".Images.logo.png";

                        return await Task.FromResult(new DynamicImageResponse
                        {
                            Format = ImageFormat.Png,
                            HasImage = true,

                            Stream = GetType().Assembly.GetManifestResourceStream(path)
                        });
                    }
                default:
                    throw new ArgumentException("Unsupported Image type: " + type);
            }
        }

        /// <summary>
        /// Gotta be kid friendly here now
        /// </summary>
        public ChannelParentalRating ParentalRating => ChannelParentalRating.GeneralAudience;

        /// <summary>
        /// Access control if you want to make the channel private
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool IsEnabledFor(string userId)
        {
            return true;
        }

        #endregion

        #region Schedualed tasks for this channel to run

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
            var results = await worker.GetChannelCategoriesAsync(logger, memoryCache);
            //Grabs all the category content so we have that in memorycache too
            foreach (var result in results.Items)
            {
                await worker.GetCategoryItemsAsync(new InternalChannelItemQuery() { FolderId = result.Id }, logger, memoryCache);
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

        #endregion

        public async Task<ChannelItemResult> GetChannelItems(InternalChannelItemQuery query, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(query.FolderId))
                return await workers.GetGenres(logger);
            else if (query.FolderId.StartsWith("https://psapi.nrk.no/tv/pages/"))
                return await worker.GetCategoryItemsAsync(query, logger, memoryCache);
            else if (query.FolderId.StartsWith("https://psapi.nrk.no/tv/catalog/series/") && !query.FolderId.Contains("seasons"))
                return await worker.GetSeasonInfoAsync(query, logger, memoryCache);
            else if (query.FolderId.StartsWith("https://psapi.nrk.no/tv/catalog/series/") && query.FolderId.Contains("seasons"))
                return await worker.GetEpisodeInfoAsync(query, logger, memoryCache);

            logger.LogInformation("This should not happen, we cant find any folderid to use " + query.FolderId);
            return null;
        }

        /// <summary>
        /// This is the function to get a row on the main page with lates videos
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ChannelItemInfo>> GetLatestMedia(ChannelLatestMediaSearch request, CancellationToken cancellationToken)
            => await worker.GetHeadlinersInfoAsync(logger, memoryCache);
    }
}
