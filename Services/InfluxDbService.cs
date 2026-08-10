using InfluxDB3.Client;
using InfluxDB3.Client.Config;
using Microsoft.Extensions.Options;
using weatherAPI.Mappers;
using weatherAPI.Models;
using weatherAPI.Options;

namespace weatherAPI.Services;

public class InfluxDbService
{
    private readonly InfluxDBClient _client;

    public InfluxDbService(IOptions<InfluxDbOptions> options)
    {
        var influxOptions = options.Value;
        var clientConfig = new ClientConfig
        {
            Host = influxOptions.Url,
            Token = influxOptions.Token,
            Database = influxOptions.Database
        };
        _client = new InfluxDBClient(clientConfig);
    }

    public async Task WriteConditionsAsync(Conditions conditions)
    {
        var point = PointMapper.MapConditions(conditions);
        await _client.WritePointAsync(point);
    }

    public async Task<List<Conditions>> QueryAsync(DateTime start, DateTime end)
    {
        const string queryString = """
        SELECT *
        FROM weather_conditions
        WHERE time >= $start AND time < $end
        ORDER BY time
        """;

        var parameters = new Dictionary<string, object>
        {
            ["start"] = start.ToUniversalTime().ToString("o"),
            ["end"] = end.ToUniversalTime().ToString("o")
        };

        var results = new List<Conditions>();

        await foreach (var row in _client.QueryPoints(queryString, namedParameters: parameters))
        {
            results.Add(new Conditions
            {
                Timestamp = DateTime.UnixEpoch.AddTicks(
                    (long)(row.GetTimestamp()!.Value / 100)
                ),
                Temperature = row.GetField<double>("temperature")!.Value,
                Pressure = row.GetField<double>("pressure")!.Value,
                Humidity = row.GetField<double>("humidity")!.Value,
                Windspeed = row.GetField<double>("windspeed")!.Value,
                WindDirection = row.GetField<double>("wind_direction")!.Value,
                Rainfall = row.GetField<double>("rainfall")!.Value
            });
        }

        return results;
    }
}