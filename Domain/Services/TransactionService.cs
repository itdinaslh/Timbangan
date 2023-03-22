using Timbangan.Data;
using Timbangan.Domain.Entities;
using Timbangan.Domain.Repositories;

namespace Timbangan.Domain.Services;

public class TransactionService : ITransaction
{
    private AppDbContext context;

    public TransactionService(AppDbContext context) => this.context = context;

    public IQueryable<Transaction> Transactions => context.Transactions;

    public async Task AddDataAsync(Transaction trans)
    {
        await context.Transactions.AddAsync(trans);

        await context.SaveChangesAsync();
    }
}
