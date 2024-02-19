using BookStore.Api.ExceptionHandlers;
using BookStore.Api.Startup;
using BookStore.Application;
using BookStore.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateBootstrapLogger();

logger.Write(Serilog.Events.LogEventLevel.Information, "Starting up the application");
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication();
builder.Services.AddExceptionHandler<DomainExceptionHandler>();
builder.ConfigureInfrastructure();
builder.ConfigureLogging();
builder.Services.AddInstrumentation();
builder.Services.AddHealthChecks();
builder.Logging.AddSerilog(logger, dispose: true);  


var app = builder.Build();

app.UseLogging();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseExceptionHandler(opt => { });
app.PrepareDatabase();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapHealthChecks("/health").AllowAnonymous();
//app.UseSerilogRequestLogging();
app.UseOpenTelemetryPrometheusScrapingEndpoint();
app.Run();

logger.Write(Serilog.Events.LogEventLevel.Information, "Closing the application");

