using Microsoft.AspNetCore.Mvc;
using weatherAPI.Models;
using weatherAPI.Services;

namespace weatherAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ConditionsController(InfluxDbService influxDbService, ILogger<ConditionsController> logger) : ControllerBase
{
    private readonly InfluxDbService _influxDbService = influxDbService;
    private readonly ILogger<ConditionsController> _logger = logger;

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Conditions conditions)
    {
        if (conditions == null)
        {
            return BadRequest("Invalid data.");
        }

        try
        {
            await _influxDbService.WriteConditionsAsync(conditions);
            return Ok("Conditions written to database.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to write weather conditions to InfluxDB");

            return StatusCode(500, "An error occurred while processing your request.");
        }
    }
    [HttpGet]
    public async Task<ActionResult<List<Conditions>>> GetConditionsByTimestampRange([FromQuery] DateTime start, [FromQuery] DateTime end)
    {
        if (start >= end)
        {
            return BadRequest("Start time must be earlier than end time.");
        }

        try
        {
            var results = await _influxDbService.QueryAsync(start, end);

            return Ok(results); // Assumes results is a structured object or DTO
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve weather conditions from InfluxDB.");
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }
}