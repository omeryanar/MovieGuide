namespace MovieGuide.Common.Model.General
{
    public class StillImages
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("stills")]
        public List<ImageData> Stills { get; set; }
    }
}
