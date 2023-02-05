using System.Globalization;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using MovieGuide.Common.Converters;
using MovieGuide.Common.Helper;
using MovieGuide.Common.Model.General;
using MovieGuide.Common.Model.Movies;
using MovieGuide.Common.Model.People;
using MovieGuide.Common.Model.Search;
using MovieGuide.Common.Model.TvShows;

namespace MovieGuide.Common
{
    public class TmdbService
    {
        private HttpClient HttpClient;

        private JsonSerializerOptions SerializerOptions;

        private const string ApiKey = "1915ba01826b36e88f574596aa8a7e54";

        private async Task<T> GetAsync<T>(string endpoint, string query = null, bool version4 = false)
        {
            string requestUri = endpoint.AddQueryString("api_key", ApiKey);

            if (query != null)
            {
                foreach (var keyValuePair in query.ParseQuery())
                    requestUri = requestUri.AddQueryString(keyValuePair.Key, keyValuePair.Value);
            }

            CultureInfo currentCulture = CultureInfo.DefaultThreadCurrentCulture;
            if (currentCulture != null && currentCulture.Name != "en")
            {
                requestUri = requestUri.AddQueryString("language", currentCulture.Name);

                if (requestUri.Contains("images"))
                    requestUri = requestUri.AddQueryString("include_image_language", "en,null");

                if (requestUri.Contains("videos"))
                    requestUri = requestUri.AddQueryString("include_video_language", "en,null");
            }

            string apiVersion = version4 ? "4/" : "3/";
            HttpResponseMessage response = await HttpClient.GetAsync(apiVersion + requestUri);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<T>(SerializerOptions);

            return default(T);
        }

        #region Search

        public async Task<SearchContainer<SearchBase>> SearchMulti(string query)
        {
            if (String.IsNullOrWhiteSpace(query) || query.Length < 1)
                return null;

            return await GetAsync<SearchContainer<SearchBase>>("search/multi", query);
        }

        public async Task<SearchContainer<SearchPerson>> SearchPerson(string query)
        {
            if (String.IsNullOrWhiteSpace(query) || query.Length < 2)
                return null;

            return await GetAsync<SearchContainer<SearchPerson>>("search/person", $"?query={query}");
        }

        public async Task<SearchContainer<Keyword>> SearchKeyword(string query)
        {
            if (String.IsNullOrWhiteSpace(query) || query.Length < 2)
                return null;

            return await GetAsync<SearchContainer<Keyword>>("search/keyword", $"?query={query}");
        }

        public async Task<SearchContainer<ProductionCompany>> SearchCompany(string query)
        {
            if (String.IsNullOrWhiteSpace(query) || query.Length < 2)
                return null;

            return await GetAsync<SearchContainer<ProductionCompany>>("search/company", $"?query={query}");
        }

        #endregion

        #region GetById

        public async Task<SearchPerson> GetPerson(string id)
        {
            return await GetAsync<SearchPerson>($"person/{id}");
        }

        public async Task<Keyword> GetKeyword(string id)
        {
            return await GetAsync<Keyword>($"keyword/{id}");
        }

        public async Task<ProductionCompany> GetCompany(string id)
        {
            return await GetAsync<ProductionCompany>($"company/{id}");
        }

        #endregion

        #region Discover

        public async Task<SearchContainer<SearchMovie>> DiscoverMovie(string query)
        {
            return await GetAsync<SearchContainer<SearchMovie>>("discover/movie", query);
        }

        public async Task<SearchContainer<SearchTvShow>> DiscoverTvShow(string query)
        {
            return await GetAsync<SearchContainer<SearchTvShow>>("discover/tv", query);
        }

        #endregion

        #region Details

        public async Task<Movie> GetMovieDetails(int movieId)
        {
            return await GetAsync<Movie>($"movie/{movieId}", "?append_to_response=credits,images,videos,keywords,translations");
        }

        public async Task<Collection> GetCollectionDetails(int collectionId)
        {
            return await GetAsync<Collection>($"collection/{collectionId}", "?append_to_response=images,translations");
        }

        public async Task<Person> GetPersonDetails(int personId)
        {
            return await GetAsync<Person>($"person/{personId}", "?append_to_response=movie_credits,tv_credits,images,translations,external_ids");
        }

        public async Task<TvShow> GetTvShowDetails(int tvShowId)
        {
            return await GetAsync<TvShow>($"tv/{tvShowId}", "?append_to_response=credits,images,videos,keywords,translations,external_ids");
        }

        public async Task<TvSeason> GetTvSeasonDetails(int tvShowId, int seasonNumber)
        {
            return await GetAsync<TvSeason>($"tv/{tvShowId}/season/{seasonNumber}", "?append_to_response=credits,images,videos,translations");
        }

        public async Task<TvEpisode> GetTvEpisodeDetails(int tvShowId, int seasonNumber, int episodeNumber)
        {
            return await GetAsync<TvEpisode>($"tv/{tvShowId}/season/{seasonNumber}/episode/{episodeNumber}", "?append_to_response=credits,images,videos,translations");
        }

        #endregion

        #region Popular

        public async Task<SearchContainer<SearchMovie>> GetPopularMovies(string query = null)
        {
            return await GetAsync<SearchContainer<SearchMovie>>("movie/popular", query);
        }

        public async Task<SearchContainer<SearchTvShow>> GetPopularTvShows(string query = null)
        {
            return await GetAsync<SearchContainer<SearchTvShow>>("tv/popular", query);
        }

        public async Task<SearchContainer<SearchPerson>> GetPopularPeople(string query = null)
        {
            return await GetAsync<SearchContainer<SearchPerson>>("person/popular", query);
        }

        #endregion

        #region List

        public async Task<SearchList<SearchBase>> GetList(int id, int page)
        {
            return await GetAsync<SearchList<SearchBase>>($"list/{id}", $"?page={page}", true);
        }

        public async Task<List<Language>> GetLanguages()
        {
            return await GetAsync<List<Language>>("configuration/languages");
        }

        #endregion

        public TmdbService(HttpClient httpClient)
        {
            HttpClient = httpClient;

            SerializerOptions = new JsonSerializerOptions();
            SerializerOptions.Converters.Add(new PartialDateConverter());
            SerializerOptions.Converters.Add(new MediaTypeConverter());      
        }
    }
}
