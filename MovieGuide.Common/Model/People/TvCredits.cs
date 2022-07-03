namespace MovieGuide.Common.Model.People
{
    public class TvCredits
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("cast")]
        public List<TvCast> Cast { get; set; }

        [JsonPropertyName("crew")]
        public List<TvCrew> Crew { get; set; }
    }
}
