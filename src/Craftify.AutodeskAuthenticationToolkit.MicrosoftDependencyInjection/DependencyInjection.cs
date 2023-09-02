using Autodesk.Forge;
using Craftify.AutodeskAuthenticationToolkit.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Craftify.AutodeskAuthenticationToolkit.MicrosoftDependencyInjection;

public static class DependencyInjection
{
    public static void AddAutodeskAuthenticationToolkit(this IServiceCollection serviceCollection, Lazy<AuthCredentials> authCredentials)
    {
        if (serviceCollection is null) throw new ArgumentNullException(nameof(serviceCollection));
        if (authCredentials is null) throw new ArgumentNullException(nameof(authCredentials));
        serviceCollection.AddTransient<ITwoLeggedApi, TwoLeggedApi>();
        serviceCollection.AddTransient<AuthCredentials>(_ => authCredentials.Value);
        serviceCollection.AddSingleton<IAutodeskAuthenticationService, AutodeskAuthenticationService>();
    }
}