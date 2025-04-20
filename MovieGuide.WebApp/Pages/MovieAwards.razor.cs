using Microsoft.AspNetCore.Components;
using MovieGuide.Common.Model.Search;
using MovieGuide.WebApp.Shared;

namespace MovieGuide.WebApp.Pages
{
    public partial class MovieAwards
    {
        [Inject]
        public CacheService CacheService { get; set; }

        public SearchContainer<MovieAward> Awards { get; private set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender || ShouldRefresh)
            {
                ShouldRefresh = false;

                string queryString = BuildQueryString();
                NavigationManager.NavigateTo(baseUri + queryString);

                Awards = await CacheService.GetMovieAwards(Page);
                PageCount = Awards.TotalPages;

                StateHasChanged();
            }
        }

        protected override void OnParametersSet()
        {
            ShouldRefresh = true;
        }

        private string baseUri => $"awards/movie";
    }
}
