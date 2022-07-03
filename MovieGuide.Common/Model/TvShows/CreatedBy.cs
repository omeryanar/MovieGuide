using MovieGuide.Common.Model.General;

namespace MovieGuide.Common.Model.TvShows
{
    public class CreatedBy
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("gender")]
        public Gender Gender { get; set; }

        [JsonPropertyName("profile_path")]
        public string ProfilePath { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
