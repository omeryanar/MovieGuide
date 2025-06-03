using MovieGuide.Common.Helper;
using MovieGuide.Common.Model.General;
using MovieGuide.Common.Model.Search;
using MovieGuide.Common.Model.Translations;

namespace MovieGuide.Common.Model.Movies
{
    public class Collection
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }        

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("overview")]
        public string Overview { get; set; }

        [JsonPropertyName("images")]
        public Images Images { get; set; }        

        [JsonPropertyName("poster_path")]
        public string PosterPath { get; set; }

        [JsonPropertyName("backdrop_path")]
        public string BackdropPath { get; set; }

        [JsonPropertyName("parts")]
        public List<SearchMovie> Parts { get; set; }

        [JsonPropertyName("translations")]
        public TranslationContainer<TranslationCollection> Translations { get; set; }

        [JsonIgnore]
        public string PosterFullPath => Constants.GetPosterFullPath(PosterPath, MediaType.Movie, Constants.W500);

        [JsonIgnore]
        public double? VoteAverage => Parts?.Where(x => x.VoteAverage > 0).DefaultIfEmpty().Average(x => x?.VoteAverage);

        [JsonIgnore]
        public int? MovieCount => Parts?.Count();

        [JsonIgnore]
        public SearchMovie FirstMovie => Parts?.Where(x => x.ReleaseDate < DateOnly.FromDateTime(DateTime.Today)).OrderBy(x => x.ReleaseDate).FirstOrDefault();

        [JsonIgnore]
        public SearchMovie LastMovie => Parts?.Where(x => x.ReleaseDate < DateOnly.FromDateTime(DateTime.Today)).OrderByDescending(x => x.ReleaseDate).FirstOrDefault();

        [JsonIgnore]
        public IEnumerable<Genre> Genres => GenreHelper.MovieGenres.Where(x => Parts?.SelectMany(y => y.GenreIds).Contains(x.Id) == true);

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

        public override string ToString()
        {
            return Name;
        }
    }
}
