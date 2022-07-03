namespace MovieGuide.Common.Model.Translations
{
    public class TranslationMovie
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("tagline")]
        public string Tagline { get; set; }

        [JsonPropertyName("homepage")]
        public string Homepage { get; set; }

        [JsonPropertyName("overview")]
        public string Overview { get; set; }
    }
}
