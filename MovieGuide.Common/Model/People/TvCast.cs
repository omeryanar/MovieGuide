using MovieGuide.Common.Model.Search;

namespace MovieGuide.Common.Model.People
{
    public class TvCast : SearchTvShow
    {
        [JsonPropertyName("credit_id")]
        public string CreditId { get; set; }

        [JsonPropertyName("character")]
        public string Character { get; set; }

        [JsonPropertyName("episode_count")]
        public int EpisodeCount { get; set; }
    }
}
