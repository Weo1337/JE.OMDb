using AutoMapper;
using JE.OMDb.Common;
using JE.OMDb.Services;
using JE.OMDb.Services.Interfaces;
using Models.Mapping;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

var configuration = builder.Configuration;

// Add mappings
AddMappings(builder.Services);

// Register services with the DI container
ConfigureServices(builder.Services, configuration);

var app = builder.Build();

Configure(app, configuration);

app.Run();

void AddMappings(IServiceCollection services)
{
    var mapperConfig = new MapperConfiguration(mc =>
    {
        mc.AddProfile<MovieDTOMappingProfile>();
        mc.AddProfile<SearchResponseDTOMappingProfile>();
        mc.AddProfile<SearchResultDTOMappingProfile>();
    });

    var mapper = mapperConfig.CreateMapper();
    services.AddSingleton(mapper);
}

void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

    services.Configure<AppSettings>(configuration.GetSection("OmdbApi"));
    services.AddControllers();
    services.AddHttpClient<OmdbService>();
    services.AddMemoryCache();

    services.AddScoped<IOmdbService, OmdbService>();
}

void Configure(WebApplication app, IConfiguration configuration)
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();
}
