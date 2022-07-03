namespace MovieGuide.Common.Model.General
{
    public class Constants
    {
        public const string ListSeparator = " & ";

        public const string W185 = "http://image.tmdb.org/t/p/w185{0}";

        public const string W300 = "http://image.tmdb.org/t/p/w300{0}";

        public const string Original = "http://image.tmdb.org/t/p/original{0}";

        public const string YouTubeImage = "http://img.youtube.com/vi/{0}/0.jpg";

        public const string YoutubeVideo = "https://www.youtube.com/embed/{0}?autoplay=1";

        public const string ImdbTitle = "https://www.imdb.com/title/";

        public const string ImdbName = "https://www.imdb.com/name/";

        public const string FaceBook = "https://www.facebook.com/";

        public const string Twitter = "https://www.twitter.com/";

        public const string Instagram = "https://www.instagram.com/";

        public static readonly string[] FeaturedJobs = { "Creator", "Director", "Novel", "Characters", "Screenplay", "Screenstory", "Story", "Writer" };

        public static string GetStillFullPath(string stillPath, string size = W300)
        {
            if (String.IsNullOrEmpty(stillPath))
                return "images/tvshow.svg";

            return String.Format(size, stillPath);
        }

        public static string GetPosterFullPath(string posterPath, MediaType mediaType = MediaType.Movie, string size = W185)
        {
            if (String.IsNullOrEmpty(posterPath))
                return mediaType == MediaType.Movie ? "images/movie.svg" : "images/tvshow.svg";

            return String.Format(size, posterPath);
        }

        public static string GetProfileFullPath(string profilePath, Gender gender = Gender.Male, string size = W185)
        {
            if (String.IsNullOrEmpty(profilePath))
                return gender == Gender.Female ? "images/female.svg" : "images/male.svg";

            return String.Format(size, profilePath);
        }
    }
}
