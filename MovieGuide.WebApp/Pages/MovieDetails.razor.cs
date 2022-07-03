using Microsoft.AspNetCore.Components;
using MovieGuide.Common;
using MovieGuide.Common.Model.Movies;
using MovieGuide.WebApp.Shared;

namespace MovieGuide.WebApp.Pages
{
    public partial class MovieDetails
    {
        [Inject]
        public TmdbService TmdbService { get; set; }

        [Inject]
        public CacheService CacheService { get; set; }

        [Parameter]
        public int Id { get; set; }

        public Movie Movie { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            if (Id != 0)
            {
                Movie = await TmdbService.GetMovieDetails(Id);
                LanguageName = await CacheService.GetLanguageName(Movie.OriginalLanguage);
            }
        }

        private string LanguageName;
    }
}
