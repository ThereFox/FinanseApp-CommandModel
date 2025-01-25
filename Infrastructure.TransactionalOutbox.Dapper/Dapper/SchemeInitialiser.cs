using System.Data;
using CSharpFunctionalExtensions;
using Dapper;

namespace Infrastructure.TransactionalOutbox.Dapper;

public class SchemeInitialiser
{
    private readonly IDbConnection _connection;

    public SchemeInitialiser(IDbConnection connection)
    {
        _connection = connection;
    }
    
    public async Task<Result> Initialise()
    {
        try
        {
            var sql = @"
                CREATE TABLE IF NOT EXISTS OutboxTable
                (
                    Id uuid PRIMARY KEY,
                    message varchar(255) NOT NULL,
                    TargetTopic varchar(255) NOT NULL,
                    CreateTime timestamp NOT NULL,
                    PublishTime timestamp NULL
                )
            ";

            var createTable = await _connection.ExecuteAsync(sql);
            
            return Result.Success();

        }
        catch (Exception e)
        {
            return Result.Failure(e.Message);
        }
    }
}