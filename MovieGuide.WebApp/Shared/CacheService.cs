﻿using System.Net.Http.Json;
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

        private HttpClient HttpClient;

        private static List<Network> Networks;

        private static List<Language> Languages;

        private static List<ProductionCountry> Countries;
    }
}
