using Microsoft.AspNetCore.Components;
using MovieGuide.Common;
using MovieGuide.Common.Helper;
using MovieGuide.Common.Model.Search;

namespace MovieGuide.WebApp.Pages
{
    public partial class MediaList
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public TmdbService TmdbService { get; set; }

        [Parameter]
        public int Id { get; set; }

        [Parameter]
        public string Title { get; set; }        

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

        public SearchContainer<SearchBase> Items { get; private set; }

        protected async override Task OnParametersSetAsync()
        {
            Items = await TmdbService.GetList(Id, Page);
            pageCount = Items.TotalPages;
        }

        private void Refresh()
        {
            string uri = $"/list/{Id}/{Title}";
            if (page > 1)
                uri = uri.AddQueryString("page", page);

            NavigationManager.NavigateTo(uri);
        }
    }
}
