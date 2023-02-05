using System;
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

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender || ShouldRefresh)
            {
                ShouldRefresh = false;

                string queryString = BuildQueryString();
                NavigationManager.NavigateTo(baseUri + queryString);

                People = await TmdbService.GetPopularPeople(queryString);
                PageCount = People.TotalPages;

                StateHasChanged();
            }
        }

        private const string baseUri = "person/popular";
    }
}
