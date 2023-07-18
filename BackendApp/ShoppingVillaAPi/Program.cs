using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ShoppingVilla.Data.Data;
using ShoppingVilla.Data.Entities.Interface;
using ShoppingVilla.Data.Entities.UnitOfWork;
using ShoppingVilla.Data.Utilies;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins,
                 builder =>
                 {
                     //builder.WithOrigins("http://localhost:4200", "http://192.168.137.1").AllowAnyHeader().AllowAnyMethod();
                     builder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();

                 });
});
#endregion Cors

#region JWT
string key = builder.Configuration["Jwt:Key"];
JwtUtility.Key = key;
string issuer = builder.Configuration["Jwt:Issuer"];
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}
    ).AddJwtBearer(x => {
        x.RequireHttpsMetadata = true;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
            ValidIssuer = issuer,
            ValidateAudience = false,
        };
    });
#endregion
#region AppSetting
string ConnStr = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(ConnStr, b => b.MigrationsAssembly("ShoppingVilla.Data")));
#endregion

#region DI

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

#endregion


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

app.UseCors(MyAllowSpecificOrigins);

app.Run();
