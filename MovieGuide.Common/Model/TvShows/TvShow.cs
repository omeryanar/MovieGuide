using MovieGuide.Common.Helper;
using MovieGuide.Common.Model.General;
using MovieGuide.Common.Model.Search;
using MovieGuide.Common.Model.Translations;

namespace MovieGuide.Common.Model.TvShows
{
    public class TvShow
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("poster_path")]
        public string PosterPath { get; set; }

        [JsonPropertyName("backdrop_path")]
        public string BackdropPath { get; set; }

        [JsonPropertyName("original_language")]
        public string OriginalLanguage { get; set; }

        [JsonPropertyName("original_title")]
        public string OriginalName { get; set; }

        [JsonPropertyName("overview")]
        public string Overview { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("tagline")]
        public string Tagline { get; set; }

        [JsonPropertyName("episode_run_time")]
        public List<int> EpisodeRunTime { get; set; }

        [JsonPropertyName("first_air_date")]
        public DateTime? FirstAirDate { get; set; }

        [JsonPropertyName("vote_average")]
        public double VoteAverage { get; set; }

        [JsonPropertyName("vote_count")]
        public int VoteCount { get; set; }

        [JsonPropertyName("account_states")]
        public AccountState AccountStates { get; set; }

        [JsonPropertyName("created_by")]
        public List<CreatedBy> CreatedBy { get; set; }

        [JsonPropertyName("credits")]
        public Credits Credits { get; set; }

        [JsonPropertyName("genres")]
        public List<Genre> Genres { get; set; }

        [JsonPropertyName("images")]
        public Images Images { get; set; }

        [JsonPropertyName("external_ids")]
        public ExternalIds ExternalIds { get; set; }

        [JsonPropertyName("keywords")]
        public ResultContainer<Keyword> Keywords { get; set; }

        [JsonPropertyName("last_air_date")]
        public DateTime? LastAirDate { get; set; }

        [JsonPropertyName("networks")]
        public List<Network> Networks { get; set; }   

        [JsonPropertyName("seasons")]
        public List<SearchTvSeason> Seasons { get; set; }

        [JsonPropertyName("videos")]
        public ResultContainer<Video> Videos { get; set; }

        [JsonPropertyName("production_companies")]
        public List<ProductionCompany> ProductionCompanies { get; set; }

        [JsonPropertyName("production_countries")]
        public List<ProductionCountry> ProductionCountries { get; set; }

        [JsonPropertyName("similar")]
        public ResultContainer<SearchTvShow> Similar { get; set; }

        [JsonPropertyName("recommendations")]
        public ResultContainer<SearchTvShow> Recommendations { get; set; }        

        [JsonPropertyName("translations")]
        public TranslationContainer<TranslationTvShow> Translations { get; set; }

        [JsonIgnore]
        public string PosterFullPath => Constants.GetPosterFullPath(PosterPath, MediaType.TvShow, Constants.W300);

        [JsonIgnore]
        public string TrailerLink
        {
            get
            {
                IEnumerable<Video> videos = Videos?.Results?.Where(x => x.Type == "Trailer");
                if (videos?.Any(x => x.Official) == true)
                    return videos?.FirstOrDefault(x => x.Official).Key;
                else if (videos?.Any() == true)
                    return videos?.FirstOrDefault().Key;
                else
                    return Videos?.Results?.FirstOrDefault()?.Key;
            }
        }

        [JsonIgnore]
        public string LocalizedTagline
        {
            get
            {
                if (!String.IsNullOrWhiteSpace(Tagline))
                    return Tagline;

                return Translations?.Translations?.FirstOrDefault(x => x.Iso_639_1 == "en")?.Data?.Tagline;
            }
        }

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
            get => CreatedBy?.Select(x => new Crew { Id = x.Id, Name = x.Name, Job = "Creator" }).
                Union(Credits?.Crew?.Where(x => Constants.FeaturedJobs.Contains(x.Job))).GroupBy(x => new { x.Id, x.Name }).
                Select(x => new Crew { Id = x.Key.Id, Name = x.Key.Name, Job = String.Join(Constants.ListSeparator, x.OrderBy(y => y.Job).Select(z => z.Job.GetLocalizedText())) }).OrderBy(x => x.Job).ToList();
        }

        [JsonIgnore]
        public List<Crew> Crew
        {
            get => Credits?.Crew?.Where(x => !Constants.FeaturedJobs.Contains(x.Job))?.OrderByDescending(x => x.ProfilePath).ToList();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
