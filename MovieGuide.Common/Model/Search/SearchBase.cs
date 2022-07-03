using MovieGuide.Common.Converters;
using MovieGuide.Common.Model.General;

namespace MovieGuide.Common.Model.Search
{
    public class SearchBase
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("media_type")]
        [JsonConverter(typeof(JsonEnumConverter<MediaType>))]
        public MediaType MediaType { get; set; }

        [JsonPropertyName("popularity")]
        public double Popularity { get; set; }
    }
}
