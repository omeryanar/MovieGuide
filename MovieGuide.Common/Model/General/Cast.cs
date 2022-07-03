namespace MovieGuide.Common.Model.General
{
    public class Cast
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("character")]
        public string Character { get; set; }

        [JsonPropertyName("order")]
        public int Order { get; set; }

        [JsonPropertyName("profile_path")]
        public string ProfilePath { get; set; }

        [JsonPropertyName("gender")]
        public Gender Gender { get; set; }

        [JsonIgnore]
        public string ProfileFullPath => Constants.GetProfileFullPath(ProfilePath, Gender);
    }
}
