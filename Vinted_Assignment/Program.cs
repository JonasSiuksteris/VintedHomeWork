using Vinted_Assignment.DiscountRules;
using Vinted_Assignment.Helpers;
using Vinted_Assignment.Storage;

namespace Vinted_Assignment;

public static class Program
{
    private static FileReader.FileReader _fileReader;
    private static TransactionProcessor.TransactionProcessor _transactionProcessor;
    private static IDataStore _dataStore;
    
    public static void Main(string[] args)
    {
        _fileReader = new FileReader.FileReader("input.txt");
        
        _dataStore = new ListDataStore();
        
        var rulesProcessor = new RulesProcessor.RulesProcessor();
        rulesProcessor.AddRule(new LowestPriceForSShipmentsRule());
        rulesProcessor.AddRule(new FirstThirdLShipmentForLpFreeRule(_dataStore));
        rulesProcessor.AddRule(new MaximumDiscountPerMonthRule(_dataStore));

        _transactionProcessor = new TransactionProcessor.TransactionProcessor(rulesProcessor);
        
        while (!_fileReader.IsEndOfFile())
        {
            ProcessNextTransaction();
        }
    }

    private static void ProcessNextTransaction()
    {
        var parsingResult = _fileReader.GetTransaction();

        if (!parsingResult.IsSuccess)
        {
            ConsoleWriter.WriteFailure(parsingResult.RawLine!);
            return;
        }
        
        var processedTransaction = _transactionProcessor.ProcessTransaction(parsingResult.Transaction!);
        _dataStore.Save(processedTransaction);
        ConsoleWriter.WriteSuccess(processedTransaction);
    }
}