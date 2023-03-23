using AutoMapper;
using Entities.DeviceRegistrationEntity;
using Google;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ServicesLibrary.ExtensionMethod;
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

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowOrigin",
        builder =>
        {
            builder.WithOrigins("https://localhost:7153", "https://localhost:44302")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Web Api's", Version = "v1" });               
});

builder.Services.ImplementPersistence(builder.Configuration);
builder.Services.SchoolServicesProvider();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseCors();
app.UseCors("AllowOrigin");


app.UseAuthorization();

app.MapControllers();

app.Run();
