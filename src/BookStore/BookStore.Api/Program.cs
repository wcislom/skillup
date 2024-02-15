using BookStore.Api.ExceptionHandlers;
using BookStore.Api.Startup;
using BookStore.Application;
using BookStore.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication();
builder.Services.AddExceptionHandler<DomainExceptionHandler>();
builder.ConfigureInfrastructure();
builder.ConfigureLogging();
builder.Services.AddHealthChecks();

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

app.Run();

