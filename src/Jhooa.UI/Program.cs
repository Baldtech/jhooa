using Jhooa.UI.Configuration;
using Jhooa.UI.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.RegisterSerilogAndApplicationInsights();

builder.Services
    .AddIdentity(builder.Configuration)
    .AddInfrastructure(builder.Configuration)
    .AddApplication(builder.Configuration)
    .AddServerUi(builder.Configuration);

var app = builder.Build();

app.ConfigureServer(builder.Configuration);

await app.RunAsync().ConfigureAwait(false);