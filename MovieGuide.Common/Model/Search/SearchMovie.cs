using MovieGuide.Common.Model.General;

namespace MovieGuide.Common.Model.Search
{
    public class SearchMovie : SearchMovieTvBase
    {
        public SearchMovie()
        {
            MediaType = MediaType.Movie;
        }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("original_title")]
        public string OriginalTitle { get; set; }

        [JsonPropertyName("release_date")]
        public DateTime? ReleaseDate { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }
}
