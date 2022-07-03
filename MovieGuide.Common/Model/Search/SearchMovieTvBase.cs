using MovieGuide.Common.Model.General;

namespace MovieGuide.Common.Model.Search
{
    public class SearchMovieTvBase : SearchBase
    {
        [JsonPropertyName("poster_path")]
        public string PosterPath { get; set; }

        [JsonPropertyName("backdrop_path")]
        public string BackdropPath { get; set; }

        [JsonPropertyName("genre_ids")]
        public List<int> GenreIds { get; set; }

        [JsonPropertyName("original_language")]
        public string OriginalLanguage { get; set; }

        [JsonPropertyName("overview")]
        public string Overview { get; set; }        

        [JsonPropertyName("vote_average")]
        public double VoteAverage { get; set; }

        [JsonPropertyName("vote_count")]
        public int VoteCount { get; set; }

        [JsonIgnore]
        public string PosterFullPath => Constants.GetPosterFullPath(PosterPath, MediaType);
    }
}
