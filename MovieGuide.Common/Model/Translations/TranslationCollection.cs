namespace MovieGuide.Common.Model.Translations
{
    public class TranslationCollection
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("overview")]
        public string Overview { get; set; }
    }
}
