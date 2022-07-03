using Microsoft.AspNetCore.Components;
using MovieGuide.Common;
using MovieGuide.Common.Model.TvShows;

namespace MovieGuide.WebApp.Pages
{
    public partial class TvEpisodeDetails
    {
        [Inject]
        public TmdbService TmdbService { get; set; }

        [Parameter]
        public int Id { get; set; }

        [Parameter]
        public int SeasonNumber { get; set; }

        [Parameter]
        public int EpisodeNumber { get; set; }

        public TvShow TvShow { get; set; }

        public TvEpisode Episode { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            if (Id != 0)
            {
                TvShow = await TmdbService.GetTvShowDetails(Id);
                Episode = await TmdbService.GetTvEpisodeDetails(Id, SeasonNumber, EpisodeNumber);
            }
        }
    }
}
