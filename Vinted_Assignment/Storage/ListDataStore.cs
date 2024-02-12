using Vinted_Assignment.Models;

namespace Vinted_Assignment.Storage;

public class ListDataStore : IDataStore
{
    private readonly List<Transaction> _transactions = [];

    public void Save(Transaction transaction)
    {
        _transactions.Add(transaction);
    }

    public IEnumerable<Transaction> GetTransactions()
    {
        return _transactions;
    }
}