using Microsoft.AspNetCore.Components;
using MovieGuide.Common;
using MovieGuide.Common.Model.Movies;
using MovieGuide.Common.Model.Search;
using MovieGuide.Common.Properties;

namespace MovieGuide.WebApp.Pages
{
    public partial class CollectionDetails
    {
        [Inject]
        public TmdbService TmdbService { get; set; }

        [Parameter]
        public int Id { get; set; }

        public Collection Collection { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            SortFunctions = new List<SortFunction>
            {
                new SortFunction { DisplayText = Resources.ReleaseDateDesc, SortFunc = x => x.ReleaseDate, IsDescending = true },
                new SortFunction { DisplayText = Resources.ReleaseDateAsc, SortFunc = x => x.ReleaseDate },
                new SortFunction { DisplayText = Resources.VoteAverageDesc, SortFunc = x => x.VoteAverage, IsDescending = true },
                new SortFunction { DisplayText = Resources.VoteAverageAsc, SortFunc = x => x.VoteAverage }                
            };

            selectedSortFunction = SortFunctions[0];
        }

        protected override async Task OnParametersSetAsync()
        {
            if (Id != 0)
            {
                Collection = await TmdbService.GetCollectionDetails(Id);
            }
        }

        private List<SortFunction> SortFunctions;

        private SortFunction selectedSortFunction;
    }

    public class SortFunction
    {
        public string DisplayText { get; set; }

        public bool IsDescending { get; set; }

        public Func<SearchMovie, object> SortFunc { get; set; }

        public override string ToString()
        {
            return DisplayText;
        }
    }
}
