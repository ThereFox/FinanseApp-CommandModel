using Infrastructure.EvensSourcerer.EventProduce;
using Infrastructure.TransactionalOutbox.Dapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Persistense.Dapper.TransactionalOutbox.PollingPublisher;

public class PollingPublisherService : IHostedService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly CancellationTokenSource _cancellationTokenSource;
    
    public PollingPublisherService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
        _cancellationTokenSource = new CancellationTokenSource();
    }
    
    public Task StartAsync(CancellationToken cancellationToken)
    {
        Task.Run(() => loop(_cancellationTokenSource.Token));
        return Task.CompletedTask;
    }

    private async Task loop(CancellationToken cancellationToken)
    {
        using var scope = _scopeFactory.CreateScope();
        
        var useCase = scope.ServiceProvider.GetRequiredService<PollingPublisherUseCase>();
        
        while (cancellationToken.IsCancellationRequested == false)
        {
            var handleResult = await useCase.HandleMessageBatch();

            if (handleResult.IsSuccess)
            {
                await Task.Delay(100);
            }
            else
            {
                await Task.Delay(10000);
            }
        }
    }
    
    public Task StopAsync(CancellationToken cancellationToken)
    {
        _cancellationTokenSource.Cancel();
        return Task.CompletedTask;
    }
}