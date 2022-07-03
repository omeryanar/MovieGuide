using MovieGuide.Common.Helper;
using MovieGuide.Common.Model.General;
using MovieGuide.Common.Model.Translations;

namespace MovieGuide.Common.Model.TvShows
{
    public class TvEpisode
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("still_path")]
        public string StillPath { get; set; }

        [JsonPropertyName("overview")]
        public string Overview { get; set; }

        [JsonPropertyName("season_number")]
        public int SeasonNumber { get; set; }

        [JsonPropertyName("episode_number")]
        public int EpisodeNumber { get; set; }

        [JsonPropertyName("air_date")]
        public DateTime? AirDate { get; set; }

        [JsonPropertyName("crew")]
        public List<Crew> Crew { get; set; }        

        [JsonPropertyName("guest_stars")]
        public List<Cast> GuestStars { get; set; }        

        [JsonPropertyName("vote_average")]
        public double VoteAverage { get; set; }

        [JsonPropertyName("vote_count")]
        public int VoteCount { get; set; }

        [JsonPropertyName("images")]
        public StillImages Images { get; set; }

        [JsonPropertyName("videos")]
        public ResultContainer<Video> Videos { get; set; }

        [JsonPropertyName("translations")]
        public TranslationContainer<TranslationTvEpisode> Translations { get; set; }

        [JsonIgnore]
        public string StillFullPath => Constants.GetStillFullPath(StillPath);

        [JsonIgnore]
        public string LocalizedOverview
        {
            get
            {
                if (!String.IsNullOrWhiteSpace(Overview))
                    return Overview;

                return Translations?.Translations?.FirstOrDefault(x => x.Iso_639_1 == "en")?.Data?.Overview;
            }
        }

        [JsonIgnore]
        public List<Crew> FeaturedCrew
        {
            get => Crew?.Where(x => Constants.FeaturedJobs.Contains(x.Job)).GroupBy(x => new { x.Id, x.Name }).
                Select(x => new Crew { Id = x.Key.Id, Name = x.Key.Name, Job = String.Join(Constants.ListSeparator, x.OrderBy(y => y.Job).Select(z => z.Job.GetLocalizedText())) }).OrderBy(x => x.Job).ToList();
        }

        public override string ToString()
        {
            return Name.StartsWith(EpisodeNumber.ToString()) ? Name : $"{EpisodeNumber}. {Name}";
        }
    }
}
