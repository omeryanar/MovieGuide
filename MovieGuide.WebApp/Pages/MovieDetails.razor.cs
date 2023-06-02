using Microsoft.AspNetCore.Components;
using MovieGuide.Common;
using MovieGuide.Common.Model.General;
using MovieGuide.Common.Model.Movies;
using MovieGuide.Common.Model.Search;
using MovieGuide.WebApp.Shared;

namespace MovieGuide.WebApp.Pages
{
    public partial class MovieDetails
    {
        [Inject]
        public TmdbService TmdbService { get; set; }

        [Inject]
        public CacheService CacheService { get; set; }

        [Inject]
        public RecentService RecentService { get; set; }

        [Parameter]
        public int Id { get; set; }

        public Movie Movie { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            if (Id != 0)
            {
                Movie = await TmdbService.GetMovieDetails(Id);                

                Language language = await CacheService.GetLanguage(Movie.OriginalLanguage);
                if (language != null)
                    LanguageName = language.ToString();

                await RecentService.AddRecentItem(new SearchMovie
                {
                    Id = Movie.Id,
                    Title = Movie.Title,
                    PosterPath = Movie.PosterPath,
                    ReleaseDate = Movie.ReleaseDate,
                    VoteAverage = Movie.VoteAverage
                });
            }
        }

        private string LanguageName;
    }
}
