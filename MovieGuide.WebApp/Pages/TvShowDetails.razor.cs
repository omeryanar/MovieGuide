using Microsoft.AspNetCore.Components;
using MovieGuide.Common;
using MovieGuide.Common.Model.TvShows;
using MovieGuide.WebApp.Shared;

namespace MovieGuide.WebApp.Pages
{
    public partial class TvShowDetails
    {
        [Inject]
        public TmdbService TmdbService { get; set; }

        [Inject]
        public CacheService CacheService { get; set; }

        [Parameter]
        public int Id { get; set; }

        public TvShow TvShow { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            if (Id != 0)
            {
                TvShow = await TmdbService.GetTvShowDetails(Id);
                LanguageName = await CacheService.GetLanguageName(TvShow.OriginalLanguage);
            }
        }

        private string LanguageName;
    }
}
