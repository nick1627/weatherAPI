namespace WeatherApi.Options;

public class InfluxDbOptions
{
    public const string SectionName = "WeatherApi:InfluxDb";

    public string Url { get; set; } = "";
    public string Token { get; set; } = "";
    public string Database { get; set; } = "";
}