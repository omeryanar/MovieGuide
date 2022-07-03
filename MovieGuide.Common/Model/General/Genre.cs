namespace MovieGuide.Common.Model.General
{
    public class Genre
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        
        [JsonPropertyName("name")]
        public string Name { get; set; }

        public override string ToString()
        {
            string name = Properties.Resources.ResourceManager.GetString(Name);
            if (name == null)
                name = Name;

            return name;
        }
    }
}
