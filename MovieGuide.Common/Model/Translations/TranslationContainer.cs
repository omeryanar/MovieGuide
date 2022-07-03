namespace MovieGuide.Common.Model.Translations
{
    public class TranslationContainer<T>
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("translations")]
        public List<Translation<T>> Translations { get; set; }
    }
}
