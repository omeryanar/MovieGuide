using Microsoft.AspNetCore.Components;
using MovieGuide.Common;
using MovieGuide.Common.Helper;
using MovieGuide.Common.Model.Search;
using MudBlazor;

namespace MovieGuide.WebApp.Pages
{
    public partial class DiscoverMovie
    {
        [Inject]
        public ISnackbar Snackbar { get; set; }

        [Inject]
        public TmdbService TmdbService { get; set; }

        [Parameter]
        [SupplyParameterFromQuery(Name = "sort_by")]
        public string SortBy { get; set; }

        [Parameter]
        [SupplyParameterFromQuery(Name = "with_genres")]
        public int[] WithGenres { get; set; }

        [Parameter]
        [SupplyParameterFromQuery(Name = "with_people")]
        public int[] WithPeople { get; set; }

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

        public SearchContainer<SearchMovie> Movies { get; private set; }

        protected async override Task OnParametersSetAsync()
        {
            if (!String.IsNullOrEmpty(SortBy))
                sortBy = SortHelper.MovieSortBy.FirstOrDefault(x => x.Id == SortBy);

            withGenres = WithGenres?.Select(x => (object)x).ToList();
            withPeople = WithPeople?.Select(x => (object)x).ToList();
            withKeywords = WithKeywords?.Select(x => (object)x).ToList();
            withCompanies = WithCompanies?.Select(x => (object)x).ToList();

            withLanguage = OriginalLanguage;
            voteAverage = VoteAverage;
            voteCount = VoteCount;
            runtime = Runtime;
            year = Year;

            try
            {
                isSearching = true;
                Uri uri = new Uri(NavigationManager.Uri);
                Movies = await TmdbService.DiscoverMovie(uri.Query);
                if (Movies != null)
                    PageCount = Movies.TotalPages;
            }
            finally
            {
                isSearching = false;
                showFilters = CurrentBreakpoint != Breakpoint.Xs;
            }
        }

        protected override string GetUriWithQueryString()
        {
            string uri = "discover/movie";
            uri = uri.AddQueryString("sort_by", sortBy.Id);

            if (withGenres?.Count > 0)
                withGenres.ToList().ForEach(x => uri = uri.AddQueryString("with_genres", x.ToString()));
            if (withPeople?.Count > 0)
                withPeople.ToList().ForEach(x => uri = uri.AddQueryString("with_people", x.ToString()));
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

            return uri;
        }
    }
}
