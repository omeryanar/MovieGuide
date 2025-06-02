using MovieGuide.Common.Model.General;
using MovieGuide.Common.Model.Search;

namespace MovieGuide.Common.Converters
{
    public class MediaTypeConverter : JsonConverter<SearchBase>
    {
        private readonly JsonEnumConverter<MediaType> MediaTypeEnumConverter = new();

        public override SearchBase Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            Utf8JsonReader readerClone = reader;
            while (readerClone.Read())
            {
                if(readerClone.TokenType == JsonTokenType.StartArray)
                {
                    readerClone.TrySkip();
                    continue;
                }

                if (readerClone.TokenType == JsonTokenType.PropertyName)
                {
                    string propertyName = readerClone.GetString();
                    if (propertyName == "media_type")
                    {
                        readerClone.Read();                        
                        MediaType mediaType = MediaTypeEnumConverter.Read(ref readerClone, typeof(MediaType), options);
                        SearchBase searchBase = mediaType switch
                        {
                            MediaType.Movie => JsonSerializer.Deserialize<SearchMovie>(ref reader, options),
                            MediaType.Person => JsonSerializer.Deserialize<SearchPerson>(ref reader, options),
                            MediaType.TvShow => JsonSerializer.Deserialize<SearchTvShow>(ref reader, options),
                            _ => JsonSerializer.Deserialize<SearchMovie>(ref reader, options)
                        };

                        return searchBase;
                    }
                }
            }

            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, SearchBase value, JsonSerializerOptions options)
        {
            switch (value)
            {
                case SearchMovie movie:
                    JsonSerializer.Serialize(writer, movie, options);
                    break;

                case SearchPerson person:
                    JsonSerializer.Serialize(writer, person, options);
                    break;

                case SearchTvShow tvShow:
                    JsonSerializer.Serialize(writer, tvShow, options);
                    break;

                default:
                    JsonSerializer.Serialize(writer, value, options);
                    break;
            }
        }
    }
}
