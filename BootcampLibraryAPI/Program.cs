using BootcampLibraryAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection")
    ));
builder.Services.AddAutoMapper(typeof(Program));






// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

/*Configuracion SWAGGER*/
var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
var xmlPath = Path.Combine(AppContext.BaseDirectory,xmlFile);

builder.Services.AddSwaggerGen(config =>
{
    config.SwaggerDoc("v1",
        new Microsoft.OpenApi.Models.OpenApiInfo()
        {
            Title = "Mi API de biblioteca - Bootcamp",
            Description = "Este es un API para ...",
            Version = "v1",
            Contact = new Microsoft.OpenApi.Models.OpenApiContact()
            {
                Name = "Bootcamp COL - 04",
                Email = "contact@jalasoft.com",
                Url = new Uri(@"https://google.com")
            }
        });
    config.IncludeXmlComments(xmlPath);
});


var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
