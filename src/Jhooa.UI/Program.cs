using Jhooa.UI;
using Jhooa.UI.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddIdentity(builder.Configuration)
    .AddInfrastructure(builder.Configuration)
    .AddApplication(builder.Configuration)
    .AddServerUi(builder.Configuration);

var app = builder.Build();

app.ConfigureServer(builder.Configuration);

await app.RunAsync().ConfigureAwait(false);