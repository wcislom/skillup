using Microsoft.AspNetCore.HttpLogging;

namespace BookStore.Api.Startup
{
    internal static class Logging
    {
        public static void ConfigureLogging(this WebApplicationBuilder builder)
        {
            builder.Services.AddHttpLogging(opt =>
            {
                opt.LoggingFields = HttpLoggingFields.All;
                opt.MediaTypeOptions.AddText("application/json");
            });

            builder.Services.AddW3CLogging(logging =>
            {
                // Log all W3C fields
                logging.LoggingFields = W3CLoggingFields.All;

                logging.AdditionalRequestHeaders.Add("x-forwarded-for");
                logging.AdditionalRequestHeaders.Add("x-client-ssl-protocol");
                logging.FileSizeLimit = 5 * 1024 * 1024;
                logging.RetainedFileCountLimit = 2;
                logging.FileName = "w3c-logs.txt";
                logging.LogDirectory = @"C:\logs";
                logging.FlushInterval = TimeSpan.FromSeconds(2);
            });

            builder.Logging.AddSeq();
            builder.Logging.ClearProviders();
        }

        public static void UseLogging(this WebApplication app)
        {
            app.UseHttpLogging();
            app.UseW3CLogging();
        }
    }
}
