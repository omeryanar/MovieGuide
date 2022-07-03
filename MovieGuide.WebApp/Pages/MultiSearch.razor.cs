using Microsoft.AspNetCore.Components;
using MovieGuide.Common;
using MovieGuide.Common.Helper;
using MovieGuide.Common.Model.Search;

namespace MovieGuide.WebApp.Pages
{
    public partial class MultiSearch
    {
        [Inject]
        public TmdbService TmdbService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        [SupplyParameterFromQuery(Name = "query")]
        public string Query { get; set; }

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

                    if (!String.IsNullOrEmpty(Query))
                        Refresh();
                }
            }
        }
        private int page = 1;

        public SearchContainer<SearchBase> Search { get; private set; }

        protected async override Task OnParametersSetAsync()
        {
            Uri uri = new Uri(NavigationManager.Uri);
            Search = await TmdbService.SearchMulti(uri.Query);
            pageCount = Search.TotalPages;
        }

        private void Refresh()
        {
            string uri = GetUriWithQueryString();
            NavigationManager.NavigateTo(uri);
        }

        private string GetUriWithQueryString()
        {
            string endpoint = "search";
            string uri = endpoint.AddQueryString("query", Query);

            if (page > 1)
                uri = uri.AddQueryString("page", page);

            return uri;
        }
    }
}
