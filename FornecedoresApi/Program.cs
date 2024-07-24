using FornecedoresApi.Data;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddFluentValidation(config =>
    {
        // Adicionado serviço FluentValidation
        config.RegisterValidatorsFromAssemblyContaining<Program>();
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adicionando o Serviço de Banco de Dados
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("FornecedoresDb")
    ));

// Adicionando o Serviço HttpClient
builder.Services.AddHttpClient();

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
