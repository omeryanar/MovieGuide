namespace MovieGuide.Common.Model.General
{
    public class Video
    {
        [JsonPropertyName("key")]
        public string Key { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("site")]
        public string Site { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}
