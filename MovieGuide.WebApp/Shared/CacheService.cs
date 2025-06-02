using System.Net.Http.Json;
using System.Text.Json;
using MovieGuide.Common.Converters;
using MovieGuide.Common.Model.General;
using MovieGuide.Common.Model.Search;
using MovieGuide.Common.Model.TvShows;

namespace MovieGuide.WebApp.Shared
{
    public class CacheService
    {
        public CacheService(HttpClient httpClient)
        {
            HttpClient = httpClient;

            SerializerOptions = new JsonSerializerOptions();
            SerializerOptions.Converters.Add(new PartialDateConverter());
            SerializerOptions.Converters.Add(new MediaTypeConverter());
        }

        public async Task<IEnumerable<Network>> SearchNetwork(string query)
        {
            if (Networks == null)
                Networks = await HttpClient.GetFromJsonAsync<List<Network>>("cache-data/networks.json");

            if (String.IsNullOrWhiteSpace(query))
                return Networks;

            return Networks?.Where(x => x.Name.Contains(query, StringComparison.InvariantCultureIgnoreCase));
        }

        public async Task<Network> GetNetwork(string networkId)
        {
            if (Networks == null)
                Networks = await HttpClient.GetFromJsonAsync<List<Network>>("cache-data/networks.json");

            return Networks?.FirstOrDefault(x => x.Id.ToString() == networkId);
        }

        public async Task<IEnumerable<ProductionCountry>> SearchCountry(string query)
        {
            if (Countries == null)
                Countries = await HttpClient.GetFromJsonAsync<List<ProductionCountry>>("cache-data/countries.json");

            if (String.IsNullOrWhiteSpace(query))
                return Countries;

            return Countries?.Where(x => x.ToString().Contains(query, StringComparison.InvariantCultureIgnoreCase));
        }

        public async Task<ProductionCountry> GetCountry(string countryCode)
        {
            if (Countries == null)
                Countries = await HttpClient.GetFromJsonAsync<List<ProductionCountry>>("cache-data/countries.json");

            return Countries?.FirstOrDefault(x => x.Iso_3166_1 == countryCode);
        }        

        public async Task<IEnumerable<Language>> SearchLanguage(string query)
        {
            if (Languages == null)
                Languages = await HttpClient.GetFromJsonAsync<List<Language>>("cache-data/languages.json");

            if (String.IsNullOrWhiteSpace(query))
                return Languages;

            return Languages?.Where(x => x.ToString().Contains(query, StringComparison.InvariantCultureIgnoreCase));
        }

        public async Task<Language> GetLanguage(string languageCode)
        {
            if (Languages == null)
                Languages = await HttpClient.GetFromJsonAsync<List<Language>>("cache-data/languages.json");

            return Languages?.FirstOrDefault(x => x.Iso_639_1 == languageCode);
        }

        #region Oscar Awards

        public async Task<SearchContainer<SearchAward>> GetBestPictures(int page)
        {
            return await GetOscars(BestPictures, "cache-data/best-picture.json", page);
        }

        public async Task<SearchContainer<SearchAward>> GetBestAnimations(int page)
        {
            return await GetOscars(BestAnimation, "cache-data/best-animation.json", page);
        }

        public async Task<SearchContainer<SearchAward>> GetBestInternational(int page)
        {
            return await GetOscars(BestInternational, "cache-data/best-international.json", page);
        }

        public async Task<SearchContainer<SearchAward>> GetBestOriginalScreenplays(int page)
        {
            return await GetOscars(BestOriginalScreenplay, "cache-data/best-original-screenplay.json", page);
        }

        public async Task<SearchContainer<SearchAward>> GetBestAdaptedScreenplays(int page)
        {
            return await GetOscars(BestAdaptedScreenplay, "cache-data/best-adapted-screenplay.json", page);
        }

        public async Task<SearchContainer<SearchAward>> GetBestActors(int page)
        {
            return await GetOscars(BestActors, "cache-data/best-actor.json", page);
        }

        public async Task<SearchContainer<SearchAward>> GetBestActresses(int page)
        {
            return await GetOscars(BestActresses, "cache-data/best-actress.json", page);
        }

        public async Task<SearchContainer<SearchAward>> GetBestDirectors(int page)
        {
            return await GetOscars(BestDirectors, "cache-data/best-director.json", page);
        }

        public async Task<SearchContainer<SearchAward>> GetBestSupportingActors(int page)
        {
            return await GetOscars(BestSupportingActors, "cache-data/best-supporting-actor.json", page);
        }

        public async Task<SearchContainer<SearchAward>> GetBestSupportingActresses(int page)
        {
            return await GetOscars(BestSupportingActresses, "cache-data/best-supporting-actress.json", page);
        }

        private async Task<SearchContainer<SearchAward>> GetOscars(List<SearchAward> cacheData, string cacheFile, int page)
        {
            if (cacheData == null)
            {
                HttpResponseMessage response = await HttpClient.GetAsync(cacheFile);
                if (response.IsSuccessStatusCode)
                    cacheData = await response.Content.ReadFromJsonAsync<List<SearchAward>>(SerializerOptions);
            }
            
            int year = cacheData.FirstOrDefault().Year;
            int start = year - (page * 10);
            int end = start + 10;
            
            List<SearchAward> results = cacheData.Where(x => x.Year > start && x.Year <= end).ToList();
            SearchContainer<SearchAward> searchContainer = new SearchContainer<SearchAward>()
            {
                Page = page,
                Results = results,
                TotalResults = cacheData.Count
            };
            searchContainer.TotalPages = (int)Math.Ceiling((decimal)cacheData.Count / 10);
            
            return searchContainer;
        }

        #endregion

        private HttpClient HttpClient;

        private JsonSerializerOptions SerializerOptions;

        private static List<Network> Networks;

        private static List<Language> Languages;

        private static List<ProductionCountry> Countries;

        private static List<SearchAward> BestPictures;

        private static List<SearchAward> BestDirectors;

        private static List<SearchAward> BestActors;

        private static List<SearchAward> BestActresses;

        private static List<SearchAward> BestSupportingActors;

        private static List<SearchAward> BestSupportingActresses;

        private static List<SearchAward> BestOriginalScreenplay;

        private static List<SearchAward> BestAdaptedScreenplay;

        private static List<SearchAward> BestAnimation;

        private static List<SearchAward> BestInternational;
    }
}
