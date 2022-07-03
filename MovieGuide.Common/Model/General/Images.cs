namespace MovieGuide.Common.Model.General
{
    public class Images
    {
        [JsonPropertyName("posters")]
        public List<ImageData> Posters { get; set; }

        [JsonPropertyName("backdrops")]
        public List<ImageData> Backdrops { get; set; }
    }
}
