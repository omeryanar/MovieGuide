using System.Collections.ObjectModel;
using MovieGuide.Common.Model.General;

namespace MovieGuide.Common.Helper
{
    public class GenreHelper
    {
        public static ReadOnlyCollection<Genre> MovieGenres { get; } = new ReadOnlyCollection<Genre>(new[]
        {
            new Genre { Id = 28, Name = "Action" },
            new Genre { Id = 12, Name = "Adventure" },
            new Genre { Id = 16, Name = "Animation" },
            new Genre { Id = 35, Name = "Comedy" },
            new Genre { Id = 80, Name = "Crime" },
            new Genre { Id = 99, Name = "Documentary" },
            new Genre { Id = 18, Name = "Drama" },
            new Genre { Id = 10751, Name = "Family" },
            new Genre { Id = 14, Name = "Fantasy" },
            new Genre { Id = 36, Name = "History" },
            new Genre { Id = 27, Name = "Horror" },
            new Genre { Id = 10402, Name = "Music" },
            new Genre { Id = 9648, Name = "Mystery" },
            new Genre { Id = 10749, Name = "Romance" },
            new Genre { Id = 878, Name = "Science_Fiction" },
            new Genre { Id = 10770, Name = "TV_Movie" },
            new Genre { Id = 53, Name = "Thriller" },
            new Genre { Id = 10752, Name = "War" },
            new Genre { Id = 37, Name = "Western" }
        });

        public static ReadOnlyCollection<Genre> TvShowGenres { get; } = new ReadOnlyCollection<Genre>(new[]
        {
            new Genre { Id = 10759, Name = "Action_Adventure" },
            new Genre { Id = 16, Name = "Animation" },
            new Genre { Id = 35, Name = "Comedy" },
            new Genre { Id = 80, Name = "Crime" },
            new Genre { Id = 99, Name = "Documentary" },
            new Genre { Id = 18, Name = "Drama" },
            new Genre { Id = 10751, Name = "Family" },
            new Genre { Id = 10762, Name = "Kids" },
            new Genre { Id = 9648, Name = "Mystery" },
            new Genre { Id = 10763, Name = "News" },
            new Genre { Id = 10764, Name = "Reality" },
            new Genre { Id = 10765, Name = "Sci_Fi_Fantasy" },
            new Genre { Id = 10766, Name = "Soap" },
            new Genre { Id = 10767, Name = "Talk" },
            new Genre { Id = 10768, Name = "War_Politics" },
            new Genre { Id = 37, Name = "Western" }
        });
    }
}
