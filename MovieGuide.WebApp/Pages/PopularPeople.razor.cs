using Microsoft.AspNetCore.Components;
using MovieGuide.Common;
using MovieGuide.Common.Model.Search;

namespace MovieGuide.WebApp.Pages
{
    public partial class PopularPeople
    {
        [Inject]
        public TmdbService TmdbService { get; set; }

        public SearchContainer<SearchPerson> People { get; private set; }

        protected async override Task OnParametersSetAsync()
        {
            Uri uri = new Uri(NavigationManager.Uri);

            People = await TmdbService.GetPopularPeople(uri.Query);
            PageCount = People.TotalPages;
        }

        protected override string GetUriWithQueryString()
        {
            return "person/popular";
        }
    }
}
