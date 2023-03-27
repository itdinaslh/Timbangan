using System.Text.Json;
using System.Text.Json.Serialization;

namespace Timbangan.Helpers;

public sealed class DateOnlyJsonConverter : JsonConverter<DateOnly>
{
    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return DateOnly.FromDateTime(reader.GetDateTime());
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
    {
        var isoDate = value.ToString("O");
        writer.WriteStringValue(isoDate);
    }
}

public sealed class TimeOnlyJsonConverter : JsonConverter<TimeOnly>
{
    public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return TimeOnly.FromDateTime(reader.GetDateTime());
    }

    public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
    {
        var isoDate = value.ToString("O");
        writer.WriteStringValue(isoDate);
    }
}
