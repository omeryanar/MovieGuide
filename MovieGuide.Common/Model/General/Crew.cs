namespace MovieGuide.Common.Model.General
{
    public class Crew
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("department")]
        public string Department { get; set; }

        [JsonPropertyName("job")]
        public string Job { get; set; }

        [JsonPropertyName("profile_path")]
        public string ProfilePath { get; set; }

        [JsonPropertyName("gender")]
        public Gender Gender { get; set; }

        [JsonIgnore]
        public string ProfileFullPath => Constants.GetProfileFullPath(ProfilePath, Gender);
    }
}
