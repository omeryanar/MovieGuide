using Microsoft.AspNetCore.Components;
using MovieGuide.Common;
using MovieGuide.Common.Model.Search;

namespace MovieGuide.WebApp.Pages
{
    public partial class MediaList
    {
        [Inject]
        public TmdbService TmdbService { get; set; }

        [Parameter]
        public int Id { get; set; }

        public string Title { get; private set; }

        public SearchList<SearchBase> Items { get; private set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender || ShouldRefresh)
            {
                ShouldRefresh = false;

                string queryString = BuildQueryString();
                NavigationManager.NavigateTo(baseUri + queryString);

                Items = await TmdbService.GetList(Id, Page);
                Title = Items.Name;
                PageCount = Items.TotalPages;

                StateHasChanged();
            }
        }

        protected override void OnParametersSet()
        {
            ShouldRefresh = true;
        }

        private string baseUri => $"/list/{Id}";
    }
}
