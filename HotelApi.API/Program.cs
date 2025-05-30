using HotelApi.Application.Handlers;
using HotelApi.Domain.Repositories;
using HotelApi.Infrastructure.Repositories;
using Microsoft.Extensions.FileProviders;
using System.Reflection;
using System.Text.Json;


var builder = WebApplication.CreateBuilder(args);

// Define a named CORS policy for reuse
string MyAllowSpecificOrigin = "_myAllowSpecificOrigins";

// Add controllers with JSON options (camelCase naming and case-insensitive matching)
builder.Services.AddControllers()
     .AddJsonOptions(options =>
     {
         options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
         options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
     });

// Add Swagger/OpenAPI support for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "HotelApi.API", Version = "v1" });
});

// Configure CORS to allow any origin, method, and header
builder.Services.AddCors(option =>
{
    option.AddPolicy(MyAllowSpecificOrigin,
        builder => builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});

#region Register mapper and MediatR
// Register AutoMapper with the assembly
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Register MediatR handlers from relevant assemblies
var assemblies = new Assembly[]
{
    Assembly.GetExecutingAssembly(),
    typeof(GetAllHotelHandler).Assembly
};
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));
#endregion

#region Register Application Services
// Register hotel repository dependency
builder.Services.AddScoped<IHotelRepository, HotelRepository>();
#endregion

var app = builder.Build();

// Enable Swagger in development environment for testing and documentation
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Apply CORS policy to the pipeline
app.UseCors(MyAllowSpecificOrigin);

// Enable serving static files from wwwroot
app.UseStaticFiles();

// Serve static files from "Resources" directory with custom URL path
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
    RequestPath = new PathString("/Resources")
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
