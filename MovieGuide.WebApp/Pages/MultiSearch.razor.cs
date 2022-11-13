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

        [Parameter]
        [SupplyParameterFromQuery(Name = "query")]
        public string Query { get; set; }

        public SearchContainer<SearchBase> Search { get; private set; }

        protected async override Task OnParametersSetAsync()
        {
            if (!String.IsNullOrEmpty(Query))
            {
                Uri uri = new Uri(NavigationManager.Uri);
                Search = await TmdbService.SearchMulti(uri.Query);
                PageCount = Search.TotalPages;
            }
        }

        protected override string GetUriWithQueryString()
        {
            string uri = "search";
            uri = uri.AddQueryString("query", Query);

            return uri;
        }
    }
}
