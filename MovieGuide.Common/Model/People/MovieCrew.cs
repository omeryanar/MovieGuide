using MovieGuide.Common.Model.Search;

namespace MovieGuide.Common.Model.People
{
    public class MovieCrew : SearchMovie
    {
        [JsonPropertyName("credit_id")]
        public string CreditId { get; set; }

        [JsonPropertyName("department")]
        public string Department { get; set; }

        [JsonPropertyName("job")]
        public string Job { get; set; }
    }
}
