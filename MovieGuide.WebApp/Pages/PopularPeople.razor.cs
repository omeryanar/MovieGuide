using Microsoft.AspNetCore.Components;
using MovieGuide.Common;
using MovieGuide.Common.Helper;
using MovieGuide.Common.Model.Search;

namespace MovieGuide.WebApp.Pages
{
    public partial class PopularPeople
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public TmdbService TmdbService { get; set; }

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

        public SearchContainer<SearchPerson> People { get; private set; }

        protected async override Task OnParametersSetAsync()
        {
            Uri uri = new Uri(NavigationManager.Uri);

            People = await TmdbService.GetPopularPeople(uri.Query);
            pageCount = People.TotalPages;
        }

        private void Refresh()
        {
            string uri = "person/popular";
            if (page > 1)
                uri = uri.AddQueryString("page", page);
            
            NavigationManager.NavigateTo(uri);
        }
    }
}
