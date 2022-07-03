using System.Collections.ObjectModel;
using MovieGuide.Common.Model.General;

namespace MovieGuide.Common.Helper
{
    public class SortHelper
    {
        public static ReadOnlyCollection<SortType> MovieSortBy { get; } = new ReadOnlyCollection<SortType>(new[]
        {
            new SortType { Id = "popularity.desc", Name = "PopularityDesc" },
            new SortType { Id = "popularity.asc", Name = "PopularityaAsc" },
            new SortType { Id = "release_date.desc", Name = "ReleaseDateDesc" },
            new SortType { Id = "release_date.asc", Name = "ReleaseDateAsc" },
            new SortType { Id = "revenue.desc", Name = "RevenueDesc" },
            new SortType { Id = "revenue.asc", Name = "RevenueAsc" },
            new SortType { Id = "vote_average.desc", Name = "VoteAverageDesc" },
            new SortType { Id = "vote_average.asc", Name = "VoteAverageAsc" },
            new SortType { Id = "vote_count.desc", Name = "VoteCountDesc" },
            new SortType { Id = "vote_count.asc", Name = "VoteCountAsc" }
        });

        public static ReadOnlyCollection<SortType> TvShowSortBy { get; } = new ReadOnlyCollection<SortType>(new[]
        {
            new SortType { Id = "popularity.desc", Name = "PopularityDesc" },
            new SortType { Id = "popularity.asc", Name = "PopularityaAsc" },
            new SortType { Id = "first_air_date.desc", Name = "FirstAirDateDesc" },
            new SortType { Id = "first_air_date.asc", Name = "FirstAirDateAsc" },
            new SortType { Id = "vote_average.desc", Name = "VoteAverageDesc" },
            new SortType { Id = "vote_average.asc", Name = "VoteAverageAsc" }
        });
    }
}
