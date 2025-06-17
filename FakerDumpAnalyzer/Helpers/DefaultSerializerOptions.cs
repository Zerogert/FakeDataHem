using System.Text.Json;
using System.Text.Json.Serialization;

namespace FakerDumpAnalyzer.Helpers
{
    public static class DefaultSerializerOptions
    {
        public static JsonSerializerOptions GetOptions()
        {
            return new()
            {
                WriteIndented = true,
                PropertyNamingPolicy = new SnakeCaseNamingPolicy(),
                Converters = { new JsonStringEnumConverter(new SnakeCaseNamingPolicy()), new JsonDoubleConverter() }
            };
        }
    }
}

public class SnakeCaseNamingPolicy : JsonNamingPolicy
{
    public static SnakeCaseNamingPolicy Instance { get; } = new SnakeCaseNamingPolicy();

    public override string ConvertName(string name)
    {
        // Conversion to other naming convention goes here. Like SnakeCase, KebabCase etc.
        return name.ToSnakeCase();
    }
}

public static class SnakeCaseStringUtil
{
    public static string ToSnakeCase(this string str)
    {
        return string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToLower();
    }
}

public class JsonDoubleConverter : JsonConverter<double>
{
    public override double Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetDouble();

        if (Double.IsNaN(value) || Double.IsInfinity(value)) return 0;

        return value;
    }

    public override void Write(Utf8JsonWriter writer, double value, JsonSerializerOptions options)
    {
        if (Double.IsNaN(value) || Double.IsInfinity(value))
        {
            writer.WriteNumberValue(0);
            return;
        }

        writer.WriteNumberValue(value);
    }
}