using MovieGuide.Common.Model.General;

namespace MovieGuide.Common.Model.TvShows
{
    public class Network
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("origin_country")]
        public string OriginCountry { get; set; }

        [JsonPropertyName("logo_path")]
        public string LogoPath { get; set; }

        [JsonIgnore]
        public string LogoFullPath => String.Format(Constants.W185, LogoPath);

        public override string ToString()
        {
            return Name;
        }
    }
}
