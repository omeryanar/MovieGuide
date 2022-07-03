namespace MovieGuide.Common.Model.General
{
    public class AccountState
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("favorite")]
        public bool Favorite { get; set; }

        [JsonPropertyName("watchlist")]
        public bool Watchlist { get; set; }

        [JsonPropertyName("rating")]
        public double? Rating { get; set; }
    }
}
