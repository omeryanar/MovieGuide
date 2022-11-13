using Microsoft.AspNetCore.Components;
using MovieGuide.Common;
using MovieGuide.Common.Helper;
using MovieGuide.Common.Model.Search;

namespace MovieGuide.WebApp.Pages
{
    public partial class MediaList
    {
        [Inject]
        public TmdbService TmdbService { get; set; }

        [Parameter]
        public int Id { get; set; }

        [Parameter]
        public string Title { get; set; }

        public SearchContainer<SearchBase> Items { get; private set; }

        protected async override Task OnParametersSetAsync()
        {
            Items = await TmdbService.GetList(Id, Page);
            PageCount = Items.TotalPages;
        }

        protected override string GetUriWithQueryString()
        {
            return $"/list/{Id}/{Title}";
        }
    }
}
