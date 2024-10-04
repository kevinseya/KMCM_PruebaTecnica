using Microsoft.EntityFrameworkCore;
using Serilog;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


using KMCM_PruebaTecnica.kmcm_models.kmcm_DbContext;
using KMCM_PruebaTecnica.kmcm_accessData;

var builder = WebApplication.CreateBuilder(args);


// Configuracion del CORS
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAllOrigins",
		builder => builder.AllowAnyOrigin() 
						  .AllowAnyMethod() 
						  .AllowAnyHeader()); 
});



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();


// CONFIGURACION DE SEGURIDAD JWT
var jwtSection = builder.Configuration.GetSection("Jwt");
var key = jwtSection.GetValue<string>("Key");
var issuer = jwtSection.GetValue<string>("Issuer");
var audience = jwtSection.GetValue<string>("Audience");

builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = true,
		ValidateAudience = true,
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		ValidIssuer = issuer,  
		ValidAudience = audience,  
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
	};
});


// Obtener la cadena de conexión desde appsettings.json
var connectionStringSQLServer = builder.Configuration.GetConnectionString("Kmcm_SqlServer");
var connectionStringMongo = builder.Configuration.GetConnectionString("Kmcm_MongoDB");
var pathString = builder.Configuration.GetValue<string>("Kmcm_PathLog");
// Agregar el contexto a los servicios
builder.Services.AddDbContext<Kmcm_DbContext>(options =>
	options.UseSqlServer(connectionStringSQLServer));

// Agregar el repositorio a los servicios
builder.Services.AddScoped<kmcm_repositoryPerson>();
builder.Services.AddScoped<kmcm_repositoryUser>();



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

// Configurar puertos

var portHTTP = builder.Configuration.GetValue<string>("Ports:http");
var portHTTPS = builder.Configuration.GetValue<string>("Ports:https");
app.Urls.Add(portHTTP);
app.Urls.Add(portHTTPS);

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//	app.UseSwagger();
//	app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseCors("AllowAllOrigins");

app.UseAuthentication();

app.UseAuthorization();


app.MapControllers();

app.Run();
