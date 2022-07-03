using MovieGuide.Common.Model.Search;

namespace MovieGuide.Common.Model.People
{
    public class MovieCast : SearchMovie
    {
        [JsonPropertyName("credit_id")]
        public string CreditId { get; set; }

        [JsonPropertyName("character")]
        public string Character { get; set; }
    }
}
