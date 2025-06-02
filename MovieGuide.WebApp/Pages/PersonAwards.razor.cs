using Microsoft.AspNetCore.Components;
using MovieGuide.Common.Helper;
using MovieGuide.Common.Model.Search;
using MovieGuide.WebApp.Shared;

namespace MovieGuide.WebApp.Pages
{
    public partial class PersonAwards
    {
        [Inject]
        public CacheService CacheService { get; set; }

        [Parameter]
        [SupplyParameterFromQuery(Name = "index")]
        public int Index { get; set; }

        public SearchContainer<SearchAward> Awards { get; private set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender || ShouldRefresh)
            {
                ShouldRefresh = false;

                string uri = String.Empty;
                if (Index > 0)
                    uri = uri.AddQueryString("index", Index);

                uri = BuildQueryString(uri);
                NavigationManager.NavigateTo(baseUri + uri);

                switch (Index)
                {
                    case 0:
                        Awards = await CacheService.GetBestActors(Page);
                        break;
                    case 1:
                        Awards = await CacheService.GetBestActresses(Page);
                        break;
                    case 2:
                        Awards = await CacheService.GetBestSupportingActors(Page);
                        break;
                    case 3:
                        Awards = await CacheService.GetBestSupportingActresses(Page);
                        break;
                    case 4:
                        Awards = await CacheService.GetBestDirectors(Page);
                        break;
                }                
                PageCount = Awards.TotalPages;

                StateHasChanged();
            }
        }

        protected override void OnParametersSet()
        {
            ShouldRefresh = true;
        }

        private string baseUri => $"awards/person";
    }
}
