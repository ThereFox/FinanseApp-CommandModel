using System.Data;
using CSharpFunctionalExtensions;
using Dapper;
using Infrastructure.TransactionalOutbox.DTOs;

namespace Infrastructure.TransactionalOutbox.Dapper;

public class OutboxStore
{
    private readonly IDbConnection _connection;

    public OutboxStore(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<Result> SaveEventAsync(ChangeEventDTO changeEvent, string targetTopic, IDbTransaction transaction = null)
    {   
        try
        {
            var sql = @"
                INSERT INTO OutboxTable
                VALUES(
                       gen_random_uuid(),
                       @Message,
                       @TargetTopic,
                       @CreateTime,
                       NULL
                )
            ";
            
            var createResult = await _connection.ExecuteAsync(sql, changeEvent, transaction);

            if (createResult != 1)
            {
                return Result.Failure("Failed to save event");
            }
            
            return Result.Success();
        }
        catch (Exception e)
        {
            return Result.Failure(e.Message);
        }        
    }

    public async Task<IList<ChangeEventDTO>> GetUnhandledEventsAsync(int limit)
    {
        try
        {
            var sql = @"
                SELECT * FROM OutboxTable
                Where PublishTime = NULL
                LIMIT @limit
            ";
            
            var events = await _connection.QueryAsync<ChangeEventDTO>(sql, limit);
            
            return events.ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return [];
        }
    }

    public async Task<Result> CommitEventHandlerAsync(Guid eventId, IDbTransaction transaction = null)
    {
        try
        {
            var sql = @"UPDATE OutboxTable SET PublishTime = @Time WHERE Id = @Id";
        
            var updateResult = await _connection.ExecuteAsync(sql, new { Time = DateTime.UtcNow, Id = eventId }, transaction);
        
            return Result.SuccessIf(updateResult == 1, "Failed to commit event");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Result.Failure(e.Message);
        }
    }
}