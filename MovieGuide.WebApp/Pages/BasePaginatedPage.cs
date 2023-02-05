using System;
using Microsoft.AspNetCore.Components;
using MovieGuide.Common.Helper;

namespace MovieGuide.WebApp.Pages
{
    public class BasePaginatedPage : ComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

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

                    if (pageCount < newValue)
                        pageCount = newValue;

                    Refresh();
                }
            }
        }
        private int page = 1;

        public int PageCount
        {
            get => pageCount;
            set => pageCount = value < 500 ? value : 500;
        }
        private int pageCount = 1;

        protected string BuildQueryString(string uri = "")
        {
            if (page > 1)
                uri = uri.AddQueryString("page", page);

            return uri;
        }

        protected virtual void Refresh()
        {
            ShouldRefresh = true;            
        }

        protected bool ShouldRefresh;
    }
}
