using System.Globalization;

namespace MovieGuide.Common.Converters
{
    public class PartialDateConverter : JsonConverter<DateOnly?>
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateOnly?);
        }

        public override DateOnly? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string str = reader.GetString();
            if (string.IsNullOrEmpty(str))
                return null;

            DateOnly result;
            if (!DateOnly.TryParse(str, CultureInfo.InvariantCulture.DateTimeFormat, DateTimeStyles.None, out result))
                return null;

            return result;
        }

        public override void Write(Utf8JsonWriter writer, DateOnly? value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value?.ToString(CultureInfo.InvariantCulture));
        }
    }
}
