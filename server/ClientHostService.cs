using Microsoft.EntityFrameworkCore;
using OpenIddict.Abstractions;
using server.Data;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace server;

public class ClientHostService : IHostedService
{
    private IServiceProvider ServiceProvider { get; set; }

    public ClientHostService(IServiceProvider serviceProvider)
    {
        this.ServiceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var serviceScope = ServiceProvider.CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
        var logger = serviceScope.ServiceProvider.GetRequiredService<ILogger<ClientHostService>>();

        await dbContext.Database.EnsureCreatedAsync(cancellationToken);
        var manager = serviceScope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();
        var existClient = await manager.FindByClientIdAsync("testThirdPart", cancellationToken);
        if (existClient is null)
        {
            var client = await manager.CreateAsync(new OpenIddictApplicationDescriptor
            {
                ClientId = "testThirdPart",
                DisplayName = "测试第三方注册应用",
                Permissions =
                {
                    Permissions.Endpoints.Authorization,
                    Permissions.Endpoints.Token,
                    Permissions.GrantTypes.AuthorizationCode,
                    Permissions.ResponseTypes.Code,
                    Permissions.Scopes.Email,
                    Permissions.Scopes.Profile,
                    Permissions.Scopes.Roles,
                    Permissions.GrantTypes.RefreshToken,
                    Permissions.Endpoints.Logout
                },
                RedirectUris = { new Uri("http://localhost:5555/callback") }
            }, cancellationToken);
            logger.LogInformation(client.ToString());
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}