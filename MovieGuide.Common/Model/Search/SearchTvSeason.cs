using MovieGuide.Common.Model.General;

namespace MovieGuide.Common.Model.Search
{
    public class SearchTvSeason
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("poster_path")]
        public string PosterPath { get; set; }

        [JsonPropertyName("overview")]
        public string Overview { get; set; }        

        [JsonPropertyName("season_number")]
        public int SeasonNumber { get; set; }

        [JsonPropertyName("air_date")]
        public DateTime? AirDate { get; set; }

        [JsonPropertyName("episode_count")]
        public int EpisodeCount { get; set; }

        [JsonIgnore]
        public string PosterFullPath => Constants.GetPosterFullPath(PosterPath, MediaType.TvShow);

        public override string ToString()
        {
            return Name;
        }
    }
}
