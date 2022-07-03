namespace MovieGuide.Common.Model.General
{
    public class PosterImages
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("posters")]
        public List<ImageData> Posters { get; set; }
    }
}
