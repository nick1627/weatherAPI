using InfluxDB3.Client.Write;
using weatherAPI.Models;

namespace weatherAPI.Mappers;

public static class PointMapper
{
    public static PointData MapConditions(Conditions conditions)
    {
        return PointData
            .Measurement("weather_conditions")
            .SetTag("source", "weather_station")
            .SetField("temperature", conditions.Temperature)
            .SetField("pressure", conditions.Pressure)
            .SetField("humidity", conditions.Humidity)
            .SetField("windspeed", conditions.Windspeed)
            .SetField("wind_direction", conditions.WindDirection)
            .SetField("rainfall", conditions.Rainfall)
            .SetTimestamp(conditions.Timestamp);
    }
}