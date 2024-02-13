using Vinted_Assignment.Helpers;
using Vinted_Assignment.Models;
using Vinted_Assignment.RulesProcessor;

namespace Vinted_Assignment.TransactionProcessor;

public class TransactionProcessor
{
    private readonly IRuleProcessor _rulesProcessor;

    public TransactionProcessor(IRuleProcessor rulesProcessor)
    {
        _rulesProcessor = rulesProcessor;
    }

    public Transaction ProcessTransaction(Transaction transaction)
    {
        transaction.FinalShipmentPrice = PriceLookup.GetPrice(transaction.Provider, transaction.PackageSize);
        var discount = _rulesProcessor.ExecuteRules(transaction);
        transaction.FinalShipmentPrice -= discount;

        return transaction;
    }
}