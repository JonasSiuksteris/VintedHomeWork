using Vinted_Assignment.Models;

namespace Vinted_Assignment.Storage;

public interface IDataStore
{
    void Save(Transaction transaction);
    IEnumerable<Transaction> GetTransactions();
}