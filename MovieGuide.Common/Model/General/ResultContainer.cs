namespace MovieGuide.Common.Model.General
{
    public class ResultContainer<T>
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("results")]
        public List<T> Results { get; set; }
    }
}
