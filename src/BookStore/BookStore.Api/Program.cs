using BookStore.Api.ExceptionHandlers;
using BookStore.Api.Startup;
using BookStore.Application;
using BookStore.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateBootstrapLogger();

logger.Write(Serilog.Events.LogEventLevel.Information, "Starting up the application");
builder.Services.AddControllers(o => o.Filters.Add(new AuthorizeFilter()));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("oAuth", new OpenApiSecurityScheme
    {
        BearerFormat="jwt",
        Type = SecuritySchemeType.OAuth2,
        
        Flows = new OpenApiOAuthFlows
        {
            
            ClientCredentials = new OpenApiOAuthFlow
            {
                TokenUrl = new Uri(builder.Configuration["oAuth:TokenUrl"]),
                Scopes = new Dictionary<string, string>
                  {
                      { "bookstore", "BookStore API" }
                  }
            }
        }
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oAuth" }
            },
            new string[] {"bookstore"}
        }
    });
});
builder.Services.AddApplication();
builder.Services.AddExceptionHandler<DomainExceptionHandler>();
builder.ConfigureInfrastructure();
builder.ConfigureLogging();
builder.Services.AddInstrumentation();
builder.Services.AddHealthChecks();
builder.Services.AddHttpContextAccessor();
builder.Logging.AddSerilog(logger, dispose: true);

var tokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    ValidIssuer = builder.Configuration["Jwt:Issuer"],
    ValidAudience = builder.Configuration["Jwt:Audience"],
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
};
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.Authority = builder.Configuration["oAuth:Authority"];
    o.TokenValidationParameters.ValidateAudience = false;
});

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseLogging();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.OAuthClientId(app.Configuration["oAuth:ClientId"]);
        c.OAuthClientSecret(app.Configuration["oAuth:ClientSecret"]);
    });
    app.UseDeveloperExceptionPage();
}

app.UseExceptionHandler(opt => { });
app.PrepareDatabase();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapHealthChecks("/health").AllowAnonymous();
//app.UseSerilogRequestLogging();
app.UseOpenTelemetryPrometheusScrapingEndpoint();
app.Run();

logger.Write(Serilog.Events.LogEventLevel.Information, "Closing the application");

