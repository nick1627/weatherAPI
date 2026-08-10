using weatherAPI.Services;
using weatherAPI.Options;
using weatherAPI.Swagger;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
    options.ParameterFilter<WeatherTimestampParameterFilter>();
}
);

builder.Services.Configure<InfluxDbOptions>
(
    builder.Configuration.GetSection(InfluxDbOptions.SectionName)
);
builder.Services.AddSingleton<InfluxDbService>();
builder.Services.AddLogging();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUi(options =>
    {
        options.DocumentPath = "/swagger/v1/swagger.json";
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
