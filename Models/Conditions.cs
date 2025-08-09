namespace weatherAPI.Models;

public class Conditions
{
    public DateTime Timestamp { get; set; } // UTC
    public float Temperature { get; set; } // Degrees celcius
    public float Pressure { get; set; } // Pa
    public float Humidity { get; set; } // %
    public float Windspeed { get; set; } // m/s
    public float WindDirection { get; set; } // bearing in degrees
    public float Rainfall { get; set; } // mm
}