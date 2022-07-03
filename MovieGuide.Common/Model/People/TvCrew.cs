using MovieGuide.Common.Model.Search;

namespace MovieGuide.Common.Model.People
{
    public class TvCrew : SearchTvShow
    {
        [JsonPropertyName("credit_id")]
        public string CreditId { get; set; }

        [JsonPropertyName("department")]
        public string Department { get; set; }

        [JsonPropertyName("job")]
        public string Job { get; set; }

        [JsonPropertyName("episode_count")]
        public int EpisodeCount { get; set; }
    }
}
