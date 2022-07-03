namespace MovieGuide.Common.Model.People
{
    public class MovieCredits
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("cast")]
        public List<MovieCast> Cast { get; set; }

        [JsonPropertyName("crew")]
        public List<MovieCrew> Crew { get; set; }
    }
}
