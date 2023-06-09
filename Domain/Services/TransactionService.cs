﻿using DocumentFormat.OpenXml.Spreadsheet;
using Timbangan.Data;
using Timbangan.Domain.Entities;
using Timbangan.Domain.Repositories;

namespace Timbangan.Domain.Services;

public class TransactionService : ITransaction
{
    private readonly AppDbContext context;

    public TransactionService(AppDbContext context) => this.context = context;

    public IQueryable<Transaction> Transactions => context.Transactions;

    public async Task AddDataAsync(Transaction trans)
    {
        await context.Transactions.AddAsync(trans);

        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Transaction trans)
    {
        Transaction? trx = await context.Transactions.FindAsync(trans.TransactionID);

        if (trx is not null)
        {
            trx.StatusID = 2;            
            trx.UpdatedBy = trans.UpdatedBy;
            trx.UpdatedAt = DateTime.Now;

            context.Transactions.Update(trx);
        }

        await context.SaveChangesAsync();
    }
}
