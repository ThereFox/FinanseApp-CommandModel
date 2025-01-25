using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.TransactionalOutbox.Dapper;

public class TransactionGetter
{
    private readonly DbContext _context;
    
    public TransactionGetter(DbContext dbContext)
    {
        _context = dbContext;
    }
    
    public IDbTransaction Transaction => _context.Database.CurrentTransaction?.GetDbTransaction();
}