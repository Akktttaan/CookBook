using Dal;
using WebApi;

var builder = WebApplication.CreateBuilder(args);
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddJsonFile("appsettings.Development.json")
    .Build();
ConnectionStrings.CookBookDB = configuration.GetConnectionString("CookBookDB");

builder.Services.AddControllers();
builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen();
builder.Services
    .AddCors(options => options.AddPolicy("CorsPolicy",
        builder => builder.SetIsOriginAllowed(_ => true)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithExposedHeaders("Content-Disposition")
            .AllowCredentials()))
    .AddUnitOfWork()
    .AddServices()
    ;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseCors();
app.UseAuthorization();

app.MapControllers();
app.UseCors("CorsPolicy");
app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.Run();