using System.Net.Http.Json;
using MovieGuide.Common.Model.General;
using MovieGuide.Common.Model.TvShows;

namespace MovieGuide.WebApp.Shared
{
    public class CacheService
    {
        public CacheService(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        public async Task<List<Network>> SearchNetwork(string query)
        {
            if (String.IsNullOrWhiteSpace(query) || query.Length < 2)
                return null;

            if (Networks == null)
                Networks = await HttpClient.GetFromJsonAsync<List<Network>>("cache-data/networks.json");

            return Networks?.Where(x => x.Name.Contains(query, StringComparison.InvariantCultureIgnoreCase)).ToList();
        }

        public async Task<Network> GetNetwork(string networkId)
        {
            if (Networks == null)
                Networks = await HttpClient.GetFromJsonAsync<List<Network>>("cache-data/networks.json");

            return Networks?.FirstOrDefault(x => x.Id.ToString() == networkId);
        }

        public async Task<string> GetLanguageName(string languageCode)
        {
            if (Languages == null)
                Languages = await HttpClient.GetFromJsonAsync<List<Language>>("cache-data/languages.json");

            return Languages?.Find(x => x.Iso_639_1 == languageCode)?.ToString();
        }

        private HttpClient HttpClient;

        private static List<Network> Networks;

        private static List<Language> Languages;
    }
}
