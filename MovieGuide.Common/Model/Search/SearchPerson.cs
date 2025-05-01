using MovieGuide.Common.Model.General;

namespace MovieGuide.Common.Model.Search
{
    public class SearchPerson : SearchBase
    {
        public SearchPerson()
        {
            MediaType = MediaType.Person;
        }

        [JsonPropertyName("known_for")]
        public List<SearchBase> KnownFor { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("gender")]
        public Gender Gender { get; set; }

        [JsonPropertyName("profile_path")]
        public string ProfilePath { get; set; }

        [JsonPropertyName("known_for_department")]
        public string KnownForDepartment { get; set; }

        [JsonIgnore]
        public string ProfileFullPath => Constants.GetProfileFullPath(ProfilePath, Gender);

        public override string ToString()
        {
            return Name;
        }
    }
}
