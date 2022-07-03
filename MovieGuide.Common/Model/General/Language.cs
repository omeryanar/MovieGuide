namespace MovieGuide.Common.Model.General
{
    public class Language
    {
        [JsonPropertyName("iso_639_1")]
        public string Iso_639_1 { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("english_name")]
        public string EnglishName { get; set; }

        public override string ToString()
        {
            if (String.IsNullOrEmpty(Name))
                return EnglishName;

            if (String.IsNullOrEmpty(EnglishName))
                return Name;

            return Name == EnglishName ? Name : $"{Name} ({EnglishName})";
        }
    }
}
