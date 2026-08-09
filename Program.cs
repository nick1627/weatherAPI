using weatherAPI.Services;
using WeatherApi.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => c.EnableAnnotations());

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
