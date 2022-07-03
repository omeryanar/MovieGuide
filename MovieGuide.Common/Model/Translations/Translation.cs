namespace MovieGuide.Common.Model.Translations
{
    public class Translation<T>
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("english_name")]
        public string EnglishName { get; set; }

        [JsonPropertyName("iso_639_1")]
        public string Iso_639_1 { get; set; }

        [JsonPropertyName("iso_3166_1")]
        public string Iso_3166_1 { get; set; }

        [JsonPropertyName("data")]
        public T Data { get; set; }
    }
}
