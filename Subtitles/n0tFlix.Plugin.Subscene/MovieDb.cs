using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using n0tFlix.Subtitles.Subscene.Models;
using MediaBrowser.Common;
using MediaBrowser.Common.Net;
using MediaBrowser.Model.Serialization;
using System.Net.Http;

namespace n0tFlix.Subtitles.Subscene
{
    public class MovieDb
    {
        private const string token = "d9d7bb04fb2c52c2b594c5e30065c23c";// Get https://www.themoviedb.org/ API token
        public readonly string _movieUrl = "https://api.themoviedb.org/3/movie/{0}?api_key={1}";
        private readonly string _tvUrl = "https://api.themoviedb.org/3/tv/{0}?api_key={1}";
        private readonly string _searchMovie = "https://api.themoviedb.org/3/find/{0}?api_key={1}&external_source={2}";

        private readonly IJsonSerializer _jsonSerializer;
        private readonly HttpClient _httpClient;
        private readonly IApplicationHost _appHost;
        public MovieDb(IJsonSerializer jsonSerializer, HttpClient httpClient, IApplicationHost appHost)
        {
            _jsonSerializer = jsonSerializer;
            _httpClient = httpClient;
            _appHost = appHost;
        }

        public async Task<MovieInformation> GetMovieInfo(string id)
        {

            /*var searchResults = await Tools.RequestUrl<MovieInformation>(opts.Url, "", HttpMethod.Get);
            return searchResults;*/
            string uri = string.Format(_movieUrl, id, token);
            using (var response = await _httpClient.GetAsync(uri).ConfigureAwait(false))
            {
                var res = await response.Content.ReadAsStringAsync();
                if (res.Length < 0)
                    return null;

                var searchResults = _jsonSerializer.DeserializeFromString<MovieInformation>(res);

                return searchResults;
            }
        }

        public async Task<FindMovie> SearchMovie(string id)
        {
           var type = id.StartsWith("tt") ? MovieSourceType.imdb_id : MovieSourceType.tvdb_id;
            string Url = string.Format(_searchMovie, id, token, type.ToString());

            using (var response = await _httpClient.GetAsync(Url).ConfigureAwait(false))
            {
                var res = await response.Content.ReadAsStringAsync();
                if (res.Length < 0)
                    return null;

                var searchResults = _jsonSerializer.DeserializeFromString<FindMovie>(res);

                return searchResults;
            }
        }

        public async Task<TvInformation> GetTvInfo(string id)
        {
            var movie = await SearchMovie(id);

            if (movie?.tv_episode_results == null || !movie.tv_episode_results.Any())
                return null;

            string Url = string.Format(_tvUrl, movie.tv_episode_results.First().show_id, token);

            using (var response = await _httpClient.GetAsync(Url).ConfigureAwait(false))
            {
                var res = await response.Content.ReadAsStringAsync();
                if (res.Length < 0)
                    return null;

                var searchResults = _jsonSerializer.DeserializeFromString<TvInformation>(res);

                return searchResults;
            }
        }


    }
}
