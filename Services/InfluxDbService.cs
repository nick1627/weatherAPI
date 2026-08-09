using InfluxDB3.Client;
using InfluxDB3.Client.Config;
using Microsoft.Extensions.Options;
using weatherAPI.Mappers;
using weatherAPI.Models;
using WeatherApi.Options;

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
            ["start"] = start,
            ["end"] = end
        };

        var results = new List<Conditions>();

        await foreach (var row in _client.Query(queryString, namedParameters: parameters))
        {
            results.Add(new Conditions
            {
                Timestamp = (DateTime)row[0]!,
                Temperature = Convert.ToSingle(row[1]),
                Pressure = Convert.ToSingle(row[2]),
                Humidity = Convert.ToSingle(row[3]),
                Windspeed = Convert.ToSingle(row[4]),
                WindDirection = Convert.ToSingle(row[5]),
                Rainfall = Convert.ToSingle(row[6])
            });
        }

        return results;
    }
}