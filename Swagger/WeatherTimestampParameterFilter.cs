using System.Text.Json.Nodes;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace weatherAPI.Swagger;

public class WeatherTimestampParameterFilter : IParameterFilter
{
    public void Apply(
        IOpenApiParameter parameter,
        ParameterFilterContext context)
    {
        if (parameter is not OpenApiParameter openApiParameter)
            return;

        if (parameter.In != ParameterLocation.Query)
            return;

        if (parameter.Name == "start")
        {
            openApiParameter.Example =
                JsonValue.Create(DateTime.UtcNow.ToString("o"));
        }
        else if (parameter.Name == "end")
        {
            openApiParameter.Example =
                JsonValue.Create(DateTime.UtcNow.AddHours(1).ToString("o"));
        }
    }
}