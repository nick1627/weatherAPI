using InfluxDB.Client;
using weatherAPI.Models;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Writes;

namespace weatherAPI.Services;

public class InfluxDbService
{
    private readonly InfluxDBClient _client;
    private readonly string _bucket;
    private readonly string _org;

    public InfluxDbService(string url, string token, string org, string bucket)
    {
        _client = new InfluxDBClient(url, token);
        _bucket = bucket;
        _org = org;
    }

    public void Write(Action<WriteApi> action)
    {
        using var write = _client.GetWriteApi();
        action(write);
    }
}