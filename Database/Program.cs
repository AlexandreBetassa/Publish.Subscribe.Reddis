using DatabaseAPI.Context.v1;
using DatabaseAPI.Contracts.v1;
using DatabaseAPI.Data.v1;
using DatabaseAPI.IService.v1;
using DatabaseAPI.Repository.v1;
using DatabaseAPI.Service.v1;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient(typeof(IService<>), typeof(Services<>));
builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddTransient(typeof(IDatabase<>), typeof(Database<>));

builder.Services.AddTransient<RedisService>();

ConnectionMultiplexer conn = ConnectionMultiplexer.Connect(builder.Configuration.GetRequiredSection("Redis").Value);
builder.Services.AddSingleton<IConnectionMultiplexer>(conn);
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(builder.Configuration["ConnectionString:DefaultConnection"]));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
