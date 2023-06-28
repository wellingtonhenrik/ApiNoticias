using Hangfire;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Noticias.Controllers;
using Noticias.Model;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ApiContext") ?? throw new InvalidOperationException("Connection string 'ApiContext' not found.")));// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//configuração refente ao hangfire
builder.Services.AddHangfire(configuration => configuration.UseRecommendedSerializerSettings().UseSqlServerStorage(builder.Configuration.GetConnectionString("ApiContext")));
builder.Services.AddHangfireServer();

//configuração refente ao cors, sem isso quando tenta usar algo para pegar informação da api da erro de politica de cors
builder.Services.AddCors();

// Define a quantidade de retentativas aplicadas a um job com falha.
// Por padrão serão 10, aqui estamos abaixando para duas com intervalo de 5 minutos.
GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute
{
    Attempts = 3,
    DelaysInSeconds = new int[] { 300 }
}); 

var app = builder.Build();

//configuração refente ao hangfire
HangFireConfig.Start();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//configuração refente ao cors, sem isso quando tenta usar algo para pegar informação da api da erro de politica de cors
app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
