using GestioneOrdiniRistorante.Infrastructure.Extension;
using GestioneOrdiniRistorante.Web.Extension;

var builder = WebApplication.CreateBuilder(args);

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

app.AddWebMiddleware();


app.Run();
