using BookStore.Application;
using BookStore.Infrastructure;
using BookStore.Infrastructure.DAL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication();
builder.Services.AddProblemDetails();

builder.ConfigureInfrastructure();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<BookstoreDbContext>();
    var database = dbContext.Database;
    database.EnsureDeleted();
    database.EnsureCreated();
    dbContext.AddData();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();
