using BookStore.Api.Controllers;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System.Reflection;

namespace BookStore.Api.Startup
{
    public static class Instrumentation
    {
        public static void AddInstrumentation(this IServiceCollection services)
        {
            services.AddOpenTelemetry()
                .ConfigureResource(static resource =>
                {
                    resource.AddService(
                        serviceName: "SkillUp.Bookstore.Api",
                        serviceVersion: Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "unknown",
                        serviceInstanceId: Environment.MachineName);
                })
                .WithMetrics(builder => builder.AddPrometheusExporter()
                    .AddAspNetCoreInstrumentation()
                    .AddMeter("Microsoft.AspNetCore.Hosting")
                    .AddMeter("Microsoft.AspNetCore.Server.Kestrel")
                    )
                .WithTracing(builder =>
                {
                    builder.AddSource(BooksController.ActivitySource.Name)
                    .AddAspNetCoreInstrumentation();
                });
        }
    }
}
