using DatabaseAPI.Context.v1;
using DatabaseAPI.Contracts.v1;
using DatabaseAPI.Data.v1;
using DatabaseAPI.IService.v1;
using DatabaseAPI.Models.v1;
using DatabaseAPI.Repository.v1;
using DatabaseAPI.Service.v1;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient(typeof(IService<>), typeof(Services<>));
builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddTransient(typeof(IDatabase<>), typeof(Database<>));

builder.Services.AddTransient<RedisService>();

ConnectionMultiplexer conn = ConnectionMultiplexer.Connect(builder.Configuration.GetSection("Redis").Value);
builder.Services.AddSingleton<IConnectionMultiplexer>(conn);
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(builder.Configuration["ConnectionString:DefaultConnection"]));

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
