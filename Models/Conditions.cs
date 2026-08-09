using Swashbuckle.AspNetCore.Annotations;

namespace weatherAPI.Models;

public class Conditions
{
    [SwaggerSchema(Description = "Timestamp, UTC")]
    public DateTime Timestamp { get; set; }
    [SwaggerSchema(Description = "Temperature in degrees Celsius")]

    public float Temperature { get; set; }
    [SwaggerSchema(Description = "Pressure in Pascals")]
    public float Pressure { get; set; }
    [SwaggerSchema(Description = "Relative humidity")]
    public float Humidity { get; set; }
    [SwaggerSchema(Description = "Windspeed in m/s")]
    public float Windspeed { get; set; }
    [SwaggerSchema(Description = "Wind direction bearing in degrees")]
    public float WindDirection { get; set; }
    [SwaggerSchema(Description = "Rainfall in millimetres")]
    public float Rainfall { get; set; }
}