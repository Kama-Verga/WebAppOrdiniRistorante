using GestioneOrdiniRistorante.Infrastructure.Extension;
using GestioneOrdiniRistorante.Web.Extension;

var builder = WebApplication.CreateBuilder(args);

//CORS necessary for development nodejs server, critical to remove at prod time
builder.Services.AddCors(options =>
{
    options.AddPolicy("SpaCors", policy =>
    {
        policy
        .WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });
});
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
app.UseCors("SpaCors");
app.AddWebMiddleware();


app.Run();
