using Swashbuckle.AspNetCore.Annotations;

namespace weatherAPI.Models;

public class Conditions
{
    [SwaggerSchema(Description = "Timestamp, UTC")]
    public DateTime Timestamp { get; set; }
    [SwaggerSchema(Description = "Temperature in degrees Celsius")]

    public double Temperature { get; set; }
    [SwaggerSchema(Description = "Pressure in Pascals")]
    public double Pressure { get; set; }
    [SwaggerSchema(Description = "Relative humidity")]
    public double Humidity { get; set; }
    [SwaggerSchema(Description = "Windspeed in m/s")]
    public double Windspeed { get; set; }
    [SwaggerSchema(Description = "Wind direction bearing in degrees")]
    public double WindDirection { get; set; }
    [SwaggerSchema(Description = "Rainfall in millimetres")]
    public double Rainfall { get; set; }
}