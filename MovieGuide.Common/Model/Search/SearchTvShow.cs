using MovieGuide.Common.Model.General;

namespace MovieGuide.Common.Model.Search
{
    public class SearchTvShow : SearchMovieTvBase
    {
        public SearchTvShow()
        {
            MediaType = MediaType.TvShow;
        }        

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("original_name")]
        public string OriginalName { get; set; }

        [JsonPropertyName("first_air_date")]
        public DateTime? FirstAirDate { get; set; }

        [JsonPropertyName("origin_country")]
        public List<string> OriginCountry { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
