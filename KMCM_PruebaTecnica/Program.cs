using Microsoft.EntityFrameworkCore;
using Serilog;
using KMCM_PruebaTecnica.kmcm_models.kmcm_DbContext;
using KMCM_PruebaTecnica.kmcm_accessData;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Obtener la cadena de conexión desde appsettings.json
var connectionStringSQLServer = builder.Configuration.GetConnectionString("Kmcm_SqlServer");
var connectionStringMongo = builder.Configuration.GetConnectionString("Kmcm_MongoDB");
var pathString = builder.Configuration.GetConnectionString("Kmcm_PathLog");
// Agregar el contexto a los servicios
builder.Services.AddDbContext<Kmcm_DbContext>(options =>
	options.UseSqlServer(connectionStringSQLServer));

// Agregar el repositorio a los servicios
builder.Services.AddScoped<kmcm_repositoryPerson>();


// Asegurarse de que el directorio de logs existe
var logDirectory = @"C:\logs";
if (!Directory.Exists(logDirectory))
{
	Directory.CreateDirectory(logDirectory);
}

// Para escribir logs en la consola
Log.Logger = new LoggerConfiguration()
	.WriteTo.Console()
	.WriteTo.MongoDB(connectionStringMongo)
	.WriteTo.File(pathString)
	.CreateLogger();

builder.Host.UseSerilog();

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
