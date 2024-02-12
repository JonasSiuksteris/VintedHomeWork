using Vinted_Assignment.DiscountRules;
using Vinted_Assignment.Errors;
using Vinted_Assignment.Helpers;
using Vinted_Assignment.Storage;

namespace Vinted_Assignment;

public static class Program
{
    private static FileReader.FileReader _fileReader;
    private static IDataStore _dataStore;
    private static RulesProcessor.RulesProcessor _rulesProcessor;
    
    public static void Main(string[] args)
    {
        _fileReader = new FileReader.FileReader("../../../input.txt");
        
        _dataStore = new ListDataStore();
        
        _rulesProcessor = new RulesProcessor.RulesProcessor();
        _rulesProcessor.AddRule(new LowestPriceForSShipmentsRule());
        _rulesProcessor.AddRule(new FirstThirdLShipmentForLpFreeRule(_dataStore));
        _rulesProcessor.AddRule(new MaximumDiscountPerMonthRule(_dataStore));
        
        while (!_fileReader.IsEndOfFile())
        {
            ProcessNextTransaction();
        }
    }

    private static void ProcessNextTransaction()
    {
        try
        {
            var transaction = _fileReader.GetTransaction();
            transaction.FinalShipmentPrice = PriceLookup.GetPrice(transaction.Provider, transaction.PackageSize);
            var discount = _rulesProcessor.ExecuteRules(transaction);
            transaction.FinalShipmentPrice -= discount;
                
            Console.WriteLine($"{transaction.Date:yyyy-MM-dd} {transaction.PackageSize} {transaction.Provider} {transaction.FinalShipmentPrice} {(discount != 0 ? discount : "-")}");

            _dataStore.Save(transaction);
        }
        catch (DataInputException e)
        {
            Console.WriteLine(e.Message + " " + "Ignored");
        }
    }
}