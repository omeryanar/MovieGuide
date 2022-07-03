namespace MovieGuide.Common.Model.General
{
    public class ProductionCountry
    {
        [JsonPropertyName("iso_3166_1")]
        public string Iso_3166_1 { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
