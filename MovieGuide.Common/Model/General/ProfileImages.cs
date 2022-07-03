namespace MovieGuide.Common.Model.General
{
    public class ProfileImages
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("profiles")]
        public List<ImageData> Profiles { get; set; }
    }
}
