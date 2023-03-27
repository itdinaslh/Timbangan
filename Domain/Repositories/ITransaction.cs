using Timbangan.Data;
using Timbangan.Domain.Entities;

namespace Timbangan.Domain.Repositories;

public interface ITransaction
{
    IQueryable<Transaction> Transactions { get; }

    Task AddDataAsync(Transaction trans);

    Task UpdateAsync(Transaction trans);
}
