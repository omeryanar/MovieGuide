using Microsoft.AspNetCore.Components;
using MovieGuide.Common;
using MovieGuide.Common.Helper;
using MovieGuide.Common.Model.Search;
using MudBlazor;

namespace MovieGuide.WebApp.Pages
{
    public partial class DiscoverTvShow
    {
        [Inject]
        public ISnackbar Snackbar { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public TmdbService TmdbService { get; set; }

        [Parameter]
        [SupplyParameterFromQuery(Name = "sort_by")]
        public string SortBy { get; set; }

        [Parameter]
        [SupplyParameterFromQuery(Name = "with_genres")]
        public int[] WithGenres { get; set; }

        [Parameter]
        [SupplyParameterFromQuery(Name = "with_networks")]
        public int[] WithNetworks { get; set; }

        [Parameter]
        [SupplyParameterFromQuery(Name = "with_keywords")]
        public int[] WithKeywords { get; set; }

        [Parameter]
        [SupplyParameterFromQuery(Name = "with_companies")]
        public int[] WithCompanies { get; set; }

        [Parameter]
        [SupplyParameterFromQuery(Name = "vote_average.gte")]
        public int VoteAverage { get; set; }

        [Parameter]
        [SupplyParameterFromQuery(Name = "vote_count.gte")]
        public int VoteCount { get; set; }

        [Parameter]
        [SupplyParameterFromQuery(Name = "with_runtime.gte")]
        public int Runtime { get; set; }

        [Parameter]
        [SupplyParameterFromQuery(Name = "with_original_language")]
        public string OriginalLanguage { get; set; }

        [Parameter]
        [SupplyParameterFromQuery(Name = "year")]
        public int? Year { get; set; }

        [Parameter]
        [SupplyParameterFromQuery(Name = "page")]
        public int Page
        {
            get => page;
            set
            {
                int newValue = value > 0 ? value : 1;
                if (page != newValue)
                {
                    page = newValue;
                    pageCount = newValue;
                    Refresh();
                }
            }
        }
        private int page = 1;

        public SearchContainer<SearchTvShow> TvShows { get; private set; }

        protected async override Task OnParametersSetAsync()
        {
            if (!String.IsNullOrEmpty(SortBy))
                sortBy = SortHelper.TvShowSortBy.FirstOrDefault(x => x.Id == SortBy);

            withGenres = WithGenres?.Select(x => (object)x).ToList();
            withNetworks = WithNetworks?.Select(x => (object)x).ToList();
            withKeywords = WithKeywords?.Select(x => (object)x).ToList();
            withCompanies = WithCompanies?.Select(x => (object)x).ToList();

            withLanguage = OriginalLanguage;
            voteAverage = VoteAverage;
            voteCount = VoteCount;
            runtime = Runtime;
            year = Year;

            Uri uri = new Uri(NavigationManager.Uri);
            isFilterActive = uri.Query.ParseQuery().Any(x => x.Key != "sort_by" && x.Key != "page");

            TvShows = await TmdbService.DiscoverTvShow(uri.Query);
            pageCount = TvShows.TotalPages;
        }

        private void Refresh()
        {
            string uri = GetUriWithQueryString();
            NavigationManager.NavigateTo(uri);
        }

        private string GetUriWithQueryString()
        {
            string endpoint = "discover/tv";
            string uri = endpoint.AddQueryString("sort_by", sortBy.Id);

            if (withGenres?.Count > 0)
                withGenres.ToList().ForEach(x => uri = uri.AddQueryString("with_genres", x.ToString()));
            if (withNetworks?.Count > 0)
                withNetworks.ToList().ForEach(x => uri = uri.AddQueryString("with_networks", x.ToString()));
            if (withKeywords?.Count > 0)
                withKeywords.ToList().ForEach(x => uri = uri.AddQueryString("with_keywords", x.ToString()));
            if (withCompanies?.Count > 0)
                withCompanies.ToList().ForEach(x => uri = uri.AddQueryString("with_companies", x.ToString()));

            if (withLanguage != null)
                uri = uri.AddQueryString("with_original_language", withLanguage);
            if (voteAverage != 0)
                uri = uri.AddQueryString("vote_average.gte", voteAverage);
            if (voteCount != 0)
                uri = uri.AddQueryString("vote_count.gte", voteCount);
            if (runtime != 0)
                uri = uri.AddQueryString("with_runtime.gte", runtime);
            if (year != null)
                uri = uri.AddQueryString("year", year);
            if (page > 1)
                uri = uri.AddQueryString("page", page);

            return uri;
        }
    }
}
