namespace MovieGuide.Common.Model.General
{
    public class Keyword
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
