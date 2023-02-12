using MovieGuide.Common.Helper;
using MovieGuide.Common.Model.General;
using MovieGuide.Common.Model.Search;
using MovieGuide.Common.Model.Translations;

namespace MovieGuide.Common.Model.People
{
    public class Person
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("gender")]
        public Gender Gender { get; set; }

        [JsonPropertyName("profile_path")]
        public string ProfilePath { get; set; }

        [JsonPropertyName("known_for_department")]
        public string KnownForDepartment { get; set; }

        [JsonPropertyName("also_known_as")]
        public List<string> AlsoKnownAs { get; set; }

        [JsonPropertyName("biography")]
        public string Biography { get; set; }

        [JsonPropertyName("place_of_birth")]
        public string PlaceOfBirth { get; set; }

        [JsonPropertyName("birthday")]
        public DateTime? Birthday { get; set; }

        [JsonPropertyName("deathday")]
        public DateTime? Deathday { get; set; }

        [JsonPropertyName("imdb_id")]
        public string ImdbId { get; set; }

        [JsonPropertyName("external_ids")]
        public ExternalIds ExternalIds { get; set; }

        [JsonPropertyName("images")]
        public ProfileImages Images { get; set; }

        [JsonPropertyName("movie_credits")]
        public MovieCredits MovieCredits { get; set; }

        [JsonPropertyName("tv_credits")]
        public TvCredits TvCredits { get; set; }

        [JsonPropertyName("translations")]
        public TranslationContainer<TranslationPerson> Translations { get; set; }

        [JsonIgnore]
        public string ProfileFullPath => Constants.GetProfileFullPath(ProfilePath, Gender, Constants.W300);

        [JsonIgnore]
        public string Sign => SignHelper.GetSignByBirthday(Birthday);

        [JsonIgnore]
        public string LocalizedBiography
        {
            get
            {
                if (!String.IsNullOrWhiteSpace(Biography))
                    return Biography;

                return Translations?.Translations?.FirstOrDefault(x => x.Iso_639_1 == "en")?.Data?.Biography;
            }
        }

        [JsonIgnore]
        public List<SearchMovieTvBase> KnownFor
        {
            get => KnownForDepartment == "Acting" ? MovieCredits?.Cast?.Cast<SearchMovieTvBase>().Union(TvCredits?.Cast?.Cast<SearchMovieTvBase>())?.OrderByDescending(x => x.VoteAverage * x.VoteCount).Take(8).ToList()
                : MovieKnownForCredits?.Cast<SearchMovieTvBase>().Union(TvKnownForCredits?.Cast<SearchMovieTvBase>())?.OrderByDescending(x => x.VoteAverage * x.VoteCount).Take(8).ToList();
        }

        [JsonIgnore]
        public IEnumerable<MovieCrew> KnownMovieCredits
        {
            get => KnownForDepartment == "Acting" ? MovieActingCredits?.Union(MovieCredits?.Crew) : MovieCredits?.Crew?.Union(MovieActingCredits);
        }

        [JsonIgnore]
        public IEnumerable<TvCrew> KnownTvCredits
        {
            get => KnownForDepartment == "Acting" ? TvActingCredits?.Union(TvCredits?.Crew) : TvCredits?.Crew?.Union(TvActingCredits);
        }

        private IEnumerable<MovieCrew> MovieActingCredits
        {
            get => MovieCredits?.Cast?.Select(x => new MovieCrew { Id = x.Id, Title = x.Title, PosterPath = x.PosterPath, VoteAverage = x.VoteAverage, Department = "Acting", Job = x.Character, ReleaseDate = x.ReleaseDate });
        }

        private IEnumerable<MovieCrew> MovieKnownForCredits
        {
            get => MovieCredits?.Crew?.Where(x => x.Department == KnownForDepartment).GroupBy(x => new { x.Id, x.Title, x.PosterPath, x.ReleaseDate, x.VoteAverage }).
               Select(x => new MovieCrew { Id = x.Key.Id, Title = x.Key.Title, PosterPath = x.Key.PosterPath, ReleaseDate = x.Key.ReleaseDate, VoteAverage = x.Key.VoteAverage, Job = String.Join(Constants.ListSeparator, x.OrderBy(y => y.Job).Select(z => z.Job)) });
        }

        private IEnumerable<TvCrew> TvActingCredits
        {
            get => TvCredits?.Cast?.Select(x => new TvCrew { Id = x.Id, Name = x.Name, PosterPath = x.PosterPath, VoteAverage = x.VoteAverage, Department = "Acting", Job = x.Character, FirstAirDate = x.FirstAirDate, EpisodeCount = x.EpisodeCount });
        }

        private IEnumerable<TvCrew> TvKnownForCredits
        {
            get => TvCredits?.Crew?.Where(x => x.Department == KnownForDepartment).GroupBy(x => new { x.Id, x.Name, x.PosterPath, x.FirstAirDate, x.VoteAverage }).
                Select(x => new TvCrew { Id = x.Key.Id, Name = x.Key.Name, PosterPath = x.Key.PosterPath, FirstAirDate = x.Key.FirstAirDate, VoteAverage = x.Key.VoteAverage, Job = String.Join(Constants.ListSeparator, x.OrderBy(y => y.Job).Select(z => z.Job)) });
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
