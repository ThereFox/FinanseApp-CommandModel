using Microsoft.Extensions.DependencyInjection;

namespace Persistense.Dapper.TransactionalOutbox.PollingPublisher;

public static class DIRegister
{
    public static IServiceCollection AddPollingPublisher(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<PollingPublisherUseCase>();
        serviceCollection.AddHostedService<PollingPublisherService>();
        
        return serviceCollection;
    }
}