using System;
using DotNetEnv;
using FluentValidation.AspNetCore;
using FornecedoresApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Carregando Variaveis de ambiente para o arquivo .env
Env.Load();

// Adicionando serviços ao container
builder.Services.AddControllers()
    .AddFluentValidation(config =>
    {
        // Adicionado serviço FluentValidation
        config.RegisterValidatorsFromAssemblyContaining<Program>();
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Confiuguração da Build
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

// Contrução de conexão por strings para as variaveis de ambiente
var server = Environment.GetEnvironmentVariable("DB_SERVER");
var database = Environment.GetEnvironmentVariable("DB_NAME");
var user = Environment.GetEnvironmentVariable("DB_USER");
var password = Environment.GetEnvironmentVariable("DB_PASSWORD");
var encrypt = Environment.GetEnvironmentVariable("DB_ENCRYPT");
var trustCert = Environment.GetEnvironmentVariable("DB_TRUST_CERT");

var connectionString  = $"Server={server};Database={database};User ID={user};Password={password};Encrypt={encrypt};TrustServerCertificate={trustCert};";

// Adicionando o Serviço de Banco de Dados
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

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
