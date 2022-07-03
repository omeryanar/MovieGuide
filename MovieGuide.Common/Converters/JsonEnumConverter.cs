using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MovieGuide.Common.Converters
{
    public class JsonEnumConverter<TEnum> : JsonConverter<TEnum> where TEnum : struct, Enum
    {
        private readonly Dictionary<TEnum, string> enumToString = new();
        private readonly Dictionary<string, TEnum> stringToEnum = new();

        public JsonEnumConverter()
        {
            var type = typeof(TEnum);
            var values = Enum.GetValues<TEnum>();

            foreach (var value in values)
            {
                var enumMember = type.GetMember(value.ToString())[0];
                var attr = enumMember.GetCustomAttributes(typeof(EnumMemberAttribute), false)
                  .Cast<EnumMemberAttribute>()
                  .FirstOrDefault();

                if (attr?.Value != null)
                {
                    enumToString.Add(value, attr.Value);
                    stringToEnum.Add(attr.Value, value);
                }
                else
                {
                    enumToString.Add(value, value.ToString().ToLower());
                    stringToEnum.Add(value.ToString().ToLower(), value);
                }
            }
        }

        public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var stringValue = reader.GetString();

            if (stringToEnum.TryGetValue(stringValue, out var enumValue))
            {
                return enumValue;
            }

            return default;
        }

        public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(enumToString[value]);
        }
    }
}
