namespace MovieGuide.Common.Model.Search
{
    public class SearchAward
    {
        [JsonPropertyName("year")]
        public int Year { get; set; }

        [JsonPropertyName("winners")]
        public List<int> Winners { get; set; }

        [JsonPropertyName("nominees")]
        public List<SearchBase> Nominees { get; set; }
    }
}
