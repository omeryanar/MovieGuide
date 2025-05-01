using MovieGuide.Common.Helper;
using MovieGuide.Common.Model.General;
using MovieGuide.Common.Model.Translations;

namespace MovieGuide.Common.Model.TvShows
{
    public class TvSeason
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("poster_path")]
        public string PosterPath { get; set; }

        [JsonPropertyName("overview")]
        public string Overview { get; set; }

        [JsonPropertyName("season_number")]
        public int SeasonNumber { get; set; }

        [JsonPropertyName("air_date")]
        public DateTime? AirDate { get; set; }

        [JsonPropertyName("credits")]
        public Credits Credits { get; set; }

        [JsonPropertyName("episodes")]
        public List<TvEpisode> Episodes { get; set; }        

        [JsonPropertyName("images")]
        public PosterImages Images { get; set; }

        [JsonPropertyName("videos")]
        public ResultContainer<Video> Videos { get; set; }

        [JsonPropertyName("translations")]
        public TranslationContainer<TranslationTvEpisode> Translations { get; set; }

        [JsonIgnore]
        public double? VoteAverage => Episodes?.Where(x => x.VoteAverage > 0).DefaultIfEmpty().Average(x => x?.VoteAverage);

        [JsonIgnore]
        public string PosterFullPath => Constants.GetPosterFullPath(PosterPath, MediaType.TvShow, Constants.W500);

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
        public List<Cast> Starring
        {
            get => Credits?.Cast?.OrderBy(x => x.Order).ToList();
        }

        [JsonIgnore]
        public List<Crew> FeaturedCrew
        {
            get => Credits?.Crew?.Where(x => Constants.FeaturedJobs.Contains(x.Job)).GroupBy(x => new { x.Id, x.Name }).
                Select(x => new Crew { Id = x.Key.Id, Name = x.Key.Name, Job = String.Join(Constants.ListSeparator, x.OrderBy(y => y.Job).Select(z => z.Job.GetLocalizedText())) }).OrderBy(x => x.Job).ToList();
        }

        [JsonIgnore]
        public List<Crew> Crew
        {
            get => Credits?.Crew?.OrderByDescending(x => Array.IndexOf(Constants.JobOrder, x.Job)).GroupBy(x => new { x.Id, x.Name, x.Gender, x.ProfilePath }).
                Select(x => new Crew { Id = x.Key.Id, Name = x.Key.Name, Gender = x.Key.Gender, ProfilePath = x.Key.ProfilePath, Job = String.Join(Constants.ListSeparator, x.OrderBy(y => y.Job).Select(z => z.Job)) }).ToList();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
