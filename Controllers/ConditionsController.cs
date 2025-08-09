using InfluxDB.Client.Writes;
using Microsoft.AspNetCore.Mvc;
using weatherAPI.Models;
using weatherAPI.Services;

namespace weatherAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ConditionsController : ControllerBase
{
    private readonly InfluxDbService _influxDbService;

    public ConditionsController(InfluxDbService influxDbService)
    {
        _influxDbService = influxDbService;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Conditions conditions)
    {
        if (conditions == null)
        {
            return BadRequest("Invalid data.");
        }

        _influxDbService.Write(write =>
        {
            var point = PointData
                .Measurement("weather_conditions")
                .Tag("source", "weather_station")
                .Field("temperature", "conditions.Temperature")
                .Field("humidity", conditions.Humidity)
                .Timestamp(conditions.Timestamp, InfluxDB.Client.Api.Domain.WritePrecision.S);

            write.WritePoint(point, "my-bucket", "my-org");
        });

        return Ok("Conditions written to database.");
    }
}