using PubSub.Contracts.v1;
using PubSub.Repositories.v1;
using PubSub.Services.v1;
using PubSubApi.Services.v1;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient(typeof(IService<>), typeof(Services<>));
builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddTransient<RedisService>();

builder.Services.AddScoped<HttpClient>();

ConnectionMultiplexer conn = ConnectionMultiplexer.Connect(builder.Configuration.GetRequiredSection("Redis").Value);
builder.Services.AddSingleton<IConnectionMultiplexer>(conn);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthentication();

app.MapControllers();

app.Run();
