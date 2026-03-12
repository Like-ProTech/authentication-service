using Authentication_Service.Factories;
using Authentication_Service.Options;
using Authentication_Service.Repositories;
using Authentication_Service.Services;
using Authentication_Service.Strategies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<IDbConnectionStrategy, DefaultConnectionStrategy>();
builder.Services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
builder.Services.AddSingleton<IAuthRepository, AuthRepository>();
builder.Services.AddSingleton<IUserService , UserService>();
builder.Services.AddSingleton<IJwtBearerHandller, JwtBearerHandller>();
builder.Services.Configure<DefaultConnectionOption>(builder.Configuration.GetSection("ConnectionStrings"));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
