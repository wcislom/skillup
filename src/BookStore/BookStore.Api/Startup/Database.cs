using BookStore.Infrastructure.DAL;

namespace BookStore.Api.Startup
{
    internal static class Database
    {
        public static void PrepareDatabase(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<BookstoreDbContext>();
                var database = dbContext.Database;
                database.EnsureDeleted();
                database.EnsureCreated();
                dbContext.AddData();
            }
        }
    }
}
