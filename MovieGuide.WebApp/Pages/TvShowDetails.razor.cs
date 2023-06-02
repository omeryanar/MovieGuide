using Microsoft.AspNetCore.Components;
using MovieGuide.Common;
using MovieGuide.Common.Model.General;
using MovieGuide.Common.Model.Search;
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

        [Inject]
        public RecentService RecentService { get; set; }

        [Parameter]
        public int Id { get; set; }

        public TvShow TvShow { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            if (Id != 0)
            {
                TvShow = await TmdbService.GetTvShowDetails(Id);

                Language language = await CacheService.GetLanguage(TvShow.OriginalLanguage);
                if (language != null)
                    LanguageName = language.ToString();

                await RecentService.AddRecentItem(new SearchTvShow
                {
                    Id = TvShow.Id,
                    Name = TvShow.Name,
                    PosterPath = TvShow.PosterPath,
                    FirstAirDate = TvShow.FirstAirDate,
                    VoteAverage = TvShow.VoteAverage
                });
            }
        }

        private string LanguageName;
    }
}
