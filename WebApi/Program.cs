using AutoMapper;
using Entities.DeviceRegistrationEntity;
using Google;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using ServicesLibrary.ExtensionMethod;
using System.Reflection;
using System.Web.Http;
using WebApi.HelpingMethods;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<WebApiDatabase>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddControllers();
builder.Services.AddCors();

//automapper configuration:
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddScoped<ApiKeyAuthorizationFilter>();
builder.Services.AddScoped<IApiKeyValidator, ApiKeyValidator>();
builder.Services.AddMemoryCache();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowOrigin",
        builder =>
        {
            builder.WithOrigins("https://localhost:7153", "http://localhost:4200")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Web Api's", Version = "v1" });

    c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        Description = "API key needed to access the endpoints. Enter the API key in the format 'Bearer {your API key}'",
        Name = "X-API-Key",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "ApiKey" // Update the ID to match the security definition ID
                }
            },
            new List<string>()
        }
    });

    // Add support for uploading images
    //c.OperationFilter<AddFileParamTypesOperationFilter>();
});


builder.Services.ImplementPersistence(builder.Configuration);
builder.Services.SchoolServicesProvider();
 builder.Services.EmployeeServiceProvider();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1");
        options.RoutePrefix = "swagger";
    });
}
app.UseHttpsRedirection();
app.UseCors();
app.UseCors("AllowOrigin");
app.UseDeveloperExceptionPage();

app.UseAuthorization();

app.MapControllers();

app.Run();
