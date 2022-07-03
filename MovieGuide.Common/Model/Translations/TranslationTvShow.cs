namespace MovieGuide.Common.Model.Translations
{
    public class TranslationTvShow
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("tagline")]
        public string Tagline { get; set; }

        [JsonPropertyName("homepage")]
        public string Homepage { get; set; }

        [JsonPropertyName("overview")]
        public string Overview { get; set; }
    }
}
