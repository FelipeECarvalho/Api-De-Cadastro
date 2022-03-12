using CadastroApi;
using CadastroApi.Data;
using CadastroApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

LoadServices(builder);

ConfigureMvc(builder);

ConfigureServices(builder);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


void LoadServices(WebApplicationBuilder builder)
{
    var baseUrl = builder.Configuration.GetValue<string>("ApiConfig");
    Configuration.BaseUrl = baseUrl;

    var smtp = new Configuration.SmtpConfiguration();
    builder.Configuration.GetSection("Smtp").Bind(smtp);
    Configuration.Smtp = smtp;


    var key = builder.Configuration.GetValue<string>("JwtKey");
    Configuration.JwtKey = key;

    builder.Services.AddTransient<TokenService>();
}

void ConfigureServices(WebApplicationBuilder builder)
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    builder.Services.AddDbContext<CadastroDataContext>(options =>
            options.UseSqlServer(connectionString));

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x =>
    {
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.JwtKey)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
}

void ConfigureMvc(WebApplicationBuilder builder)
{
    builder.Services
        .AddControllers()
        .ConfigureApiBehaviorOptions(x =>
            x.SuppressModelStateInvalidFilter = true)
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
        });
}