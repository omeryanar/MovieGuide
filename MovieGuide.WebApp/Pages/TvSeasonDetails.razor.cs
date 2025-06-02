using Microsoft.AspNetCore.Components;
using MovieGuide.Common;
using MovieGuide.Common.Model.TvShows;

namespace MovieGuide.WebApp.Pages
{
    public partial class TvSeasonDetails
    {
        [Inject]
        public TmdbService TmdbService { get; set; }

        [Parameter]
        public int Id { get; set; }

        [Parameter]
        public int SeasonNumber { get; set; }

        public TvShow TvShow { get; set; }

        public TvSeason Season { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            if (Id != 0)
            {
                TvShow = await TmdbService.GetTvShowDetails(Id);
                Season = await TmdbService.GetTvSeasonDetails(Id, SeasonNumber);
            }
        }

        private int LastSeasonNumber => TvShow.Seasons.Last().SeasonNumber; 

        private bool IsFirstSeason => SeasonNumber == 1;

        private bool IsLastSeason => SeasonNumber == LastSeasonNumber;
    }
}
