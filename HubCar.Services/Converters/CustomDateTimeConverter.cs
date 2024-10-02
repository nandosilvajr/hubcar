using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HubCar.Services.Converters
{

    public class CustomDateTimeConverter : JsonConverter<DateTime>
    {
        private readonly string _format = "yyyy/MM/dd HH:mm:ss"; // Custom format

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            if (DateTime.TryParseExact(value, _format, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
            {
                return date;
            }
            throw new JsonException($"Unable to convert \"{value}\" to DateTime with format {_format}.");
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(_format));
        }
    }
}