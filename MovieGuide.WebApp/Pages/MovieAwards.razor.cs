﻿using Microsoft.AspNetCore.Components;
using MovieGuide.Common.Helper;
using MovieGuide.Common.Model.Search;
using MovieGuide.WebApp.Shared;

namespace MovieGuide.WebApp.Pages
{
    public partial class MovieAwards
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
                        Awards = await CacheService.GetBestPictures(Page);
                        break;
                    case 1:
                        Awards = await CacheService.GetBestOriginalScreenplays(Page);
                        break;
                    case 2:
                        Awards = await CacheService.GetBestAdaptedScreenplays(Page);
                        break;
                    case 3:
                        Awards = await CacheService.GetBestAnimations(Page);
                        break;
                    case 4:
                        Awards = await CacheService.GetBestInternational(Page);
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

        private string baseUri => $"awards/movie";
    }
}
