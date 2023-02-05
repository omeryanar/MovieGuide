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

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender || ShouldRefresh)
            {
                ShouldRefresh = false;

                if (!String.IsNullOrEmpty(Query))
                {
                    string queryString = GetQueryString();
                    NavigationManager.NavigateTo(baseUri + queryString);

                    Search = await TmdbService.SearchMulti(queryString);
                    PageCount = Search.TotalPages;

                    StateHasChanged();
                }
            }
        }

        protected override void OnParametersSet()
        {
            ShouldRefresh = true;
        }

        private string GetQueryString()
        {
            string uri = String.Empty;
            uri = uri.AddQueryString("query", Query);

            return BuildQueryString(uri);
        }

        private const string baseUri = "search";
    }
}
