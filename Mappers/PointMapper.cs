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
            .SetField("temperature", "conditions.Temperature")
            .SetField("humidity", conditions.Humidity)
            .SetTimestamp(conditions.Timestamp);
    }
}