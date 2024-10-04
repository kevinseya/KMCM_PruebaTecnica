using Microsoft.EntityFrameworkCore;
using Serilog;
using KMCM_PruebaTecnica.kmcm_models.kmcm_DbContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Obtener la cadena de conexión desde appsettings.json
var connectionStringSQLServer = builder.Configuration.GetConnectionString("Kmcm_SqlServer");
var connectionStringMongo = builder.Configuration.GetConnectionString("Kmcm_MongoDB");
var collectionStringMongo = builder.Configuration.GetConnectionString("collection");
// Agregar el contexto a los servicios
builder.Services.AddDbContext<Kmcm_DbContext>(options =>
	options.UseSqlServer(connectionStringSQLServer));

// Para escribir logs en la consola
Log.Logger = new LoggerConfiguration()
	.WriteTo.Console() 
	.WriteTo.MongoDB(connectionStringMongo, collectionName: collectionStringMongo) 
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
