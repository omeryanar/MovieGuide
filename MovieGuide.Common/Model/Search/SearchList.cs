using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieGuide.Common.Model.Search
{
    public class SearchList<T> : SearchContainer<T>
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("poster_path")]
        public string PosterPath { get; set; }

        [JsonPropertyName("backdrop_path")]
        public string BackdropPath { get; set; }

        [JsonPropertyName("average_rating")]
        public double AverageRating { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
