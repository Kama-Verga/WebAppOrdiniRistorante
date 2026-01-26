using GestioneOrdiniRistorante.Infrastructure.Extension;
using GestioneOrdiniRistorante.Web.Extension;

var builder = WebApplication.CreateBuilder(args);
const string envVarName = "GESTIONE_ORDINI_DEPLOY";
var deploy = Environment.GetEnvironmentVariable(envVarName, EnvironmentVariableTarget.Machine)
    ?? Environment.GetEnvironmentVariable(envVarName, EnvironmentVariableTarget.User)
    ?? Environment.GetEnvironmentVariable(envVarName);

//CORS necessary for development nodejs server, critical to remove at prod time
if(deploy == "True") { 
builder.Services.AddCors(options =>
{
    options.AddPolicy("SpaCors", policy =>
    {
        policy
        .WithOrigins(["http://localhost:4200", "http://192.168.1.12:4200", "http://0.0.0.0:4200"])
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });
});
}
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddModelServices(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddWebServices(builder.Configuration);
builder.Services.AddOptions(builder.Configuration);


var app = builder.Build();

if(deploy == "True")
    app.UseCors("SpaCors");

app.AddWebMiddleware();


app.Run();
