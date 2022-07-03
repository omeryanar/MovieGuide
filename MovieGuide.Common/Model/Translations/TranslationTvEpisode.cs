namespace MovieGuide.Common.Model.Translations
{
    public class TranslationTvEpisode
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("overview")]
        public string Overview { get; set; }
    }
}
