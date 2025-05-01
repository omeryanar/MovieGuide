using Microsoft.AspNetCore.Components;
using MovieGuide.Common;
using MovieGuide.Common.Model.People;
using MovieGuide.Common.Model.Search;
using MovieGuide.WebApp.Shared;

namespace MovieGuide.WebApp.Pages
{
    public partial class PersonDetails
    {
        [Inject]
        public TmdbService TmdbService { get; set; }

        [Inject]
        public RecentService RecentService { get; set; }

        [Parameter]
        public int Id { get; set; }

        public Person Person { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            if (Id != 0)
            {
                Person = await TmdbService.GetPersonDetails(Id);
                movieSelectedDepartment = Person.KnownForDepartment;
                tvShowSelectedDepartment = Person.KnownForDepartment;

                await RecentService.AddRecentItem(new SearchPerson
                {
                    Id = Person.Id,
                    Name = Person.Name,
                    Gender = Person.Gender,
                    ProfilePath = Person.ProfilePath,
                    KnownForDepartment = Person.KnownForDepartment
                });
            }
        }

        private Func<MovieCrew, bool> movieFilter => x =>
        {
            if (!String.IsNullOrEmpty(movieSelectedDepartment) && x.Department != movieSelectedDepartment)
                return false;

            if (String.IsNullOrWhiteSpace(movieSearchString))
                return true;

            if (x.Title.Contains(movieSearchString, StringComparison.OrdinalIgnoreCase))
                return true;

            if (x.Job.Contains(movieSearchString, StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        };

        private Func<TvCrew, bool> tvShowFilter => x =>
        {
            if (!String.IsNullOrEmpty(tvShowSelectedDepartment) && x.Department != tvShowSelectedDepartment)
                return false;

            if (string.IsNullOrWhiteSpace(tvShowSearchString))
                return true;

            if (x.Name.Contains(tvShowSearchString, StringComparison.OrdinalIgnoreCase))
                return true;

            if (x.Job.Contains(tvShowSearchString, StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        };

        private string movieSearchString;

        private string movieSelectedDepartment;

        private string tvShowSearchString;

        private string tvShowSelectedDepartment;
    }
}
