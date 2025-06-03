using MovieGuide.Common.Helper;
using MovieGuide.Common.Model.General;
using MovieGuide.Common.Model.Search;
using MovieGuide.Common.Model.Translations;

namespace MovieGuide.Common.Model.Movies
{
    public class Movie
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("imdb_id")]
        public string ImdbId { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("poster_path")]
        public string PosterPath { get; set; }

        [JsonPropertyName("backdrop_path")]
        public string BackdropPath { get; set; }

        [JsonPropertyName("original_language")]
        public string OriginalLanguage { get; set; }

        [JsonPropertyName("original_title")]
        public string OriginalTitle { get; set; }

        [JsonPropertyName("overview")]
        public string Overview { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("tagline")]
        public string Tagline { get; set; }

        [JsonPropertyName("runtime")]
        public int? Runtime { get; set; }

        [JsonPropertyName("release_date")]
        public DateOnly? ReleaseDate { get; set; }

        [JsonPropertyName("budget")]
        public long Budget { get; set; }

        [JsonPropertyName("revenue")]
        public long Revenue { get; set; }

        [JsonPropertyName("vote_average")]
        public double VoteAverage { get; set; }

        [JsonPropertyName("vote_count")]
        public int VoteCount { get; set; }

        [JsonPropertyName("account_states")]
        public AccountState AccountStates { get; set; }        

        [JsonPropertyName("belongs_to_collection")]
        public SearchCollection BelongsToCollection { get; set; }        

        [JsonPropertyName("credits")]
        public Credits Credits { get; set; }

        [JsonPropertyName("genres")]
        public List<Genre> Genres { get; set; }

        [JsonPropertyName("images")]
        public Images Images { get; set; }

        [JsonPropertyName("keywords")]
        public KeywordsContainer Keywords { get; set; }

        [JsonPropertyName("videos")]
        public ResultContainer<Video> Videos { get; set; }

        [JsonPropertyName("production_companies")]
        public List<ProductionCompany> ProductionCompanies { get; set; }

        [JsonPropertyName("production_countries")]
        public List<ProductionCountry> ProductionCountries { get; set; }        

        [JsonPropertyName("similar")]
        public SearchContainer<SearchMovie> Similar { get; set; }

        [JsonPropertyName("recommendations")]
        public SearchContainer<SearchMovie> Recommendations { get; set; }

        [JsonPropertyName("translations")]
        public TranslationContainer<TranslationMovie> Translations { get; set; }

        [JsonIgnore]
        public string PosterFullPath => Constants.GetPosterFullPath(PosterPath, MediaType.Movie, Constants.W500);

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
            return Title;
        }
    }
}
