namespace MovieGuide.Common.Model.General
{
    public class Credits
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("cast")]
        public List<Cast> Cast { get; set; }

        [JsonPropertyName("crew")]
        public List<Crew> Crew { get; set; }
    }
}
