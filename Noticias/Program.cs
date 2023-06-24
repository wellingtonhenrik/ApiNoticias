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
builder.Services.AddHangfire(configuration => configuration.UseRecommendedSerializerSettings().UseSqlServerStorage(builder.Configuration.GetConnectionString("ApiContext")));
builder.Services.AddHangfireServer();

// Define a quantidade de retentativas aplicadas a um job com falha.
// Por padrão serão 10, aqui estamos abaixando para duas com intervalo de 5 minutos.
GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute
{
    Attempts = 3,
    DelaysInSeconds = new int[] { 300 }
}); 

var app = builder.Build();

app.UseHangfireDashboard("/hangfire", new DashboardOptions()
{
});

HangFireConfig.Start();

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
