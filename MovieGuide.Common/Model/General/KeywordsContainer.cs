namespace MovieGuide.Common.Model.General
{
    public class KeywordsContainer
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("keywords")]
        public List<Keyword> Keywords { get; set; }
    }
}
