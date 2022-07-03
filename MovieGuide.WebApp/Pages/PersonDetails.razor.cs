using Microsoft.AspNetCore.Components;
using MovieGuide.Common;
using MovieGuide.Common.Model.People;

namespace MovieGuide.WebApp.Pages
{
    public partial class PersonDetails
    {
        [Inject]
        public TmdbService TmdbService { get; set; }

        [Parameter]
        public int Id { get; set; }

        public Person Person { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            if (Id != 0)
            {
                Person = await TmdbService.GetPersonDetails(Id);
            }
        }

        private Func<MovieCrew, bool> movieFilter => x =>
        {
            if (string.IsNullOrWhiteSpace(movieSearchString))
                return true;

            if (x.Title.Contains(movieSearchString, StringComparison.OrdinalIgnoreCase))
                return true;

            if (x.Job.Contains(movieSearchString, StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        };

        private Func<TvCrew, bool> tvShowFilter => x =>
        {
            if (string.IsNullOrWhiteSpace(tvShowSearchString))
                return true;

            if (x.Name.Contains(tvShowSearchString, StringComparison.OrdinalIgnoreCase))
                return true;

            if (x.Job.Contains(tvShowSearchString, StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        };

        private string movieSearchString;

        private string tvShowSearchString;
    }
}
