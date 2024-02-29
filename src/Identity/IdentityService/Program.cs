using Duende.IdentityServer.Services;
using IdentityService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ICorsPolicyService>((container) =>
{
    var logger = container.GetRequiredService<ILogger<DefaultCorsPolicyService>>();

    return new DefaultCorsPolicyService(logger)
    {
        AllowAll = true
    };
});
builder.Services.AddIdentityServer(o =>
{
    o.AccessTokenJwtType = "at+jwt";
})
          .AddInMemoryApiScopes(Config.ApiScopes)
          .AddInMemoryClients(Config.Clients);
var app = builder.Build();

app.UseIdentityServer();

app.Run();
