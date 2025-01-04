using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using money_tracker.Application.Interfaces;
using money_tracker.Application.Services;
using money_tracker.Domain.Entities;
using money_tracker.Infrastructure.Data;
using money_tracker.Infrastructure.Repositories;
using money_tracker.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        }
    );
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default"))
);

builder
    .Services.AddIdentity<User, IdentityRole<int>>(options =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredLength = 8;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder
    .Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
            ),
        };
    });

builder.Services.AddMemoryCache();

builder.Services.AddSingleton<ConfigManager>();
builder.Services.AddHostedService<CurrenciesUpdaterService>();

builder.Services.AddScoped<IJarsService, JarsService>();
builder.Services.AddScoped<IStoresService, StoresService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<ITransactionsService, TransactionsService>();
builder.Services.AddScoped<ICurrenciesService, CurrenciesService>();

builder.Services.AddScoped<DbTransaction>();
builder.Services.AddScoped<JarsRepository>();
builder.Services.AddScoped<CurrenciesRepository>();
builder.Services.AddScoped<StoresRepository>();
builder.Services.AddScoped<TransactionsRepository>();
builder.Services.AddScoped<CurrencyBalancesRepository>();

builder.Services.AddAuthorization();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}
app.UseCors("AllowAll");

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
