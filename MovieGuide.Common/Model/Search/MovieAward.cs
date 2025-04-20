namespace MovieGuide.Common.Model.Search
{
    public class MovieAward
    {
        [JsonPropertyName("year")]
        public int Year { get; set; }

        [JsonPropertyName("winner_id")]
        public int WinnerId { get; set; }

        [JsonPropertyName("nominees")]
        public SearchMovie[] Nominees { get; set; }
    }
}
